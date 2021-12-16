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
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.listTiles.transform.childCount);
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

    protected IEnumerator lol()
    {
        yield return new WaitForSeconds(1.0f);
    }
    

    public void createGrid()
    {
        if(this.listTiles.transform.childCount != 0) return;
        
        tiles = new Tile[WIDTH, HEIGHT];

        for (var x = 0; x < WIDTH; x++)
        {
            for (var y = 0; y < HEIGHT; y++)
            {
                Debug.Log("createTile");
                GameObject obj = Instantiate(Resources.Load("Prefabs/Tile"), new Vector3(x,0,y), transform.rotation, listTiles.transform) as GameObject;
                Tile tile = obj.GetComponent<Tile>();
                tile.setPosition(new Vector2Int(x,y));
                tiles[x,y] = obj.GetComponent<Tile>();
            }
        }
        elementGenerator.GenerateElement(tiles);
    }

    public void cleanGrid()
    {
        
    }

    private void OnDrawGizmos()
    {
        //createGrid();
        //this.listTiles = Instantiate(new GameObject(), transform.position, transform.rotation, this.transform);
    }
}
