using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloodFill : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase paint;
    public TileBase way;
    public float delay;
    public bool stop;
    public bool run=true;
    public Dictionary<Vector3Int, Vector3Int> cFrom = new();
    public Queue<Vector3Int> frontier = new();
    public Vector3Int start;
    public Vector3Int target;
    public Set reached = new Set();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& run)
        {
            FloodFillStartCoroutine();
            run = false;
        }
    }
    public void FloodFillStartCoroutine()
    {
        frontier.Enqueue(start);
        cFrom.Add(start, Vector3Int.zero);
        StartCoroutine(FloodFillCoroutine());
    }

    IEnumerator FloodFillCoroutine()
    {
        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();
            List<Vector3Int> neighbours = GetNeighbours(current);
            if (current == target && stop) break;
            foreach (Vector3Int next in neighbours)
            {
                if (!reached.set.Contains(next) && tilemap.GetSprite(next) != null)
                {
                    if (next != start && next != target) { tilemap.SetTile(next, paint); }
                    reached.Add(next);
                    frontier.Enqueue(next);
                    if (!cFrom.ContainsKey(next))
                    {
                        cFrom.Add(next,current);
                        
                    }
                }
            }
            yield return new WaitForSeconds(delay);
        }
        DrawPath();
    }

    private List<Vector3Int> GetNeighbours(Vector3Int current)
    {
        List<Vector3Int> neighbours = new List<Vector3Int>();
        neighbours.Add(current + new Vector3Int(0 , 1,0));
        neighbours.Add(current + new Vector3Int(0, -1, 0));
        neighbours.Add(current + new Vector3Int(1, 0, 0));
        neighbours.Add(current + new Vector3Int(-1, 0, 0));
        return neighbours;
    }

    private void DrawPath()
    {
        Vector3Int tile = cFrom[target];
        while (tile!= start)
        {
            tilemap.SetTile(tile, way);
            tile = cFrom[tile];
        }
        
    }
}