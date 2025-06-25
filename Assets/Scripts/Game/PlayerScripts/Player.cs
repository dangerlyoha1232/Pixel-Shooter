using Game.Services;
using UnityEngine;

namespace Game.PlayerScripts
{
    public class Player : MonoBehaviour, IDamageable, IService
    {
        public Health Health {get; private set;}
        
        private PlayerAnimationsHandler _animationsHandler;
        private PlayerData _data;

        public void Init()
        {
            _animationsHandler = ServiceLocator.Current.Get<PlayerAnimationsHandler>();
            _data = ServiceLocator.Current.Get<PlayerData>();
            
            Health = new Health(_data.PlayerStats.Health);
        }
        
        public void TakeDamage(float damage)
        {
            Health.TakeDamage(damage);
            _animationsHandler.HurtAnimation();
            Debug.Log("Player taking damage " + damage);
        }
    }
}