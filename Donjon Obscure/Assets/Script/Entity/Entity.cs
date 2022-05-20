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

public enum StateEntity
{
    IDLE,
    ON_FORWARD_ATTAK,
    ON_BACKWARD_ATTAK,
    ON_MOVING
}

public abstract class Entity : ElementGrid
{
    [SerializeField] protected int healthPoint;
    [SerializeField] protected int healthPointMax;
    [SerializeField] protected int attackStrenght;
    [SerializeField, Range(0f, 1f)] protected float smoothTime = .3f;
    [SerializeField] protected Vector2Int position;
    [SerializeField] protected float reachedTargetPositionAccuracy;
    protected Vector3 velocity = Vector3.zero;
    public StateEntity state;
    protected Vector2Int positionOrigin;
    protected Entity target;
    
    public Vector2Int Position
    {
        get {return position;}
        set
        {
            animationOver = false;
            position = value;
        }
    }
    
    /*void Update()
    {
        
        // if (transform.position == new Vector3(position.x, 0, position.y))
        Vector3 direction = new Vector3(position.x, 0, position.y) - this.transform.position;
        if(direction.magnitude > reachedTargetPositionAccuracy)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(position.x, 0, position.y), ref velocity, smoothTime);
        }
        else
        {
            animationOver = true;
            state = StateEntity.IDLE;
        }
    }*/
    
    void Update()
    {
        switch(state)
        {
            case StateEntity.ON_MOVING:
                PerformMove();
                break;
            case StateEntity.ON_FORWARD_ATTAK:
            case StateEntity.ON_BACKWARD_ATTAK:
               PerformAttak();
                break;
        }
    }

    private void PerformMove()
    {
        Vector3 direction = new Vector3(position.x, 0, position.y) - this.transform.position;
        if(direction.magnitude > reachedTargetPositionAccuracy)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(position.x, 0, position.y), ref velocity, smoothTime);
        }
        else
        {
            animationOver = true;
            state = StateEntity.IDLE;
        }
    }
    
    public virtual void Move(Vector2Int tilePosition)
    {
        var pos = position - tilePosition;
        transform.rotation = Quaternion.LookRotation(new Vector3(-pos.x, 0, -pos.y));
        Position = tilePosition;
        state = StateEntity.ON_MOVING;
    }
    
    public void Attack(Entity target)
    {
        positionOrigin = Position;
        Position = target.Position;
        this.target = target;
        state = StateEntity.ON_FORWARD_ATTAK;
    }
    
    private void PerformAttak()
    {
        Vector3 direction = new Vector3(position.x, 0, position.y) - this.transform.position;
        
        if(direction.magnitude > reachedTargetPositionAccuracy)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(position.x, 0, position.y), ref velocity, smoothTime);
        }
        else
        {
            if(state == StateEntity.ON_FORWARD_ATTAK)
            {
                Position = positionOrigin;
                target.TakeDamage(attackStrenght);
                state = StateEntity.ON_BACKWARD_ATTAK;
            }
            else if(state == StateEntity.ON_BACKWARD_ATTAK)
            {
                animationOver = true;
                state = StateEntity.IDLE;
            }
        }
    }

    public virtual void TeleportTo(Vector2Int tilePosition, Vector2Int lookAt)
    {
        position = tilePosition;
        transform.position = new Vector3(tilePosition.x, 0, tilePosition.y);
        transform.LookAt(new Vector3(lookAt.x, 0, lookAt.y));
    }

    public virtual void TakeDamage(int damage)
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            Die();
        }
    }

    public virtual void LookAtPosition(Vector2Int tilePosition)
    {
        this.transform.LookAt(new Vector3(tilePosition.x, 0, tilePosition.y) );
    }

    public bool CanAttack(Entity entity)
    {
        return Vector2.Distance(entity.position, this.position) == 1;
        //return Pathfinding.GetPathFromPosition(position, entity.position, Game.game.room.grid)[1] == entity.position;
    }

    public bool IsDead()
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

    public virtual void UpdateStat(Stat stat, int addedValue)
    {
        switch (stat)
        {
            case Stat.HealthPoint:
                {
                    if (healthPoint + addedValue > healthPointMax)
                    {
                        healthPoint = healthPointMax;
                    }
                    else healthPoint += addedValue;
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
