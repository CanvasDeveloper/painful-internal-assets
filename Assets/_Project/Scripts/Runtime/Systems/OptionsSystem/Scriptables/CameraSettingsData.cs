using PainfulSmile.Runtime.Core;
using UnityEngine;

namespace PainfulSmile.Runtime.Systems.OptionsSystem.Camera
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = PainfulSmileKeys.ScriptablePath + "CameraSettings")]
    public class CameraSettingsData : ScriptableObject
    {
        [Header("Mouse Sensibility Values")]
        public float MouseSensDefaultValue;
        public float MinMouseSens;
        public float MaxMouseSens;

        [Header("FOV Values")]
        public float FovDefaultValue;
        public float MinFovValue;
        public float MaxFovValue;
    }
}

