using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    int Damage = 1;

    [SerializeField]
    ParticleSystem Projectile;

    private bool _firing = false;

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
        if (_firing && !Projectile.isPlaying)
		{
            Projectile.Play();
        }

        if (!_firing)
		{
            Projectile.Stop();
		}
    }
}
