using System;
using UnityEngine;

public class Health : IObservableCharacterCharacteristic
{
    public event Action<int> OnHealthChangedEvent;
    public event Action OnDeathEvent;
    
    public int Amount
    {
        get => _amount;
        private set
        {
            _amount = Mathf.Clamp(value, _minHealth, _maxHealth);
            OnHealthChangedEvent?.Invoke(_amount);
            
            if (value <= _minHealth)
                OnDeathEvent?.Invoke();
        }
    }
    
    public float NormalizedAmount => Mathf.Abs(_amount / _maxHealth);

    [SerializeField] private int _amount;

    private readonly int _minHealth = 0;
    private readonly int _maxHealth = 100;
}
