using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lantern : MonoBehaviour
{
    [SerializeField]
    Light light, lantern;

    //public static Lantern Instance {get; private set;}

    int fuelInReserveMax = 10;
    public int FuelInReserveMax
    {
        get; set;
    }
    int currentFuelInReserve = 7;
    int radiusMax;
    int radius = 1;
    int intensity;
    int intensityMax;

    bool isActive = false;
    [SerializeField, Range(0, 10)]
    int fuelConsumptionCost;


    public AudioClip[] torchSound;

    public void SetActiveLantern()
    {
        if (!IsActive() && currentFuelInReserve > 0)
        {
            isActive = true;
            light.enabled = true;
            lantern.enabled = true;
            this.GetComponent<AudioSource>().PlayOneShot(torchSound[1]);
            this.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            isActive = false;
            lantern.enabled = false;
            light.enabled = false;
            this.GetComponent<AudioSource>().mute = true;
        }
    }
    public bool IsActive()
    {
        return isActive;
    }
    
    public void ConsumeFuel()
    {
        if (currentFuelInReserve - fuelConsumptionCost <= 0)
        {
            currentFuelInReserve = 0;
        }
        else
        {
            currentFuelInReserve -= fuelConsumptionCost;
        }

        if (currentFuelInReserve == 0)
            SetActiveLantern();
        
    } 
    public void AddFuelInReserve(int fuelToAdd)
    {
        if (currentFuelInReserve + fuelToAdd > fuelInReserveMax)
        {
            currentFuelInReserve = fuelInReserveMax;
        }
        else
        {
            currentFuelInReserve += fuelToAdd;
        }
    }
    public int LightRange()
    {
        /*if (radius > radiusMax)
        {
            radius = radiusMax;
        }*/
        return radius;
    }    
    public int IntensityStatus()
    {
        return intensity;
    }
    //int intensity;
    //int intensityMax;

}
