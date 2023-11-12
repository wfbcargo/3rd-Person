using Assets.Scripts.TriggerType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour, ITriggerOnCollide
{
    public float DamageOnHit = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InflictDamage(Health health)
    {
        health.CurrentHealth -= DamageOnHit;
    }

    public void OnCollide(GameObject collidedWith)
    {
        var healthComponents = collidedWith.GetComponents<Health>();
        foreach (var health in healthComponents)
        {
            InflictDamage(health);
        }
    }
}
