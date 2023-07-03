using PainfulSmile.Runtime.Systems.OptionsSystem.Camera;
using PainfulSmile.Runtime.Systems.OptionsSystem.Core;
using PainfulSmile.Runtime.Systems.OptionsSystem.UI.Base;
using UnityEngine;

namespace PainfulSmile.Runtime.Systems.OptionsSystem.UI
{
    public class OptionSlider : SliderBase
    {
        [SerializeField] private CameraSettingsData _camSettings;
        [SerializeField] protected OptionType optionType;

        private void OnEnable()
        {
            SetMinMax(_camSettings.MinFovValue, _camSettings.MaxFovValue);

            slider.value = OptionsManager.Instance.GetCurrentValueFloat(optionType, _camSettings.FovDefaultValue);

            AddListener();
        }

        private void OnDisable()
        {
            RemoveListener();
        }

        protected override void SliderUpdateAction(float value)
        {
            OptionsManager.Instance.UpdateValueFloat(optionType, value);
        }
    }
}