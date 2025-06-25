using Game.Bullet;
using Game.PlayerScripts;
using UnityEngine;

namespace Game.Services
{
    public class ServiceLocatorLoader : MonoBehaviour
    {
        [Header("Player Services")]
        [SerializeField] private Player _player;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private PlayerAnimationsHandler _playerAnimationsHandler;
        [SerializeField] private PlayerMovement _playerMovement;
        private InputHandler _inputHandler;
        
        [Header("Game Services")]
        [SerializeField] private GameScore _gameScore;
        
        private PlayerBulletPool _playerBulletPool;
        private EnemyBulletPool _enemyBulletPool;
        
        private void Awake()
        {
            _inputHandler = new InputHandler();
            _playerBulletPool = new PlayerBulletPool();
            _enemyBulletPool = new EnemyBulletPool();
            
            RegisterServices();
            Init();
        }

        private void RegisterServices()
        {
            ServiceLocator.Initialize();
            
            ServiceLocator.Current.Register<Player>(_player);
            ServiceLocator.Current.Register<PlayerData>(_playerData);
            ServiceLocator.Current.Register<PlayerAnimationsHandler>(_playerAnimationsHandler);
            ServiceLocator.Current.Register<InputHandler>(_inputHandler);
            ServiceLocator.Current.Register<PlayerBulletPool>(_playerBulletPool);
            ServiceLocator.Current.Register<EnemyBulletPool>(_enemyBulletPool);
            ServiceLocator.Current.Register<PlayerMovement>(_playerMovement);
            ServiceLocator.Current.Register<GameScore>(_gameScore);
        }

        private void Init()
        {
            _playerAnimationsHandler.Init();
            _player.Init();
            _gameScore.Init();
        }
    }
}