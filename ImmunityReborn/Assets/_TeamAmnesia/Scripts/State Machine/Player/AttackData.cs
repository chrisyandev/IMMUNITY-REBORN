using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackData
{
    [field: SerializeField]
    public string AnimationName { get; private set; }

    [field: SerializeField]
    public float TransitionDuration { get; private set; }

    [field: SerializeField]
    public int Damage { get; private set; }

    [field: SerializeField]
    public float Knockback { get; private set; }
}
