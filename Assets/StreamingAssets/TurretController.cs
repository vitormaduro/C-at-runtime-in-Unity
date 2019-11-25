using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    public GameObject gunBarrel;
    public GameObject bullet;

    Vector3 barrelPos;

    void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gunBarrel = GameObject.Find("Bullet Spawn Point");
        bullet = Resources.Load("Prefabs/bullet") as GameObject;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            barrelPos = gunBarrel.transform.position;
            Fire();
        }
    }

    public void Fire() {
        GameObject projectile = Instantiate(bullet, barrelPos, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = gunBarrel.transform.TransformDirection(Vector3.forward * 200f);
    }
}
