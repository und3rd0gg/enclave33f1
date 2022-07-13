using System;
using UnityEngine;

public abstract class CharacterBarCharacteristic : MonoBehaviour
{
    public virtual event Action<int> OnAmountChangedEvent;
    public event Action OnAmountEndedEvent;
    
    public virtual int Amount
    {
        get => _amount;
        private set
        {
            _amount = Mathf.Clamp(value, _minAmount, _maxAmount);
            OnAmountChangedEvent?.Invoke(_amount);
            
            if (value <= _minAmount)
                OnAmountEndedEvent?.Invoke();
        }
    }
    
    public float NormalizedAmount => Mathf.Abs(_amount / _maxAmount);

    [SerializeField] private int _amount;

    private readonly int _minAmount = 0;
    private readonly int _maxAmount = 100;
}