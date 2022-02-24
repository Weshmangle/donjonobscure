using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{   
    [SerializeField] internal Character character;
    [SerializeField] public GameObject panelDie;
    [SerializeField] public Grid grid;
    [SerializeField] public List<Enemy> Enemies;
    [SerializeField] public GameObject cursor;

    public static Game Instance = null;

    protected bool eventSet = false;

    static Vector2Int characterSpawnPosition;
    
    static public Vector2Int CharacterSpawnPosition {get; set;}

    public static bool DEBUG = true;
    [SerializeField] public Tile tileCliked;
    
    void Awake()
    {
        if(Game.Instance == null)
        {
            Game.Instance = this;
        }
        else
        {
            Debug.LogError("GAME IS ALREADY INSTANTIATE");
        }
        
        this.initRoom();
        this.panelDie.SetActive(false);
    }
    
    void Update()
    {
        showTileInRangeLantern();

        if(tileCliked != null)
        {
            if(character.IsAnimationOver())
            {
                EnemyTurn(tileCliked);
                tileCliked = null;
            }
        }

        if(DEBUG)
        {
            foreach (var tile in grid.GetTiles())
            {
                tile.showTile(true);
            }
        }
    }

    protected void initRoom()
    {
        grid.createGrid();
        addEventsOnTiles();
        character.TeleportTo(CharacterSpawnPosition, CharacterSpawnPosition - (grid.Entry.Position - CharacterSpawnPosition));
        //tileCharacter = room.grid.getTile(CharacterSpawnPosition);
        //tileCharacter.setContent(character);
        grid.getTile(CharacterSpawnPosition).Content = character;
    }

    public void addEventsOnTiles()
    {
        foreach (var tile in grid.GetTiles())
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
                Instance.character.UpdateStat(item.BonusType, item.BonusValue);
                
                break;
            case Stat.CurrentFuelInReserve:
                Instance.character.lantern.AddFuelInReserve(item.BonusValue);
                break;
            case Stat.FuelInReserveMax:
                Instance.character.lantern.FuelInReserveMax = item.BonusValue;
                break;
            default:
                break;
        }
    }

    protected void EnemyTurn(Tile tile)
    {
        foreach (var enemy in Instance.Enemies)
        {
            if (enemy.CanAttack(character))
            {
                enemy.Attack(character);
            }
            else
            {
                grid.SetTileContent(enemy.Position, null);
                enemy.Move(character.Position);
                grid.SetTileContent(enemy.Position, enemy);
            }
        }
    }

    protected bool tileIsClickable(Tile tile)
    {
        float distance = Vector2Int.Distance(tile.Position, character.Position);
        return distance == 1 || distance == 0;
    }
    
    public void reloadRoom()
    {
        SceneManager.LoadScene("Main");
        /*Destroy(grid.gameObject);
        var vec = new Vector2Int(0,0);
        grid = Instantiate(grid);
        initRoom();*/
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
            foreach (var tile in grid.GetTiles())
            {
                tile.showTile(false);
            }

            for (int range = 0; range < character.lantern.LightRange(); range++)
            {
                foreach (var coordinate in Utils.MidPointCircleDraw(character.Position.x, character.Position.y, range))
                {
                    grid.getTile(new Vector2Int(Mathf.Clamp(coordinate.Item1, 0, grid.Width-1), Mathf.Clamp(coordinate.Item2, 0, grid.Height-1))).showTile(true);
                }
            }
        }
        else
        {
            Tile tileCharacter = null;

            foreach (var tile in grid.GetTiles())
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
                    grid.getTile(position).showTile(true);
                }
            }
        }
    }
//character.IsAnimationOver = true
    void CheckTileContent(Tile tile)
    {
        if(tileIsClickable(tile) && character.IsAnimationOver())
        {
            tileCliked = tile;

            switch (tile.Content)
            {
                case null:

                    Tile tileCharacter = null;

                    foreach (var tileGrid in grid.GetTiles())
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
                    character.MoveOverHole(tile.Position);
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

            tileCliked = tile;
        }
    }
}