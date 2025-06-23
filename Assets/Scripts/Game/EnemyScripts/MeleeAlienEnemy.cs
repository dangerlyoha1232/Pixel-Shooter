using UnityEngine;
using Game.PlayerScripts;

namespace Game.EnemyScripts
{
    public class MeleeAlienEnemy : Enemy
    {
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _attackPointRadius;


        public void DealDamage()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, _attackPointRadius, _playerMask);

            foreach (Collider2D collider in colliders)
            {
                collider.TryGetComponent<Player>(out Player player);

                if (player != null)
                {
                    player.TakeDamage(_data.Stats.Damage);
                }
            }
        }


    }
}