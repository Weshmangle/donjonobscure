using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Room room;
    
    [SerializeField]
    internal Character character;
    

    [SerializeField]
    public GameObject panelDie;

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
        character.TeleportTo(CharacterSpawnPosition, CharacterSpawnPosition - (room.grid.Entry.Position - CharacterSpawnPosition));
        //tileCharacter = room.grid.getTile(CharacterSpawnPosition);
        //tileCharacter.setContent(character);
        room.grid.getTile(CharacterSpawnPosition).Content = character;
    }

    public void addEventsOnTiles()
    {
        foreach (var tile in this.room.grid.GetTiles())
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
                game.character.UpdateStat(item.BonusType, item.BonusValue);
                
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
        foreach (var enemy in game.room.Enemies)
        {
            if (enemy.CanAttack(character))
            {
                Debug.Log("Attacking player");
                enemy.Attack(character);
            }
            //else
            //{
            //    enemy.Move(character.Position);
            //    Debug.Log("Enemy trying to move");
            //    Vector2Int vector2;
            //    ElementGrid elt = null;
            //    do
            //    {
            //        vector2 = new Vector2Int((int)enemy.Position.x + Random.Range(0,2), (int)enemy.Position.y + Random.Range(0,2));
            //        elt = this.room.grid.getTile(vector2).Content;
            //    }
            //    while(elt != null);
            //    enemy.Position = vector2;
            //    //this.room.grid.getTile(vector2).setContent(enemy);
            //}
        }
    }

    protected bool tileIsClickable(Tile tile)
    {
        float distance = Vector2Int.Distance(tile.Position, character.Position);
        return distance == 1 || distance == 0;
    }

    protected void reloadRoom()
    {
        SceneManager.LoadScene("Main");
        
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
            foreach (var tile in room.grid.GetTiles())
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

            foreach (var tile in room.grid.GetTiles())
            {
                tile.showTile(tile.Content as Character);
                if(tile.Content as Character)
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
            switch (tile.Content)
            {
                case null:

                    Tile tileCharacter = null;

                    foreach (var tileGrid in this.room.grid.GetTiles())
                    {
                        if(tileGrid.Content as Character)
                        {
                            tileCharacter = tileGrid;
                        }
                    }
                    character.Move(tile.Position);
                    tile.Content = character;
                    if(tileCharacter != null)
                    {
                       tileCharacter.Content = null;
                    }
                    break;
                case Chest chest:
                    chest.Open();
                    break;
                case Hole hole:
                    character.Move(tile.Position);
                    this.panelDie.SetActive(true);
                    break;
                case Enemy enemy:
                    character.Attack(enemy);
                    if(enemy.IsDead())
                    {
                        tile.Content = null;
                    }
                    break;
                case Character contentCharacter:
                    character.LightLantern();
                break;

                //case "MOB":
                //    EnemyTurn();
                //    break;
                //case "EMPTY":
                //    EnemyTurn();
                //    break;
                case Gate gate:
                    if(gate.IsExitGate)
                    {
                        this.reloadRoom();
                    }
                    break;
                //case "WALL":
                //    break;
                //default:
                //    throw new System.Exception("ERROR - Quelqu'un n'a pas bien fait sont boulot ! ");
            }

            if(character.lantern.IsActive())
            {
                character.lantern.ConsumeFuel();
            }
        }

        EnemyTurn(tile);
    }
}
