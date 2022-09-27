using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapon Data")]

    public class WeaponData : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _damage;
        [SerializeField] private float _shotsPerSec;
        [SerializeField] private float _shotRange;

        public GameObject Prefab => _prefab;
        public int Damage => _damage;
        public float ShotsPerSec => _shotsPerSec;
        public float ShotRange => _shotRange;
    }
}