using System;
using UnityEngine;

namespace EffectorValues
{
    /// <summary>
    /// A simple effector that animates in and out
    /// </summary>
    [Serializable]
    public class TemporaryEffector : Effector
    {
        [SerializeField] private AnimatedValue inAnimation = AnimatedValue.Default;
        [SerializeField] private AnimatedValue outAnimation = AnimatedValue.Default;

        public static TemporaryEffector Default => new TemporaryEffector(AnimatedValue.Default, AnimatedValue.Default);

        public TemporaryEffector(AnimatedValue inAnimation, AnimatedValue outAnimation, float intensity = 1)
        {
            this.inAnimation = inAnimation;
            this.outAnimation = outAnimation;
            this.Intensity = intensity;
        }

        public TemporaryEffector(TemporaryEffector o)
        {
            inAnimation = new AnimatedValue(o.inAnimation);
            outAnimation = new AnimatedValue(o.outAnimation);
            this.Intensity = o.Intensity;
        }

        public void Start()
        {
            inAnimation.SetStartTime(Time.time);
            outAnimation.SetStartTime(inAnimation.GetEndTime());
        }

        protected override float Evaluate()
        {
            if (!inAnimation.IsCompleted())
            {
                return inAnimation.Evaluate();
            }
            else
            {
                return 1 - outAnimation.Evaluate();
            }
        }

        public override bool IsCompleted()
        {
            return outAnimation.IsCompleted();
        }
    }
}