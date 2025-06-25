using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
    public class GameData : ScriptableObject
    {
        [SerializeField] private float _scoreMultiplier = 1;
        [SerializeField] private float _scoreMultiplierPerMinute = 0.1f;
        [SerializeField] private float _multiplierStepTime = 60;
        
        public float ScoreMultiplier => _scoreMultiplier;
        public float ScoreMultiplierPerMinute => _scoreMultiplierPerMinute;
        public float MultiplierStepTime => _multiplierStepTime;
    }
}