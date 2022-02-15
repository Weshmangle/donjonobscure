using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    protected object character;
    [SerializeField]
    protected List<Enemy> enemies;
    public List<Enemy> Enemies
    {
        get { return enemies; }
        set { enemies = value; }
    }
        

    [SerializeField]
    public Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
