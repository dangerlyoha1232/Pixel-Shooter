using UnityEngine;
using Game.Services;

namespace Game.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour, IService
    {
        public bool IsFacingRight { get; private set; }

        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;

        private PlayerData _playerData;
        private InputHandler _inputHandler;
        private PlayerAnimationsHandler _animationHandler;

        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        private float _horizontalInput;
        private bool _isPlayerDie = false;

        private void Start()
        {
            _playerData = ServiceLocator.Current.Get<PlayerData>();
            _inputHandler = ServiceLocator.Current.Get<InputHandler>();
            _animationHandler = ServiceLocator.Current.Get<PlayerAnimationsHandler>();

            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            EventBus.OnPlayerDie += OnPlayerDie;
        }

        private void OnDestroy()
        {
            EventBus.OnPlayerDie -= OnPlayerDie;
        }
        
        private void Update()
        {
            if(_isPlayerDie)
                return;
            
            if (_inputHandler.JumpInput() && IsGrounded())
                Jump();

            _animationHandler.IsGrounded(IsGrounded());
        }

        private void FixedUpdate()
        {
            if(_isPlayerDie)
                return;
            
            HorizontalMove();
        }

        private void HorizontalMove()
        {
            _horizontalInput = _inputHandler.HorizontalInput();

            Vector2 direction = new Vector2(_horizontalInput, 0);

            Vector2 velocity = _rigidbody.linearVelocity;
            velocity.x = _horizontalInput * _playerData.PlayerStats.Speed;
            _rigidbody.linearVelocity = velocity;

            _animationHandler.RunAnimation(direction.sqrMagnitude);

            FlipSpriteView();
        }

        private void FlipSpriteView()
        {
            if (_horizontalInput > 0)
            {
                _spriteRenderer.flipX = true;
                IsFacingRight = true;
            }
            else if (_horizontalInput < 0)
            {
                _spriteRenderer.flipX = false;
                IsFacingRight = false;
            }
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _playerData.PlayerStats.JumpForce, ForceMode2D.Impulse);
            _animationHandler.JumpAnimation();
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
        }
        
        public Transform GiveTransform() => transform;
        
        private void OnPlayerDie() => _isPlayerDie = true;
    }
}