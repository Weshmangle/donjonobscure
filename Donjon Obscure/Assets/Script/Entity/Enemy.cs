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
        Game.Instance.Enemies.Remove(this);
        Debug.Log("Enemy is dead");
        Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.LookAt(new Vector3(10, 0, 0));
    }

    public override void Move(Vector2Int playerTilePosition)
    {
        var path = Pathfinding.GetPathFromPosition(position, playerTilePosition, Game.Instance.grid);

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