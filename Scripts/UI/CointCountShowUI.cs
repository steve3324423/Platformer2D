using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CointCountShowUI : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _wallet.ChangedCountCoin += OnChangedCountCoin;
    }

    private void OnDestroy()
    {
        _wallet.ChangedCountCoin -= OnChangedCountCoin;
    }

    private void OnChangedCountCoin(int countValue)
    {
        _text.text = countValue.ToString();
    }
}
