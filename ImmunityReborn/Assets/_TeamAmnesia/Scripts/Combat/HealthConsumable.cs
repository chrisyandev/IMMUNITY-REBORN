using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : MonoBehaviour
{
    [field: SerializeField]
    public Health TargetHealth { get; private set; }

    [field: SerializeField]
    public int HealAmount { get; private set; } = 20;

    [field: SerializeField]
    public int MaxItemCount { get; private set; } = 5;

    [field: SerializeField]
    public int CurrentItemCount { get; private set; }

    [field: SerializeField]
    public ParticleSystem UseVFX { get; private set; }

    private void Start()
    {
        CurrentItemCount = MaxItemCount;
    }

    public void Use()
    {
        if (CurrentItemCount > 0)
        {
            TargetHealth.Heal(HealAmount);
            UseVFX.Play();
            CurrentItemCount--;
        }
    }

    public void AddItemCount(int count)
    {
        CurrentItemCount = CurrentItemCount + count > MaxItemCount ? MaxItemCount : CurrentItemCount + count;
    }
}
