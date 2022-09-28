using UnityEngine;

namespace Common
{
    public abstract class EnemyGenerator : MonoBehaviour
    {
        public Enemy Spawn()
        {
            return null;
        }

        protected abstract Vector3 GetSpawnPosition();

    }
}
