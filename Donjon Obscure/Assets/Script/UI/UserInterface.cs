using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] Image HealthBar, SanityBar, OilBar;
    public Text textNameLevel;
    void Start()
    {
        Game.Instance.character.OnPlayerStatChange += PlayerStatChanged;
        Game.Instance.character.lantern.OnLanternOilInReserveChange += PlayerStatChanged;
    }

    private void Update()
    {
        textNameLevel.text = Game.Instance.grid.seed.name;   
    }

    private void PlayerStatChanged(float current, float maximum, Stat stat)
    {
        switch (stat)
        {
            case Stat.HealthPoint:
                HealthBar.fillAmount = current / (float)maximum;
                break;
            case Stat.HealthPointMax:
                break;
            case Stat.AttackStrength:
                break;
            case Stat.ArmorPoint:
                break;
            case Stat.ArmorPointMax:
                break;
            case Stat.MentalSanity:
                SanityBar.fillAmount = current / (float)maximum;
                break;
            case Stat.MentalSanityMax:
                break;
            case Stat.CurrentFuelInReserve:
                OilBar.fillAmount = current / (float)maximum;
                break;
            case Stat.FuelInReserveMax:
                break;
            default:
                break;
        }
        //
    }

}
