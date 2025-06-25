using TMPro;
using UnityEngine;

namespace Game.UI.PlayerUI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;

        private float _gameTime;
        
        private void Update()
        {
            _gameTime += Time.deltaTime;
            
            _timerText.text = _gameTime.ToString("F1");
        }
    }
}