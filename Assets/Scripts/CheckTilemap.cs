using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Tét");
    }

    // Update is called once per frame
    void Update()
    {
        // Lấy vị trí thế giới của GameObject
        Vector3 worldPosition = transform.position;

        // Chuyển vị trí thế giới thành tọa độ ô trên Tilemap
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);

        // Kiểm tra ô tại tọa độ đó
        TileBase tile = tilemap.GetTile(cellPosition);

        if (tile != null)
        {
            Debug.Log($"GameObject đang ở ô: {cellPosition} trên Tilemap {tilemap.name}, Tile: {tile.name}");
        }
        else
        {
            Debug.Log($"GameObject đang ở ô: {cellPosition}, nhưng không có tile tại đây.");
        }
    }
}
