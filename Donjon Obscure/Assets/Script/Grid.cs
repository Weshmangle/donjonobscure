using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    [SerializeField]
    protected Transform tile;
    protected int WIDTH = 15;
    protected int HEIGHT = 10;
    
    [SerializeField]
    protected GameObject listTiles;

    [SerializeField]
    protected MapGenerator mapGenerator;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tile[] getTiles()
    {
        //return listTiles.transform.GetComponentsInChildren<Tile>();
        
        return listTiles.transform.GetComponentsInChildren<Tile>();
    }

    protected void createGrid()
    {
        if(this.listTiles.transform.childCount != 0) return;

        Debug.Log("Create MAP");

        for (var x = 0; x < WIDTH; x++)
        {
            for (var y = 0; y < HEIGHT; y++)
            {
                GameObject obj = Instantiate(Resources.Load("Prefabs/Tile"), new Vector3(x,0,y), transform.rotation, listTiles.transform) as GameObject;
            }
        }
    }

    private void OnDrawGizmos()
    {
        createGrid();
    }
}