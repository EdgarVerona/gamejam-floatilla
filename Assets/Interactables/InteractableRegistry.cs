using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class InteractableRegistry
{
	public static List<InteractableComponent> ActiveInteractables = new List<InteractableComponent>();

	public static void Add(InteractableComponent newInteractable)
	{
		InteractableRegistry.ActiveInteractables.Add(newInteractable);
	}

	public static void Remove(InteractableComponent interactable)
	{
		InteractableRegistry.ActiveInteractables.Remove(interactable);
	}
}
