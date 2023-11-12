using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover
{
    public Transform DirectionReference;
    public float Speed = 12.0f;
    public CharacterController controller;
    public Gravity Gravity = null;
    public bool EnableGravity = true;
    // Start is called before the first frame update

    private Vector3 gravSpeed = Vector3.zero;
    public CharacterMover(Transform directionReference, float speed, CharacterController controller, Gravity gravity, ref bool enableGravity)
    {
        DirectionReference = directionReference;
        Speed = speed;
        this.controller = controller;
        Gravity = gravity;
        EnableGravity = enableGravity; 

        gravSpeed = Vector3.zero;
    }

    public void Move(bool forward, bool backward, bool left, bool right)
    {
        var xDiff = 0f;
        var yDiff = 0f;
        //implement WASD Movements
        if (forward)
        {
            yDiff = 1;
        }
        if (left)
        {
            xDiff = -1;
        }
        if (backward)
        {
            yDiff = -1;
        }
        if (right)
        {
            xDiff = 1;
        }

        var velocity = DirectionReference.forward * yDiff + DirectionReference.right * xDiff;
        velocity.Normalize();
        velocity *= Speed * Time.deltaTime;

        controller.Move(velocity);

        if (EnableGravity)
        {
            GravityMove();
        }
    }

    public void Move(Vector3 direction)
    {
        direction.Normalize();
        var velocity = direction * Speed * Time.deltaTime;

        controller.Move(velocity);

        if (EnableGravity)
        {
            GravityMove();
        }
    }

    private void GravityMove()
    {
        var nextPosition = GetNextPosition();
        var ray = new Ray(controller.transform.position, nextPosition - controller.transform.position);

        var distance = Vector3.Distance(controller.transform.position, nextPosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            var hitObject = hit.collider.gameObject;
            controller.transform.position = hit.point;
            gravSpeed = Vector3.zero;
        }
        controller.Move(gravSpeed);
    }

    private Vector3 GetNextPosition()
    {
        if (Gravity != null)
        {
            gravSpeed.y += Gravity.GravityDirection.y * Gravity.GravityForce * Time.deltaTime;
            gravSpeed.x += Gravity.GravityDirection.x * Gravity.GravityForce * Time.deltaTime;
            gravSpeed.z += Gravity.GravityDirection.z * Gravity.GravityForce * Time.deltaTime;
        }
        return controller.transform.position + gravSpeed * Time.deltaTime;
    }
}
