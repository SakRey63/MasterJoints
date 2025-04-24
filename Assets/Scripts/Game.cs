using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Swing _swing;

    private void OnEnable()
    {
        _inputReader.OnSwingKeyPressed += _swing.StartSwingMotor;
    }

    private void OnDisable()
    {
         _inputReader.OnSwingKeyPressed -= _swing.StartSwingMotor;
    }
}