using UnityEngine;
using UnityEngine.Tilemaps;

public class DetectCell : MonoBehaviour
{
    public GridLayout grid;
    private Vector3 worldPosition;
    public Tilemap tilemap;
    public TileBase origin;
    public TileBase end;
    public TileBase over;
    public TileBase firstTile;
    public FloodFill start;
    public FloodFill final;
    public Dikstras dikstra;
    public Heuristic heuristic;
    public Astar aStar;
    public bool floodFillB;
    public bool floodFillEarlyExitB;
    public bool dijkstrasB;
    public bool heuristicB;
    public bool aStarB;

    private TileBase originalTileBase;
    private Vector3Int? originTile;
    private Vector3Int? originalTile;
    private Vector3Int? endTile;

    private void Start()
    {
        originTile = null;
    }

    private Vector3Int GetPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int cellPosition = grid.WorldToCell(worldPosition);
        mousePos = grid.CellToWorld(cellPosition);
        cellPosition.z = 0;
        return cellPosition;
    }

    private void Update()
    {
        if (floodFillB == true)
        {
            FloodFill();
            dikstra.enabled = false;
            heuristic.enabled = false;
            aStar.enabled = false;
        }
        else if (floodFillEarlyExitB == true)
        {
            FloodFill();
            start.stop = true;
            dikstra.enabled = false;
            heuristic.enabled = false;
            aStar.enabled = false;
        }
        else if (dijkstrasB == true)
        {
            Dijkstras();
            start.enabled = false;
            heuristic.enabled = false;
            aStar.enabled = false;
        }
        else if (heuristicB == true)
        {
            Heuristic();
            dikstra.enabled = false;
            start.enabled = false;
            aStar.enabled = false;

        }
        else if (aStarB == true){
            AEstrella();
            start.stop = true;
            dikstra.enabled = false;
            heuristic.enabled = false;
        }
    }

    public void FloodFill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var actualTile = tilemap.GetTile(GetPosition());
            if (actualTile == null) { return; }
            tilemap.SetTile(GetPosition(), origin);

            start.start = GetPosition();

            if (originTile != null)
            {
                tilemap.SetTile(originTile.Value, firstTile);
            }
            originTile = GetPosition();
        }

        if (Input.GetMouseButtonDown(1))
        {
            var actualTile = tilemap.GetTile(GetPosition());
            if (actualTile == null) { return; }
            tilemap.SetTile(GetPosition(), end);
            final.target = GetPosition();

            if (endTile != null)
            {
                tilemap.SetTile(endTile.Value, firstTile);
            }
            endTile = GetPosition();
        }

        if (originalTile != GetPosition())
        {
            if (originalTile != null && originalTileBase != null && originalTile.Value != originTile && originalTile.Value != endTile)
            {
                tilemap.SetTile(originalTile.Value, originalTileBase);
            }
            originalTile = GetPosition();
            originalTileBase = tilemap.GetTile(GetPosition());
            if (tilemap.GetSprite(GetPosition()) != null && originalTile.Value != originTile && originalTile.Value != endTile)
            {
                tilemap.SetTile(originalTile.Value, over);
            }
        }
    }

    public void Dijkstras()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var actualTile = tilemap.GetTile(GetPosition());
            if (actualTile == null) { return; }
          
            tilemap.SetTile(GetPosition(), origin);
            dikstra.startingPoint = GetPosition();
            if (originTile != null)
            {
                tilemap.SetTile(originTile.Value, firstTile);
            }
            originTile = GetPosition();
        }

        if (Input.GetMouseButtonDown(1))
        {
            var actualTile = tilemap.GetTile(GetPosition());
            if (actualTile == null) { return; }
            tilemap.SetTile(GetPosition(), end);
            dikstra.target = GetPosition();
            if (endTile != null)
            {
                tilemap.SetTile(endTile.Value, firstTile);
            }
            endTile = GetPosition();
        }

        if (originalTile != GetPosition())
        {
            if (originalTile != null && originalTileBase != null && originalTile.Value != originTile && originalTile.Value != endTile)
            {
                tilemap.SetTile(originalTile.Value, originalTileBase);
            }
            originalTile = GetPosition();
            originalTileBase = tilemap.GetTile(GetPosition());
            if (tilemap.GetSprite(GetPosition()) != null && originalTile.Value != originTile && originalTile.Value != endTile)
            {
                tilemap.SetTile(originalTile.Value, over);
            }
        }
    }

    public void Heuristic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var actualTile = tilemap.GetTile(GetPosition());
            if (actualTile == null) { return; }
            tilemap.SetTile(GetPosition(), origin);
            heuristic.start = GetPosition();
            if (originTile != null)
            {
                tilemap.SetTile(originTile.Value, firstTile);
            }
            originTile = GetPosition();
        }

        if (Input.GetMouseButtonDown(1))
        {
            var actualTile = tilemap.GetTile(GetPosition());
            if (actualTile == null) { return; }
            tilemap.SetTile(GetPosition(), end);
            heuristic.target = GetPosition();
            if (endTile != null)
            {
                tilemap.SetTile(endTile.Value, firstTile);
            }
            endTile = GetPosition();
        }

        if (originalTile != GetPosition())
        {
            if (originalTile != null && originalTileBase != null && originalTile.Value != originTile && originalTile.Value != endTile)
            {
                tilemap.SetTile(originalTile.Value, originalTileBase);
            }
            originalTile = GetPosition();
            originalTileBase = tilemap.GetTile(GetPosition());
            if (tilemap.GetSprite(GetPosition()) != null && originalTile.Value != originTile && originalTile.Value != endTile)
            {
                tilemap.SetTile(originalTile.Value, over);
            }
        }
    }

    public void AEstrella()
    {
         if (Input.GetMouseButtonDown(0))
         {
            var actualTile = tilemap.GetTile(GetPosition());
            if (actualTile == null) { return; }     
            tilemap.SetTile(GetPosition(), origin);
            aStar.start = GetPosition();
            if (originTile != null)
            {
                tilemap.SetTile(originTile.Value, firstTile);
            }
            originTile = GetPosition();
        }

        if (Input.GetMouseButtonDown(1))
        {
            var actualTile = tilemap.GetTile(GetPosition());
            if (actualTile == null) { return; }
            tilemap.SetTile(GetPosition(), end);
            aStar.start = GetPosition();
            if (endTile != null)
            {
                tilemap.SetTile(endTile.Value, firstTile);
            }
            endTile = GetPosition();
        }

        if (originalTile != GetPosition())
        {
            if (originalTile != null && originalTileBase != null && originalTile.Value != originTile && originalTile.Value != endTile)
            {
                tilemap.SetTile(originalTile.Value, originalTileBase);
            }

            originalTile = GetPosition();
            originalTileBase = tilemap.GetTile(GetPosition());

            if (tilemap.GetSprite(GetPosition()) != null && originalTile.Value != originTile && originalTile.Value != endTile)
            {
                tilemap.SetTile(originalTile.Value, over);
            }
        }
    }
}

