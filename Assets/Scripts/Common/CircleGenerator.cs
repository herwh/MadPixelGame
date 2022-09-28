using UnityEngine;
using Random = UnityEngine.Random;

namespace Common
{
    public class CircleGenerator : EnemyGenerator
    {
        [SerializeField] private float _radius;

        protected override Vector3 GetSpawnPosition()
        {
            var randomPointInCircle = Random.insideUnitCircle * _radius;
            var spawnPoint = new Vector3(randomPointInCircle.x, 0, randomPointInCircle.y) + transform.position;
            return spawnPoint;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color=Color.red;
            Gizmos.DrawWireSphere(transform.position,_radius);
        }
    }
}