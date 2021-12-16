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

    static Vector2Int characterSpawnPosition;
    
    static public Vector2Int CharacterSpawnPosition {get; set;}
    
    void Awake()
    {
        if(Game.game == null)
        {
            Game.game = this;
        }
        
        this.initRoom();
    }
    
    void Update()
    {
        
    }

    protected void initRoom()
    {
        this.room.grid.createGrid();
        addEventsOnTiles();
        character.Position = CharacterSpawnPosition;
    }

    public void addEventsOnTiles()
    {
        foreach (var tile in this.room.grid.getTiles())
        {
            tile.setEvent(CheckTileContent);
        }
    }

    protected void EnemyTurn(Tile tile)
    {
        foreach (var enemy in game.room.getEnnemies())
        {
            if(enemy.canAttak(character))
            {
                enemy.Attack(character);
            }
            else
            {
                //enemy.Move(calculatePathfinding(enemy)[0]);   
                Vector2Int vector2;
                ElementGrid elt = null;
                do
                {
                    vector2 = new Vector2Int((int)enemy.Position.x + Random.Range(0,2), (int)enemy.Position.y + Random.Range(0,2));
                    elt = this.room.grid.getTile(vector2).getContent();
                }
                while(elt != null);
                enemy.Position = vector2;
                //this.room.grid.getTile(vector2).setContent(enemy);
            }
        }
    }

    protected bool tileIsClickable(Tile tile)
    {
        float distance = Vector2Int.Distance(tile.Position, character.Position);
        return distance == 1 || distance == 0;
    }

    protected void reloadRoom()
    {
        Room newRoom = Instantiate((Resources.Load("Prefabs/Room") as GameObject).GetComponent<Room>());
        Destroy(this.room.gameObject);
        this.room = newRoom;
        initRoom();
    }

    void CheckTileContent(Tile tile)
    {
        if(tileIsClickable(tile))
        {
            switch (tile.getContent())
            {
                case null:
                    //character.Move(tile.getPosition);
                    character.Move(new Vector2Int(tile.Position.x, tile.Position.y));
                    Debug.Log(character.Position);
                    //tile.setContent(character);
                    break;
                case Chest chest:
                    chest.Open();
                    break;
                case Hole hole:
                    character.Move(tile.Position);
                    this.reloadRoom();
                    break;
                    /*
                case "MOB":
                    EnemyTurn();
                    break;
                case "EMPTY":
                    EnemyTurn();
                    break;
                case "DOOR":
                    break;
                case "WALL":
                    break;
                default:
                    throw new System.Exception("ERROR - Quelqu'un n'a pas bien fait sont boulot ! ");*/
            }

            if(character.lantern.IsActive())
            {
                character.lantern.ConsumeFuel();
            }
        }
    }
}
