using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshPosition : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject destinationObject1;
    public GameObject destinationObject2;

    public Vector3 destination1;
    public Vector3 destination2;
    private bool isTravelling;

    private Vector3 targetPosition;

    private float _waitTime;
    private float _waitTimer;
    public float wanderDistance;
    // Start is called before the first frame update
    void Start()
    {
        _waitTime = 5f;
        destination1 = destinationObject1.transform.position;
        destination2 = destinationObject2.transform.position;
        isTravelling = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (agent.enabled == false)
            return;

        if(Vector3.Distance(destination1, transform.position) < 5)
        {
            if(!isTravelling)
            {
                agent.SetDestination(destination2);
                agent.speed = 1f;
                isTravelling = true;
            } else
            {
                isTravelling = false;
            }
            
            
        } else if(Vector3.Distance(destination2, transform.position) < 5) 
        {
            if (!isTravelling)
            {
                agent.SetDestination(destination1);
                agent.speed = 1f;
                isTravelling = true;
            }
            else
            {
                isTravelling = false;
            }
        }

        //if (_waitTimer < _waitTime)
        //{
        //    _waitTimer += Time.deltaTime;
        //}
        //else
        //{

        //    Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderDistance;
            

        //    randomDirection += transform.position;

        //    NavMeshHit navHit;

        //    NavMesh.SamplePosition(randomDirection, out navHit, wanderDistance, NavMesh.AllAreas);

        //    targetPosition = navHit.position;

        //    agent.speed = 1.0f;

        //    agent.SetDestination(targetPosition);

        //    _waitTimer = 0f;
        //}
    }
}
