using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HealthValue { get; private set; } = 100f;

    public event Action MurderPlayer;
    public event Action TakeDamage;

    public void TakedDamage(float damage)
    {
        HealthValue -= damage;
        TakeDamage?.Invoke();

        if (HealthValue <= 0)
        {
            MurderPlayer?.Invoke();
        }
    }

    public void TakeFirstAidKit(float valueIncrease)
    {
        HealthValue += valueIncrease;
    }
}
