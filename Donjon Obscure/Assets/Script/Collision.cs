using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    int layerMask, layerMask2;

    public float CheckD;
    public GameObject CheckPoint;
    
    void Start()
    {
        layerMask = LayerMask.GetMask("wall");
        layerMask2 = LayerMask.GetMask("trou");

    }


    public bool CheckFront()
    {
        bool front_result = false;
        if (Physics.Raycast(CheckPoint.transform.position, this.transform.forward, CheckD, layerMask))
        {
            front_result = true;
        }        
        return front_result;
    }

    public bool CheckBack()
    {
        bool back_result = false;

        if (Physics.Raycast(CheckPoint.transform.position, -this.transform.forward, CheckD, layerMask))
        {
            back_result = true;
        }        
        return back_result;
    }

    public bool CheckLeft()
    {
        bool left_result = false;

        if (Physics.Raycast(CheckPoint.transform.position, -this.transform.right, CheckD, layerMask))
        {
            left_result = true;
        }        
        return left_result;
    }

    public bool CheckRight()
    {
        bool right_result = false;

        if (Physics.Raycast(CheckPoint.transform.position, this.transform.right, CheckD, layerMask))
        {
            right_result = true;
        }        
        return right_result;
    }    
    void Update()
    {

    }
}
