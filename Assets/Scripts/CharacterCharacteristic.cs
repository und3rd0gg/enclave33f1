using System;
using UnityEngine;

public class CharacterCharacteristic : MonoBehaviour
{
    public event Action<int> AmountChangedEvent;
    public event Action AmountEndedEvent;
    
    public int Amount
    {
        get => _amount;
        protected set
        {
            _amount = Mathf.Clamp(value, _minAmount, _maxAmount);
            AmountChangedEvent?.Invoke(_amount);
            
            if (value <= _minAmount)
                AmountEndedEvent?.Invoke();
        }
    }
    
    public float NormalizedAmount => Mathf.Abs((float)_amount / _maxAmount);

    [SerializeField] protected int _amount;
    [Header("Basic Values")]
    [SerializeField] protected int _minAmount = 0;
    [SerializeField] protected int _maxAmount = 100;

    protected void OnEnable()
    {
        AmountChangedEvent?.Invoke(_amount);
    }
}