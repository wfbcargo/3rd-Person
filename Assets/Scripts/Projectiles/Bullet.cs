using Assets.Scripts.TriggerType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float XSpeed = 0;
    public float YSpeed = 0;
    public float ZSpeed = 0;

    public bool Stop = false;
    public float LifeTime = 5;

    private float startTime;
    private Vector3 speed;
    private Vector3 nextPosition;

    private Gravity gravity;
    private bool hitSomething;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        speed = new Vector3(XSpeed, YSpeed, ZSpeed);
        nextPosition = transform.position + speed * Time.deltaTime;
        gravity = FindObjectOfType<Gravity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Stop)
        {
            return;
        }

        var ray = new Ray(transform.position, nextPosition - transform.position);

        var distance = Vector3.Distance(transform.position, nextPosition);

        RaycastHit hit;
        Debug.DrawRay(transform.position, nextPosition - transform.position, Color.red);
        if (Physics.Raycast(ray, out hit, distance))
        {
            var hitObject = hit.collider.gameObject;
            if(hitObject.layer != LayerMask.NameToLayer("Bullets"))
            {
                Stop = true;
                transform.position = hit.point; 
                hitSomething = true;
                OnHitSomething(hitObject);
            }
        }
        

        if(!hitSomething)
        {
            transform.position = nextPosition;
            SetNextPosition();
        }
    }

    private void LateUpdate()
    {
        if (Time.time - startTime > LifeTime || hitSomething)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void SetNextPosition()
    {
        if(gravity != null)
        {
            speed.y += gravity.GravityDirection.y * gravity.GravityForce;
            speed.x += gravity.GravityDirection.x * gravity.GravityForce;
            speed.z += gravity.GravityDirection.z * gravity.GravityForce;
        }
        nextPosition += speed * Time.deltaTime;
    }

    private void OnHitSomething(GameObject hitObject)
    {
        var triggerOnCollide = gameObject.GetComponents<ITriggerOnCollide>();
        foreach (var trigger in triggerOnCollide)
        {
            trigger.OnCollide(hitObject);
        }
    }
}
