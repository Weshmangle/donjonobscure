using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanterne : MonoBehaviour
{
    int fuelMax;
    int fuelReserve;
    int radiusMax;
    int radius;
    int intensity;
    int intensityMax;

    bool isActive;

    int fuelConsumation;
    int fuelConsumationMax;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetActiveLanterne(bool active)
    {
        isActive = active;
    }
    public bool IsActive()
    {
        return isActive;
    }
    public int ConsumeFuelStatus()
    {
        return fuelConsumation;
    } 
    public int FuelInReserveStatus()
    {
        if (fuelReserve > fuelMax)
        {
            fuelReserve = fuelMax;
        }
        return fuelReserve;
    }
    public int LightRangeStatus()
    {
        if (radius > radiusMax)
        {
            radius = radiusMax;
        }
        return radius;
    }    
    public int IntensytyStatus()
    {
        return intensity;
    }
    //int intensity;
    //int intensityMax;

}
