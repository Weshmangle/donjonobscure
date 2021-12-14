using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mapGenerator;
    [SerializeField] bool mapIsStarted = false, mapIsInProgresse = false, mapIsEnd = true, gameIsEnd = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
        if (mapIsEnd)
        {
            mapGenerator.GetComponent<MapGenerator>().GenerateMap();
            mapIsEnd = false;
            mapIsStarted = true;
        }
        if (mapIsStarted)
        {

        }
        if (mapIsInProgresse)
        {
            if (gameIsEnd)
            {
                mapIsEnd = true;
                mapIsStarted = false;
                mapIsInProgresse = false;
            }
        }*/
    }
}
