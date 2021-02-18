using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Floatilla))]
public class FloatillaControls : MonoBehaviour
{
	[SerializeField]
	float MaxDegreesPerSecondRotation = 90.0f; // Default 1/4 circle per second at max turn speed

	[SerializeField]
	float MinDegreesPerSecondRotation = 10.0f; // No matter how big the floatilla gets, it cannot rotate slower than this.

	[SerializeField]
	float TurnReductionPerHullPiece = 5.0f; // For each hull piece in the floatilla, reduce max speed by this many degrees.

	private Floatilla _floatilla;

	private int _rotateDirection = 0;
	private int _thrustX = 0;
	private int _thrustY = 0;

	public void Start()
	{
		_floatilla = GetComponent<Floatilla>();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		var moveDirection = context.ReadValue<Vector2>();

		if (moveDirection.x > Mathf.Epsilon)
		{
			print($"Moving right! ({moveDirection.x})");
			_thrustX = 1;
		}
		else if (moveDirection.x < -Mathf.Epsilon)
		{
			print($"Moving left! ({moveDirection.x})");
			_thrustX = -1;
		}
		else
		{
			_thrustX = 0;
		}

		if (moveDirection.y > Mathf.Epsilon)
		{
			print($"Moving up! ({moveDirection.y})");
			_thrustY = 1;
		}
		else if (moveDirection.y < -Mathf.Epsilon)
		{
			print($"Moving down! ({moveDirection.y})");
			_thrustY = -1;
		}
		else
		{
			_thrustY = 0;
		}
	}

	public void OnFire(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			print("Firing!");
		}
		else if (context.canceled)
		{
			print("Firing stopped!");
		}
	}

	public void OnRotateLeft(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			print("Rotating Left!");
			_rotateDirection = -1;
		}
		else if (context.canceled)
		{
			print("Rotating Left stopped!");
			if (_rotateDirection == -1)
			{
				_rotateDirection = 0;
			}
		}
	}

	public void OnRotateRight(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			print("Rotating Right!");
			_rotateDirection = 1;
		}
		else if (context.canceled)
		{
			print("Rotating Right stopped!");
			if (_rotateDirection == 1)
			{
				_rotateDirection = 0;
			}
		}
	}

	public void OnMenu(InputAction.CallbackContext context)
	{
		print("Menu Screen");
	}

	public void OnBoatManagement(InputAction.CallbackContext context)
	{
		print("Boat Management Screen");
	}

    // Update is called once per frame
    void Update()
	{
		RotateShip();
		ThrustShip();
	}

	private void ThrustShip()
	{
		float translateX = 0.0f;
		float translateY = 0.0f;
		if (_thrustX != 0)
		{
			float thrustPerSecond = _floatilla.GetEngineThrust(_thrustX > 0 ? Direction.Right : Direction.Left);

			translateX = thrustPerSecond * _thrustX *Time.deltaTime;
		}
		if (_thrustY != 0)
		{
			float thrustPerSecond = _floatilla.GetEngineThrust(_thrustY > 0 ? Direction.Up : Direction.Down);

			translateY = thrustPerSecond * _thrustY * Time.deltaTime;
		}

		this.transform.Translate(translateX, 0.0f, translateY, Space.Self);
	}

	private void RotateShip()
	{
		float rotateAngle = 0.0f;

		switch (_rotateDirection)
		{
			case 0:
				break;
			default:
				rotateAngle = CalculateRotationAngleMagnitude(Time.deltaTime) * _rotateDirection;
				break;
		}

		this.transform.RotateAround(_floatilla.GetWorldMidpoint(), Vector3.up, rotateAngle);
	}

	private float CalculateRotationAngleMagnitude(float deltaTime)
	{
		float turnsPerSecond = Math.Max(
			this.MaxDegreesPerSecondRotation - (this.TurnReductionPerHullPiece * _floatilla.GetHullCount()),
			this.MinDegreesPerSecondRotation);

		return turnsPerSecond * deltaTime;
	}
}
