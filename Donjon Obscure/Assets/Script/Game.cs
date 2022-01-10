using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField]
    protected Room room;
    
    [SerializeField]
    protected Character character;
    

    [SerializeField]
    protected GameObject panelDie;

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
        this.panelDie.SetActive(false);
    }
    
    void Update()
    {
        showTileInRangeLantern();
    }

    protected void initRoom()
    {
        this.room.grid.createGrid();
        addEventsOnTiles();
        character.TeleportTo(CharacterSpawnPosition);
        //tileCharacter = room.grid.getTile(CharacterSpawnPosition);
        //tileCharacter.setContent(character);
        room.grid.getTile(CharacterSpawnPosition).setContent(character);
    }

    public void addEventsOnTiles()
    {
        foreach (var tile in this.room.grid.getTiles())
        {
            tile.setEvent(CheckTileContent);
        }
    }

    public static void UpdateCharacter(Item item)
    {
        
        switch (item.BonusType)
        {
            case Stat.HealthPoint:
            case Stat.HealthPointMax:
            case Stat.AttackStrength:
            case Stat.ArmorPoint:
            case Stat.ArmorPointMax:
            case Stat.MentalSanity:
            case Stat.MentalSanityMax:
                game.character.UpdateStat(item.BonusValue, item.BonusType);
                
                break;
            case Stat.CurrentFuelInReserve:
                game.character.lantern.AddFuelInReserve(item.BonusValue);
                break;
            case Stat.FuelInReserveMax:
                game.character.lantern.FuelInReserveMax = item.BonusValue;
                break;
            default:
                break;
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
        this.panelDie.SetActive(true);
        /*Room newRoom = Instantiate((Resources.Load("Prefabs/Room") as GameObject).GetComponent<Room>());
        Destroy(this.room.gameObject);
        this.room = newRoom;
        initRoom();*/
    }

    protected void showTileInRangeLantern()
    {
        Lantern lantern = character.lantern;
        if(lantern.IsActive())
        {
            foreach (var tile in room.grid.getTiles())
            {
                tile.showTile(false);
            }

            for (var x = -character.lantern.LightRange(); x <= character.lantern.LightRange(); x++)
            {
                for (var y = -character.lantern.LightRange(); y <= character.lantern.LightRange(); y++)
                {
                    var vect = new Vector2Int(character.Position.x + x, character.Position.y + y);
                    Tile tile = room.grid.getTile(vect);
                    tile.showTile(true);
                }
            }
            
            room.grid.getTile(new Vector2Int(1,1)).showTile(true);
        }
        else
        {
            Tile tileCharacter = null;

            foreach (var tile in room.grid.getTiles())
            {
                tile.showTile(tile.getContent() as Character);
                if(tile.getContent() as Character)
                    tileCharacter = tile;
            }

            if(tileCharacter != null)
            {
                Vector2Int pos = tileCharacter.Position;
                var positions = new Vector2Int[]{new Vector2Int(pos.x+1, pos.y), new Vector2Int(pos.x-1, pos.y), new Vector2Int(pos.x, pos.y+1), new Vector2Int(pos.x, pos.y-1)};
                foreach (var position in positions)
                {
                    this.room.grid.getTile(position).showTile(true);
                }
            }
        }
    }

    void CheckTileContent(Tile tile)
    {
        if(tileIsClickable(tile))
        {
            switch (tile.getContent())
            {
                case null:

                    Tile tileCharacter = null;

                    foreach (var tileGrid in this.room.grid.getTiles())
                    {
                        if(tileGrid.getContent() as Character)
                        {
                            tileCharacter = tileGrid;
                        }
                    }
                    character.Move(tile.Position);
                    tile.setContent(character);
                    if(tileCharacter != null)
                    {
                       tileCharacter.setContent(null);
                    }
                    break;
                case Chest chest:
                    chest.Open();
                    break;
                case Hole hole:
                    character.Move(tile.Position);
                    this.reloadRoom();
                    break;
                case Enemy enemy:
                    character.Attack(enemy);
                    if(enemy.isDie())
                    {
                        tile.setContent(null);
                    }
                    break;
                case Character contentCharacter:
                    character.LightLantern();
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

        EnemyTurn(tile);
    }
}
