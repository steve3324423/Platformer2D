using System.Collections;
using UnityEngine;

public class EnemyAttacked : Enemy
{
    [SerializeField] private Health _player;

    private float _timeForCoroutine = .5f;
    private WaitForSeconds _waitSeconds;
    private IEnumerator _coroutine;
    private float _damage = 10f;

    private void Awake()
    {
        _waitSeconds = new WaitForSeconds(_timeForCoroutine);
        _coroutine = ComparePlayerDistance();
    }

    private void Start()
    {
        StartCoroutine(_coroutine);
    }

    private void OnEnable()
    {
        _player.MurderPlayer += OnMurderPlayer;
    }

    private void OnDisable()
    {
        _player.MurderPlayer -= OnMurderPlayer;
    }

    private IEnumerator ComparePlayerDistance()
    {
        while (enabled)
        {
            Vector2 offsetPlayerPosition = _player.transform.position - transform.position;
            float minDistancePlayerForAttack = 1f;
            float minDistancePlayer = 3f;

            if (CheckDistancePlayer(offsetPlayerPosition, minDistancePlayerForAttack) == true)
                _player.TakedDamage(_damage);

            if (CheckDistancePlayer(offsetPlayerPosition, minDistancePlayer) == true)
            {
                Target = _player.transform.position;
                IsSeePlayer = true;
            }
            else
            {
                IsSeePlayer = false;
            }

            yield return _waitSeconds;
        }
    }

    private bool CheckDistancePlayer(Vector2 playerPosition, float minDistance)
    {
        return playerPosition.sqrMagnitude < minDistance * minDistance;
    }

    private void OnMurderPlayer()
    {
        StopCoroutine(_coroutine);
    }
}
