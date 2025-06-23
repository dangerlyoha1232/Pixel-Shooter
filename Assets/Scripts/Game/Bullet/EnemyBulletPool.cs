using Game.Services;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Bullet
{
    public class EnemyBulletPool : IService
    {
        private ObjectPool<EnemyBullet> _pool;
        
        private Transform _bulletSpawnOrigin;
        private float _bulletDamage;
        private bool _isRightDirection;

        public EnemyBulletPool()
        {
            _pool = new ObjectPool<EnemyBullet>(OnCreate, OnGet, OnRelease, collectionCheck: true, defaultCapacity: 5, maxSize: 30);
        }

        private EnemyBullet OnCreate()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Enemy Bullet");
            var prefabGo = GameObject.Instantiate(prefab);
            var bullet = prefabGo.GetComponent<EnemyBullet>();
            return bullet;
        }

        private void OnGet(EnemyBullet bullet)
        {
            bullet.gameObject.SetActive(true);
            bullet.Init(_isRightDirection, _bulletSpawnOrigin, _bulletDamage);
        }

        private void OnRelease(EnemyBullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        public void GetBullet(bool isRightDirection, Transform spawnOrigin, float damage)
        {
            _isRightDirection = isRightDirection;
            _bulletSpawnOrigin = spawnOrigin;
            _bulletDamage = damage;
            _pool.Get();
        }

        public void ReleaseBullet(EnemyBullet bullet)
        {
            _pool.Release(bullet);
        }
    }
}