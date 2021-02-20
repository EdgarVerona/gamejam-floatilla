using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float Damage = 1.0f;

    [SerializeField]
    float Speed = 30.0f;

    [SerializeField]
    float LifetimeInSeconds = 3.0f;

    bool _isDestroying = false;

    float _timeCreated = 0.0f;

    public float GetDamage()
	{
        return _isDestroying ? 0.0f : this.Damage;
	}

    // Start is called before the first frame update
    void Start()
    {
        _timeCreated = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy if you can.
        if (_isDestroying)
		{
            Destroy(this.gameObject);
        }
        else if ((_timeCreated + this.LifetimeInSeconds) <= Time.time)
		{
            // If the ball's lifetime has expired, kill it.
            Destroy(this.gameObject);
        }
        else
		{
            // Move forward.
            this.transform.position = this.transform.position + (this.transform.forward * this.Speed * Time.deltaTime);
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        _isDestroying = true;
	}
}
