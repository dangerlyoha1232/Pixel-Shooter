using Game.Services;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Bullet
{
    public class PlayerBulletPool : IService
    {
        private ObjectPool<PlayerBullet> _pool;

        private Transform _bulletSpawnOrigin;

        public PlayerBulletPool()
        {
            _pool = new ObjectPool<PlayerBullet>(OnCreateBullet, OnGetBullet, OnReleaseBullet, collectionCheck: true, defaultCapacity: 10, maxSize: 30);
        }

        private PlayerBullet OnCreateBullet()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Player Bullet");
            var prefabGo = GameObject.Instantiate(prefab);
            var bullet = prefabGo.GetComponent<PlayerBullet>();
            return bullet;
        }

        private void OnGetBullet(PlayerBullet playerBullet)
        {
            playerBullet.gameObject.SetActive(true);
            playerBullet.Init(_bulletSpawnOrigin);
        }

        private void OnReleaseBullet(PlayerBullet playerBullet)
        {
            playerBullet.gameObject.SetActive(false);
        }

        public void GetBullet(Transform bulletSpawnOrigin)
        {
            _bulletSpawnOrigin = bulletSpawnOrigin;
            _pool.Get();
        }

        public void ReleaseBullet(PlayerBullet playerBullet)
        {
            _pool.Release(playerBullet);
        }
    }
}