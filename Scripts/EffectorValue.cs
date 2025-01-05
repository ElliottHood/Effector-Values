using System.Collections.Generic;

namespace EffectorValues
{
    /// <summary>
    /// A single value that outputs the result of multiple animated inputs
    /// </summary>
    public abstract class EffectorValue
    {
        protected float defaultValue;
        protected List<TemporaryEffector> temporaryEffectors = new List<TemporaryEffector>();
        protected List<ToggleEffector> toggleEffectors = new List<ToggleEffector>();

        public void AddTemporaryEffector(TemporaryEffector effector)
        {
            if (effector.Intensity == 0)
                return;

            // Deep copy so you can add multiple of the same temporary effector at once
            TemporaryEffector effectorCopy = new TemporaryEffector(effector);
            effectorCopy.Start();
            temporaryEffectors.Add(effectorCopy);
        }

        public void AddToggleableEffector(ToggleEffector effector)
        {
            // Shallow copy so you can edit the toggled state in other scripts
            if (!toggleEffectors.Contains(effector))
            {
                toggleEffectors.Add(effector);
            }
        }

        public void RemoveToggleableEffector(ToggleEffector effector)
        {
            toggleEffectors.Remove(effector);
        }

        public abstract float Evaluate();
    }
}