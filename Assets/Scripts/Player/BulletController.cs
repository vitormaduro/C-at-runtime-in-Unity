using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage = 20;

    readonly float maxLifeTime = 1f;

    public void Activate() {
        Destroy(gameObject, maxLifeTime);
    }
    
    
    /* Essa função muda o valor da variável de dano da arma.
     * <Parâmetros>: int newDamage - um número inteiro que será o novo dano
     * <Retorno>: nada (void)*/
    public void SetDamage(int newDamage) {
        damage = newDamage;
    }
}
