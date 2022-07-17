using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Bar : MonoBehaviour
{
    private Image _filler;
    
    [SerializeField] private CharacterCharacteristic _targetValue;

    private void Awake()
    {
        _filler = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _targetValue.AmountChangedEvent += OnAmountChangedEvent;
    }

    private void OnDisable()
    {
        _targetValue.AmountChangedEvent -= OnAmountChangedEvent;
    }

    private void OnAmountChangedEvent(int currentAmount)
    {
        _filler.fillAmount = _targetValue.NormalizedAmount;
    }
}
