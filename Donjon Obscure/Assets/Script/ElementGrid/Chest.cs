using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : ElementGrid
{
    [SerializeField] GameObject head, lootPopup;
    
    Item content = null;
    [SerializeField] List<Item> loots;

    bool isOpen;

    //target rotation y is at 180 because the transform of the chest is 180 when generated to face the player.
    Quaternion targetRotation = Quaternion.Euler(-25, 180, 0);
    float smooth = 3;

    AudioSource chestsource;//source du son
    AudioClip chestsound;//son qui serra jou�

    void Awake()
    {
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
                head.transform.rotation = Quaternion.Slerp(head.transform.rotation, targetRotation, Time.deltaTime * smooth);
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
        lootPopup.GetComponentInChildren<TMPro.TextMeshPro>().text = content.BonusValue.ToString();
    }

    Item TakeContent()
    {
        lootPopup.SetActive(false);
        return content;
        
    }
}
