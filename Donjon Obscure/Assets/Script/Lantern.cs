using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lantern : MonoBehaviour
{
    [SerializeField]
    Light light;
    //public static Lantern Instance {get; private set;}

    int fuelInReserveMax = 10;
    int currentFuelInReserve = 7;
    int radiusMax;
    int radius = 1;
    int intensity;
    int intensityMax;

    bool isActive = false;
    [SerializeField, Range(0, 10)]
    int fuelConsumptionCost;


    public AudioClip[] torchSound;
    private void Awake()
    {

        //if (Instance)
        //{
        //    Debug.LogError("Instance not null");
        //}

        //Instance = this;
    }

    public void SetActiveLantern()
    {
        if (!IsActive() && currentFuelInReserve > 0)
        {
            ConsumeFuel();
            isActive = true;
            light.enabled = true;
            this.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            isActive = false;
            light.enabled = true;
            this.GetComponent<AudioSource>().PlayOneShot(torchSound[1]);
            this.GetComponent<AudioSource>().mute = false;
        }
    }
    public bool IsActive()
    {
        return isActive;
    }
    
    public void ConsumeFuel()
    {
        currentFuelInReserve -= fuelConsumptionCost;
    } 
    public void AddFuelInReserve(int fuelToAdd)
    {
        Debug.Log("Current fuel before if " + (currentFuelInReserve + fuelToAdd));
        if (currentFuelInReserve + fuelToAdd > fuelInReserveMax)
        {
            currentFuelInReserve = fuelInReserveMax;
        }
        else
        {
            currentFuelInReserve += fuelToAdd;
        }
        Debug.Log("Current fuel after if " + (currentFuelInReserve));
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
