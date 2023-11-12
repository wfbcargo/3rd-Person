using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GoToPoint : MonoBehaviour
{
    public float Speed = 1;
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            var direction = Target.position - transform.position;
            var distance = direction.magnitude;
            var movement = direction.normalized * Speed * Time.deltaTime;
            if (movement.magnitude > distance)
            {
                movement = direction;
            }
            transform.position += movement;
        }
    }

    public bool PathToPoint(Vector3 point)
    {
        RaycastHit hit;
        var ray = new Ray(transform.position, point - transform.position);
        var distance = Vector3.Distance(transform.position, point);
        Debug.DrawRay(transform.position, point - transform.position, Color.green);
        if (Physics.Raycast(ray, out hit, distance))
        {
            return false;
        }
        return true;
    }
}
