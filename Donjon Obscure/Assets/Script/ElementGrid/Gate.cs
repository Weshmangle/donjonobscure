using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : ElementGrid
{
    public GameObject doorExit;
    public GameObject doorEntrance;
    public bool isExitGate = false;
    public bool IsExitGate
    {
        get { return isExitGate; } 
        set { isExitGate = value; } 
    }
    bool isOpen, isLocked;
    public void GameSetEnd()
    {
        this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
    }
    private void Update()
    {
        if(doorEntrance && doorExit)
        {
            doorEntrance.SetActive(!isExitGate);
            doorExit.SetActive(isExitGate);
        }    
    }
    public void LoadNextRoom()
    {

    }
    public void GoToNextRoom()
    {

    }
}
