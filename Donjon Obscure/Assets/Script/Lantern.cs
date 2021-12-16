using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    int fuelMax;
    int fuelReserve;
    int radiusMax;
    int radius;
    int intensity;
    int intensityMax;

    bool isActive;

    int fuelConsumption;
    int fuelConsumptionMax;

    public void SetActiveLantern(bool active)
    {
        isActive = active;
    }
    public bool IsActive()
    {
        return isActive;
    }
    public int ConsumeFuelStatus()
    {
        return fuelConsumption;
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
    public int IntensityStatus()
    {
        return intensity;
    }
    //int intensity;
    //int intensityMax;

}
