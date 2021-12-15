using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    protected Room room;
    void Start()
    {
        Debug.Log("lol " + this.room.grid.getTiles());
        Debug.Log("tile " + this.room.grid.getTiles().Length);
        foreach (var tile in this.room.grid.getTiles())
        {
            Debug.Log(tile);
            tile.setEvent(check);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void check()
    {
        Debug.Log("Tile clicked");
    }
}
