using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{// controle la taille de la map (groundside est la taille d'une tuille)
    [SerializeField] int with, heigh, groundSize; 
    // liste des prefab utilis� dans une map pour en ajouter des nouveau facilement
    [SerializeField] GameObject[] wall, hole,enemy, player, doorStart, doorEnd, chest, item, levier, key; 
    [SerializeField] Tile[] ground;
    [SerializeField] int nbEnemy, nbChest, nbItem, nbLevier, nbKey, nbHole;
    int nbChestSpawned, nbChestToSpawn;
    int nbHoleExist = 0;
    bool startDoorIsPresent, endDoorIsPresent;
    int startDoorSide, endDoorSide;
    public Vector3[] interneGroundPosition, chestPosition, endDoorPosition;
    public Vector3[] originePosition;
    public int originePositionInTabler;
    public int wallPrefab, groundPrefab, enemyPrefab, playerPrefab, doorStartPrefab, doorEndPrefab, holePrefab, chestPrefab, itemPrefab, levierPrefab, keyPrefab; // [le choix du skin] quel prefab de quel object on utilise dans la liste (exemple: chest[chestP] = je veux l'object chest avec le numero chestP de la liste) 

    //List mapEllementCoordonnee<> = n

    // Start is called before the first frame update
    void Start()
    {
        //GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
        for (var i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                
            }
        }*/
    public void GenerateMap(Tile[,] tiles)
    {
        GenerateExternWall();
        GenerateGround();
        GenerateHole();
        GenerateDoor();
        GenerateChest();
        GenerateEnemy();
    }    
    public void GenerateExternWall()
    {
        //MUR EXTERIEUR//
        Vector3 _origine = originePosition[originePositionInTabler];

        Quaternion _rotationWallBot = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        Quaternion _rotationWallTop = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        Quaternion _rotationWallLeft = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        Quaternion _rotationWallRight = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        //Quaternion _rotationWallRight = Quaternion.EulerAngles();

        for (int i = 0; i < with; i++)
        {
            _origine.z += groundSize;
            Instantiate(wall[wallPrefab], _origine, _rotationWallBot, this.transform);
        }
        _origine.z += groundSize;
        for (int j = 0; j < heigh; j++)
        {
            _origine.x += groundSize;
            Instantiate(wall[wallPrefab], _origine, _rotationWallTop, this.transform);
        }
        _origine = originePosition[originePositionInTabler];
        for (int l = 0; l < heigh; l++)
        {
            _origine.x += groundSize;
            Instantiate(wall[wallPrefab], _origine, _rotationWallLeft, this.transform);
        }
        _origine.x += groundSize;
        for (int k = 0; k < with; k++)
        {
            _origine.z += groundSize;
            Instantiate(wall[wallPrefab], _origine, _rotationWallRight, this.transform);
        }
    }
    public void GenerateGround()
    {
        
        for (int i = 0; i < with; i++)
        {
            Vector3 _groundPosition = new Vector3(1.0f+i, 0.0f, 1.0f);
            
            for (int j = 0; j< heigh; j++)
            {                
                Instantiate(ground[groundPrefab], _groundPosition, Quaternion.identity, this.transform);
                _groundPosition.z++;                
            }
          
        }
    }
    public void GenerateHole()
    {
        for (int i = 0; i<nbHole; i++)
        {
            int randomx = Random.Range(1, with);
            int randomy = Random.Range(1, heigh);
            Vector3 holePosition = new Vector3(randomx, 0.0f, randomy);
            Instantiate(hole[holePrefab], holePosition, Quaternion.identity, this.transform);
        }        
    }
    public void GenerateDoor() 
    {
        if (!startDoorIsPresent)
        {
            Vector3 _doorStartPosition = new Vector3(0.0f, 0.0f, 0.0f);
            Quaternion _doorStartRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            int _randomSide = Random.Range(0, 4);
            int _randDoorStartPositionWith = Random.Range(1, with);
            int _randDoorStartPositionHeigh = Random.Range(1, heigh);            
            if (_randomSide == 0)
            {
                _doorStartPosition = new Vector3(0.0f, 0.0f, _randDoorStartPositionWith);
                _doorStartRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }
            if (_randomSide == 1)
            {
                _doorStartPosition = new Vector3(with+1, 0.0f, _randDoorStartPositionWith);
                _doorStartRotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            }
            if (_randomSide == 2)
            {
                _doorStartPosition = new Vector3(_randDoorStartPositionHeigh, 0.0f, 0.0f);
                _doorStartRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            if (_randomSide == 3)
            {
                _doorStartPosition = new Vector3(_randDoorStartPositionHeigh, 0.0f, heigh+1);
                _doorStartRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            Instantiate(doorStart[doorStartPrefab], _doorStartPosition, _doorStartRotation, this.transform);
            startDoorIsPresent = true;
            startDoorSide = _randomSide;
            GenerateEndDoor();
        }
    }
    public void GenerateEndDoor()
    {
        Vector3 _doorEndPosition = new Vector3(0.0f, 0.0f, 0.0f);
        Quaternion _doorEndRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        int _randomSide = Random.Range(0, 4);
        int _randDoorEndPositionWith = Random.Range(1, with);
        int _randDoorEndPositionHeigh = Random.Range(1, heigh);
        
        if (_randomSide == 0)
        {
            _doorEndPosition = new Vector3(0.0f, 0.0f, _randDoorEndPositionWith);
            _doorEndRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
        if (_randomSide == 1)
        {
            _doorEndPosition = new Vector3(with, 0.0f, _randDoorEndPositionWith);
            _doorEndRotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
        if (_randomSide == 2)
        {
            _doorEndPosition = new Vector3(_randDoorEndPositionHeigh, 0.0f, 0.0f);
            _doorEndRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (_randomSide == 3)
        {
            _doorEndPosition = new Vector3(_randDoorEndPositionHeigh, 0.0f, heigh);
            _doorEndRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        Instantiate(doorEnd[doorEndPrefab], _doorEndPosition, _doorEndRotation, this.transform);
        endDoorIsPresent = true;
    }
    public void GenerateChest()
    {
        nbChestToSpawn = nbChest;
        for (int i = 0; i < nbChestToSpawn; i++)
        {
            int _myRandPositionX = Random.Range(1, 9);
            int _myRandPositionZ = Random.Range(1, 9);
            Vector3 _randomPosition = new Vector3(_myRandPositionX, 0.0f, _myRandPositionZ);
            Instantiate(chest[chestPrefab], _randomPosition, Quaternion.identity, this.transform);
            nbChestSpawned++;            
        }
    }
    public void GenerateEnemy()
    {
        for (int i = 0; i < nbEnemy; i++)
        {
            int randomx = Random.Range(1, with);
            int randomy = Random.Range(1, heigh);
            Vector3 enemyPosition = new Vector3(randomx, 0.0f, randomy);
            Instantiate(enemy[enemyPrefab], enemyPosition, Quaternion.identity, this.transform);
        }
    }
}

