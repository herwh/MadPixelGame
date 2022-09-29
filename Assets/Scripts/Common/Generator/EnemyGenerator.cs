using UnityEngine;

namespace Common.Generator
{
    public abstract class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        
        public Enemy Spawn()
        {
            Enemy newEnemy = Instantiate(_enemyPrefab, GetSpawnPosition(), Quaternion.identity);
            return newEnemy;
        }

        protected abstract Vector3 GetSpawnPosition();

    }
}
