using UnityEngine;
using Game.EnemyScripts;
using Game.PlayerScripts;
using Game.Services;

namespace Game.Bullet
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private LayerMask _environment;
        
        private bool _isRightDirection;
        private float _damage;
        private bool _isReleased;
        
        private Animator _animator;
        private EnemyBulletPool _pool;

        public void Init(bool isRightDirection, Transform spawnOrigin, float damage)
        {
            _pool = ServiceLocator.Current.Get<EnemyBulletPool>();
            _isReleased = false;
            _isRightDirection = isRightDirection;

            transform.position = spawnOrigin.position;
            _damage = damage;

            _animator = GetComponent<Animator>();
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
                return;

            if (other.TryGetComponent<IDamageable>(out IDamageable player))
            {
                player.TakeDamage(_damage);
            }

            _animator.SetTrigger("OnDestroy");
            _isReleased = true;
        }
        
        private bool IsTouchingEnvironment()
        {
            return Physics2D.OverlapCircle(transform.position, 0.2f, _environment);
        }

        public void ReleaseYourself()
        {
            _pool.ReleaseBullet(this);
        }
    }
}