using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
	public void TriggerGameOver()
	{
		this.gameObject.SetActive(true);
		PauseState.Pause();
	}

	private void Update()
	{
		if (Keyboard.current.enterKey.IsPressed())
		{
			InteractableRegistry.ActiveInteractables.Clear();
			SceneManager.LoadScene(0);
			PauseState.Unpause();
		}
	}
}
