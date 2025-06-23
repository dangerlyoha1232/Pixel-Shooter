using Game.Bullet;
using UnityEngine;
using Game.Services;

namespace Game.PlayerScripts
{
    public class PlayerShoot : MonoBehaviour
    {
        private InputHandler _inputHandler;
        private PlayerAnimationsHandler _animationsHandler;
        private PlayerBulletPool _playerBulletPool;

        private void Start()
        {
            _inputHandler = ServiceLocator.Current.Get<InputHandler>();
            _animationsHandler = ServiceLocator.Current.Get<PlayerAnimationsHandler>();
            _playerBulletPool = ServiceLocator.Current.Get<PlayerBulletPool>();
        }

        private void Update()
        {
            if (_inputHandler.LeftMouseInput())
            {
                ShootAnimation();
            }
        }

        private void ShootAnimation()
        {
            _animationsHandler.ShootAnimation();
        }

        public void InitializeBullet()
        {
            _playerBulletPool.GetBullet(transform);
        }
    }
}