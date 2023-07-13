using PainfulSmile.Runtime.Core;
using UnityEngine;

namespace PainfulSmile.Runtime.Systems.OptionsSystem
{
    [CreateAssetMenu(fileName = "New Float Value", menuName = PainfulSmileKeys.ScriptablePath + "Float Value")]
    public class FloatDefaultValueData : ScriptableObject
    {
        [field: SerializeField] public float Value { get; private set; }
    }
}