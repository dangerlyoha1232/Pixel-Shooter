using Game.Bullet;
using Game.Services;
using UnityEngine;

namespace Game.EnemyScripts
{
    public class RangedAlienEnemy : Enemy
    {
        private EnemyBulletPool _bulletPool;
        
        public override void Start()
        {
            base.Start();
            
            _bulletPool = ServiceLocator.Current.Get<EnemyBulletPool>();
        }

        public void DealDamage()
        {
            _bulletPool.GetBullet(_isFacingRight, transform, _data.Stats.Damage);
        }
    }
}