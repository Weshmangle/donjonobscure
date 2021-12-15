using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    protected bool open;
    
    [SerializeField]
    protected GameObject head;
    
    protected Item content = null;
    
    void Start()
    {
        this.open = false;
    }

    void Update()
    {
        if(this.open)
        {
            Quaternion rot = this.head.transform.rotation;
            
            if(rot.w > 0.75)
            {
                this.head.transform.Rotate(new Vector3(-.25f,0,0), Space.World);
            }
        }
    }
    
    
    public void Open()
    {
        this.open = true;
    }

    public bool isOpen()
    {
        return this.open;
    }

    public Item getContent()
    {
        return this.content;
    }

    public Item takeContent()
    {
        Item content = this.content;
        
        this.content = null;
        
        return content;
    }
}
