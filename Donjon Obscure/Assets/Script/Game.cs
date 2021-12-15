using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    protected Room room;

    protected bool eventSet = false;
    void Start()
    {
        this.room.grid.setEvent(addEventsOnTiles);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void addEventsOnTiles()
    {
        Debug.Log("addEventsOnTiles");
        foreach (var tile in this.room.grid.getTiles())
        {
            tile.setEvent(check);
        }
    }

    void check()
    {
        Debug.Log("Tile clicked");
    }
}
