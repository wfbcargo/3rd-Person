using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDetector : MonoBehaviour
{
    public Gravity Gravity = null;
    public bool Stop = false;

    // Start is called before the first frame update
    private float groundDistance = 0.1f;
    private Vector3 Speed = Vector3.zero;

    void Start()
    {
        if(Gravity == null)
        {
            Gravity = FindObjectOfType<Gravity>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGrounded())
        {
            Speed = Vector3.zero;
            return;
        }
        var nextPosition = GetNextPosition();
        var ray = new Ray(transform.position, nextPosition - transform.position);

        var distance = Vector3.Distance(transform.position, nextPosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            var hitObject = hit.collider.gameObject;
            transform.position = hit.point;
            Speed = Vector3.zero;
        }
        else
        {
            transform.position = nextPosition;
        }
    }

    private bool IsGrounded()
    {
        var gravityDirection = Gravity.GravityDirection.normalized;
        var ray = new Ray(transform.position, gravityDirection);
        RaycastHit hit;
        Debug.DrawRay(transform.position, gravityDirection * groundDistance, Color.green);
        return Physics.Raycast(ray, out hit, groundDistance);
    }

    private Vector3 GetNextPosition()
    {
        if (Gravity != null)
        {
            Speed.y += Gravity.GravityDirection.y * Gravity.GravityForce * Time.deltaTime;
            Speed.x += Gravity.GravityDirection.x * Gravity.GravityForce * Time.deltaTime;
            Speed.z += Gravity.GravityDirection.z * Gravity.GravityForce * Time.deltaTime;
        }
        return transform.position + Speed;
    }
}
