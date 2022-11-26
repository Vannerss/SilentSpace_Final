
using System;
using System.Collections.Generic;
using System.Collections;
using SilentSpace.DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SilentSpace.Core
{
    public class SceneLoader : MonoBehaviour
    {
        private readonly List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

        public GameObject menu;
        public Button loadButton;
        public GameObject loadingInterface;
        public Image loadingProgressBar;

        public void StartGame()
        {
            LoadScenes();
            PlayerPrefs.SetInt("NewGame", 1);
            PlayerPrefs.SetInt("LoadGame", 0);
            //DataPersistenceManager.Instance.NewGame();

        }

        public void LoadGame()
        {
            LoadScenes();
            PlayerPrefs.SetInt("LoadGame", 1);
            PlayerPrefs.SetInt("NewGame", 0);
            //DataPersistenceManager.Instance.LoadGame();
        }

        private void LoadScenes()
        {
            HideMenu();
            ShowLoadingScreen();
            _scenesToLoad.Add(SceneManager.LoadSceneAsync("Map"));
            _scenesToLoad.Add(SceneManager.LoadSceneAsync("LunarLandscape3D", LoadSceneMode.Additive));
            StartCoroutine(LoadingScreen());
        }

        private void EnableLoadButton()
        {
            loadButton.interactable = true;
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
