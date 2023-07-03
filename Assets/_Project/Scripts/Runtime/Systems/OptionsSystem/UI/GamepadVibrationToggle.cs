using PainfulSmile.Runtime.Systems.OptionsSystem.Core;
using PainfulSmile.Runtime.Systems.OptionsSystem.UI.Base;

namespace PainfulSmile.Runtime.Systems.OptionsSystem.UI
{
    public class GamepadVibrationToggle : OptionToggleBase
    {
        protected override void Awake()
        {
            base.Awake();

            _optionType = OptionType.GAMEPAD_VIBRATION_TOGGLE;
        }
    }
}