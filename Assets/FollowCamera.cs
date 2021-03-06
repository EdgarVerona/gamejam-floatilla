using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    public GameObject ObjectToFollow;

    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.ObjectToFollow != null)
		{
            var objectMidpoint = this.ObjectToFollow.transform.position;

            _camera.transform.SetPositionAndRotation(
                new Vector3(
                    objectMidpoint.x,
                    _camera.transform.position.y,
                    objectMidpoint.z),
                _camera.transform.rotation);
        }
    }
}
