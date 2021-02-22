using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public static class BoatManagementLoader
{
	//$TODO - Figure out best way to implement the actual management screen, this is placeholder
	public static void LoadBoatManagementScreen(
		Floatilla floatilla,
		FloatillaControls normalFloatillaControls)
	{
		PauseState.Pause();
		
		// Pass control over to the Ship Management Controls
		normalFloatillaControls.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");

		//$TEMP - There was another idea I had to load a new scene, not sure if that's going to work the way I want.
		//SceneManager.LoadScene("Scenes/ShipManagement", LoadSceneMode.Additive);

		//var shipManagementScene = SceneManager.GetSceneByName("ShipManagement");

		// Marshall the Floatilla into the new scene
		//SceneManager.MoveGameObjectToScene(floatilla.gameObject, shipManagementScene);
	}
}