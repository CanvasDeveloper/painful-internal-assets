using PainfulSmile.Runtime.Systems.OptionsSystem.Core;

namespace PainfulSmile.Runtime.Systems.OptionsSystem.UI.Base
{
    public class OptionToggleBase : ToggleBase
    {
        protected OptionType _optionType;

        protected virtual void OnEnable()
        {
            toggle.isOn = OptionsManager.Instance.GetCurrentValueInt(_optionType, defaultValue ? 1 : 0) == 1;
        }

        protected override void ToggleAction(bool value)
        {
            int boolValue = value ? 1 : 0;

            OptionsManager.Instance.UpdateValueInt(_optionType, boolValue);
        }
    }
}