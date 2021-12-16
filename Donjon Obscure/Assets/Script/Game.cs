using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    protected Room room;
    
    [SerializeField]
    protected Character character;

    public static Game game = null;

    protected bool eventSet = false;
    
    void Start()
    {
        if(Game.game == null)
        {
            Game.game = this;
        }
        
        addEventsOnTiles();

        //character.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addEventsOnTiles()
    {
        foreach (var tile in this.room.grid.getTiles())
        {
            tile.setEvent(CheckTileContent);
        }
    }

    protected void EnemyTurn()
    {
        
    }

    void CheckTileContent(Tile tile)
    {
        Debug.Log("Tile " + tile.transform);
        
        switch (tile.getContent())
        {
            case null:
                character.Move(tile.getPosition());
                break;
            case Chest chest:
                chest.Open();
                break;/*
            case "MOB":
                EnemyTurn();
                break;
            case "EMPTY":
                EnemyTurn();
                break;
            case "DOOR":
                break;
            case "HOLE":
                break;
            case "WALL":
                break;
            default:
                throw new System.Exception("ERROR - Quelqu'un n'a pas bien fait sont boulot ! ");*/
        }

    }
}
