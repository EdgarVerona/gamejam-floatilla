using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtilities
{
    public static Bounds GetMaxBounds(GameObject g)
    {
        var b = new Bounds(g.transform.position, Vector3.zero);
        foreach (Renderer r in g.GetComponentsInChildren<Renderer>())
        {
            b.Encapsulate(r.bounds);
        }
        return b;
    }

    public static Bounds GetMaxBoundsOfChildren(GameObject g)
    {
        Bounds b = new Bounds(Vector3.zero, Vector3.zero);
        foreach (Renderer r in g.GetComponentsInChildren<Renderer>())
        {
            if (b.size == Vector3.zero)
			{
                b = r.bounds;
			}
            else
			{
                b.Encapsulate(r.bounds);
            }
        }
        return b;
    }
}
