using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    int mentalSanity;
    int mentalSanityMax;
    public Character(int _healthPointMax, int _mentalSanityMax)
    {
        healthPointMax = _healthPointMax;
        healthPoint = healthPointMax;

        mentalSanityMax = _mentalSanityMax;
        mentalSanity = mentalSanityMax;
    }

    public override void Die()
    {
        healthPoint = 0;
    }
}
