using Assets.Scripts.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cannon : Gun
{
    private float lastShotTime;
    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CreateProjectiles()
    {
        var projectile = Object.Instantiate(Projectile, Origin.position, transform.rotation);
        var velocity = transform.forward * BulletSpeed;
        projectile.GetComponent<Bullet>().XSpeed = velocity.x;
        projectile.GetComponent<Bullet>().YSpeed = velocity.y;
        projectile.GetComponent<Bullet>().ZSpeed = velocity.z;
    }

    protected override void Fire()
    {
        CreateProjectiles();
    }
}
