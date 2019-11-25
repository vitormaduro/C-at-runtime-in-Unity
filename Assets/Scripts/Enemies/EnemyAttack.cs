using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    GameObject player;
    GameObject statue;
    GameObject gameController;

    bool isPlayerInRange = false;
    bool isStatueInRange = false;
    int damage = 10;
    float timeBetweenAttacks = 2f;
    float distanceToAttack = 2f;
    float totalTime = 0f;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        statue = GameObject.FindGameObjectWithTag("Statue");
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    private void Update() {
        totalTime += Time.deltaTime;

        if(isPlayerInRange) {
            RaycastHit ray;
            Physics.Linecast(transform.position, player.transform.position, out ray);

            if(ray.distance <= distanceToAttack && totalTime >= timeBetweenAttacks) {
                Attack("Player");
            }
        }
        else if(isStatueInRange) {
            RaycastHit ray;
            Physics.Linecast(transform.position, statue.transform.position, out ray);

            if (ray.distance <= distanceToAttack && totalTime >= timeBetweenAttacks) {
                Attack("Statue");
            }
        }
    }

    void Attack(string target) {
        totalTime = 0;

        switch(target) {
            case "Player":
                gameController.GetComponent<GameController>().DamagePlayerHealth(damage);
                break;

            case "Statue":
                gameController.GetComponent<GameController>().DamageStatueHealth(damage);
                break;
        }
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
