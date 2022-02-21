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

    public override void Move(Vector2Int playerTilePosition)
    {
        var path = Pathfinding.GetPathFromPosition(position, playerTilePosition, Game.game.grid);

        if (path != null)
        {
            base.Move(path[1]);
            LookAtPlayer(playerTilePosition);
        }
        //else
        //{
        //    Vector2Int randomPosition;
        //    do
        //    {
        //        randomPosition = Game.game.grid.RandomNeibgbour(tilePosition);
        //    } while (Pathfinding.FindPathFromPosition(tilePosition, randomPosition, Game.game.grid));
        //    base.Move(randomPosition);

        //    LookAtPlayer(tilePosition);
        //}
            
    }
    
    public void LookAtPlayer(Vector2Int position)
    {
        transform.LookAt(new Vector3(position.x, 0, position.y));
    }
}