using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [SerializeField]
    protected ElementGrid content;

    [SerializeField]
    protected GameObject Over;
    
    [SerializeField]
    protected UnityEvent<Tile> uEvent;
    protected Vector2Int position;
    
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

    public void setEvent(UnityAction<Tile> action)
    {
        uEvent.AddListener(action);
    }

    public ElementGrid getContent()
    {
        return this.content;
    }

    public void setContent(ElementGrid element)
    {
        if(this.content != null)
        {
            Destroy(this.content.gameObject);
        }
        
        this.content = element;
        
        element.transform.SetParent(this.transform);

        if(typeof(Hole).IsInstanceOfType(element))
        {
            this.GetComponentInChildren<Renderer>().enabled = false;
        }
    }

    public Vector2Int getPosition()
    {
        return new Vector2Int((int)this.transform.position.x, (int)this.transform.position.z);
        //return this.position;
    }
    public void setPosition(Vector2Int position)
    {
        this.position = position;
    }
}
