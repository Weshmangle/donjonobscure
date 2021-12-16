using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButon : MonoBehaviour
{
    // Start is called before the first frame update
    public void Scene1()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
