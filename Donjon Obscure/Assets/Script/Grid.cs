using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    [SerializeField]
    protected Transform tile;
    protected int WIDTH = 8;
    protected int HEIGHT = 8;
    
    [SerializeField]
    protected GameObject listTiles;
    
    void Start()
    {
        createGrid();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Tile[] getTiles()
    {
        return listTiles.transform.GetComponentsInChildren<Tile>();
    }

    protected void createGrid()
    {
        if(0 == 0)
        {
            for (var x = 0; x < WIDTH; x++)
            {
                for (var y = 0; y < HEIGHT; y++)
                {
                    Instantiate(tile, new Vector3(x,0,y), transform.rotation, listTiles.transform);
                    //Debug.Log(x + HEIGHT * y);
                    //listTiles.transform.GetChild(WIDTH + HEIGHT * y);
                }
            }   
        }
    }

    private void OnDrawGizmos()
    {
        //createGrid();
    }
}
