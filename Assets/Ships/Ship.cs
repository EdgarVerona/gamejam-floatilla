using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ship : MonoBehaviour
{

    private KeyedLists<Vector3, Engine> _engines = new KeyedLists<Vector3, Engine>();
    private List<Cannon> _cannons = new List<Cannon>();
    private List<MooringPoint> _mooringPoints = new List<MooringPoint>();
    private List<Hull> _hullPieces = new List<Hull>();

    public float GetEngineThrust(Vector3 direction)
	{
        float result = 0.0f;

        _engines.ForEach(direction, Engine => result += Engine.ForcePerSecond);

        return result;
	}

    public void FireActiveCannons(Vector3 direction)
	{
        _cannons.ForEach(cannon => cannon.Fire());
	}

    public void StopActiveCannons(Vector3 direction)
    {
        _cannons.ForEach(cannon => cannon.CeaseFire());
    }

    public int GetHullCount()
	{
        return _hullPieces.Count;
	}

    void Start()
    {
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
            _cannons.Add(cannon);
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
}
