using UnityEngine;

namespace Game.Services
{
    public class MainMenuServiceLocatorLoader : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        
        private void Awake()
        {
            _sceneLoader = new SceneLoader();
            
            RegisterServices();
            Init();
        }

        private void RegisterServices()
        {
            ServiceLocator.Initialize();
            
            ServiceLocator.Current.Register<SceneLoader>(_sceneLoader);
        }

        private void Init()
        {
            
        }
    }
}