namespace PainfulSmile.Runtime.Systems.AudioSystem.Triggers
{
    public class StopAudioOnEnable : AudioTriggerBase
    {
        private void OnEnable()
        {
            StopSound();
        }
    }
}