using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Ship : MonoBehaviour
{
    private KeyedLists<Vector3, Engine> _engines;
    private KeyedLists<Vector3, Cannon> _cannons;
    private List<MooringPoint> _mooringPoints;
    private List<Hull> _hullPieces;

    private bool _isDying = false;
    private Health _health;



    public KeyedLists<Vector3, Cannon> GetCannons()
	{
        if (_isDying)
		{
            return KeyedLists<Vector3, Cannon>.Empty;
		}

        if (_cannons == null)
		{
            Initialize();
        }

        return _cannons;
	}

    public KeyedLists<Vector3, Engine> GetEngines()
    {
        if (_isDying)
        {
            return KeyedLists<Vector3, Engine>.Empty;
        }

        if (_engines == null)
		{
            Initialize();
        }

        return _engines;
    }

    public List<Hull> GetHullPieces()
	{
        if (_hullPieces == null)
		{
            Initialize();
		}

        return _hullPieces;
	}

    public float GetEngineThrust(Vector3 direction)
	{
        float result = 0.0f;

        if (!_isDying)
		{
            GetEngines().ForEach(direction * -1, Engine => result += Engine.ForcePerSecond);
        }

        return result;
	}

    public void FireActiveCannons(Vector3 direction)
	{
        if (!_isDying)
		{
            GetCannons().ForEach(direction, cannon => cannon.Fire());
        }
	}

    public void StopActiveCannons(Vector3 direction)
    {
        if (!_isDying)
		{
            GetCannons().ForEach(direction, cannon => cannon.CeaseFire());
        }
    }

    public void FireActiveCannonsAllDirections()
    {
        if (!_isDying)
        {
            GetCannons().ForEachAllKeys(cannon => cannon.Fire());
        }
    }

    public void StopActiveCannonsAllDirections()
    {
        if (!_isDying)
		{
            GetCannons().ForEachAllKeys(cannon => cannon.CeaseFire());
        }
    }

    public int GetHullCount()
	{
        return GetHullPieces().Count;
	}

    void Start()
    {
        _isDying = false;
        _health = GetComponent<Health>();
    }

    private void Initialize()
	{
        _hullPieces = new List<Hull>();
        _engines = new KeyedLists<Vector3, Engine>();
        _mooringPoints = new List<MooringPoint>();
        _cannons = new KeyedLists<Vector3, Cannon>();

        _hullPieces = GetComponentsInChildren<Hull>().ToList();

        foreach (var hull in _hullPieces)
        {
            foreach (var device in hull.GetDevices())
            {
                AddDevice(device.Key, device.Value);
            }
        }
    }

    private void AddDevice(Vector3 direction, GameObject device)
	{
        var engine = device.GetComponent<Engine>();
        if (engine != null)
		{
            _engines.Add(direction, engine);
		}

        var cannon = device.GetComponent<Cannon>();
        if (cannon != null)
		{
            _cannons.Add(direction, cannon);
		}

        var mooringPoint = device.GetComponent<MooringPoint>();
        if (mooringPoint != null)
		{
            _mooringPoints.Add(mooringPoint);
		}
	}

    void Update()
    {
        if (_isDying)
		{
            SendMessageUpwards("OnShipDeath", this, SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
    }

    void OnCollision(Collider other)
    {
        if (!_isDying)
		{
            var damageComponent = other.GetComponent<DamageComponent>();

            if (damageComponent)
            {
                _health.Damage(damageComponent);

                if (_health.CurrentHitpoints <= 0)
				{
                    _isDying = true;
				}
            }
            else
            {
                print($"Ship {this.name} Collided with {other.name}, but no damage component found");
            }
        }
    }
}
