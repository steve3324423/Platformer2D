using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _positions;
    [SerializeField] private Health _player;

    private Vector3 _firstPosition;
    private Vector3 _secondPosition;
    private Vector3 _target;
    private WaitForSeconds _waitSeconds;
    private float _timeForCoroutine = .5f;
    private bool _isSeePlayer;
    private float _damage = 10f;
    private float _speed = 2f;

    public event Action<float> AttackedPlayer;

    private void Awake()
    {
        _waitSeconds = new WaitForSeconds(_timeForCoroutine);
        _firstPosition  = _positions[0].position;
        _secondPosition = _positions[1].position;
    }

    private void OnEnable()
    {
        _player.MurderPlayer += OnMurderPlayer;
    }

    private void OnDisable()
    {
        _player.MurderPlayer -= OnMurderPlayer;
    }

    private void Start()
    {
        StartCoroutine(ComparePlayerDistance());
    }

    private void Update()
    {
        Moves();
    }

    private void Moves()
    {
        SetTarget();
        transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    private IEnumerator ComparePlayerDistance()
    {
        while(enabled)
        {
            Vector2 offsetPlayerPosition = _player.transform.position - transform.position;
            float minDistancePlayerForAttack = 1f;
            float minDistancePlayer = 3f;

            if (offsetPlayerPosition.sqrMagnitude < minDistancePlayerForAttack * minDistancePlayerForAttack)
                AttackedPlayer?.Invoke(_damage);

            if (offsetPlayerPosition.sqrMagnitude < minDistancePlayer * minDistancePlayer)
            {
                _target = _player.transform.position;
                _isSeePlayer = true;
            }
            else
            {
                _isSeePlayer = false;
            }

            yield return _waitSeconds;
        }
    }

    private void SetTarget()
    {
        Vector2 offsetPositionOne = _firstPosition - transform.position;
        Vector2 offsetPositionTwo = _secondPosition - transform.position;
        float minDistance = .1f;

        if (_isSeePlayer == false && _target != _firstPosition && _target != _secondPosition)
            _target = _firstPosition;
        else if (offsetPositionOne.sqrMagnitude < minDistance * minDistance && offsetPositionTwo.sqrMagnitude > minDistance * minDistance)
            _target = _secondPosition;
        else if (offsetPositionTwo.sqrMagnitude < minDistance * minDistance && offsetPositionOne.sqrMagnitude > minDistance * minDistance)
            _target = _firstPosition;
    }

    private void OnMurderPlayer()
    {
        StopCoroutine(ComparePlayerDistance());
    }
}
