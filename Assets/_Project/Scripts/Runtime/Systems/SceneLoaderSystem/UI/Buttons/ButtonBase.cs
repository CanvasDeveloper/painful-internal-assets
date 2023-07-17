using UnityEngine;
using UnityEngine.UI;

namespace PainfulSmile.Runtime.Systems.SceneLoaderSystem.UI.Buttons
{
    public abstract class ButtonBase : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(ButtonAction);
        }

        protected abstract void ButtonAction();
    }
}