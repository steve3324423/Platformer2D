using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;

    private PlayerTakesHandler _playerTakesHandler;

    public float HealthValue { get; private set; } = 100f;

    public event Action MurderPlayer;
    public event Action TakeDamage;

    private void Awake()
    {
        _playerTakesHandler = GetComponent<PlayerTakesHandler>();
    }

    private void OnEnable()
    {
        _playerTakesHandler.TakedFirstAidKit += OnTakedFirstAidKit;

        foreach (Enemy enemy in _enemies)
            enemy.AttackedPlayer += OnAttackedPlayer;
    }

    private void OnDisable()
    {
        _playerTakesHandler.TakedFirstAidKit -= OnTakedFirstAidKit;

        foreach (Enemy enemy in _enemies)
            enemy.AttackedPlayer -= OnAttackedPlayer;
    }

    private void OnAttackedPlayer(float damage)
    {
        HealthValue -= damage;
        TakeDamage?.Invoke();

        if (HealthValue <= 0)
        {
            MurderPlayer?.Invoke();
        }
    }

    private void OnTakedFirstAidKit(float valueIncrease)
    {
        HealthValue += valueIncrease;
    }
}
