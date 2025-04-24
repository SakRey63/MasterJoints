using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private KeyCode _swingKey = KeyCode.R;
    
    public event Action OnSwingKeyPressed;

    private void Update()
    {
        if (Input.GetKeyDown(_swingKey))
        {
            OnSwingKeyPressed?.Invoke();
        }
    }
}