using Game.Services;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuScripts
{
    public class MainMenuButtons : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        
        private SceneLoader _sceneLoader;
        private void Start()
        {
            _sceneLoader = ServiceLocator.Current.Get<SceneLoader>();
            
            ButtonResponse();
        }
        private void ButtonResponse()
        {
            _startButton.onClick.AddListener((() => { _sceneLoader.LoadScene(1); }));
        }
    }
}