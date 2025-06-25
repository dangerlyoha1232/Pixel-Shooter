using System;
using UnityEngine;
using Game.Services;

namespace Game
{
    public class GameScore : MonoBehaviour, IService
    {
        [SerializeField] private GameData _gameData;
        
        public float ScoreMultiplier {get; private set;}
        public float Score { get; private set; }
        
        private float _scoreMultiplierPerMinute;
        private float _multiplierStepTime;

        public void Init()
        {
            EventBus.OnEnemyDie += UpdateScore;

            ScoreMultiplier = _gameData.ScoreMultiplier;
            _scoreMultiplierPerMinute = _gameData.ScoreMultiplierPerMinute;
            _multiplierStepTime = _gameData.MultiplierStepTime;
            Score = 0;
        }

        private void OnDestroy()
        {
            EventBus.OnEnemyDie -= UpdateScore;
        }

        private void Update()
        {
            _multiplierStepTime -= Time.deltaTime;

            if (_multiplierStepTime <= 0)
            {
                ScoreMultiplier += _scoreMultiplierPerMinute;
                _multiplierStepTime = _gameData.MultiplierStepTime;
            }
        }
        
        private void UpdateScore(float score)
        {
            if (score < 0)
                throw new ArgumentOutOfRangeException(nameof(score), "Score cannot be negative.");

            Score += score * ScoreMultiplier;
        }
    }
}