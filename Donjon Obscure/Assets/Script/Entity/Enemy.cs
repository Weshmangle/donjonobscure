using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    void Awake()
    {
        healthPoint = healthPointMax;
    }
    protected override void Die()
    {
        Game.game.Enemies.Remove(this);
        Debug.Log("Enemy is dead");
        Destroy(this.gameObject);
    }

    public override void Move(Vector2Int tilePosition)
    {
        var path = Pathfinding.GetPathFromPosition(position, tilePosition, Game.game.grid);
        
        if (path != null)
            base.Move(path[1]);
        else
            base.Move(Game.game.grid.RandomNeibgbour(tilePosition));
    }
    
    public void LookAtPlayer(Character player)
    {
        transform.LookAt(player.gameObject.transform);
    }
}