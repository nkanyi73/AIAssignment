using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshPosition : MonoBehaviour
{
    public NavMeshAgent agent;

    public Animator npcAnimator;

    private Vector3 targetPosition;

    private float _waitTime;
    private float _waitTimer;
    public float wanderDistance;
    // Start is called before the first frame update
    void Start()
    {
        _waitTime = 5f;
        npcAnimator.Play("Thriller Part 3");

    }

    // Update is called once per frame
    void Update()
    {
        //if (agent.speed > 1)
        //{
        //    walkinginganimation
        //    npcAnimator.Play("Walking");
        //}
        //else
        //{
        //    idleanimation
        //    npcAnimator.Play("Thriller Part 3");

        //}

        if (agent.enabled == false)
            return;

        if (_waitTimer < _waitTime)
        {
            _waitTimer += Time.deltaTime;
        }
        else
        {
            //Vector3 currentPosition = transform.position;
            //float randomZ = UnityEngine.Random.Range(-1f, 1f);

            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderDistance;
            //Vector3 randomDirection = new Vector3(currentPosition.x, currentPosition.y, randomZ) * wanderDistance;

            randomDirection += transform.position;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randomDirection, out navHit, wanderDistance, NavMesh.AllAreas);

            targetPosition = navHit.position;

            agent.speed = 1.0f;

            agent.SetDestination(targetPosition);

            _waitTimer = 0f;
        }
    }
}
