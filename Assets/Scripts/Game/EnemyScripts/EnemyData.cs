using System;
using UnityEngine;

namespace Game.EnemyScripts
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private EnemyStats _stats;
        
        public EnemyStats Stats => _stats;
    }

    [Serializable]
    public struct EnemyStats
    {
        public float Health;
        public float Damage;
        public float Speed;
        public float ViewRange;
        public float AttackRange;
    }
}