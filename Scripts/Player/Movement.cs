using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Movement : MonoBehaviour
{
    private InputReader _inputReader;
    private Health _health;
    private Rigidbody2D _rigidbody;
    private float _force = 10f;
    private float _speed = 5f;
    private bool _isJump = true;
    private bool _isDead;

    public event Action<float> Running;
    public event Action<bool> Jumped;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.MurderPlayer += OnMurderPlayer;
    }

    private void OnDisable()
    {
        _health.MurderPlayer -= OnMurderPlayer;
    }

    private void Update()
    {
        if(_isDead == false)
        {
            Moves();
            Jump();
        }
    }

    private void Moves()
    {
        Vector2 diretion = Vector2.right * _inputReader.Direction;
        transform.Translate(-diretion * _speed * Time.deltaTime);

        Running?.Invoke(diretion.x);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isJump)
        {
            _rigidbody.AddForce(transform.up * _force, ForceMode2D.Impulse);
            Jumped?.Invoke(_isJump);
            _isJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<TilemapCollider2D>(out TilemapCollider2D ground))
        {
            _isJump = true;
            Jumped?.Invoke(!_isJump);
        }
    }

    private void OnMurderPlayer()
    {
        _isDead = true;
    }
}
