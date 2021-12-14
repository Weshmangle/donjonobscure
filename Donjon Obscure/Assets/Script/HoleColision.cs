using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleColision : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "trou")
        {
            player.GetComponent<Player>().FallInHole();         

        }
        if (other.gameObject.tag == "bonus")
        {

        }
    }
}
