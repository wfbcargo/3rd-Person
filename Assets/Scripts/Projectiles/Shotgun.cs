using Assets.Scripts.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public float ShotsInBlast = 8;
    public float SpreadRadius = .1f;
    public float SpreadAngle = 10f;

    //last shotTime
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

    private void CreateShot()
    {
        var spreadPosition = Random.insideUnitCircle * SpreadRadius;
        var position = Origin.position;
        //Add spread x to right of position
        position += transform.right * spreadPosition.x;
        //Add spread y to up of position
        position += transform.up * spreadPosition.y;

        var projectile = Object.Instantiate(Projectile, position, transform.rotation);
        var velocity = transform.forward * BulletSpeed;
        //Add random spread angle to velocity
        velocity = Quaternion.AngleAxis(Random.Range(-SpreadAngle, SpreadAngle), transform.up) * velocity;
        velocity = Quaternion.AngleAxis(Random.Range(-SpreadAngle, SpreadAngle), transform.right) * velocity;
        projectile.GetComponent<Bullet>().XSpeed = velocity.x;
        projectile.GetComponent<Bullet>().YSpeed = velocity.y;
        projectile.GetComponent<Bullet>().ZSpeed = velocity.z;
    }

    protected override void Fire()
    {
        for (int i = 0; i < ShotsInBlast; i++)
        {
            CreateShot();
        }
    }
}
