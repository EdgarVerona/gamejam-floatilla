using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePromptComponent : MonoBehaviour
{
    [SerializeField]
    public float Offset = 5.0f;

    private InteractableComponent _component;
    private Camera _mainCamera;

	private void Start()
	{
        _mainCamera = Camera.main;
    }

	public void Activate(InteractableComponent component)
	{
        _component = component;
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _component = null;
        if (this != null)
		{
            this.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (_component != null)
		{
            this.transform.position = _mainCamera.WorldToScreenPoint(_component.transform.position + (Vector3.forward * this.Offset));
        }
    }
}
