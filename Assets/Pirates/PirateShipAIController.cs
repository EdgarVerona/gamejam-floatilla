using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Ship))]
public class PirateShipAIController : MonoBehaviour
{
	[SerializeField]
	public float PreferredDistance = 5.0f;

    private Ship _ship;
    private Floatilla _floatilla;

    public List<Vector3> _preferredCannonDirections;
    public List<Vector3> _thrustDirectionsByPower;

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
