using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Element/Entity/Player", order = 0)]
public class PlayerData : EntityData
{
    public int SanityPoint;
    public int SanityPointMax;
    public int FuelPoint;
    public int FuelPointMax;
}
