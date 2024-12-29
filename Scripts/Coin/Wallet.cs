using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private PlayerTakesHandler _playerTakeCoin;

    private int _count = 0;

    public event Action<int> ChangedCountCoin;

    private void OnEnable()
    {
        _playerTakeCoin.TakedCoin += OnTakedCoin;
    }

    private void OnDestroy()
    {
        _playerTakeCoin.TakedCoin -= OnTakedCoin;
    }

    private void OnTakedCoin()
    {
        _count++;
        ChangedCountCoin?.Invoke(_count);
    }
}
