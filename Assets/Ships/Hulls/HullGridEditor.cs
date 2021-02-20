using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

/// <summary>
/// The Unity Editor behavior that binds an in-game cube to a fixed grid in the world,
/// and updates the GridPosition.
/// 
/// It doesn't have to know *how* to translate back and forth from grid to world,
/// but is responsible for actually performing that translation to make sure
/// that, as the cube moves around in the Unity UI, both the world and the grid
/// locations are kept in sync and obey the laws of each.
/// </summary>
[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Hull))]
public class HullGridEditor : MonoBehaviour
{
	private Vector2Int _lastKnownPosition;

	private void Start()
	{
		_lastKnownPosition = HullGridAdapter.GetGridPosition(this.transform.position.x, this.transform.position.z);
	}

	void Update()
	{
		// Only allow for this editing in prefab mode
		if (EditorUtilities.IsEditMode())
		{
			AffixToGrid();
		}
	}


	private void OnDrawGizmos()
	{
		if (EditorUtilities.IsEditMode())
		{
			Hull hull = this.GetComponent<Hull>();

			Gizmos.matrix = this.transform.localToWorldMatrix;
			Gizmos.color = Color.blue;
			var prefabs = hull.GetDevicePrefabs();

			foreach (var prefab in prefabs)
			{
				DrawGizmoForDevice(prefab.Key, prefab.Value);
			}
		}
	}

	private void DrawGizmoForDevice(Vector3 direction, GameObject device)
	{
		if (device != null)
		{
			Vector3 destination = GetGizmoDestVector(direction);
			
			Gizmos.DrawLine(
				new Vector3(0.0f, 0.55f, 0.0f), destination);

			Gizmos.DrawIcon(this.transform.localToWorldMatrix.MultiplyPoint(destination), device.name + ".png", false);
		}
	}

	private Vector3 GetGizmoDestVector(Vector3 direction)
	{
		
		return (direction * 0.45f) 
			+ new Vector3(0.0f, 0.55f, 0.0f);
	}

	private void AffixToGrid()
	{
		// Find the position that the Grid thinks our current position in the world
		// should equate to, and assign the waypoint to that new grid position.
		// This makes sure that, if the user is trying to move the cube in the world
		// to a spot that would equate to a new Grid position, that the new Grid position
		// will actually be stored successfully for this cube.
		Vector2Int newGridPosition = HullGridAdapter.GetGridPosition(
			this.transform.localPosition.x,
			this.transform.localPosition.z);

		// Now that the grid position is updated, reset our position in the world
		// to match whatever the Grid thinks it should be.  This "Snaps" the in-world Cube
		// to the grid's alignment, making sure it can't be placed in an illegal "partway" point
		// in the world, or be rotated illegally.
		Vector2 worldCoords = HullGridAdapter.GetWorldPosition(newGridPosition);
		this.transform.localPosition = new Vector3(worldCoords.x, 0.0f, worldCoords.y);
		this.transform.localRotation = Quaternion.identity;

		_lastKnownPosition = newGridPosition;

		UpdateLabel();
	}

	private void UpdateLabel()
	{
		//print($"{_lastKnownPosition.x},{_lastKnownPosition.y}");
	}
}