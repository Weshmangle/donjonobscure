using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsGenerator : MonoBehaviour
{
    [SerializeField]
    int nbChestToSpawn, nbHoleToSpawn, nbEnemyToSpawn;
    
    [SerializeField] ElementGrid chest, hole, enemy, gate, wall, obstacle;

    int width = 10;
    int height = 10;

    

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
        GameObject obj = Resources.Load("Prefabs/Wall") as GameObject;
        
        Debug.Log(obj);
        
        for (int i = 0; i < width; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(wall, tiles[i, 0].getPosition());
            tiles[i,0].setContent(wallExternElement);
            
        }
        for (int i = 0; i < width; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(wall, tiles[i, width-1].getPosition());
            tiles[i, width-1].setContent(wallExternElement);
            
        }
        for (int i = 0; i < height; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(wall, tiles[0, i].getPosition());
            tiles[0, i].setContent(wallExternElement);
            
        }
        for (int i = 0; i < height; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(wall, tiles[height-1, i].getPosition());
            tiles[height-1, i].setContent(wallExternElement);
        }

    }
    public void GenerateDoor(Tile[,] tiles, bool isEndDoor)
    {
        int _sideRandPosition = Random.Range(0, 4);        
        Tile currentTile = null;
        do
        {
            int _myRandPosition = Random.Range(1, width);
            switch (_sideRandPosition)
            {
                case 0:
                    currentTile = tiles[_myRandPosition, 0];                    
                    break;
                case 1:
                    currentTile = tiles[_myRandPosition, width-1];                    
                    break;
                case 2:
                    currentTile = tiles[0, _myRandPosition];
                    
                    break;
                case 3:
                    currentTile = tiles[height-1, _myRandPosition];                    
                    break;
                default:
                    Debug.Log("Provided stat does not exist.");
                    break;
            }
        }
        while (typeof(Gate).IsInstanceOfType(currentTile.getContent()));

        ElementGrid doorElement = InstantiateElementGrid(gate, currentTile.getPosition());
        currentTile.setContent(doorElement);
        
        }    
    public void GenerateChest(Tile[,] tiles)
    {
        for (int i = 0; i < nbChestToSpawn; i++)
        {
            int _myRandPositionX = Random.Range(1, width-1);
            int _myRandPositionZ = Random.Range(1, height-1);

            if (tiles[_myRandPositionX, _myRandPositionZ].getContent() == null)
            {
                ElementGrid chestElement = InstantiateElementGrid(chest, tiles[_myRandPositionX, _myRandPositionZ].getPosition());
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
                ElementGrid holeElement = InstantiateElementGrid(hole, tiles[_myRandPositionX, _myRandPositionZ].getPosition());
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
                ElementGrid enemyElement = InstantiateElementGrid(enemy, tiles[i, 0].getPosition());
                tiles[_myRandPositionX, _myRandPositionZ].setContent(enemyElement);
                
            }
        }
    }
    public ElementGrid InstantiateElementGrid(ElementGrid element, Vector2Int position)
    {
        ElementGrid elementReturn = Instantiate(element, new Vector3(position.x, 0, position.y), Quaternion.identity, this.transform);
        return elementReturn;
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