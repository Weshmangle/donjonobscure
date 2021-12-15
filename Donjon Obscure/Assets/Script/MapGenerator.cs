using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    [SerializeField] int with, heigh, groundSize; // controle la taille de la map (groundside est la taille d'une tuille)
    [SerializeField] GameObject[] wall, ground, enemy, player, doorStart, doorEnd, hole, chest, item, levier, key; // liste des prefab utilisé dans une map pour en ajouter des nouveau facilement
    [SerializeField] int nbMonster, nbChest, nbItem, nbLevier, nbKey;
    int nbChestSpawned, nbChestToSpawn;
    bool startDoorIsPresent, endDoorIsPresent;
    int startDoorSide, endDoorSide;
    public Vector3[] interneGroundPosition, chestPosition, endDoorPosition ;

    public int wallPrefab, groundPrefab, enemyPrefab, playerPrefab, doorStartPrefab, doorEndPrefab, holePrefab, chestPrefab, itemPrefab, levierPrefab, keyPrefab; // [le choix du skin] quel prefab de quel object on utilise dans la liste (exemple: chest[chestP] = je veux l'object chest avec le numero chestP de la liste) 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            //GenerateChest();
            GenerateMap();
            GenerateDoor();
            

        }
    }
    public void GenerateMap()
    {
        //MUR EXTERIEUR//
        Vector3 _origine = new Vector3(0.0f, 0.0f, 0.0f);
        for (int i = 0; i < with; i++)
        {
            Instantiate(wall[wallPrefab], _origine, Quaternion.identity);
            _origine.z += groundSize;
            Debug.Log("Z+ : " + _origine);
        }
        for (int j = 0; j < heigh; j++)
        {
            Instantiate(wall[wallPrefab], _origine, Quaternion.identity);
            _origine.x += groundSize;
            Debug.Log("X+ : " + _origine);
        }
        _origine = new Vector3(0.0f, 0.0f, 0.0f);
        for (int l = 0; l < heigh; l++)
        {
            Instantiate(wall[wallPrefab], _origine, Quaternion.identity);
            //if (!Physics.CheckBox(_origine, _origine))
            // {

            // }
            _origine.x += groundSize;
            Debug.Log("X- : " + _origine);
        }
        for (int k = 0; k < with; k++)
        {
            Instantiate(wall[wallPrefab], _origine, Quaternion.identity);           
            _origine.z += groundSize;
            Debug.Log("Z- : " + _origine);
        }
        _origine.z = with;
        _origine.x = heigh;
        Instantiate(wall[wallPrefab], _origine, Quaternion.identity);

        //GROUND && HOLE//


    }
    public void GenerateDoor() 
    {
        if (!startDoorIsPresent)
        {
            Vector3 _doorStartPosition = new Vector3(0.0f, 0.0f, 0.0f);
            int _randomSide = Random.Range(0, 4);
            int _randDoorStartPositionWith = Random.Range(1, with);
            int _randDoorStartPositionHeigh = Random.Range(1, heigh);            
            if (_randomSide == 0)
            {
                _doorStartPosition = new Vector3(0.0f, 1.0f, _randDoorStartPositionWith);                
            }
            if (_randomSide == 1)
            {
                _doorStartPosition = new Vector3(with, 1.0f, _randDoorStartPositionWith);               
            }
            if (_randomSide == 2)
            {
                _doorStartPosition = new Vector3(_randDoorStartPositionHeigh, 1.0f, 0.0f);
            }
            if (_randomSide == 3)
            {
                _doorStartPosition = new Vector3(_randDoorStartPositionHeigh, 1.0f, heigh);
            }
            Instantiate(doorStart[doorStartPrefab], _doorStartPosition, Quaternion.identity);
            startDoorIsPresent = true;
            startDoorSide = _randomSide;
            GenerateEndDoor();
        }
        
    }
    public void GenerateEndDoor()
    {
        Vector3 _doorEndPosition = new Vector3(0.0f, 0.0f, 0.0f);
        int _randomSide = Random.Range(0, 5);
        int _randDoorEndPositionWith = Random.Range(1, with);
        int _randDoorEndPositionHeigh = Random.Range(1, heigh);
        if (startDoorSide == _randomSide)
        {

        }
        if (_randomSide == 0)
        {
            _doorEndPosition = new Vector3(0.0f, 1.0f, _randDoorEndPositionWith);            
        }
        if (_randomSide == 1)
        {
            _doorEndPosition = new Vector3(0.0f, 1.0f, -_randDoorEndPositionWith);
        }
        if (_randomSide == 2)
        {
            _doorEndPosition = new Vector3(_randDoorEndPositionHeigh, 1.0f, 0.0f);
        }
        if (_randomSide == 3)
        {
            _doorEndPosition = new Vector3(-_randDoorEndPositionHeigh, 1.0f, 0.0f);
        }
        Instantiate(doorEnd[doorEndPrefab], _doorEndPosition, Quaternion.identity);
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

            if (!Physics.CheckBox(_randomPosition, _randomPosition))
            {
                Instantiate(chest[0], _randomPosition, Quaternion.identity);
                nbChestSpawned++;
            }
        }
    }
}

