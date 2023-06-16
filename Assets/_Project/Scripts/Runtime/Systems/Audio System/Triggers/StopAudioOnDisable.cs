namespace PainfulSmile.Runtime.Systems.AudioSystem.Triggers
{
    public class StopAudioOnDisable : AudioTriggerBase
    {
        private void OnDisable()
        {
            StopSound();
        }
    }
}