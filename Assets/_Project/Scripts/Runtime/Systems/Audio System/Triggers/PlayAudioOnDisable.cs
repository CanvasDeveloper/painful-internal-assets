namespace PainfulSmile.Runtime.Systems.AudioSystem.Triggers
{
    public class PlayAudioOnDisable : AudioTriggerBase
    {
        private void OnDisable()
        {
            PlaySound();
        }
    }
}