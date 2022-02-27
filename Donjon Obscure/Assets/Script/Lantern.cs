using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lantern : MonoBehaviour
{
    public delegate void LanternOilInReserveEventHandler(float current, float max, Stat stat);
    public event LanternOilInReserveEventHandler OnLanternOilInReserveChange;
    [SerializeField] Light lantern, lanternHalo;

    //public static Lantern Instance {get; private set;}

    int fuelInReserveMax = 10;
    public int FuelInReserveMax
    {
        get; set;
    }
    [SerializeField] float currentFuelInReserve = 7;
    int radiusMax;
    int radius = 3;
    int intensity;
    int intensityMax;

    bool isActive = false;
    [SerializeField, Range(0, 10)] float fuelConsumptionCost;

    public AudioClip[] torchSound;

    private void Start()
    {
        OnLanternOilInReserveChange?.Invoke(currentFuelInReserve, fuelInReserveMax, Stat.CurrentFuelInReserve);
    }

    public void SetActiveLantern()
    {
        if (!IsActive() && currentFuelInReserve > 0)
        {
            isActive = true;
            lantern.enabled = true;
            lanternHalo.enabled = true;
            this.GetComponent<AudioSource>().PlayOneShot(torchSound[1]);
            this.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            isActive = false;
            lanternHalo.enabled = false;
            lantern.enabled = false;
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

        OnLanternOilInReserveChange?.Invoke(currentFuelInReserve, fuelInReserveMax, Stat.CurrentFuelInReserve);

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
        OnLanternOilInReserveChange?.Invoke(currentFuelInReserve, fuelInReserveMax, Stat.CurrentFuelInReserve);
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
