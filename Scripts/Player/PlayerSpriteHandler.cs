using UnityEngine;

[RequireComponent(typeof(PlayerTakesHandler))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Health))]
public class PlayerSpriteHandler : MonoBehaviour
{
    [SerializeField] private Sprite _spriteDead;

    private PlayerTakesHandler _takeHandler;
    private SpriteRenderer _spriteRenderer;
    private Health _health;
    private float _timeForInvoke = .5f;

    private void Awake()
    { 
        _takeHandler = GetComponent<PlayerTakesHandler>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _takeHandler.TakedFirstAidKit += OnTakedFirstAidKit;
        _health.MurderPlayer += OnMurderPlayer;
        _health.TakeDamage += OnTakeDamage;
    }

    private void OnDisable()
    {
        _takeHandler.TakedFirstAidKit -= OnTakedFirstAidKit;
        _health.MurderPlayer += OnMurderPlayer;
        _health.TakeDamage -= OnTakeDamage;
    }

    private void OnTakeDamage()
    {
        ChangeColor(Color.red);
    }

    private void OnTakedFirstAidKit(float value)
    {
        ChangeColor(Color.green);
    }

    private void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
        Invoke(nameof(SetColorToDefault), _timeForInvoke);
    }

    private void OnMurderPlayer()
    {
        _spriteRenderer.sprite = _spriteDead;
    }

    private void SetColorToDefault()
    {
        _spriteRenderer.color = Color.white;
    }
}
