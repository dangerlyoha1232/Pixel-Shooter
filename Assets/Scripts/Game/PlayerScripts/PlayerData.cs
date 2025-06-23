using System;
using UnityEngine;
using Game.Services;

namespace Game.PlayerScripts
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public class PlayerData : ScriptableObject, IService
    {
        [SerializeField] private PlayerStats _playerStats;
        
        public PlayerStats PlayerStats => _playerStats;
    }

    [Serializable]
    public struct PlayerStats
    {
        public float Speed;
        public float JumpForce;
        public float Health;
        public float Damage;
    }
}