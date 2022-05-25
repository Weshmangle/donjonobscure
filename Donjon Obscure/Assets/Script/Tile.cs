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

            this.GetComponentInChildren<Renderer>().enabled = !typeof(Hole).IsInstanceOfType(value);
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

    public void OnMouseDown()
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
        
        if(content is Enemy)
        {
            CursorManager.Instance.SetAttaktCursor();
        }
        else if(content is Chest || content is Gate || content is Hole || content is Chest)
        {
            CursorManager.Instance.SetInterractionCursor();
        }
        else if(!(content is Wall))
        {
            CursorManager.Instance.SetMovementCursor();
        }
        else
        {
            CursorManager.Instance.SetDefaultCursor();
        }
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

    public override string ToString()
    {
        return "Tile -> " + ((content == null) ? "Empty" : content.name);
    }
}
