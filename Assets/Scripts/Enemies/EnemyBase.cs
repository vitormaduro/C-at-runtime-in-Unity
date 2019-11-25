using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBase : MonoBehaviour
{
    private int health = 100;
    private int energy;
    private float moveSpeed;
    private Color color;

    public int GetHealth() { return health; }

    public int GetEnergy() { return energy; }

    public float GetMoveSpeed() { return moveSpeed; }

    public Color GetColor() { return GetComponent<Renderer>().material.color; }

    public void SetHealth(int newHealth) {
        health = newHealth;
    }

    public void DamageHealth(int damage) {
        health -= damage;
    }

    public void SetEnergy(int newEnergy) {
        energy = newEnergy;
    }

    public void SetMoveSpeed(float newSpeed) {
        moveSpeed = newSpeed;
    }

    public void SetColor(float r, float g, float b) {
        color.r = r;
        color.g = g;
        color.b = b;
        GetComponent<Renderer>().material.color = color;
    }

    public void SetColor(Color color) {
        GetComponent<Renderer>().material.color = color;
    }

    private void Update() {
        if (health <= 0) {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().totalEnemies--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Bullet") {
            health -= collision.gameObject.GetComponent<BulletController>().damage;
            Destroy(collision.gameObject);
        }
    }
}
