using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    float ReloadInSeconds = 2.0f;

    [SerializeField]
    Projectile ProjectilePrefab;

    [SerializeField]
    GameObject FiringPoint;


    private bool _firing = false;

    private float _timeLastFired = 0.0f;

    public void Fire()
	{
        _firing = true;
    }

    public void CeaseFire()
	{
        _firing = false;

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_firing)
		{
            // If time to fire again, do so.
            if ((_timeLastFired + this.ReloadInSeconds) <= Time.time)
			{
                InitiateFiringInternal();
			}
        }
    }

    private void InitiateFiringInternal()
	{
        GameObject.Instantiate(
            this.ProjectilePrefab,
            this.FiringPoint.transform.position,
            this.transform.rotation);

        _timeLastFired = Time.time;
    }
}
