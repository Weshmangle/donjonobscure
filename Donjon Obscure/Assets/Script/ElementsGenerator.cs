using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsGenerator : MonoBehaviour
{
    int nbChestToSpawn, nbHoleToSpawn, nbEnemyToSpawn;
    
    ElementGrid chest, hole, enemy, door, wall;

    

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
        GenerateEnemy(tiles);


    }
    public void GenerateExternWall(Tile[,] tiles)
    {
        
        for (int i = 0; i < 10; i++)
        {
            tiles[i,0].setContent(wall);
            InstantiateElementGrid(hole, tiles[i, 0].getPosition());
        }
        for (int i = 0; i < 10; i++)
        {
            tiles[i, 10].setContent(wall);
            InstantiateElementGrid(hole, tiles[i, 10].getPosition());
        }
        for (int i = 0; i < 10; i++)
        {
            tiles[0, i].setContent(wall);
            InstantiateElementGrid(hole, tiles[0, i].getPosition());
        }
        for (int i = 0; i < 10; i++)
        {
            tiles[10, i].setContent(wall);
            InstantiateElementGrid(hole, tiles[10, i].getPosition());
        }

    }
    public void GenerateDoor(Tile[,] tiles, bool isEndDoor)
    {
        int _sideRandPosition = Random.Range(0, 4);        
        Tile currentTile = null;
        do
        {
            int _myRandPosition = Random.Range(1, 9);
            switch (_sideRandPosition)
            {
                case 0:
                    currentTile = tiles[_myRandPosition, 0];                    
                    break;
                case 1:
                    currentTile = tiles[_myRandPosition, 10];                    
                    break;
                case 2:
                    currentTile = tiles[0, _myRandPosition];
                    
                    break;
                case 3:
                    currentTile = tiles[10, _myRandPosition];                    
                    break;
                default:
                    Debug.Log("Provided stat does not exist.");
                    break;
            }
        }
        while (typeof(Gate).IsInstanceOfType(currentTile.getContent()));
        currentTile.setContent(door);
        
        }    
    public void GenerateChest(Tile[,] tiles)
    {        
        for (int i = 0; i < nbChestToSpawn; i++)
        {
            int _myRandPositionX = Random.Range(1, 9);
            int _myRandPositionZ = Random.Range(1, 9);            
            if (tiles[_myRandPositionX, _myRandPositionZ].getContent() == null)
            {
                tiles[_myRandPositionX, _myRandPositionZ].setContent(chest);
            }
        }
    }
    public void GenerateHole(Tile[,] tiles)
    {
        for (int i = 0; i < nbHoleToSpawn; i++)
        {
            int _myRandPositionX = Random.Range(1, 9);
            int _myRandPositionZ = Random.Range(1, 9);
            if (tiles[_myRandPositionX, _myRandPositionZ].getContent() == null)
            {
                tiles[_myRandPositionX, _myRandPositionZ].setContent(hole);
                InstantiateElementGrid( hole, tiles[_myRandPositionX, _myRandPositionZ].getPosition());
            }
        }
    }
    public void GenerateEnemy(Tile[,] tiles)
    {
        for (int i = 0; i < nbEnemyToSpawn; i++)
        {
            int _myRandPositionX = Random.Range(1, 9);
            int _myRandPositionZ = Random.Range(1, 9);
            if (tiles[_myRandPositionX, _myRandPositionZ].getContent() == null)
            {
                tiles[_myRandPositionX, _myRandPositionZ].setContent(enemy);
            }
        }
    }
    public void InstantiateElementGrid(ElementGrid element, Vector2 position)
    {

        Instantiate(element, position, Quaternion.identity, this.transform);

    }

    public int lol()
    {
        return 0;
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