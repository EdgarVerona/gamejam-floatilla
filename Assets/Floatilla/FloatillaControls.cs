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

	public void Start()
	{
		_floatilla = GetComponent<Floatilla>();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		var moveDirection = context.ReadValue<Vector2>();
		bool moved = false;

		if (moveDirection.x > Mathf.Epsilon)
		{
			print($"Moving right! ({moveDirection.x})");
			moved = true;
		}
		else if (moveDirection.x < -Mathf.Epsilon)
		{
			print($"Moving left! ({moveDirection.x})");
			moved = true;
		}

		if (moveDirection.y > Mathf.Epsilon)
		{
			print($"Moving up! ({moveDirection.y})");
			moved = true;
		}
		else if (moveDirection.y < -Mathf.Epsilon)
		{
			print($"Moving down! ({moveDirection.y})");
			moved = true;
		}

		if (!moved)
		{
			print("Not moving!");
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
