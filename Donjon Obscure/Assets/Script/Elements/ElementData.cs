using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element", menuName = "Element/Element", order = 0)]
public class ElementData : ScriptableObject
{
    public string Name;
    public Texture2D Icon;
    public GameObject Prefab
    {
        get { return Prefab; }
        set{ Prefab = value;
        Type = Prefab.GetComponent<ElementGrid>(); }
    }
    
    public ElementGrid Type;
    
    
    [HideInInspector]
    public GUIStyle Style;
}
