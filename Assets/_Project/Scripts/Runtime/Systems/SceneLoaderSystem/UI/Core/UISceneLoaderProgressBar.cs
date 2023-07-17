using PainfulSmile.Runtime.Systems.SceneLoaderSystem.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PainfulSmile.Runtime.Systems.SceneLoaderSystem.UI.Core
{
    public class UISceneLoaderProgressBar : MonoBehaviour
    {
        [Header("Progress Bar Settings")]
        [SerializeField] private GameObject _loadingBar;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _progressText;

        private void Start()
        {
            SceneLoader.Instance.OnLoadStartEvent += ShowProgressBar;
            SceneLoader.Instance.OnLoadFinishedEvent += HideProgressBar;
            SceneLoader.Instance.OnUpdateLoadProgressEvent += UpdateProgressBar;
        }

        private void OnDestroy()
        {
            SceneLoader.Instance.OnLoadStartEvent -= ShowProgressBar;
            SceneLoader.Instance.OnLoadFinishedEvent -= HideProgressBar;
            SceneLoader.Instance.OnUpdateLoadProgressEvent -= UpdateProgressBar;
        }

        private void ShowProgressBar()
        {
            _loadingBar.SetActive(true);
        }

        private void HideProgressBar()
        {
            _loadingBar.SetActive(false);
        }

        private void UpdateProgressBar(float progress)
        {
            _slider.value = progress;
            _progressText.text = Mathf.FloorToInt(progress * 100f).ToString() + "%";
        }
    }
}