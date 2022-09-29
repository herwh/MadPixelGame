using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Common
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private int _health;
        [SerializeField] private float _deathTime;
        [SerializeField] private WeaponController _weaponController;
        [SerializeField] private TargetSearcher _targetSearcher;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _navTarget;

        public Transform Transform => transform;
        public bool IsDead => _currentHealth <= 0;

        private Coroutine _deathCoroutine;
        private int _currentHealth;
        private static readonly int Dying = Animator.StringToHash("Death");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public void SetNavTarget(IDamageable target)
        {
            _navTarget = target.Transform;
        }
        
        public void Punch()
        {
            _weaponController.Shoot();
        }

        public void TakeDamage(int damageAmount)
        {
            _currentHealth -= damageAmount;

            if (_currentHealth <= 0)
            {
                Death();
            }
        }

        private void Start()
        {
            _currentHealth = _health;
        }

        private void Update()
        {
            if (IsDead)
            {
                _navMeshAgent.isStopped = true;
                return;
            }

            if (_navTarget != null)
            {
                _navMeshAgent.isStopped = false;
                _animator.SetBool(IsMoving, true);
                _navMeshAgent.SetDestination(_navTarget.position);
            }

            if (_targetSearcher.HasTarget)
            {
                if (_weaponController.IsWeaponReady())
                {
                    _navMeshAgent.isStopped = true;
                    _animator.SetBool(IsMoving, false);
                    _animator.SetTrigger(Attack);
                }
            }
        }

        private void Death() //to abstract class
        {
            _animator.SetTrigger(Dying);
            _deathCoroutine = StartCoroutine(WaitCoroutine(_deathTime, DestroyObject)); //death animation time
        }

        private void DestroyObject() //to abstract class
        {
            Destroy(gameObject);
        }

        private IEnumerator WaitCoroutine(float waitTime, Action action) //to abstract class
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }
    }
}
