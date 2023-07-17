using DG.Tweening;
using PainfulSmile.Runtime.Systems.SceneLoaderSystem.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PainfulSmile.Runtime.Systems.SceneLoaderSystem.UI.Core
{
    public class UISceneLoader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Fade Settings")]
        [SerializeField] private Image _fadePanel;

        private void Start()
        {
            SceneLoader.Instance.OnRequestLoadSceneEvent += ShowLoadScreenAnimation;
            SceneLoader.Instance.OnLoadFinishedEvent += HideLoadScreenAnimation;
        }

        private void OnDestroy()
        {
            SceneLoader.Instance.OnRequestLoadSceneEvent -= ShowLoadScreenAnimation;
            SceneLoader.Instance.OnLoadFinishedEvent -= HideLoadScreenAnimation;
        }

        private void HideLoadScreenAnimation()
        {
            HideLoadScreen();
        }

        private void ShowLoadScreenAnimation()
        {
            _canvasGroup.blocksRaycasts = true;
            _fadePanel.gameObject.SetActive(true);

            _fadePanel.DOFade(1, SceneLoader.Instance.DelayToStartLoad);
        }

        private void HideLoadScreen(bool forceDisable = false)
        {
            _canvasGroup.blocksRaycasts = false;


            if (forceDisable)
            {
                _fadePanel.gameObject.SetActive(false);

                return;
            }

            _fadePanel.DOFade(0, SceneLoader.Instance.DelayToStartLoad).OnComplete(() =>
            {
                _fadePanel.gameObject.SetActive(false);
            });
        }
    }
}