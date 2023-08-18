using System.Collections.Generic;
using UnityEngine;

namespace PainfulSmile.Runtime.Utilities.AutoTimer.Core
{
    public class AutoTimerManager : MonoBehaviour
    {
        private readonly List<AutoTimer> timers = new();

        private void Update()
        {
            if (timers.Count <= 0)
                return;

            for (int i = timers.Count - 1; i >= 0; i--)
            {
                AutoTimer timer = timers[i];

                timer.Update(Time.deltaTime);
            }
        }

        public void AddToManager(AutoTimer timer)
        {
            timers.Add(timer);
        }

        public void RemoveFrom(AutoTimer timer)
        {
            timers.Remove(timer);
        }
    }
}