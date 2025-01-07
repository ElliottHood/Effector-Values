namespace EffectorValues
{
    /// <summary>
    /// Evaluates to the product of all inputs
    /// </summary>
    public class MultiplicativeEffectorValue : EffectorValue
    {
        public MultiplicativeEffectorValue(float defaultValue = 1)
        {
            this.defaultValue = defaultValue;
        }

        public override float Evaluate()
        {
            float product = defaultValue;

            for (int i = temporaryEffectors.Count - 1; i >= 0; --i)
            {
                if (temporaryEffectors[i].IsCompleted())
                {
                    temporaryEffectors.Remove(temporaryEffectors[i]);
                }
                else
                {
                    product *= temporaryEffectors[i].Value;
                }
            }

            foreach (var item in toggleEffectors)
            {
                product *= item.Value;
            }

            return product;
        }
    }
}