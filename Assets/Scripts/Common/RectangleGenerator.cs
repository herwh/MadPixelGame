using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common
{
    public class RectangleGenerator : EnemyGenerator
    {
        [SerializeField] private float _width;
        [SerializeField] private float _height;
        
        private Vector3 Position => transform.position;
        
        protected override Vector3 GetSpawnPosition()
        {
            var randomPointWidth = Random.Range(Position.x, Position.x+_width);
            var randomPointHeight = Random.Range(Position.y, Position.y+_height);
            var spawnPoint = new Vector3(randomPointWidth, 0, randomPointHeight);
            return spawnPoint;
        }

        private void OnDrawGizmos()
        {
            var leftBottom = new Vector3(Position.x, 0, Position.z);
            var rightBottom = new Vector3(Position.x + _width, 0, Position.z);
            var leftTop = new Vector3(Position.x, 0, Position.z+_height);
            var rightTop = new Vector3(Position.x + _width, 0, Position.z + _height);
            
            Gizmos.color=Color.red;
            Gizmos.DrawLine(leftBottom, rightBottom);
            Gizmos.DrawLine(leftTop,rightTop);
            Gizmos.DrawLine(leftBottom,leftTop);
            Gizmos.DrawLine(rightBottom,rightTop);
        }
    }
}