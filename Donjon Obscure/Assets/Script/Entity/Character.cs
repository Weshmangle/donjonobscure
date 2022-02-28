using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    public delegate void PlayerStatChangedEventHandler(float health, float maxHealth, Stat stat);
    public event PlayerStatChangedEventHandler OnPlayerStatChange;
    [SerializeField] int mentalSanity;
    [SerializeField] int mentalSanityMax;
    [SerializeField] int armorPoint;
    [SerializeField] int armorPointMax;
    [SerializeField] public Lantern lantern;
    
    void Awake()
    {
        if(healthPoint == 0)
        {
            healthPoint = healthPointMax;
        }
        if(mentalSanity == 0)
        {
            mentalSanity = mentalSanityMax;
        }
        if(armorPoint == 0)
        {
            armorPoint = armorPointMax;
        }
        
    }
    private void Start()
    {
        OnPlayerStatChange?.Invoke(healthPoint, healthPointMax, Stat.HealthPoint);
        OnPlayerStatChange?.Invoke(mentalSanity, mentalSanityMax, Stat.MentalSanity);
    }
    /// <summary>
    /// Update one stat for the added value (can be negative)
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="addedValue"></param>
    public override void UpdateStat(Stat stat, int addedValue)
    {
        switch (stat)
        {
            case Stat.HealthPoint:
                {
                    if (healthPoint + addedValue > healthPointMax)
                    {
                        healthPoint = healthPointMax;
                    }
                    else healthPoint += addedValue;
                    OnPlayerStatChange?.Invoke(healthPoint, healthPointMax, Stat.HealthPoint);
                }
                break;
            case Stat.HealthPointMax:
                {
                    healthPointMax += addedValue;
                    healthPoint = healthPointMax;
                }
                break;
            case Stat.AttackStrength:
                {
                    attackStrenght += addedValue;
                }
                break;
            case Stat.ArmorPoint:
                {
                    if (armorPoint + addedValue > armorPointMax)
                    {
                        armorPoint = armorPointMax;
                    }
                    else armorPoint += addedValue;
                }
                break;
            case Stat.ArmorPointMax:
                {
                    armorPointMax += addedValue;
                    armorPoint = armorPointMax;
                }
                break;
            case Stat.MentalSanity:
                {
                    if (mentalSanity + addedValue > mentalSanityMax)
                    {
                        mentalSanity = mentalSanityMax;
                        OnPlayerStatChange?.Invoke(mentalSanity, mentalSanityMax, Stat.MentalSanity);
                    }
                    else mentalSanity += addedValue;
                }
                break;
            case Stat.MentalSanityMax:
                {
                    mentalSanityMax += addedValue;
                    mentalSanity = mentalSanityMax;
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
            OnPlayerStatChange?.Invoke(healthPoint, healthPointMax, Stat.HealthPoint);
        }
    }

    public void MoveOverHole(Vector2Int position)
    {
        Move(position);
    }

    public void LightLantern()
    {
        lantern.SetActiveLantern();
    }

    protected override void Die()
    {

        Game.Instance.panelDie.SetActive(true);
        Debug.Log("Character is dead");
        //jour le son de la mort
        this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
    }
}
