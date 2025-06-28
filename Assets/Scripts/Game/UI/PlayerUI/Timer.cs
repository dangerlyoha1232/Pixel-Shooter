using TMPro;
using UnityEngine;

namespace Game.UI.PlayerUI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;

        private float _gameTimeSeconds;
        private int _gameTimeMinutes;
        
        private void Update()
        {
            _gameTimeSeconds += Time.deltaTime;

            if (_gameTimeSeconds > 59)
            {
                _gameTimeMinutes++;
                _gameTimeSeconds = 0;
            }
            
            _timerText.text = _gameTimeMinutes.ToString() + ":" + _gameTimeSeconds.ToString("F0");
        }
    }
}