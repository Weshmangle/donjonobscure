using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelDie : MonoBehaviour
{

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }

    public void backMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
