using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Mover : MonoBehaviour
{
    private InputReader _inputReader;
    private Health _health;
    private Rigidbody2D _rigidbody;
    private float _force = 10f;
    private float _speed = 5f;
    private bool _isJump = true;

    public event Action<float> Running;
    public event Action<bool> Jumped;

    public Vector2 Direction { get;private set; }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _inputReader.TouchedKeyJump += OnTouchedKeyJump;
        _health.RunOutValue += OnRunOutValue;
    }

    private void OnDisable()
    {
        _inputReader.TouchedKeyJump -= OnTouchedKeyJump;
        _health.RunOutValue -= OnRunOutValue;
    }

    private void Update()
    {
        if(enabled == true)
            Move();
    }

    private void Move()
    {
        Direction = transform.right * -_inputReader.Direction;
        transform.Translate(-Direction * _speed * Time.deltaTime);

        Running?.Invoke(Direction.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out TilemapCollider2D ground))
        {
            _isJump = true;
            Jumped?.Invoke(!_isJump);
        }
    }

    private void OnTouchedKeyJump()
    {
        if (_isJump && enabled == true)
        {
            _rigidbody.AddForce(transform.up * _force, ForceMode2D.Impulse);
            Jumped?.Invoke(_isJump);
            _isJump = false;
        }
    }

    private void OnRunOutValue()
    {
        enabled = false;
    }
}
