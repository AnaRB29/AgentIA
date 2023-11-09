using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ESarkis;

public class Dikstras : MonoBehaviour
{
    public Set r = new Set();
    public Tilemap tilemap;
    public TileBase origin;
    public TileBase paint;
    public float delay;
    public bool run = true;
    public bool early;
    public TileBase c1;
    public TileBase c2;
    public TileBase c3;
    public PriorityQueue<Vector3Int> frontier = new();
    public Vector3Int startingPoint;
    public Vector3Int target;
    public Dictionary<Vector3Int, Vector3Int> cFrom = new();
    public Dictionary<Vector3Int, int> cost = new();
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& run && !early)
        {
            FloodFillStartCoroutine();
            run = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && run && early)
        {
            FloodFillStartCoroutine();
            run = false;
        }
    }
    public void FloodFillStartCoroutine()
    {
        frontier.Enqueue(startingPoint,0);
        cFrom.Add(startingPoint,Vector3Int.zero);
        cost.Add(startingPoint,0);
        StartCoroutine(FloodFillCoroutine());
    }

    IEnumerator FloodFillCoroutine()
    {
        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();
            List<Vector3Int> neighbours = GetNeighbours(current);
            if (early && current == target) break;

            foreach (Vector3Int next in neighbours)
            {
                if (tilemap.GetSprite(next) != null)
                {
                    int new_cost = cost[current] + GetCost(tilemap.GetTile(next));
                    if (!cost.ContainsKey(next) || new_cost < cost[next])
                    {
                        cost[next] = new_cost;
                        int priority = new_cost;
                        frontier.Enqueue(next, priority);
                        if (!cFrom.ContainsKey(next))
                        {
                            cFrom.Add(next, current);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(delay);
        }
        DrawPath();   
    }

    private int GetCost(TileBase tile)
    {
        int cost = 0;
       if(tile == c1)
        {
            cost = 0;
        }
       else if (tile == c2)
        {
            cost = 1;
        }
       else if (tile == c3)
        {
            cost = 2;
        }
        return cost;
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
        while (tile!= startingPoint)
        {
            tilemap.SetTile(tile, paint);
            tile = cFrom[tile];
        } 
    }
}
