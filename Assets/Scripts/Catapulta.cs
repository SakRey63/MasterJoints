using System;
using System.Collections;
using UnityEngine;

public class Catapulta : MonoBehaviour
{
    [SerializeField] private SpringJoint _springJoint;
    [SerializeField] private HingeJoint _hingeJoint;
    [SerializeField] private Transform _projectileHolder;
    [SerializeField] private Transform _spoon;
    [SerializeField] private float _loadAngle = -85f;
    [SerializeField] private float _reloadAngle = 2f;
    [SerializeField] private float _hingeForce = 20f;
    [SerializeField] private float _launchForce = 10f;
    [SerializeField] private float _springForse = 1000f;
    
    private float _resetForce;
    private JointMotor _hingeMotor;
    private Projectile _projectile;
    private bool _isLoaded;
    private bool _isClicked;

    public event Action OnCharged;

    public Transform Spoon => _spoon;
    public Transform ProjectileHolder => _projectileHolder;

    public void SetProjectile(Projectile projectile)
    {
        _projectile = projectile;
        
        _projectile.ChangeKinematic();
    }
    
    public void Attack()
    {
        if (_isLoaded && _isClicked == false)
        {
            _isClicked = true;

            StartCoroutine(LaunchAttack());
        }
    }
    
    public void ChargeSpoon()
    {
        if (_isLoaded == false && _isClicked == false)
        {
            SetSpringForce(_hingeForce, _springForse);
            
            _hingeJoint.useMotor = true;
            
            _springJoint.spring = _resetForce;

            _isClicked = true;
                    
            StartCoroutine(Charge());
        }
    }

    private IEnumerator LaunchAttack()
    {
        _hingeJoint.useMotor = false;
        
        _projectile.InitiateImpulseAndDetonationSequence(_launchForce);
        
        _projectile.transform.parent = null;

        _projectile = null;
            
        _springJoint.spring = _springForse;
        
        while (_hingeJoint.angle < _loadAngle)
        {
            yield return null;
        }
        
        _isLoaded = false;

        _isClicked = false;
    }

    private IEnumerator Charge()
    {
        while (_hingeJoint.angle > _loadAngle)
        {
            yield return null;
        }

        SetSpringForce(_resetForce, _resetForce);

        _isLoaded = true;

        _isClicked = false;
        
        OnCharged?.Invoke();
    }
    
    private void SetSpringForce(float targetVelocity, float forse)
    {
        _hingeMotor = _hingeJoint.motor;
        _hingeMotor.targetVelocity = targetVelocity;
        _hingeMotor.force = forse;
        
        _hingeJoint.motor = _hingeMotor;
    }
}