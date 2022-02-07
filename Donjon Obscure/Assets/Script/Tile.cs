using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [SerializeField]
    protected ElementGrid content;
    public ElementGrid Content
    {
        get => this.content;
        set
        {
            this.content = value;

            if (this.content != null)
            {
                //Destroy(this.content.gameObject);
                value.transform.SetParent(this.transform);
            }

            if (typeof(Hole).IsInstanceOfType(value))
            {
                this.GetComponentInChildren<Renderer>().enabled = false;
            }
        }
    }

    [SerializeField]
    protected GameObject Over;
    
    [SerializeField]
    protected GameObject lighted;
    
    [SerializeField]
    protected UnityEvent<Tile> uEvent;
    protected Vector2Int position;

    public Vector2Int Position
    {
        get{return position;}
        set{position = value;}
    }
    
    void Start()
    {
        Over.GetComponent<Renderer>().enabled = false;
    }

    private void OnMouseDown()
    {
        uEvent.Invoke(this);
    }

    private void OnMouseExit()
    {
        Over.GetComponent<Renderer>().enabled = false;
    }

    private void OnMouseOver()
    {
        Over.GetComponent<Renderer>().enabled = true;
    }

    void Update()
    {
        
    }

    public void showTile(bool show)
    {
        lighted.GetComponentInChildren<Renderer>().enabled = !show;
            
        if(content != null)
        {
            if(!typeof(Gate).IsInstanceOfType(content) && !typeof(Wall).IsInstanceOfType(content))
            {
                content.transform.localScale = show ? new Vector3(1,1,1) : new Vector3(0,0,0);
            }
            if(typeof(Hole).IsInstanceOfType(content))
            {
                 this.GetComponentInChildren<Renderer>().enabled = !show;
            }
        }
        
    }

    public void setEvent(UnityAction<Tile> action)
    {
        uEvent.AddListener(action);
    }

    
}
