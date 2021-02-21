using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PauseState
{
	public static bool IsPaused { get; private set; }

	public static void TogglePause()
	{
		if (PauseState.IsPaused)
		{
			PauseState.Unpause();
		}
		else
		{
			PauseState.Pause();
		}
	}

	public static void Pause()
	{
		Time.timeScale = 0.0f;
		PauseState.IsPaused = true;
	}

	public static void Unpause()
	{
		Time.timeScale = 1.0f;
		PauseState.IsPaused = false;
	}
}
