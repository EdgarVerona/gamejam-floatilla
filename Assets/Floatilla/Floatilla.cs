using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Floatilla : MonoBehaviour
{
    [SerializeField]
    GameState GameState;

    private LazyValue<Bounds> _lastBounds;

    private List<Ship> _shipsInFloatilla = new List<Ship>();

    public int GetHullCount()
	{
        return _shipsInFloatilla.Sum(ship => ship.GetHullCount());
    }

    public void SetCannonFireStatuses(Dictionary<Vector3, bool> fireStatuses)
	{
        foreach (var status in fireStatuses)
		{
            if (status.Value)
			{
                _shipsInFloatilla.ForEach(ship => ship.FireActiveCannons(status.Key));
            }
            else
			{
                _shipsInFloatilla.ForEach(ship => ship.StopActiveCannons(status.Key));
            }
        }
	}

    public void FireActiveCannonsAllDirections()
	{
        _shipsInFloatilla.ForEach(ship => ship.FireActiveCannonsAllDirections());
    }

    public void StopActiveCannonsAllDirections()
	{
        _shipsInFloatilla.ForEach(ship => ship.StopActiveCannonsAllDirections());
    }

    internal void ApplyThrust(Vector3 direction)
    {
        _shipsInFloatilla.ForEach(ship => ship.ApplyThrust(direction));
    }

    internal void ApplyRotation(float rotateAngle)
    {
        _shipsInFloatilla.FirstOrDefault()?.ApplyRotation(rotateAngle);
    }

    public Vector3 GetWorldMidpoint()
    {
        return _lastBounds?.GetValue().center ?? Vector3.zero;
    }

    public Bounds GetWorldBounds()
	{
        return _lastBounds?.GetValue() ?? new Bounds();
    }

    // Start is called before the first frame update
    void Start()
    {
        _lastBounds = new LazyValue<Bounds>(
            1.0f,
            (lastBoundValue) => MathUtilities.GetMaxBoundsOfChildren(this.gameObject));

        _shipsInFloatilla = this.GetComponentsInChildren<Ship>().ToList();
    }

	private void OnDrawGizmos()
	{
        var bounds = MathUtilities.GetMaxBounds(this.gameObject);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(bounds.center, 0.1f);
    }

    void OnCollision(Collider other)
	{
        print($"Floatilla Collided with {other.name}");
	}

    void OnShipDeath(Ship shipDying)
	{
        _shipsInFloatilla.Remove(shipDying);

        if (_shipsInFloatilla.Count == 0)
		{
            this.GameState.TriggerGameOver();
        }
    }

	
}
