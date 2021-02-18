using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Ship))]
public class ShipControls : MonoBehaviour
{
	[SerializeField]
	float MaxRadiansPerSecondRotation = 0.78f; // Default 1/4 circle per second at max turn speed

	[SerializeField]
	float MinRadiansPerSecondRotation = 0.10f; // No matter how big the floatilla gets, it cannot rotate slower than this.

	[SerializeField]
	float TurnReductionPerHullPiece = 0.05f; // For each hull piece in the floatilla, reduce max speed by this many radians.


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
		}
		else if (context.canceled)
		{
			print("Rotating Left stopped!");
		}
	}

	public void OnRotateRight(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			print("Rotating Right!");
		}
		else if (context.canceled)
		{
			print("Rotating Right stopped!");
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

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
