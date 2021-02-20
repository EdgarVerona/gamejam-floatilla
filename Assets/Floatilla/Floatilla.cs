using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Floatilla : MonoBehaviour
{
    [SerializeField]
    GameState GameState;

    private Vector3 _localCenterpoint;

    private List<Ship> _shipsInFloatilla = new List<Ship>();

    public int GetHullCount()
	{
        return _shipsInFloatilla.Sum(ship => ship.GetHullCount());
    }

    public void FireActiveCannons(Vector3 direction)
	{
        _shipsInFloatilla.ForEach(ship => ship.FireActiveCannons(direction));
	}

    public void StopActiveCannons(Vector3 direction)
	{
        _shipsInFloatilla.ForEach(ship => ship.StopActiveCannons(direction));
    }

    public void FireActiveCannonsAllDirections()
	{
        _shipsInFloatilla.ForEach(ship => ship.FireActiveCannonsAllDirections());
    }

    public void StopActiveCannonsAllDirections()
	{
        _shipsInFloatilla.ForEach(ship => ship.StopActiveCannonsAllDirections());
    }

    public float GetEngineThrust(Vector3 direction)
    {
        return _shipsInFloatilla.Sum(ship => ship.GetEngineThrust(direction));
    }

    public Vector3 GetWorldMidpoint()
    {
        return this.transform.localToWorldMatrix.MultiplyPoint(_localCenterpoint);
    }

    // Start is called before the first frame update
    void Start()
    {
        _shipsInFloatilla = this.GetComponentsInChildren<Ship>().ToList();

        var bounds = MathUtilities.GetMaxBounds(this.gameObject);

        _localCenterpoint = this.transform.worldToLocalMatrix.MultiplyPoint(bounds.center);
    }

    // Update is called once per frame
    void Update()
    {
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
