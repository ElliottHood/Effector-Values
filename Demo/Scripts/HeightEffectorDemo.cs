using EffectorValues;
using UnityEngine;
using UnityEngine.UI;

public class HeightEffectorDemo : MonoBehaviour
{
    [SerializeField] private TemporaryEffector temporaryEffector = TemporaryEffector.Default;
    [SerializeField] private ToggleEffector toggleEffector = ToggleEffector.Default;
    [SerializeField] private Button temporaryButton;
    [SerializeField] private Button toggleButton;
    [SerializeField] private Text toggleButtonText;
    private AdditiveEffectorValue heightEffectorValue;

    private void Awake()
    {
        heightEffectorValue = new AdditiveEffectorValue(transform.position.y);
        heightEffectorValue.AddToggleableEffector(toggleEffector); // Only add a toggle once, then you can turn it on or off

        temporaryButton.onClick.AddListener(() =>
        {
            heightEffectorValue.AddTemporaryEffector(temporaryEffector);
        });
        toggleButton.onClick.AddListener(() =>
        {
            if (toggleEffector.Enabled)
            {
                toggleEffector.Disable();
                toggleButtonText.text = "Enable Toggle Effect";
            }
            else
            {
                toggleEffector.Enable();
                toggleButtonText.text = "Disable Toggle Effect";
            }
        });
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, heightEffectorValue.Evaluate(), transform.position.z);
    }
}
