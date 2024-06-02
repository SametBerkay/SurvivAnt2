using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroySandTileOnCollision : MonoBehaviour
{
    public Tilemap tileMap;
    public float maxDistance = 1.0f;
    public Transform playerTransform;

    private void Start()
    {
        if (!tileMap)
            tileMap = GetComponent<Tilemap>();
    }

    public void OnMouseDown()
    {
        if (!playerTransform)
            return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        float distanceToPlayer = Vector3.Distance(playerTransform.position, mousePosition);
        if (distanceToPlayer > maxDistance)
        {
            Debug.Log("Player is too far from the tile.");
            return;
        }

        Vector3Int tilePos = tileMap.WorldToCell(mousePosition);
        TileBase tile = tileMap.GetTile(tilePos);
        if (!tile)
            return;

        Debug.Log("Mouse clicked @ " + Input.mousePosition);

        // Destroy ground 
        tileMap.SetTile(tilePos, null);
    }
}
