using Assets.Scripts.TriggerType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDead())
        {
            Die();
        }
    }

    private void Die()
    {
        var onDie = GetComponents<ITriggerOnDie>();

        foreach(var trigger in onDie)
        {
            trigger.OnDie();
        }
    }

    private bool IsDead()
    {
        return CurrentHealth <= 0;
    }
}
