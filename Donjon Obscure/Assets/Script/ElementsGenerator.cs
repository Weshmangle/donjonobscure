using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsGenerator : MonoBehaviour
{
    [SerializeField]
    int nbChestToSpawn, nbHoleToSpawn, nbEnemyToSpawn;
    
    Wall prefabWall;
    Hole prefabHole;
    Chest prefabChest;
    Obstacle prefabObstacle;
    Gate prefabGate;
    Gate prefabEnemy;

    int width = 10;
    int height = 10;
    
    
    Quaternion rotationTop = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    Quaternion rotationRight = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    Quaternion rotationBot = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    Quaternion rotationLeft = Quaternion.Euler(0.0f, 270.0f, 0.0f);

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateElement(Tile[,] tiles)
    {
         prefabWall = (Resources.Load("Prefabs/Wall") as GameObject).GetComponent<Wall>();
         prefabHole = (Resources.Load("Prefabs/Hole") as GameObject).GetComponent<Hole>();
         prefabChest = (Resources.Load("Prefabs/Chest") as GameObject).GetComponent<Chest>();
         prefabObstacle = (Resources.Load("Prefabs/Obstacle") as GameObject).GetComponent<Obstacle>();
         prefabGate = (Resources.Load("Prefabs/Gate") as GameObject).GetComponent<Gate>();
         //prefabEnemy = (Resources.Load("Prefabs/Enemy") as GameObject).GetComponent<Enemy>();

        //Tile chestTile = tiles[0,0];
        //GenerateChestList(tiles);

        GenerateExternWall(tiles);
        GenerateDoor(tiles,true);
        GenerateDoor(tiles,false);
        GenerateChest(tiles);
        GenerateHole(tiles);
        //GenerateEnemy(tiles);
    }
    public void GenerateExternWall(Tile[,] tiles)
    {
        for (int i = 0; i < width; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[i, 0].getPosition(),rotationTop);
            tiles[i,0].setContent(wallExternElement);
            
        }
        for (int i = 0; i < width; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[i, width-1].getPosition(), rotationBot);
            tiles[i, width-1].setContent(wallExternElement);
            
        }
        for (int i = 0; i < height; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[0, i].getPosition(), rotationRight);
            tiles[0, i].setContent(wallExternElement);
            
        }
        for (int i = 0; i < height; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[height-1, i].getPosition(), rotationLeft);
            tiles[height-1, i].setContent(wallExternElement);
        }

    }
    public void GenerateDoor(Tile[,] tiles, bool isEndDoor)
    {
        
        int _sideRandPosition = Random.Range(0, 4);
        Tile currentTile = null;
        Vector2Int characterSpawnPositionRelativeToDoor = Vector2Int.zero;
        Quaternion rotation = Quaternion.identity;
        do
        {
            int _myRandPosition = Random.Range(1, width-1);
            
            switch (_sideRandPosition)
            {
                case 0:
                    currentTile = tiles[_myRandPosition, 0];
                    rotation = rotationTop;
                    characterSpawnPositionRelativeToDoor = new Vector2Int(currentTile.getPosition().x, currentTile.getPosition().y+1);
                    break;
                case 1:
                    currentTile = tiles[_myRandPosition, width-1]; 
                    rotation = rotationBot;
                    characterSpawnPositionRelativeToDoor = new Vector2Int(currentTile.getPosition().x, currentTile.getPosition().y - 1);
                    break;
                case 2:
                    currentTile = tiles[0, _myRandPosition];
                    rotation = rotationRight;
                    characterSpawnPositionRelativeToDoor = new Vector2Int(currentTile.getPosition().x + 1, currentTile.getPosition().y);
                    break;
                case 3:
                    currentTile = tiles[height-1, _myRandPosition];
                    rotation = rotationLeft;
                    characterSpawnPositionRelativeToDoor = new Vector2Int(currentTile.getPosition().x - 1, currentTile.getPosition().y);
                    break;
                default:
                        Debug.Log("Provided stat does not exist.");
                    break;
            }
        }
        while (typeof(Gate).IsInstanceOfType(currentTile.getContent()));

        ElementGrid doorElement = InstantiateElementGrid(prefabGate, currentTile.getPosition(), rotation);
        currentTile.setContent(doorElement);
        Game.CharacterSpawnPosition = characterSpawnPositionRelativeToDoor;


        }    
    public void GenerateChest(Tile[,] tiles)
    {
        for (int i = 0; i < nbChestToSpawn; i++)
        {
            int _myRandPositionX = Random.Range(1, width-1);
            int _myRandPositionZ = Random.Range(1, height-1);

            if (tiles[_myRandPositionX, _myRandPositionZ].getContent() == null)
            {
                ElementGrid chestElement = InstantiateElementGrid(prefabChest, tiles[_myRandPositionX, _myRandPositionZ].getPosition());
                tiles[_myRandPositionX, _myRandPositionZ].setContent(chestElement);                
            }
        }
    }
    public void GenerateHole(Tile[,] tiles)
    {
        for (int i = 0; i < nbHoleToSpawn; i++)
        {
            int _myRandPositionX = Random.Range(1, width);
            int _myRandPositionZ = Random.Range(1, height);
            if (tiles[_myRandPositionX, _myRandPositionZ].getContent() == null)
            {
                ElementGrid holeElement = InstantiateElementGrid(prefabHole, tiles[_myRandPositionX, _myRandPositionZ].getPosition());
                tiles[_myRandPositionX, _myRandPositionZ].setContent(holeElement);
                
            }
        }
    }
    public void GenerateEnemy(Tile[,] tiles)
    {
        for (int i = 0; i < nbEnemyToSpawn; i++)
        {
            int _myRandPositionX = Random.Range(1, width);
            int _myRandPositionZ = Random.Range(1, height);
            if (tiles[_myRandPositionX, _myRandPositionZ].getContent() == null)
            {
                ElementGrid enemyElement = InstantiateElementGrid(prefabEnemy, tiles[i, 0].getPosition());
                tiles[_myRandPositionX, _myRandPositionZ].setContent(enemyElement);
                
            }
        }
    }
    public ElementGrid InstantiateElementGrid(ElementGrid element, Vector2Int position)
    {
        ElementGrid elementReturn = Instantiate(element, new Vector3(position.x, 0, position.y), Quaternion.identity, this.transform);
        return elementReturn;
    }
    public ElementGrid InstantiateElementGrid(ElementGrid element, Vector2Int position, Quaternion rotation)
    {
        return Instantiate(element, new Vector3(position.x, 0, position.y), rotation, this.transform);
    }

    /*
    public void GenerateChestList(List<Tile> tiles)
    {
        for (int i = 0; i < nbChestToSpawn; i++)
        {
            int indexElement = Random.Range(0, tiles.Count);
            tiles[indexElement].setContent(chest);
            tiles.RemoveAt(indexElement);
        }
    }
    */
}