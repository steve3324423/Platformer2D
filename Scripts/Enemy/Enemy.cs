using UnityEngine;

[RequireComponent(typeof(EnemyAttacker))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _firstPosition;
    [SerializeField] private Transform _secondPosition;
    [SerializeField] private EnemyZone _enemyZone;

    private bool _isSeePlayer;
    private float _speed = 2f;

    public Vector3 Target { get; private set; }

    private void OnEnable()
    {
        _enemyZone.PlayerEnteredZone += OnPlayerEnteredZone;
        _enemyZone.PlayerLeftedZone += OnPlayerLeftedZone;
    }

    private void OnDisable()
    {
        _enemyZone.PlayerEnteredZone -= OnPlayerEnteredZone;
        _enemyZone.PlayerLeftedZone -= OnPlayerLeftedZone;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Patrol();
        transform.position = Vector2.MoveTowards(transform.position, Target, _speed * Time.deltaTime);
    }

    private void Patrol()
    {
        Vector2 offsetPositionOne = _firstPosition.position - transform.position;
        Vector2 offsetPositionTwo = _secondPosition.position - transform.position;
        float minDistance = .1f;

        if (_isSeePlayer == false && Target != _firstPosition.position && Target != _secondPosition.position)
            Target = _firstPosition.position;
        else if (IsDistanceForSetTarget(offsetPositionOne,offsetPositionTwo,minDistance) == true)
            Target = _secondPosition.position;
        else if (IsDistanceForSetTarget(offsetPositionTwo, offsetPositionOne, minDistance) == true)
            Target = _firstPosition.position;
    }

    private void OnPlayerEnteredZone(Transform player)
    {
        _isSeePlayer = true;
        Target = player.position;
    }

    private void OnPlayerLeftedZone()
    {
        _isSeePlayer = false;
    }

    private bool IsDistanceForSetTarget(Vector2 positionOne,Vector2 positionTwo,float minDistance)
    {
        return positionOne.sqrMagnitude < minDistance * minDistance && positionTwo.sqrMagnitude > minDistance * minDistance;
    }
}
