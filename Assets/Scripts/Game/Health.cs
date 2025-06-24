using System;
using UnityEngine;

namespace Game
{
    public class Health
    {
        private float _value;
        
        public event Action OnDeath;
        public event Action<float> OnDamage;
        
        public Health(float health)
        {
            _value = health;
        }

        public void TakeDamage(float damage)
        {
            float actualDamage = Mathf.Min(damage, _value);
            
            _value -= actualDamage;
            OnDamage?.Invoke(actualDamage);
            
            if(_value == 0)
                OnDeath?.Invoke();
        }
    }
}