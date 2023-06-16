namespace PainfulSmile.Runtime.Systems.AudioSystem.Triggers
{
    public class PlayAudioOnEnable : AudioTriggerBase
    {
        private void OnEnable()
        {
            PlaySound();
        }
    }
}