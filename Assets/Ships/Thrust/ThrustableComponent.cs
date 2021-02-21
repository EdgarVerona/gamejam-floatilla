using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustableComponent : MonoBehaviour
{
    [SerializeField]
    Rigidbody RigidBody;

    public void ApplyThrust(Vector3 direction, float thrustValue)
	{
        if (this.RigidBody != null)
		{
            //this.RigidBody.AddForce(direction * thrustValue * Time.deltaTime, ForceMode.VelocityChange);
            Vector3 directionWorldSpace = this.RigidBody.transform.TransformDirection(direction);
            this.RigidBody.velocity = directionWorldSpace * thrustValue * Time.deltaTime;
        }
	}

    public void ApplyRotation(float rotateAngle)
	{
        if (this.RigidBody != null)
		{
            this.RigidBody.angularVelocity = Vector3.zero;

            this.transform.Rotate(Vector3.up * rotateAngle);
            //this.RigidBody.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.up);
            //this.RigidBody.MoveRotation(this.RigidBody.rotation * Quaternion.AngleAxis(rotateAngle, Vector3.up));
        }
    }
}
