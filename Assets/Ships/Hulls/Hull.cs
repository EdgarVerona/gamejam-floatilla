using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : MonoBehaviour
{
    [SerializeField]
    GameObject TopDevice;
    [SerializeField]
    GameObject BottomDevice;
    [SerializeField]
    GameObject LeftDevice;
    [SerializeField]
    GameObject RightDevice;

    private Dictionary<Vector3, GameObject> _instantiatedDevices = new Dictionary<Vector3, GameObject>();

    public IEnumerable<KeyValuePair<Vector3, GameObject>> GetDevices()
	{
        return _instantiatedDevices;
	}

    public IEnumerable<KeyValuePair<Vector3, GameObject>> GetDevicePrefabs()
	{
        Dictionary<Vector3, GameObject> results = new Dictionary<Vector3, GameObject>();

        if (this.TopDevice != null)
		{
            results.Add(Vector3.forward, this.TopDevice);
        }
        if (this.BottomDevice != null)
        {
            results.Add(Vector3.back, this.BottomDevice);
        }
        if (this.LeftDevice != null)
        {
            results.Add(Vector3.left, this.LeftDevice);
        }
        if (this.RightDevice != null)
        {
            results.Add(Vector3.right, this.RightDevice);
        }

        return results;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Application.isPlaying)
		{
            this.InstantiateDevice(this.TopDevice, Vector3.forward);
            this.InstantiateDevice(this.BottomDevice, Vector3.back);
            this.InstantiateDevice(this.LeftDevice, Vector3.left);
            this.InstantiateDevice(this.RightDevice, Vector3.right);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateDevice(GameObject prefabDevice, Vector3 direction)
	{
        if (prefabDevice != null)
        {
            var newObject = GameObject.Instantiate(prefabDevice, this.transform.position, this.transform.rotation * Quaternion.LookRotation(direction), this.transform);
            _instantiatedDevices.Add(direction, newObject);
        }
    }
}
