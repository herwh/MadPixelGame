using System;
using System.Collections;
using UnityEngine;

namespace Common
{
    public class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _characterModel;
        [SerializeField] private InputController _inputController;
        [SerializeField] private WeaponController _weaponController;
        [SerializeField] private TargetSearcher _targetSearcher;
        [SerializeField] private int _health;
        [SerializeField] private float _deathTime;
        
        public Transform Transform => transform;
        public bool IsDead => _currentHealth <= 0;

        private Coroutine _deathCoroutine;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Dying = Animator.StringToHash("Death");
        private float _xMovement;
        private float _yMovement;
        private bool _isIdling = true;
        private Vector3 _direction;
        private int _currentHealth;
       

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

        private void FixedUpdate()
        {
            if (IsDead)
            {
                return;
            }
            
            _rigidbody.MovePosition(transform.position + _direction * _speed * Time.deltaTime);
        }

        private void Update()
        {
            if (IsDead)
            {
                return;
            }
            
            var axis = _inputController.GetMoveAxis();

            _direction = new Vector3(_xMovement, 0, _yMovement);
            _xMovement = axis.x;
            _yMovement = axis.y;
            _isIdling = _xMovement == 0 && _yMovement == 0;

            _animator.SetBool(IsMoving, !_isIdling);

            if (!_targetSearcher.HasTarget)
            {
                if (!_isIdling)
                {
                    RotateByDirection(_direction.normalized);
                }
            }
            else
            {
                RotateByDirection(_targetSearcher.GetDirectionToTarget());
                if (_weaponController.IsWeaponReady()) 
                {
                    _weaponController.Shoot();
                }
            }
        }

        private void RotateByDirection(Vector3 direction)
        {
            var rotation = direction==Vector3.zero ?
                Quaternion.identity : 
                Quaternion.LookRotation(direction, Vector3.up);
            
            var smoothRotation =
                Quaternion.Slerp(_characterModel.rotation, rotation, _rotationSpeed * Time.deltaTime);
            _characterModel.rotation = smoothRotation;
        }
        
        private void Death()//to abstract class
        {
            _animator.SetTrigger(Dying);
            _deathCoroutine = StartCoroutine(WaitCoroutine(_deathTime, DestroyObject)); //death animation time
        }

        private void DestroyObject()//to abstract class
        {
            Destroy(gameObject);
        }
        private IEnumerator WaitCoroutine(float waitTime, Action action)//to abstract class
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }
        
    }
}
