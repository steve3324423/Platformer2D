using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
public class AnimatorHandler : MonoBehaviour
{
    private const string S_JumpAnimationName = "jump_hero";
    private const string S_IdleAnimationName = "idle_hero";
    private const string S_RunAnimationName = "run_hero";

    private Movement _player;
    private Animator _animator;
    private Health _health;
    private bool _isJump;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Movement>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.MurderPlayer += OnMurderPlayer;
        _player.Jumped += OnJumped;
        _player.Running += OnRunning;
    }

    private void OnDestroy()
    {
        _health.MurderPlayer -= OnMurderPlayer;
        _player.Jumped -= OnJumped;
        _player.Running -= OnRunning;
    }

    private void OnRunning(float valueXPosition)
    {
        if (valueXPosition != 0 && _isJump == false)
            _animator.Play(S_RunAnimationName);
        else if (_isJump == false)
            _animator.Play(S_IdleAnimationName);
    }

    private void OnMurderPlayer()
    {
        _animator.enabled = false;
    }

    private void OnJumped(bool isJump)
    {
        _animator.Play(S_JumpAnimationName);
        _isJump = isJump;
    }
}