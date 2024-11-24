using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPrefab : MonoBehaviour
{
    private void Start()
    {
        var map = GetComponentInParent<Tilemap>();
        transform.rotation = map.GetTransformMatrix(map.WorldToCell(transform.position)).rotation;
    }
}
