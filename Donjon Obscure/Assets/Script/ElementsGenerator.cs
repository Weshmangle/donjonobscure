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
        GenerateDoor(tiles);
        GenerateChest(tiles);
        GenerateHole(tiles);
        GenerateEnemy(tiles);


    }
    public void GenerateExternWall(Tile[,] tiles)
    {
        
        for (int i = 0; i < 10; i++)
        {
            tiles[i,0].setContent(wall);
        }
        for (int i = 0; i < 10; i++)
        {
            tiles[i, 10].setContent(wall);
        }
        for (int i = 0; i < 10; i++)
        {
            tiles[0, i].setContent(wall);
        }
        for (int i = 0; i < 10; i++)
        {
            tiles[10, i].setContent(wall);
        }

    }
    public void GenerateDoor(Tile[,] tiles)
    {
        int _sideRandPosition = Random.Range(0, 4);
        int _myRandPositionX = Random.Range(1, 9);
        int _myRandPositionZ = Random.Range(1, 9);
        if (_sideRandPosition == 0)
        {
            tiles[_myRandPositionX, 0].setContent(door);
        }
        if (_sideRandPosition == 1)
        {
            tiles[0, _myRandPositionZ].setContent(door);
        }
        if (_sideRandPosition == 2)
        {
            tiles[_myRandPositionX, 10].setContent(door);
        }
        if (_sideRandPosition == 3)
        {
            tiles[10, _myRandPositionZ].setContent(door);
        }
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