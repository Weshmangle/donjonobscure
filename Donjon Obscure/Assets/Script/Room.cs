using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    protected object character;
    [SerializeField]
    protected List<Enemy> ennemies;
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

    public List<Enemy> getEnnemies()
    {
        return this.ennemies;
    }
}
