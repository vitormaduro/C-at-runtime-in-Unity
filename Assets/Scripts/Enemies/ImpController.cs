using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpController : EnemyBase
{
    private void Awake() {
        SetHealth(15);
        SetMoveSpeed(5);
    }
}
