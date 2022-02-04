using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : ElementGrid
{
    bool isExitGate = false;
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
    public void LoadNextRoom()
    {

    }
    public void GoToNextRoom()
    {

    }
}
