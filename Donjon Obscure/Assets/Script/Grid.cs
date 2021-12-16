using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{
    [SerializeField]
    protected Transform tile;
    protected int WIDTH = 10;
    protected int HEIGHT = 10;
    
    [SerializeField]
    protected GameObject listTiles;
    protected Tile[,] tiles;

    [SerializeField]
    protected ElementsGenerator elementGenerator;

    void Start()
    {
        //elementGenerator.GenerateElement(new Tile[10,10]);
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

    public Tile getTile(Vector2Int vector2)
    {
        return this.getTiles()[0];
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
                //obj.setPosition(new Vector3(x,0,y));
                //tiles[x,y] = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        createGrid();
    }
}
