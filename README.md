# Effector Values

Animate float values and combine them into a single output that can be altered at runtime.

## Features

- Combine multiple animated inputs (Effectors) into a unified output (Effector Value).
- Can edit individual animation parameters during runtime, including:
    - Duration
    - Easing
    - Amplitude
    - Period
- Compatible with Unity 2022.3.44f1 and later.

## Example Implementation

![Demo](./GithubResources/Demo1.gif)

```csharp
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
```

## Dependencies

- Relies on Demigant's DOTween package

## Installation

### Using Unity Package Manager

1. Go to **Window > Package Manager**.
2. Install [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676#description) and follow the installation popups (no extra libraries necessary)
3. Click the **+** button in the top-left corner.
4. Select **Add package from git URL...**.
5. Enter the following URL: https://github.com/ElliottHood/Effector-Values.git