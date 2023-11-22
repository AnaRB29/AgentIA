using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAnimationController : MonoBehaviour
{
    public Tilemap tilemap;
    public KeyCode activarAnimacionKey = KeyCode.Space;
    public float duracionAnimacion = 2.0f;
    public float alturaLevantamiento = 0.5f;

    private bool animacionActivada = false;

    void Update()
    {
        if (Input.GetKeyDown(activarAnimacionKey) && !animacionActivada)
        {
            // Obtener la posición del tile bajo el cursor del mouse
            Vector3Int mousePos = GetMouseTilePosition();

            if (mousePos != Vector3Int.zero)
            {
                StartCoroutine(LevantarTile(mousePos));
            }
        }
    }

    Vector3Int GetMouseTilePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = tilemap.WorldToCell(mouseWorldPos);
        return tilePos;
    }

    IEnumerator LevantarTile(Vector3Int tilePos)
    {
        animacionActivada = true;

        // Obtener el sprite del tile actual
        Sprite sprite = tilemap.GetSprite(tilePos);

        // Si el sprite existe, animar el levantamiento
        if (sprite != null)
        {
            // Guardar la posición original del tile
            Vector3 originalPosition = tilemap.GetCellCenterWorld(tilePos);

            // Levantar el tile
            tilemap.SetTile(tilePos, null); // Eliminar el tile temporalmente
            tilemap.SetTile(tilePos, tilemap.GetTile(tilePos)); // Volver a colocar el tile (esto puede variar según la versión de Unity)

            // Animar el levantamiento
            float elapsedTime = 0f;
            while (elapsedTime < duracionAnimacion)
            {
                float yOffset = Mathf.Lerp(0f, alturaLevantamiento, elapsedTime / duracionAnimacion);
                tilemap.SetTile(tilePos, null); // Eliminar el tile temporalmente
                tilemap.SetTransformMatrix(tilePos, Matrix4x4.Translate(new Vector3(0f, yOffset, 0f))); // Aplicar la transformación
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            // Restaurar la posición original
            tilemap.SetTile(tilePos, null); // Eliminar el tile temporalmente
            tilemap.SetTransformMatrix(tilePos, Matrix4x4.identity); // Restaurar la transformación
            tilemap.SetTile(tilePos, tilemap.GetTile(tilePos)); // Volver a colocar el tile (esto puede variar según la versión de Unity)
        }

        animacionActivada = false;
    }
}
