using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour
{

    [SerializeField]
    protected Transform tile;
    protected int WIDTH = 8;
    protected int HEIGHT = 8;
    
    [SerializeField]
    protected GameObject listTiles;

    [SerializeField]
    protected UnityEvent uEvent;
    
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

    public void setEvent(UnityAction action)
    {
        this.uEvent.AddListener(action);
    }

    protected void createGrid()
    {
        for (var x = 0; x < WIDTH; x++)
        {
            for (var y = 0; y < HEIGHT; y++)
            {
                Instantiate(tile, new Vector3(x,0,y), transform.rotation, listTiles.transform);
                Object prefab = Resources.Load("Prefabs/YourPrefab");
                GameObject gameObject =Instantiate(prefab) as GameObject;
            }
        }
        Debug.Log("Invoke");
        uEvent.Invoke();
    }

    private void OnDrawGizmos()
    {
        //createGrid();
    }
}
