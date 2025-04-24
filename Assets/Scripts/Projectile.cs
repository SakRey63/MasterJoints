using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _delay;

    public event Action<Projectile> BeginTimedReturn;

    public void ChangeKinematic()
    {
        _rigidbody.isKinematic = true;
    }

    public void InitiateImpulseAndDetonationSequence(float lanchForse)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.left * lanchForse, ForceMode.Impulse);
            
        StartCoroutine(ActivateTimedRecall());
    }

    private IEnumerator ActivateTimedRecall()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        yield return delay;
        
        BeginTimedReturn?.Invoke(this);
    }
}
