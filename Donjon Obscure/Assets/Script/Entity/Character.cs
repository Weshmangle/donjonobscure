using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    int mentalSanity;
    int mentalSanityMax;

    protected int armorPoint;
    protected int armorPointMax;

    [SerializeField]
    public Lantern lantern;

    public Character(int _healthPointMax, int _mentalSanityMax, int _attackStrength)
    {
        healthPointMax = _healthPointMax;
        healthPoint = healthPointMax;

        mentalSanityMax = _mentalSanityMax;
        mentalSanity = mentalSanityMax;

        attackStrenght = _attackStrength;
    }

    public override void UpdateStat(int addedValue, Stat stat)
    {
        switch (stat)
        {
            case Stat.HealthPoint:
                {
                    healthPoint += addedValue;
                }
                break;
            case Stat.HealthPointMax:
                {
                    healthPointMax += addedValue;
                }
                break;
            case Stat.AttackStrength:
                {
                    attackStrenght += addedValue;
                }
                break;
            case Stat.ArmorPoint:
                {
                    armorPoint += addedValue;
                }
                break;
            case Stat.ArmorPointMax:
                {
                    armorPointMax += addedValue;
                }
                break;
            case Stat.MentalSanity:
                {
                    mentalSanity += addedValue;
                }
                break;
            case Stat.MentalSanityMax:
                {
                    mentalSanityMax += addedValue;
                }
                break;
            default:
                Debug.Log("Provided stat does not exist.");
                break;
        }
    }
    /// <summary>
    /// Inflige n damage to Character.
    /// </summary>
    /// <param name="damage">Damage value to inflige</param>
    public override void TakeDamage(int damage)
    {
        damage -= armorPoint;
        if(damage > 0)
        {
            base.TakeDamage(damage);
        }
    }

    public void LightLantern()
    {
        lantern.SetActiveLantern();
        
    }

    protected override void Die()
    {
        Debug.Log("Character is dead");
        //jour le son de la mort
        this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
    }
}
