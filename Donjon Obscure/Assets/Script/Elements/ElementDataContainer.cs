using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ElementData Container", menuName = "Element/ElementData Container", order = 0)]
public class ElementDataContainer : ScriptableObject
{
    public ElementData[] Elements;
}