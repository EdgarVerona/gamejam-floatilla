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
	}

	private void Update()
	{
		if (Keyboard.current.enterKey.IsPressed())
		{
			SceneManager.LoadScene(0);
		}
	}
}
