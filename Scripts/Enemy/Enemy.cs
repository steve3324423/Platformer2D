using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _firstPosition;
    [SerializeField] private Transform _secondPosition;

    protected Vector3 Target;
    protected bool IsSeePlayer;
    private float _speed = 2f;

    private void Update()
    {
        Moves();
    }

    private void Moves()
    {
        SetTarget();
        transform.position = Vector2.MoveTowards(transform.position, Target, _speed * Time.deltaTime);
    }

    private void SetTarget()
    {
        Vector2 offsetPositionOne = _firstPosition.position - transform.position;
        Vector2 offsetPositionTwo = _secondPosition.position - transform.position;
        float minDistance = .1f;

        if (IsSeePlayer == false && Target != _firstPosition.position && Target != _secondPosition.position)
            Target = _firstPosition.position;
        else if (CheckDistanceForTarget(offsetPositionOne,offsetPositionTwo,minDistance) == true)
            Target = _secondPosition.position;
        else if (CheckDistanceForTarget(offsetPositionTwo, offsetPositionOne, minDistance) == true)
            Target = _firstPosition.position;
    }

    private bool CheckDistanceForTarget(Vector2 positionOne,Vector2 positionTwo,float minDistance)
    {
        return positionOne.sqrMagnitude < minDistance * minDistance && positionTwo.sqrMagnitude > minDistance * minDistance;
    }
}
