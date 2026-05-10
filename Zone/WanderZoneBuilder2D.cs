using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WanderZoneBuilder2D : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap groundTilemap;
    public Tilemap obstacleTilemap;

    [Header("Result")]
    public List<Vector2> walkablePoints = new List<Vector2>();

    [Header("Debug")]
    public bool buildOnStart = true;
    public bool drawGizmos = true;

    private void Start()
    {
        if (buildOnStart)
        {
            BuildZone();
        }
    }

    [ContextMenu("Build Wander Zone")]
    public void BuildZone()
    {
        walkablePoints.Clear();

        if (groundTilemap == null)
        {
            Debug.LogWarning("Ground Tilemap is not assigned.");
            return;
        }

        BoundsInt bounds = groundTilemap.cellBounds;

        foreach (Vector3Int cellPosition in bounds.allPositionsWithin)
        {
            bool hasGround = groundTilemap.HasTile(cellPosition);
            bool hasObstacle = obstacleTilemap != null && obstacleTilemap.HasTile(cellPosition);

            if (!hasGround || hasObstacle)
                continue;

            Vector3 worldPosition = groundTilemap.GetCellCenterWorld(cellPosition);
            walkablePoints.Add(worldPosition);
        }

        Debug.Log("Wander zone built. Points: " + walkablePoints.Count);
    }

    public Vector2 GetRandomPoint()
    {
        if (walkablePoints.Count == 0)
        {
            BuildZone();
        }

        if (walkablePoints.Count == 0)
        {
            return transform.position;
        }

        int index = Random.Range(0, walkablePoints.Count);
        return walkablePoints[index];
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos || walkablePoints == null)
            return;

        if (groundTilemap == null)
            return;

        Vector3 cellSize = groundTilemap.cellSize;

        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);

        foreach (Vector2 point in walkablePoints)
        {
            Gizmos.DrawCube(point, cellSize * 0.9f);
        }

        Gizmos.color = Color.green;

        foreach (Vector2 point in walkablePoints)
        {
            Gizmos.DrawWireCube(point, cellSize * 0.9f);
        }
    }
}
