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
    CurrentFuelInReserve,
    FuelInReserveMax,
}
public abstract class Entity : ElementGrid
{
    protected int healthPoint;
    
    protected int healthPointMax;
    
    protected int attackStrenght;

    protected Vector2Int position;

    Vector3 velocity = Vector3.zero;

    [SerializeField, Range(0f, 300f)]
    float smoothTime = 150f;
    public GameObject model3D;

    public Vector2Int Position
    {
        get {return position;}
        set
        {
            isAnimationOver = false;
            position = value;
        }
    }
    void Update()
    {
        if (transform.position == new Vector3(position.x, 0, position.y))
        {
            isAnimationOver = true;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(position.x, 0, position.y), ref velocity, smoothTime * Time.deltaTime);
        }


    }

    public virtual void Move(Vector2Int tilePosition)
    {
        var pos = Position - tilePosition;
        //Debug.Log("pos " +  pos);
        this.model3D.transform.rotation = Quaternion.LookRotation(new Vector3(-pos.x, 0, -pos.y));
        Position = tilePosition;
    }

    public virtual void TeleportTo(Vector2Int tilePosition)
    {
        position = tilePosition;
        transform.position = new Vector3(tilePosition.x, 0, tilePosition.y) ;
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

    public bool isDie()
    {
        return this.healthPoint <= 0;
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
