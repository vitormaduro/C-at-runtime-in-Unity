using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    GameObject laserOrigin;
    GameObject bulletOrigin;

    GameObject target;
    LineRenderer lineRender;
    GameObject upperBody;

    int damage = 100;
    float timeBetweenShots = .8f;
    float totalTime = 0;

    List<GameObject> enemyList = new List<GameObject>();

    private void Awake() {
        laserOrigin = GameObject.Find("Laser Origin");
        bulletOrigin = GameObject.Find("Bullet Origin");
        lineRender = GetComponent<LineRenderer>();
        upperBody = GameObject.Find("Upper Body");
    }

    private void Update() {
        totalTime += Time.deltaTime;

        if (target != null) {
            Vector3[] positions = new Vector3[2];
            positions[0] = laserOrigin.transform.position;
            positions[1] = target.transform.position;
            lineRender.SetPositions(positions);

            upperBody.transform.LookAt(target.transform.position);

            if(totalTime >= timeBetweenShots) {
                Shoot();
            }
        } else if(enemyList.Count > 0) {
            enemyList.RemoveAll(item => item == null);
            target = enemyList[0];
        }
    }

    void Shoot() {
        totalTime = 0;
        target.GetComponent<EnemyBase>().DamageHealth(damage);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy") {
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            enemyList.Remove(other.gameObject);
            enemyList.RemoveAll(item => item == null);
            target = enemyList[0];
        }
    }
}
