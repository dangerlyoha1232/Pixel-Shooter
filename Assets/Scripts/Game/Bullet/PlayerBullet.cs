using Game.PlayerScripts;
using Game.Services;
using UnityEngine;

namespace Game.Bullet
{
    public class PlayerBullet : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private LayerMask _environment;
        
        private PlayerMovement _playerMovement;
        private PlayerData _playerData;
        private PlayerBulletPool _playerBulletPool;
        
        private Animator _animator;
        
        private bool _isRightDirection;
        private bool _isReleased;

        public void Init(Transform spawnOrigin)
        {
            _playerMovement = ServiceLocator.Current.Get<PlayerMovement>();
            _playerData = ServiceLocator.Current.Get<PlayerData>();
            _playerBulletPool = ServiceLocator.Current.Get<PlayerBulletPool>();
            
            _animator = GetComponent<Animator>();
            
            _isReleased = false;
            transform.position = spawnOrigin.position;
            _isRightDirection = _playerMovement.IsFacingRight;
        }

        private void Update()
        {
            Translate();

            if (IsTouchingEnvironment())
            {
                _animator.SetTrigger("OnDestroy");
                _isReleased = true;
            }
        }

        private void Translate()
        {
            if(_isReleased)
                return;
            
            if (_isRightDirection)
                transform.Translate(Vector3.right * _velocity * Time.deltaTime);
            else
                transform.Translate(Vector3.left * _velocity * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent<Player>(out var player))
                return;

            if (other.TryGetComponent<IDamageable>(out var enemy))
            {
                enemy.TakeDamage(_playerData.PlayerStats.Damage);
                _animator.SetTrigger("OnDestroy");
                _isReleased = true;
            }
        }

        private bool IsTouchingEnvironment()
        {
            return Physics2D.OverlapCircle(transform.position, 0.2f, _environment);
        }

        public void ReleaseYourself()
        {
            _playerBulletPool.ReleaseBullet(this);
        }
    }
}