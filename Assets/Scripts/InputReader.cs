using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private KeyCode _swingKey = KeyCode.R;
    private KeyCode _catapultLoadKey = KeyCode.W; 
    private KeyCode _catapultLaunchKey = KeyCode.S;
    
    public event Action OnSwingKeyPressed;
    public event Action OnCatapultLoadKeyPressed;
    public event Action OnCatapultLaunchKeyPressed;

    private void Update()
    {
        if (Input.GetKeyDown(_swingKey))
        {
            OnSwingKeyPressed?.Invoke();
        }
        
        if (Input.GetKeyDown(_catapultLoadKey))
        {
            OnCatapultLoadKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(_catapultLaunchKey))
        {
            OnCatapultLaunchKeyPressed?.Invoke();
        }
    }
}