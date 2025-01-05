using EffectorValues;
using UnityEngine;

public class HeightEffectorDemo : MonoBehaviour
{
    [SerializeField] private TemporaryEffector temporaryEffector = TemporaryEffector.Default;
    [SerializeField] private ToggleEffector toggleEffector = ToggleEffector.Default;
    private AdditiveEffectorValue heightEffectorValue;

    private void Awake()
    {
        // transform.position.y is the default value
        heightEffectorValue = new AdditiveEffectorValue(transform.position.y); 

        // Only add a toggle once, and turn it on and off with ToggleEffect()
        heightEffectorValue.AddToggleableEffector(toggleEffector); 
    }

    public void TemporaryEffect()
    {
        // Add a new temporary effector each time you want to play the effect
        heightEffectorValue.AddTemporaryEffector(temporaryEffector); 
    }

    public void ToggleEffect()
    {
        if (toggleEffector.Enabled)
        {
            toggleEffector.Disable();
        }
        else
        {
            toggleEffector.Enable();
        }
    }

    private void Update()
    {
        // Read the sum of each effector and apply it to the transform
        transform.position = new Vector3(transform.position.x, heightEffectorValue.Evaluate(), transform.position.z);
    }
}
