using PainfulSmile.Runtime.Core;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PainfulSmile.Runtime.Systems.SceneLoaderSystem.Core
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        public event Action OnRequestLoadSceneEvent;
        public event Action OnLoadStartEvent;
        public event Action OnLoadFinishedEvent;
        public event Action<float> OnUpdateLoadProgressEvent;

        [Header("Main Scenes")]
        [SerializeField] private int _titleSceneIndex = 0;
        [SerializeField] private int _gameplaySceneIndex = 1;

        [Header("Other Scenes")]
        [Tooltip("This is only used when we take an object from one scene to another.")]
        [SerializeField] private int _emptyLoadingScene = -1;

        [field: Header("Settings")]
        [field: SerializeField] public float DelayToStartLoad { get; private set; } = 2f;

        public int CurrentSceneIndex { get; private set; }

        public void LoadTitleScene()
        {
            LoadSceneByIndex(_titleSceneIndex);
        }

        public void LoadGameplayScene()
        {
            LoadSceneByIndex(_gameplaySceneIndex);
        }

        [ContextMenu("Next Level")]
        public void NextScene()
        {
            int targetIndex = GetNextSceneIndex();

            if (targetIndex < 0)
            {
                return;
            }

            LoadSceneByIndex(targetIndex);
        }

        [ContextMenu("Reload Level")]
        public void ReloadScene()
        {
            LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadSceneByIndex(int sceneIndex)
        {
            StopAllCoroutines();
            StartCoroutine(LoadAsynchronously(sceneIndex));
        }

        private IEnumerator LoadAsynchronously(int sceneIndex)
        {
            OnRequestLoadSceneEvent?.Invoke();

            yield return new WaitForSeconds(DelayToStartLoad);

            OnLoadStartEvent?.Invoke();

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                OnUpdateLoadProgressEvent?.Invoke(progress);

                yield return null;
            }

            CurrentSceneIndex = sceneIndex;
            OnLoadFinishedEvent?.Invoke();
        }

        public int GetSceneCountInBuildSettings()
        {
            return SceneManager.sceneCountInBuildSettings;
        }

        public int GetNextSceneIndex()
        {
            int targetSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (targetSceneIndex >= GetSceneCountInBuildSettings())
            {
                Debug.LogWarning("You are trying to load a next scene that does not exist in the build settings.");

                return -1;
            }

            return targetSceneIndex;
        }

        public void MoveToOtherScene(int targetScene, GameObject objectToMove, Action preLoadScene = null, Action postLoadScene = null)
        {
            StartCoroutine(MovingObject(targetScene, objectToMove, preLoadScene, postLoadScene));
        }

        private IEnumerator MovingObject(int targetSceneIndex, GameObject objectToMove, Action preLoadScene, Action postLoadScene)
        {
            if(_emptyLoadingScene < 0)
            {
                Debug.LogWarning("You need an empty loading scene to temporarily hold the object you want to take to the next scene.");

                yield break;
            }

            preLoadScene?.Invoke();

            AsyncOperation sceneAsyncOperation = null;

            sceneAsyncOperation = SceneManager.LoadSceneAsync(_emptyLoadingScene, LoadSceneMode.Additive);
            yield return new WaitUntil(() => sceneAsyncOperation.isDone);

            SceneManager.MoveGameObjectToScene(objectToMove, SceneManager.GetSceneByBuildIndex(_emptyLoadingScene));

            sceneAsyncOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            yield return new WaitUntil(() => sceneAsyncOperation.isDone);

            sceneAsyncOperation = SceneManager.LoadSceneAsync(targetSceneIndex, LoadSceneMode.Additive);

            while (!sceneAsyncOperation.isDone)
            {
                float progress = Mathf.Clamp01(sceneAsyncOperation.progress / .9f);
                OnUpdateLoadProgressEvent?.Invoke(progress);

                yield return new WaitForEndOfFrame();
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(targetSceneIndex));
            SceneManager.MoveGameObjectToScene(objectToMove, SceneManager.GetActiveScene());

            CurrentSceneIndex = targetSceneIndex;

            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(_emptyLoadingScene));

            postLoadScene?.Invoke();

            preLoadScene = null;
            postLoadScene = null;
        }
    }
}