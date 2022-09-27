using UnityEngine;

namespace Common
{
    public interface IDamageable
    {
        Transform Transform { get; }
        bool IsDead { get; }
        void TakeDamage(int damageAmount);
        
    }
}