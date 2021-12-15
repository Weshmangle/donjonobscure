using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [SerializeField]
    protected IElementGrid content = null;
    [SerializeField]
    protected UnityEvent<Tile> uEvent;
    protected Vector2 position;
    
    void Start()
    {
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

    public IElementGrid getContent()
    {
        return this.content;
    }
}
