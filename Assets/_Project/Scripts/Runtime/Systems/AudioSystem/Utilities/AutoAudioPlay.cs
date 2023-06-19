using PainfulSmile.Runtime.Systems.AudioSystem.Core;
using PainfulSmile.Runtime.Systems.AudioSystem.Scriptables;
using UnityEngine;

namespace PainfulSmile.Runtime.Systems.AudioSystem.Utilities
{
    public class AutoAudioPlay : MonoBehaviour
    {
        [SerializeField] private SoundData _data;

        private void Start()
        {
            AudioManager.Instance.ChangeMainMusic(_data);
        }
    }
}