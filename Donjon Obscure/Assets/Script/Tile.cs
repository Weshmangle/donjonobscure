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
    protected Vector2 position;
    
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
        this.content = element;
        element.transform.SetParent(this.transform);
    }

    public Vector2 getPosition()
    {
        return this.position;
    }
}
