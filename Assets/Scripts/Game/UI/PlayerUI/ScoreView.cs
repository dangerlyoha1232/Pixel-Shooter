using TMPro;
using UnityEngine;
using Game.Services;

namespace Game.UI.PlayerUI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private GameScore _gameScore;
        
        private void Start()
        {
            _gameScore = ServiceLocator.Current.Get<GameScore>();

            _scoreText.text = "0";

            
        }
        
        private void Update()
        {
            _scoreText.text = $"Score: {_gameScore.Score} \nScore Multiplier: {_gameScore.ScoreMultiplier}";
        }
    }
}