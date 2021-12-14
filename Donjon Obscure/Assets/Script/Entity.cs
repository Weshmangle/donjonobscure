using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    protected int healthPoint;
    protected int healthPointMax;
    
    protected int attackStrenght;
    protected int attackStrenghtMax;

    public virtual void Die()
    {
        healthPoint = 0;
    }
        
}
