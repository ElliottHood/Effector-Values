using System;
using UnityEngine;

namespace EffectorValues
{
    /// <summary>
    /// An effector that you can add to an EffectorValue that can be toggled on and off
    /// - Animated transition between the two states
    /// - Waits for previous transition to finish before changing state
    /// </summary>
    [Serializable]
    public class ToggleEffector : Effector
    {
        [SerializeField] private AnimatedValue inAnimation = AnimatedValue.Default;
        [SerializeField] private AnimatedValue outAnimation = AnimatedValue.Default;
        public bool Enabled { get; private set;}
        private bool bufferedEnabledState = false;

        public static ToggleEffector Default => new ToggleEffector(AnimatedValue.Default, AnimatedValue.Default);

        public ToggleEffector(AnimatedValue inAnimation, AnimatedValue outAnimation, float intensity = 1)
        {
            this.inAnimation = inAnimation;
            this.outAnimation = outAnimation;
            this.Intensity = intensity;
        }

        public void Enable(bool skipEasing = false)
        {
            bufferedEnabledState = true;

            if (Enabled)
                return;

            if (skipEasing || outAnimation.IsCompleted())
            {
                UpdateEnabledState(true, Time.time);
            }
        }

        public void Disable(bool skipEasing = false)
        {
            bufferedEnabledState = false;

            if (!Enabled)
                return;

            if (skipEasing || inAnimation.IsCompleted())
            {
                UpdateEnabledState(false, Time.time);
            }
        }

        protected void CheckForBufferedStateUpdate()
        {
            if (Enabled == bufferedEnabledState)
                return;

            if (Enabled)
            {
                if (!inAnimation.IsCompleted())
                    return;

                UpdateEnabledState(bufferedEnabledState, inAnimation.GetEndTime());
            }
            else
            {
                if (!outAnimation.IsCompleted())
                    return;

                UpdateEnabledState(bufferedEnabledState, outAnimation.GetEndTime());
            }
        }

        private void UpdateEnabledState(bool desiredState, float animationStartTime)
        {
            Enabled = desiredState;
            if (Enabled)
            {
                inAnimation.SetStartTime(animationStartTime);
            }
            else
            {
                outAnimation.SetStartTime(animationStartTime);
            }
        }

        protected override float Evaluate()
        {
            CheckForBufferedStateUpdate();

            if (Enabled)
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
            if (Enabled)
            {
                return inAnimation.IsCompleted();
            }
            else
            {
                return outAnimation.IsCompleted();
            }
        }
    }
}