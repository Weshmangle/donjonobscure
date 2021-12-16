using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat
{
    HealthPoint,
    HealthPointMax,
    AttackStrength,
    ArmorPoint,
    ArmorPointMax,
    MentalSanity,
    MentalSanityMax,
}
public abstract class Entity : MonoBehaviour
{

    protected int healthPoint;
    protected int healthPointMax;
    
    protected int attackStrenght;

    protected Vector2Int position;

    public Vector2Int Position
    {
        get { return position; }
        set
        {
            position = value;
            transform.position = new Vector3(value.x, 0, value.y);
        }
    }

    public virtual void Move(Vector2Int tilePosition)
    {
        Position = tilePosition;
    }
    public void Attack(Entity target)
    {
        target.TakeDamage(attackStrenght);
    }

    public virtual void TakeDamage(int damage)
    {
        healthPoint =- damage;
        if (healthPoint <= 0)
        {
            Die();
        }
    }

    public bool canAttak(Entity entity)
    {
        return false;
        //return entity.position - this.position;
    }

    protected virtual void Die()
    {
        Debug.Log("Entity died");
    }
    public virtual void KillEntity()
    {
        TakeDamage(healthPoint);
    }

    public virtual void UpdateStat(int addedValue, Stat stat)
    {
        switch (stat)
        {
            case Stat.HealthPoint:
                {
                    healthPoint += addedValue;
                }
                break;
            case Stat.HealthPointMax:
                {
                    healthPointMax += addedValue;
                }
                break;
            case Stat.AttackStrength:
                {
                    attackStrenght += addedValue;
                }
                break;
            default:
                Debug.Log("Provided stat does not exist.");
                break;
        }
    }
        
}