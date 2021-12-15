using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy(int _healthPointMax, int _attackStrength)
    {
        healthPointMax = _healthPointMax;
        healthPoint = healthPointMax;

        attackStrenght = _attackStrength;
    }
    protected override void Die()
    {
        Debug.Log("Enemy is dead");
        Destroy(this.gameObject);
    }
}
