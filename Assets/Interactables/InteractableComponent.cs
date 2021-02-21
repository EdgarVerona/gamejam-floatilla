using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableComponent : MonoBehaviour
{
	[SerializeField]
	ReactionComponentBase Reaction;

	[SerializeField]
	public float MinimumDistanceToInteract = 15.0f;

	[SerializeField]
	InteractablePromptComponent InteractionPrompt;

	private void Start()
	{
		this.InteractionPrompt.Deactivate();
		InteractableRegistry.Add(this);
	}

	private void OnDestroy()
	{
		this.InteractionPrompt.Deactivate();
		InteractableRegistry.Remove(this);
	}

	internal void Interact()
	{
		if (this.Reaction != null)
		{
			this.Reaction.React();
		}
	}
	
	internal void StartInteractivity()
	{
		this.InteractionPrompt.Activate(this);
	}

	internal void EndInteractivity()
	{
		this.InteractionPrompt.Deactivate();
	}
}
