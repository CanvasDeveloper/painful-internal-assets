using PainfulSmile.Runtime.Systems.AudioSystem.Scriptables;
using System.Collections;
using UnityEngine;

namespace PainfulSmile.Runtime.Systems.AudioSystem.Triggers
{
    public enum AudioPlayType
    {
        WorldSpace3D,
        Music,
        Ambience,
    }

    public abstract class AudioTriggerBase : MonoBehaviour
    {
        [SerializeField] private SoundData _soundToPlay;

        [Header("Settings")]
        public float _delay;
        [SerializeField] private float _cooldown;
        [SerializeField] private bool _canTrigger;
        

        [ContextMenu("Play Audio")]
        public virtual void PlaySound()
        {
            if (!AudioManager.Instance)
            {
                return;
            }

            if (!_canTrigger)
            {
                return;
            }
            
            AudioManager.Instance.Play3DAudio(_soundToPlay, transform.position, _delay);

            if (_soundToPlay.Loop)
            {
                return;
            }

            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            StartCoroutine(DelayAndCooldown());
        }
        public virtual void StopSound()
        {
            if (!AudioManager.Instance)
            {
                return;
            }
            AudioManager.Instance.StopAudio(_soundToPlay);
        }
        IEnumerator DelayAndCooldown()
        {
            _canTrigger = false;
            yield return new WaitForSeconds(_delay);
            yield return new WaitForSeconds(_cooldown);
            _canTrigger = true;
        }
    }
}