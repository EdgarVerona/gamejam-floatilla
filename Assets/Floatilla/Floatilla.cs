using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floatilla : MonoBehaviour
{

    private Vector3 _localCenterpoint;

    public int GetHullCount()
	{
        //$$TODO - Replace with actual hull count
        return 3;
	}

    public float GetEngineThrust(Direction direction)
    {
        //$$TODO - Replace with actual engine thrust
        return 10.0f;
    }

    public Vector3 GetWorldMidpoint()
    {
        return this.transform.localToWorldMatrix.MultiplyPoint(_localCenterpoint);
    }

    // Start is called before the first frame update
    void Start()
    {
        var ships = this.GetComponentsInChildren<Ship>();
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
}
