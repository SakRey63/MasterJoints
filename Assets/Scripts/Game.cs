using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Swing _swing;
    [SerializeField] private Catapulta _catapulta;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _inputReader.OnSwingKeyPressed += _swing.StartSwingMotor;
        _inputReader.OnCatapultLoadKeyPressed += _catapulta.ChargeSpoon;
        _inputReader.OnCatapultLaunchKeyPressed += _catapulta.Attack;
        _catapulta.OnCharged += SpawnProjectile;
    }

    private void OnDisable()
    {
         _inputReader.OnSwingKeyPressed -= _swing.StartSwingMotor;
         _inputReader.OnCatapultLoadKeyPressed -= _catapulta.ChargeSpoon;
         _inputReader.OnCatapultLaunchKeyPressed -= _catapulta.Attack;
         _catapulta.OnCharged -= SpawnProjectile;
    }

    private void SpawnProjectile()
    {
        Projectile projectile = _spawner.GetProjectile(_catapulta.ProjectileHolder);
        
        projectile.BeginTimedReturn += ProjectileOnBeginTimedReturn;

        projectile.transform.parent = _catapulta.Spoon;
        
        _catapulta.SetProjectile(projectile);
    }

    private void ProjectileOnBeginTimedReturn(Projectile projectile)
    {
        projectile.BeginTimedReturn -= ProjectileOnBeginTimedReturn;
        
        _spawner.ReturnToPool(projectile);
    }
}