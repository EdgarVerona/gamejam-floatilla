using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FloatillaControls : MonoBehaviour
{
	[SerializeField]
	Floatilla FloatillaReference;

	[SerializeField]
	float MaxDegreesPerSecondRotation = 90.0f; // Default 1/4 circle per second at max turn speed

	[SerializeField]
	float MinDegreesPerSecondRotation = 10.0f; // No matter how big the floatilla gets, it cannot rotate slower than this.

	[SerializeField]
	float TurnReductionPerHullPiece = 5.0f; // For each hull piece in the floatilla, reduce max speed by this many degrees.

	private int _rotateDirection = 0;
	private int _thrustX = 0;
	private int _thrustY = 0;

	public void Start()
	{
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
			this.FloatillaReference.FireActiveCannonsAllDirections();
		}
		else if (context.canceled)
		{
			this.FloatillaReference.StopActiveCannonsAllDirections();
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
    void FixedUpdate()
	{
		RotateShip();
		ThrustShip();
	}

	private void ThrustShip()
	{
		Vector3 speedPerDirection = Vector3.zero;

		if (_thrustX != 0)
		{
			speedPerDirection = speedPerDirection + (_thrustX > 0 ? Vector3.left : Vector3.right);
		}
		if (_thrustY != 0)
		{
			speedPerDirection = speedPerDirection + (_thrustY > 0 ? Vector3.forward : Vector3.back);
		}

		this.FloatillaReference.ApplyThrust(speedPerDirection);
	}

	private void RotateShip()
	{
		float rotateAngle = 0.0f;

		switch (_rotateDirection)
		{
			case 0:
				break;
			default:
				rotateAngle = CalculateRotationAngleMagnitude() * _rotateDirection;
				break;
		}

		this.FloatillaReference.ApplyRotation(rotateAngle);
	}

	private float CalculateRotationAngleMagnitude()
	{
		float turnsPerSecond = Math.Max(
			this.MaxDegreesPerSecondRotation - (this.TurnReductionPerHullPiece * this.FloatillaReference.GetHullCount()),
			this.MinDegreesPerSecondRotation);

		return turnsPerSecond * Time.deltaTime;
	}
}
