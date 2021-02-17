using UnityEngine;

public static class HullGridAdapter
{
	private const int GridSize = 1;

	public static Vector2Int GetGridPosition(float x, float y)
	{
		return new Vector2Int(
			WorldToGridPosition(x),
			WorldToGridPosition(y));
	}

	public static Vector2 GetWorldPosition(Vector2Int gridPosition)
	{
		return new Vector2(
			GridToWorldPosition(gridPosition.x),
			GridToWorldPosition(gridPosition.y));
	}

	private static int WorldToGridPosition(float position)
	{
		return Mathf.RoundToInt(position / HullGridAdapter.GridSize);
	}

	private static int GridToWorldPosition(int gridPosition)
	{
		return gridPosition * HullGridAdapter.GridSize;
	}
}