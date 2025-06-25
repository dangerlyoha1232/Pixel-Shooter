using Game.PlayerScripts;   
using Game.Services;
using UnityEngine.UI;
using UnityEngine;

namespace Game.UI.PlayerUI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        
        private Player _player;
        private PlayerData _playerData;

        private void Start()
        {
            _player = ServiceLocator.Current.Get<Player>();
            _playerData = ServiceLocator.Current.Get<PlayerData>();
            
            _player.Health.OnDamage += ChangeHealth;
            
            _slider.maxValue = _playerData.PlayerStats.Health;
            _slider.value = _playerData.PlayerStats.Health;
        }

        private void OnDestroy()
        {
            _player.Health.OnDamage -= ChangeHealth;
        }
        
        private void ChangeHealth(float health)
        {
            _slider.value -= health;
        }
    }
}