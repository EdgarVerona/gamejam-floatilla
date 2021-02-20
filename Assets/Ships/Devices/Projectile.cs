using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float Speed = 30.0f;

    [SerializeField]
    float LifetimeInSeconds = 3.0f;

    [SerializeField]
    Rigidbody Rigidbody;

    float _timeCreated = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _timeCreated = Time.time;
    }

    void FixedUpdate()
    {
        if ((_timeCreated + this.LifetimeInSeconds) <= Time.time)
		{
            // If the ball's lifetime has expired, kill it.
            DoDestroy();
        }
        else
		{
            // Move forward.
            this.transform.position = this.transform.position + (this.transform.forward * this.Speed * Time.deltaTime);
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        DoDestroy();
    }

	private void DoDestroy()
	{
        Destroy(this.gameObject);
    }
}
