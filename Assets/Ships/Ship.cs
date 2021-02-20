using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ship : MonoBehaviour
{

    private KeyedLists<Vector3, Engine> _engines;
    private KeyedLists<Vector3, Cannon> _cannons;
    private List<MooringPoint> _mooringPoints;
    private List<Hull> _hullPieces;

    public KeyedLists<Vector3, Cannon> GetCannons()
	{
        if (_cannons == null)
		{
            Initialize();
        }

        return _cannons;
	}

    public KeyedLists<Vector3, Engine> GetEngines()
    {
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

        GetEngines().ForEach(direction * -1, Engine => result += Engine.ForcePerSecond);

        return result;
	}

    public void FireActiveCannons(Vector3 direction)
	{
        GetCannons().ForEach(direction, cannon => cannon.Fire());
	}

    public void StopActiveCannons(Vector3 direction)
    {
        GetCannons().ForEach(direction, cannon => cannon.CeaseFire());
    }

    public void FireActiveCannonsAllDirections()
    {
        GetCannons().ForEachAllKeys(ship => ship.Fire());
    }

    public void StopActiveCannonsAllDirections()
    {
        GetCannons().ForEachAllKeys(ship => ship.CeaseFire());
    }

    public int GetHullCount()
	{
        return GetHullPieces().Count;
	}

    void Start()
    {
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
        
    }

    void OnCollision(Collider other)
    {
        print($"Ship {this.name} Collided with {other.name}");
    }
}
