using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    protected Room room;

    public static Game game = null;

    protected bool eventSet = false;
    
    void Start()
    {
        if(Game.game == null)
        {
            Game.game = this;
        }
        
        addEventsOnTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addEventsOnTiles()
    {
        foreach (var tile in this.room.grid.getTiles())
        {
            tile.setEvent(CheckTileContent);
        }
    }

    void CheckTileContent(Tile tile)
    {
        Debug.Log("Tile " + tile.transform);
    }
}
