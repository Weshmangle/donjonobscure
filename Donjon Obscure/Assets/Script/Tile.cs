using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [SerializeField]
    protected GameObject content = null;
    [SerializeField]
    protected UnityEvent<Tile> uEvent;
    protected Vector2 position;
    
    void Start()
    {
        if(content)
        {
            
            Instantiate(content, new Vector3(0,.25f,0), Quaternion.Euler(0,0,0), transform);
        }
    }

    private void OnMouseDown()
    {
        uEvent.Invoke(this);
    }

    void Update()
    {
        
    }

    public void setEvent(UnityAction<Tile> action)
    {
        uEvent.AddListener(action);
    }

    public GameObject getContent()
    {
        return this.content;
    }
}
