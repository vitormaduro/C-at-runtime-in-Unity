using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    int unit = 0;
    int tens = 0;
    float delta = 0;

    public int ammoCounter;
    public float timePerShot = 0.5f;
    public GameObject gunBarrel;
    public GameObject bullet;

    Vector3 barrelPos;

    private void Start() {
        bullet = GameObject.Find("Main Bullet");
        enabled = false;
    }

    void Update()
    {
        barrelPos = gunBarrel.transform.position;
        delta += Time.deltaTime;
        if (Input.GetMouseButtonDown(0)) {
            Fire();
        }
    }

    public void Fire() {
        if(delta >= timePerShot) {
            delta = 0;
            GameObject projectile = Instantiate(bullet, barrelPos, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = gunBarrel.transform.TransformDirection(Vector3.forward * 200f);
            projectile.GetComponent<BulletController>().Activate();
        }
    }
}
