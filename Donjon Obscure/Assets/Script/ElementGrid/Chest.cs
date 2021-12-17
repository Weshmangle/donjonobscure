using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : ElementGrid
{
    protected bool open;
    
    [SerializeField]
    protected GameObject head;
    
    protected Item content = null;

    AudioSource chestsource;//source du son
    AudioClip chestsound;//son qui serra joué
    void Start()
    {
        this.open = false;
        // je donne la source a audiosource et le clip a audioclip
        chestsource = this.GetComponent<AudioSource>();
        chestsound = this.GetComponent<AudioSource>().clip;
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
        //quand le coffre s'ouvre on joue le son
        chestsource.PlayOneShot(chestsound);
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
