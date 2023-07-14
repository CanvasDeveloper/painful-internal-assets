using PainfulSmile.Runtime.Systems.SceneLoaderSystem.Core;
using UnityEngine;

namespace PainfulSmile.Runtime.Systems.SceneLoaderSystem.UI.Buttons
{
    public class UILoadSceneByIndexButton : ButtonBase
    {
        [SerializeField] private int _sceneIndex;

        protected override void ButtonAction()
        {
            SceneLoader.Instance.LoadSceneByIndex(_sceneIndex);
        }
    }
}