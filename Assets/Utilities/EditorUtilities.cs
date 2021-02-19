using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class EditorUtilities
{
	public static bool IsEditMode()
	{
		return Application.isEditor && !Application.isPlaying;
	}
}
