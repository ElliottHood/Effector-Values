namespace EffectorValues
{
    /// <summary>
    /// A float input that can be added to an EffectorValue
    /// </summary>
    public abstract class Effector
    {
        public float Intensity = 1;
        public float Value => Evaluate() * Intensity;
        protected abstract float Evaluate();
        public abstract bool IsCompleted();
    }
}