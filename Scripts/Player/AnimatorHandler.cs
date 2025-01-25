using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]
public class AnimatorHandler : MonoBehaviour
{
    private const string JumpAnimationName = "jump_hero";
    private const string IdleAnimationName = "idle_hero";
    private const string RunAnimationName = "run_hero";

    private Mover _player;
    private Animator _animator;
    private Health _health;
    private bool _isJump;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Mover>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.RunOutValue += OnRunOutValue;
        _player.Jumped += OnJumped;
        _player.Running += OnRunning;
    }

    private void OnDestroy()
    {
        _health.RunOutValue -= OnRunOutValue;
        _player.Jumped -= OnJumped;
        _player.Running -= OnRunning;
    }

    private void OnRunning(float valueXPosition)
    {
        if (valueXPosition != 0 && _isJump == false)
            Animator.StringToHash(RunAnimationName);
        else if (_isJump == false)
            Animator.StringToHash(IdleAnimationName);
    }

    private void OnRunOutValue()
    {
        _animator.enabled = false;
    }

    private void OnJumped(bool isJump)
    {
        Animator.StringToHash(JumpAnimationName);
        _isJump = isJump;
    }
}
