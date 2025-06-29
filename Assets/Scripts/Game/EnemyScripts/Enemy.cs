using Game.PlayerScripts;
using Game.Services;
using TMPro;
using UnityEngine;

namespace Game.EnemyScripts
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] protected EnemyData _data;
        [SerializeField] protected LayerMask _playerMask;
        [SerializeField] private Animator _animator;
        
        private PlayerMovement _playerMovement;
        private Rigidbody2D _rigidbody2D;
        private CapsuleCollider2D _capsuleCollider2D;
        
        protected bool IsFacingRight;
        private Transform _playerTransform;
        private Vector3 _originPosition;
        private Vector3 _vectorToOrigin;
        
        private Health _health;
        private bool _isDead;
        private bool _isPlayerDie = false;
        
        public virtual void Start()
        {
            _playerMovement = ServiceLocator.Current.Get<PlayerMovement>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            _health = new Health(_data.Stats.Health);

            _originPosition = transform.position;

            _health.OnDeath += Die;
            EventBus.OnPlayerDie += IsPlayerDie;
        }

        private void OnDestroy()
        {
            _health.OnDeath -= Die;
            EventBus.OnPlayerDie -= IsPlayerDie;
        }

        public virtual void Update()
        {
            if (_isDead || _isPlayerDie)
            {
                _animator.SetBool("IsMoving", false);
                return;
            }

            _playerTransform = _playerMovement.GiveTransform();
            _vectorToOrigin = _originPosition - transform.position;
            
            if (IsPlayerInSight())
            {
                ChasePlayer();
                if (IsPlayerOnAttackRange())
                    Attack();
            }
            else if (transform.position != _originPosition && !IsPlayerInSight() || _isPlayerDie)
            {
                BackToOrigin();
                
                if(_vectorToOrigin.x > 0f && !IsFacingRight)
                    Flip();
                else if(_vectorToOrigin.x < 0f && IsFacingRight)
                    Flip();
            }

            _animator.SetBool("IsMoving", IsPlayerInSight());
        }
        
        private void ChasePlayer()
        {
            Vector2 vectorToPlayer = _playerTransform.position - transform.position;
            float distanceToPlayer = vectorToPlayer.magnitude;

            if (vectorToPlayer.x > 0f && !IsFacingRight)
                Flip();
            else if (vectorToPlayer.x <0f && IsFacingRight)
                Flip();
            
            if(distanceToPlayer <= _data.Stats.AttackRange)
                return;

            Vector2 attackTarget = (Vector2)_playerTransform.position - vectorToPlayer.normalized * _data.Stats.AttackRange;
            
            transform.position =
                Vector2.MoveTowards(transform.position, attackTarget, _data.Stats.Speed * Time.deltaTime);
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }
        
        private void Flip()
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

        private bool IsPlayerOnAttackRange()
        {
            return Vector2.Distance(_playerTransform.position, transform.position) <= _data.Stats.AttackRange;
        }

        private bool IsPlayerInSight()
        {
            Vector2 directionToPlayer = (_playerTransform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, _data.Stats.ViewRange, _playerMask);
            Debug.DrawRay(transform.position, directionToPlayer * _data.Stats.ViewRange, Color.red);
            
            if (hit.collider != null && hit.collider.TryGetComponent<Player>(out Player player) && player != null)
                return true;

            return false;
        }

        private void BackToOrigin()
        {
            transform.position = Vector2.MoveTowards(transform.position, _originPosition, _data.Stats.Speed * Time.deltaTime);
        }
        
        public void TakeDamage(float damage)
        {
            if (_isDead)
                return;
            
            _health.TakeDamage(damage);
            
            _animator.SetTrigger("OnHurt");
        }

        private void Die()
        {
            Destroy(gameObject, 4f);
            _isDead = true;
            EventBus.SendEnemyDie(_data.Stats.PointsForKill);
            _animator.SetTrigger("OnDead");
            _animator.SetBool("IsDead", true);
        }

        public void DisablePhysics()
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            _capsuleCollider2D.enabled = false;
        }
        
        private void IsPlayerDie() => _isPlayerDie = true;
    }
}