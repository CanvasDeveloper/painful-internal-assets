using System;
using UnityEngine;

namespace PainfulSmile.Runtime.Utilities.AutoTimer.Core
{
    [Serializable]
    public class AutoTimer
    {
        public static AutoTimerManager ManagerInstance;

        public event Action OnExpire;
        public event Action<UpdateProgressArgs> OnUpdateProgress;
        public float InitTime { get; private set; }
        [field: SerializeField] public float Time { get; private set; }

        public bool Initialized { get; private set; }
        private bool finished;
        private bool loop;

        private readonly float updateEventThresholder = 0.2f;
        private float _currentUpdateEventThresholder;

        public void Start(float timer, bool loopable = false)
        {
            if (Initialized)
            {
                Restart();
                return;
            }

            if (ManagerInstance == null)
            {
                ManagerInstance = new GameObject("AutoTimerManager").AddComponent<AutoTimerManager>();
            }

            ManagerInstance.AddToManager(this);

            InitTime = timer;
            Time = timer;
            loop = loopable;

            finished = false;
            Initialized = true;

            _currentUpdateEventThresholder = updateEventThresholder;
        }

        public void Update(float deltaTime)
        {
            if (!Initialized)
                return;

            if (finished)
                return;

            Time -= deltaTime;

            _currentUpdateEventThresholder -= deltaTime;

            if (_currentUpdateEventThresholder <= 0)
            {
                _currentUpdateEventThresholder = updateEventThresholder;
                OnUpdateProgress?.Invoke(new UpdateProgressArgs
                {
                    progressAmount = Time,
                    progressPercent = 1 - Time / InitTime
                });
            }

            if (Time <= 0)
            {
                finished = true;
                OnExpire?.Invoke();

                if (loop)
                {
                    Restart();
                }
            }
        }

        public void Kill()
        {
            if (Initialized)
            {
                ManagerInstance.RemoveFrom(this);
            }

            Initialized = false;
            finished = true;

            Time = 0f;
            InitTime = 0f;

            OnExpire = null;
            OnUpdateProgress = null;
        }

        public void Restart()
        {
            Time = InitTime;
            finished = false;

            if (!Initialized)
            {
                Debug.LogError("You need Start() before Restart() timers!");
            }
        }
    }
}