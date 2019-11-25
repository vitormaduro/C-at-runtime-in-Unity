using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent navAgent;
    GameObject player;
    GameObject statue;
    //Vector3 statueDestination;

    bool isPlayerInRange = false;
    bool isStatueInRange = false;

    private void Start() {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        statue = GameObject.FindGameObjectWithTag("Statue");
        //statueDestination = GameObject.Find("Destination " + Random.Range(1, 5)).transform.position;
        navAgent.speed = GetComponent<EnemyBase>().GetMoveSpeed();
    }

    private void Update() {
        if (isPlayerInRange && !isStatueInRange)
            navAgent.destination = player.transform.position;
        else
            navAgent.destination = statue.transform.position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
            isPlayerInRange = true;
        else if (other.gameObject.tag == "Statue")
            isStatueInRange = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
            isPlayerInRange = false;
        else if (other.gameObject.tag == "Statue")
            isStatueInRange = true;
    }
}
