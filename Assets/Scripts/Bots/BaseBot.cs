using Assets.Scripts.Projectiles;
using Assets.Scripts.TriggerType;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class BaseBot : MonoBehaviour, ITriggerOnDie
{
    public Transform directionReference;
    public float Speed = 12.0f;
    public Gun Weapon;
    public float AttackRange = 10;
    public float AttacksPerSecond = 1;
    public GameObject Target;
    public Gravity Gravity = null;
    public bool EnableGravity = true;

    public BotRoute Route;
    public float KeepDistance = 5;
    private float lastAttackTime = 0;
    private NavMeshAgent agent;
    private bool canSeeTarget = false;

    private Vector3 lastPosition;
    private int stuckCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (Gravity == null)
        {
            Gravity = FindObjectOfType<Gravity>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetCanSeeTarget();
        if (Vector3.Distance(Target.transform.position, transform.position) <= AttackRange && canSeeTarget)
        {
            // Turn To Face Target
            var direction = Target.transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);

            if (Time.time - lastAttackTime > 1 / AttacksPerSecond)
            {
                Weapon.TriggerAction();
                lastAttackTime = Time.time;
            }
        }

        Move();
    }

    private void Move()
    {
        if (Target != null)
        {
            var targetPosition = transform.position;
            if (Vector3.Distance(Target.transform.position, transform.position) <= AttackRange)
            {
                if ((Vector3.Distance(Target.transform.position, transform.position) > KeepDistance) || !canSeeTarget)
                {
                    targetPosition = Target.transform.position;
                }
            }
            else if(Route.Route.Count > 0)
            {
                var nextPoint = Route.GetNextPoint();
                if (Vector3.Distance(nextPoint.Position, transform.position) <= 1 || IsStuck())
                {
                    nextPoint.VisitPoint();
                    nextPoint = Route.GetNextPoint();
                }

                targetPosition = nextPoint.Position;
            }
            agent.SetDestination(targetPosition);
        }
    }

    private bool IsStuck()
    {
        if (Vector3.Distance(Target.transform.position, transform.position) <= AttackRange && canSeeTarget)
        {
            return false;
        }
        if( Vector3.Distance(transform.position, lastPosition) < 0.01f)
        {
            stuckCount++;
        }
        else
        {
            stuckCount = 0;
        }
        lastPosition = transform.position;
        if(stuckCount > 2000)
        {
            stuckCount = 0;
            return true;
        }
        return false;
    }

    private void SetCanSeeTarget()
    {
        RaycastHit hit;
        var ray = new Ray(directionReference.position, Target.transform.position - directionReference.position);
        var distance = Vector3.Distance(directionReference.position, Target.transform.position);
        Debug.DrawRay(directionReference.position, Target.transform.position - directionReference.position, Color.green);
        if(Physics.Raycast(ray, out hit, distance))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Walls"))
            {
                canSeeTarget = false;
            }
            else
            {
                canSeeTarget = true;
            }
        }
        else
        {
            canSeeTarget = true;
        }
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
}
