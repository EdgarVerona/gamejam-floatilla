using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNotifier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        SendMessageUpwards("OnCollision", other, SendMessageOptions.DontRequireReceiver);
    }

	private void OnCollisionEnter(Collision collision)
	{
        SendMessageUpwards("OnCollision", collision.collider, SendMessageOptions.DontRequireReceiver);
    }
}
