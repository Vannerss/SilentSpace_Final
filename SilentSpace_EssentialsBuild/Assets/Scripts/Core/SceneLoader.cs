using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SilentSpace.Core
{
    public class SceneLoader : MonoBehaviour
    {
        public GameObject menu;
        public GameObject loadingInterface;
        public Image loadingProgressBar;

        private readonly List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

        public void StartGame()
        {
            HideMenu();
            ShowLoadingScreen();
            _scenesToLoad.Add(SceneManager.LoadSceneAsync("Map"));
            _scenesToLoad.Add(SceneManager.LoadSceneAsync("LunarLandscape3D", LoadSceneMode.Additive));
            StartCoroutine(LoadingScreen());
        }

        private void HideMenu()
        {
            menu.SetActive(false);
        }
        
        private void ShowLoadingScreen()
        {
            loadingInterface.SetActive(true);
        }

        //Shows a loading bar that increased by scene loading progress that unity gives us scene.progress
        private IEnumerator LoadingScreen()
        {
            foreach (var scene in _scenesToLoad)
            {
                while (!scene.isDone)
                {
                    var totalProgress = Mathf.Clamp01(scene.progress / .9f);
                    loadingProgressBar.fillAmount = totalProgress / _scenesToLoad.Count;
                    yield return null;
                }
            }
        }
    }
}
