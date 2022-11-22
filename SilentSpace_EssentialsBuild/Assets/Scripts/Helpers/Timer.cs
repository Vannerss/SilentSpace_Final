using System;
using UnityEngine;

namespace SilentSpace.Helpers
{
    public class Timer
    {

        public float RemainingSeconds;

        public Timer(float duration = 0f)
        {
            RemainingSeconds = duration;
        }

        public event Action OnTimerEnd;

        public void Tick()
        {
            if(RemainingSeconds == 0) return;

            RemainingSeconds -= Time.deltaTime;

            CheckForTimerEnd();
        }

        private void CheckForTimerEnd()
        {
            if (RemainingSeconds >= 0f) return;

            RemainingSeconds = 0f;

            OnTimerEnd?.Invoke();
        }
    }
}
