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

        private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

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

        private IEnumerator LoadingScreen()
        {
            var totalProgress = 0f;
            for (var i = 0; i < _scenesToLoad.Count; i++)
            {
                while (!_scenesToLoad[i].isDone)
                {
                    totalProgress = Mathf.Clamp01(_scenesToLoad[i].progress / .9f);
                    loadingProgressBar.fillAmount = totalProgress / _scenesToLoad.Count;
                    yield return null;
                }
            }
        }
    }
}
