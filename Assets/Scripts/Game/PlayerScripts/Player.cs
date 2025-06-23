using Game.Services;
using UnityEngine;

namespace Game.PlayerScripts
{
    public class Player : MonoBehaviour, IDamageable
    {
        private PlayerAnimationsHandler _animationsHandler;

        private void Start()
        {
            _animationsHandler = ServiceLocator.Current.Get<PlayerAnimationsHandler>();
        }
        
        public void TakeDamage(float damage)
        {
            _animationsHandler.HurtAnimation();
            Debug.Log("Player taking damage " + damage);
        }
    }
}