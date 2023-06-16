using PainfulSmile.Runtime.Systems.AudioSystem.Scriptables;
using UnityEngine;

namespace PainfulSmile.Runtime.Systems.AudioSystem
{
    public class AutoAudioPlay : MonoBehaviour
    {
        [SerializeField] private SoundData data;

        private void Start()
        {
            if(AudioManager.Instance.GetCurrentSoundData() == data)
            {
                return;
            }

            AudioManager.Instance.ChangeMainMusic(data);
        }
    }
}