using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : EnemyBase
{
    private void Awake() {
        SetHealth(50);
        SetMoveSpeed(3.5f);
    }
}
