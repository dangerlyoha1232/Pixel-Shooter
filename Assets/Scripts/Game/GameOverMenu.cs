using Game.Services;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Game
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private RectTransform _root;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;

        private GameScore _gameScore;
        private SceneLoader _sceneLoader;

        private void Start()
        {
            _gameScore = ServiceLocator.Current.Get<GameScore>();
            _sceneLoader = ServiceLocator.Current.Get<SceneLoader>();

            EventBus.OnPlayerDie += ShowGameOverMenu;

            _root.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            EventBus.OnPlayerDie -= ShowGameOverMenu;
        }

        private void ShowGameOverMenu()
        {
            _root.gameObject.SetActive(true);

            _score.text = "Your score: " + _gameScore.Score;
            ButtonResponse();
        }

        private void ButtonResponse()
        {
            _restartButton.onClick.AddListener((() => { _sceneLoader.LoadScene(1); }));
            
            _mainMenuButton.onClick.AddListener((() => { _sceneLoader.LoadScene(0); }));
        }
    }
}