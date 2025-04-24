using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HingeJoint))]
public class Swing : MonoBehaviour
{
    [SerializeField] private float _motorSpeed = 300;
    [SerializeField] private float _maxMotorForce = 300;
    [SerializeField] private float _delay = 0.1f;
    [SerializeField] private HingeJoint _hingeJoint;
    
    private const float _invertSpeedFactor = -1f;
    private JointMotor _motor;

    private void Start()
    {
        _motor = _hingeJoint.motor;
        _motor.force = _maxMotorForce;
        
        _hingeJoint.motor = _motor;
        _hingeJoint.useMotor = false; 
    }

    public void StartSwingMotor()
    {
        _motor.targetVelocity = _motorSpeed;
        
        _hingeJoint.motor = _motor;
        
        _hingeJoint.useMotor = true;
        
        StartCoroutine(StopMotorAfterDelay(_delay));
        
        _motorSpeed *= _invertSpeedFactor;
    }

    private IEnumerator StopMotorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _hingeJoint.useMotor = false;
    }
}