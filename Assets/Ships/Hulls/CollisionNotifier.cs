using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNotifier : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
        SendMessageUpwards("OnCollision", other, SendMessageOptions.DontRequireReceiver);
    }

	private void OnCollisionEnter(Collision collision)
	{
        SendMessageUpwards("OnCollision", collision.collider, SendMessageOptions.DontRequireReceiver);
    }
}
