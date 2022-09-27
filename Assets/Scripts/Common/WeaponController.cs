using Data;
using UnityEngine;

namespace Common
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private Transform _weaponGrab;
        [SerializeField] private TargetSearcher _targetSearcher;
        
        private float _lastTimeShoot;

        public bool IsWeaponReady()
        {
            return Time.time - _lastTimeShoot >= _weaponData.ShotsPerSec;
        }
        
        public void Shoot()
        {
            if (_targetSearcher.CurrentTarget != null)
            {
                Fire(_targetSearcher.CurrentTarget);
                _lastTimeShoot = Time.time;
            }
        }
        
        private void CreateWeapon(WeaponData weaponData)
        {
            if (_weaponGrab == null)
            {
                Debug.LogError("Character has no grab for weapon"); 
                return;
            }
            
            if (weaponData.Prefab != null)
            {
                Instantiate(weaponData.Prefab, _weaponGrab);
            }
            
            _targetSearcher.UpdateRange(weaponData.ShotRange);
        }

        private void Start()
        {
            _lastTimeShoot = Time.time;
            CreateWeapon(_weaponData);
        }

        private void Fire(IDamageable target)
        {
            target.TakeDamage(_weaponData.Damage);
        }
    }
}