using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerTakesThings : MonoBehaviour
{
    private Health _health;
    private float _valueIncreaseHealth = 10f;
    private float _maxHealth = 100f;

    public event Action TakedCoin;
    public event Action TakedFirstAidKit;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<TakedThing>(out TakedThing thing))
        {
            if(CheckThings(thing) == true)
                thing.Destroy();
        }
    }

    private bool CheckThings(TakedThing thing)
    {
        if (thing is Coin)
        {
            TakedCoin?.Invoke();
            return true;
        }
        
        if (thing is FirstAidKit && _health.HealthValue < _maxHealth)
        {
            _health.TakeFirstAidKit(_valueIncreaseHealth);
            TakedFirstAidKit?.Invoke();
            return true;
        }

        return false;
    }
}
