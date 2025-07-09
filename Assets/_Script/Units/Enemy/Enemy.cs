using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum TargetType
    {
        Player,
        Waypoint
    }

    enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    [Header("References")]
    [SerializeField] GameObject[] waypoints;
    [SerializeField] TargetType target;
    [SerializeField] EnemyState state;
    [SerializeField] Transform head;

    [Header("Properties")]
    [SerializeField] float rayDistance = 10f;
    [SerializeField] LayerMask layerMask;

    Transform playerPosition;
    Transform targetPosition;
    NavMeshAgent m_agent;
    Animator m_animator;
    Ray ray;
    float m_distance;
    int currentWaypointIndex = 0;
    float radius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        playerPosition = Player.Instance.transform;

        m_agent.speed += GameManager.Instance.level;
    }

    // Update is called once per frame
    void Update()
    {
        Patroling();
    }

    void Patroling()
    {

        targetPosition = waypoints[currentWaypointIndex].transform;

        // Chase when player in radius range
        if (Vector3.Distance(transform.position, playerPosition.position) < radius)
        {
            Chase();
        }
        else
        {
            m_agent.speed = 3.5f;

            m_animator.SetBool("isWalking", true);
            m_animator.SetBool("isIdle", false);
            m_animator.SetBool("isRunning", false);

            if (Vector3.Distance(transform.position, targetPosition.position) < 2f)
            {

                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0; // Reset to the first waypoint
                } else
                {
                    currentWaypointIndex++;
                }
                    
            }
        }

        m_agent.destination = targetPosition.position;
        
       
    }

    void Attack()
    {
        GameManager.Instance.SetGameState("GameOver");
    }

    void Chase()
    {
        targetPosition = playerPosition;

        transform.LookAt(targetPosition);

        m_agent.speed = GameManager.Instance.level + 5f; // Increase speed when chasing

        m_animator.SetBool("isWalking", false);
        m_animator.SetBool("isIdle", false);
        m_animator.SetBool("isRunning", true);

        ray = new Ray(head.position, head.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance))
        {
            if (hitInfo.collider.tag == "Player")
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
                if (Vector3.Distance(transform.position, playerPosition.position) < 2f)
                {
                    
                    Attack();
                }
            }
            else
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
            }

        }
    }
}
