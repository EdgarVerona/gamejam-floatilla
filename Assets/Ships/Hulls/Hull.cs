using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : MonoBehaviour
{
    [SerializeField]
    public GameObject TopDevice;
    [SerializeField]
    public GameObject BottomDevice;
    [SerializeField]
    public GameObject LeftDevice;
    [SerializeField]
    public GameObject RightDevice;

    private Dictionary<Vector3, GameObject> _instantiatedDevices = new Dictionary<Vector3, GameObject>();

    public IEnumerable<KeyValuePair<Vector3, GameObject>> GetDevices()
	{
        return _instantiatedDevices;
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
            var newObject = GameObject.Instantiate(prefabDevice, this.transform.position, Quaternion.LookRotation(direction), this.transform);
            _instantiatedDevices.Add(direction, newObject);
        }
    }
}
