using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    Stat bonusType;
    [SerializeField]
    new string name, description;
    [SerializeField]
    int bonusValue;

    public int BonusValue
    {
        get { return bonusValue; }
    }
    [SerializeField]
    Sprite artwork;

    public Sprite Artwork
    {
        get { return artwork; }
    }

    public Stat BonusType
    {
        get { return bonusType; }
    }

    
}
