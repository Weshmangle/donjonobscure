using UnityEngine;


[CreateAssetMenu(fileName = "New Grid Seed", menuName = "Grid")]
public class GridSeed : ScriptableObject
{
    int Width;
    int Height;
    Vector2Int Entry;
    Vector2Int Exit;
    
}
