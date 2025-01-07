using DG.Tweening;
using System;
using UnityEngine;

namespace EffectorValues
{
    /// <summary>
    /// Helper class used for the Effectors. Used to animated a float over time
    /// </summary>
    [Serializable]
    public class AnimatedValue
    {
        public Ease easing = Ease.InOutSine;
        public float duration = 0.25f;
        public float amplitude = 1;
        public float period = 1;
        private float startTime = Mathf.NegativeInfinity;
        private bool initialized = false;
        private EaseFunction easeFunction;

        public static AnimatedValue Default => new AnimatedValue(0.25f, 1, 1, Ease.InOutSine);

        public AnimatedValue(float duration, float amplitude, float period, Ease easing)
        {
            this.duration = duration;
            this.amplitude = amplitude;
            this.period = period;
            this.easing = easing;
            startTime = Mathf.NegativeInfinity;
            initialized = false;
        }

        public AnimatedValue(AnimatedValue o)
        {
            duration = o.duration;
            amplitude = o.amplitude;
            period = o.period;
            easing = o.easing;
            startTime = Mathf.NegativeInfinity;
            initialized = false;
        }

        public void SetStartTime(float startTime)
        {
            this.startTime = startTime;
        }

        public float GetEndTime()
        {
            return startTime + duration;
        }

        public float Evaluate()
        {
            EnsureEaseFunctionExist();

            if (Time.time < startTime)
            {
                return 0;
            }
            else if (Time.time >= GetEndTime())
            {
                return 1;
            }

            return (float)DG.Tweening.Core.Easing.EaseManager.Evaluate(easing, easeFunction, Time.time - startTime, duration, amplitude, period);
        }

        private void EnsureEaseFunctionExist()
        {
            if (!initialized)
            {
                easeFunction = DG.Tweening.Core.Easing.EaseManager.ToEaseFunction(easing);
                initialized = true;
            }
        }

        public bool IsCompleted()
        {
            return Time.time > GetEndTime();
        }
    }
}