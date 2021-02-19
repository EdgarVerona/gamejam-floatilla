using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

public static class PrefabUtilities
{
	public static bool IsInAllowedPrefab(GameObject gameObject)
	{
		bool isInAllowedPrefab = false;

#if UNITY_EDITOR
		var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
		isInAllowedPrefab = prefabStage != null
			&& prefabStage.mode == PrefabStage.Mode.InIsolation
			&& prefabStage.scene.path == gameObject.scene.path;
#endif
		return isInAllowedPrefab;
	}
}
