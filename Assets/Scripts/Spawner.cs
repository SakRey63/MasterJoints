using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _poolCapaciti = 15;
    [SerializeField] private int _poolMaxSize = 30;
    [SerializeField] private Projectile _projectile;

    private Transform _spawnPosition;

    private ObjectPool<Projectile> _pool;
    
    private void Awake()
    {
        _pool = new ObjectPool<Projectile>(
            createFunc: () => Instantiate(_projectile),
            actionOnGet: (obj) => SetAction(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapaciti,
            maxSize: _poolMaxSize);
    }

    public Projectile GetProjectile(Transform transform)
    {
        _spawnPosition = transform;

        return _pool.Get();
    }

    public void ReturnToPool(Projectile projectile)
    {
        _pool.Release(projectile);
    }
    
    private void SetAction(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
        projectile.transform.position = _spawnPosition.position;
    }
}
