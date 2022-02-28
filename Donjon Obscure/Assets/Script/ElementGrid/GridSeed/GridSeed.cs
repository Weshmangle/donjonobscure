using UnityEngine;
using System;
[Serializable]
public class GridSeed : ScriptableObject
{
    public string Description;
    public string Name;
    
    public ElementData[] ElementGrid;
    public int Row;
    public int Column;
}
