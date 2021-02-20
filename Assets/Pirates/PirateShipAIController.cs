using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Ship))]
public class PirateShipAIController : MonoBehaviour
{
	[SerializeField]
	public float PreferredDistance = 15.0f;

	[SerializeField]
	float MaxDegreesPerSecondRotation = 90.0f; // Default 1/4 circle per second at max turn speed

	[SerializeField]
	float MinDegreesPerSecondRotation = 10.0f; // No matter how big the floatilla gets, it cannot rotate slower than this.

	[SerializeField]
	float TurnReductionPerHullPiece = 5.0f; // For each hull piece in the floatilla, reduce max speed by this many degrees.

	private Ship _ship;
    private Floatilla _floatilla;

	private List<Vector3> _preferredCannonDirections;
	private List<Vector3> _thrustDirectionsByPower;

    // Start is called before the first frame update
    void Start()
	{
		_floatilla = GameObject.FindObjectOfType<Floatilla>();
		_ship = this.GetComponent<Ship>();

		// Build options we'll use later to decide movement for this frame.
		BuildCannonOptions();
		BuildThrustOptions();
	}

	// Update is called once per frame
	void Update()
    {
		if (_thrustDirectionsByPower.Count == 0 || _preferredCannonDirections.Count == 0)
		{
			print($"ERROR: Pirate {name} is missing thrust or cannon data");
			return;
		}
		/* 
        Each frame, we are going to need to determine:
		
		- If we are not within our preferred firing distance:
			- Find the direction we can rotate that will give us more preferable thrust toward the floatilla until we reach the direction with maximum thrust toward the floatilla.
			- Rotate in that direction
			- Apply thrust in the direction of the floatilla if we can.  If we cannot, apply lateral thrust if we can.
			- If cannons are facing the floatilla, fire.
		- If we are within our preferred firing distance:
			- If we are not facing the floatilla with our preferred cannon direction, find which direction we can rotate
		    that will give us a more preferrable cannon direction on the way to that preferred direction.
			- If cannons are facing the floatilla, fire.
			- Find the direction with the most engines toward/lateral to the floatilla, and ignite in that direction.
		*/
		Vector3 relativePosition = this.transform.position - _floatilla.transform.position;
		Vector3 relativePositionLocalSpace = this.transform.worldToLocalMatrix.MultiplyPoint(_floatilla.transform.position);

		if (relativePosition.magnitude > this.PreferredDistance)
		{	
			// The direction to face is the opposite of the thrusting direction!
			Vector3 destinationLocalHeading = _thrustDirectionsByPower[0] * -1;

			AngleTowardOpponentInDirection(relativePositionLocalSpace, destinationLocalHeading);

			ThrustInDirection(_thrustDirectionsByPower[0]);
		}
		else
		{
			// Once in range, make the turning preference toward the side with the most cannons
			Vector3 destinationLocalHeading = _preferredCannonDirections[0];
			AngleTowardOpponentInDirection(relativePositionLocalSpace, destinationLocalHeading);

			// Once in range, thrust in the first engine direction you find that is toward or perpendicular
			// to the player.
			foreach (var engineDirection in _thrustDirectionsByPower)
			{
				if (Vector3.Angle(engineDirection, relativePositionLocalSpace) < 90.0f)
				{
					ThrustInDirection(engineDirection);
					break;
				}
			}
		}

		// Fire cannons if facing the right direction
		foreach (var cannonDirection in _preferredCannonDirections)
		{
			if (Vector3.Angle(cannonDirection, relativePositionLocalSpace) < 15.0f)
			{
				_ship.FireActiveCannons(cannonDirection);
			}
			else
			{
				_ship.StopActiveCannons(cannonDirection);
			}
		}
	}

	private void ThrustInDirection(Vector3 direction)
	{
		var thrust = _ship.GetEngineThrust(direction);

		_ship.transform.Translate(direction * thrust * Time.deltaTime);
	}

	private void AngleTowardOpponentInDirection(Vector3 relativePositionLocalSpace, Vector3 destinationLocalHeading)
	{
		float angleToMove = Vector3.SignedAngle(
						destinationLocalHeading,
						relativePositionLocalSpace,
						Vector3.up);

		if (angleToMove > Mathf.Epsilon || angleToMove < -Mathf.Epsilon)
		{
			float absoluteAngleToMove = Mathf.Abs(angleToMove);
			float rotateDirection = angleToMove / absoluteAngleToMove;

			RotateShip(rotateDirection, absoluteAngleToMove);
		}
	}

	private void RotateShip(float rotateDirection, float destinationAngle)
	{
		float rotateAngle = CalculateRotationAngleMagnitude(Time.deltaTime, destinationAngle) * rotateDirection;

		_ship.transform.Rotate(Vector3.up, rotateAngle);
	}

	private float CalculateRotationAngleMagnitude(float deltaTime, float destinationAngle)
	{
		float turnsPerSecond = Mathf.Max(
			this.MaxDegreesPerSecondRotation - (this.TurnReductionPerHullPiece * _ship.GetHullCount()),
			this.MinDegreesPerSecondRotation);

		return Mathf.Min(destinationAngle, turnsPerSecond * deltaTime);
	}

	private void BuildCannonOptions()
	{
		var cannons = _ship.GetCannons();

		Dictionary<Vector3, int> directionCannonCounts = new Dictionary<Vector3, int>()
		{
			{ Vector3.forward, 0 },
			{ Vector3.back, 0 },
			{ Vector3.left, 0 },
			{ Vector3.right, 0 }
		};

		foreach (var cannon in cannons)
		{
			directionCannonCounts[cannon.Key] += cannon.Value.Count;
		}

		_preferredCannonDirections = directionCannonCounts
			.OrderByDescending(kvp => kvp.Value)
			.Where(kvp => kvp.Value > 0)
			.Select(kvp => kvp.Key)
			.ToList();
	}

	private void BuildThrustOptions()
	{
		Dictionary<Vector3, float> directionEngineThrust = new Dictionary<Vector3, float>()
		{
			{ Vector3.forward, _ship.GetEngineThrust(Vector3.forward) },
			{ Vector3.back, _ship.GetEngineThrust(Vector3.back) },
			{ Vector3.left, _ship.GetEngineThrust(Vector3.left) },
			{ Vector3.right, _ship.GetEngineThrust(Vector3.right) }
		};

		_thrustDirectionsByPower = directionEngineThrust
			.OrderByDescending(kvp => kvp.Value)
			.Where(kvp => kvp.Value > 0.0f)
			.Select(kvp => kvp.Key)
			.ToList();
	}
}
