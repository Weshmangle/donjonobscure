using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField]
    Image HealthBar, SanityBar;
    void Start()
    {
        Game.game.character.OnPlayerStatChange += PlayerStatChanged;
    }

    private void PlayerStatChanged(int current, int maximum, Stat stat)
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
                break;
            case Stat.FuelInReserveMax:
                break;
            default:
                break;
        }
        //
    }

}
