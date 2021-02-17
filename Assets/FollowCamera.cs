using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    GameObject ObjectToFollow;

    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _camera.transform.SetPositionAndRotation(
            new Vector3(
                this.ObjectToFollow.transform.position.x,
                _camera.transform.position.y,
                this.ObjectToFollow.transform.position.z),
            _camera.transform.rotation);
    }
}
