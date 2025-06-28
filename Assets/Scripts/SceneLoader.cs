using UnityEngine.SceneManagement;
using System.Collections;
using Game.Services;

public class SceneLoader : IService
{
    public void LoadScene(int sceneIndex) => SceneManager.LoadSceneAsync(sceneIndex);
}