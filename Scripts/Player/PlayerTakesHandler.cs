using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerTakesHandler : MonoBehaviour
{
    private Health _health;
    private float _valueIncreaseHealth = 10f;
    private float _maxHealth = 100f;

    public event Action TakedCoin;
    public event Action<float> TakedFirstAidKit;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Coin>(out Coin coin))
        {
            TakedCoin?.Invoke();
            coin.Destroy();
        }

        if (other.TryGetComponent<FirstAidKit>(out FirstAidKit firstAidKit) && _health.HealthValue < _maxHealth)
        {
            TakedFirstAidKit?.Invoke(_valueIncreaseHealth);
            firstAidKit.Destroy();
        }
    }
}
