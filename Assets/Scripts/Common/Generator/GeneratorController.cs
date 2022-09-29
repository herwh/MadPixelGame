using System.Collections.Generic;
using UnityEngine;

namespace Common.Generator
{
    public class GeneratorController : MonoBehaviour
    {
        [SerializeField] private List<EnemyGenerator> _generators;
        [SerializeField] private int _maxEnemies;
        [SerializeField] private Character _target;

        private void Start()
        {
            for (int i = 0; i < _maxEnemies; i++)
            {
                var generator = _generators[i % _generators.Count];
                var newEnemy = generator.Spawn();
                newEnemy.SetNavTarget(_target);
            }
        }
    }
}