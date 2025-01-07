namespace EffectorValues
{
    /// <summary>
    /// Evaluates to the sum of all inputs
    /// </summary>
    public class AdditiveEffectorValue : EffectorValue
    {
        public AdditiveEffectorValue(float defaultValue = 0)
        {
            this.defaultValue = defaultValue;
        }

        public override float Evaluate()
        {
            float sum = defaultValue;


            for (int i = temporaryEffectors.Count - 1; i >= 0; --i)
            {
                if (temporaryEffectors[i].IsCompleted())
                {
                    temporaryEffectors.Remove(temporaryEffectors[i]);
                }
                else
                {
                    sum += temporaryEffectors[i].Value;
                }
            }

            foreach (var toggleEffector in toggleEffectors)
            {
                sum += toggleEffector.Value;
            }

            if (sum == float.NaN)
                sum = defaultValue;

            return sum;
        }
    }
}