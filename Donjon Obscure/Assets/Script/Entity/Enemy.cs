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
        Game.game.room.Enemies.Remove(this);
        Debug.Log("Enemy is dead");
        Destroy(this.gameObject);
    }
    public override void Move(Vector2Int tilePosition)
    {
        //if (Pathfinding.FindPathFromPosition(position, tilePosition, Game.game.room.grid))
        //    base.Move(Pathfinding.GetPathFromPosition(position, tilePosition, Game.game.room.grid)[1]);
        //else base.Move(Game.game.room.grid.RandomNeibgbour(tilePosition));

    }
    public void LookAtPlayer(Character player)
    {
        Debug.Log(player);
        transform.LookAt(player.gameObject.transform);
    }

}
