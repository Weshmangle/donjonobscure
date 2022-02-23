using UnityEngine;


public class SeedTileManager : MonoBehaviour 
{
    public SeedTile[] seedTiles;
}
[System.Serializable]
public struct SeedTile
{

    public Texture2D Icon;
    public string ButtonText;
    [HideInInspector]
    public GUIStyle NodeStyle;
}