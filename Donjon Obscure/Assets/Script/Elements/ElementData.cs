using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element", menuName = "Element/Element", order = 0)]
public class ElementData : ScriptableObject
{
    public string Name;
    public Texture2D Icon;
    public GameObject prefab;
    
    [HideInInspector]
    public GUIStyle Style;
}
