using PainfulSmile.Runtime.Systems.AudioSystem.Scriptables;
using UnityEngine;

namespace PainfulSmile.Runtime.Systems.AudioSystem.Triggers
{
    public class PlayRandomAudio3D : MonoBehaviour
    {
        [SerializeField] private SoundData[] sounds;

        public void Play()
        {
            if (sounds.Length > 0)
            {
                int randSound = Random.Range(0, sounds.Length);
                AudioManager.Instance.Play3DAudio(sounds[randSound], transform.position);
            }
        }
    }
}