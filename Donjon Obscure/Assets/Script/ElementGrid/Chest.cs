using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : ElementGrid
{
    [SerializeField]
    GameObject head, lootPopup;
    
    Item content = null;
    [SerializeField]
    List<Item> loots;

    bool isOpen;

    float targetRotation = 0.75f;
    Quaternion rot;

    AudioSource chestsource;//source du son
    AudioClip chestsound;//son qui serra jou�

    Character character;
    void Awake()
    {
        rot = head.transform.rotation;
        isOpen = false;
        // je donne la source a audiosource et le clip a audioclip
        chestsource = this.GetComponent<AudioSource>();
        chestsound = this.GetComponent<AudioSource>().clip;

        GenerateContent();
    }

    void Update()
    {
        
        if (isOpen)
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
        if(isOpen)
        {
            
            Game.UpdateCharacter(TakeContent());
        }
        else
        {
            isOpen = true;
            //quand le coffre s'ouvre on joue le son
            chestsource.PlayOneShot(chestsound);
            lootPopup.SetActive(true);
        }

    }

    void GenerateContent()
    {
        content = loots[Random.Range(0, loots.Count)];
        lootPopup.GetComponent<SpriteRenderer>().sprite = content.Artwork;
    }

    Item TakeContent()
    {
        lootPopup.SetActive(false);
        return content;
        
    }
}
