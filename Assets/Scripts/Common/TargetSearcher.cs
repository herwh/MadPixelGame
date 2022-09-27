using UnityEngine;

namespace Common
{
    public class TargetSearcher : MonoBehaviour
    {
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private int _targetMark;
        public bool HasTarget => _targetIsExist && !_currentTarget.IsDead;
        public IDamageable CurrentTarget => _currentTarget;

        private IDamageable _currentTarget;
        private bool _targetIsExist;

        public Vector3 GetDirectionToTarget()
        {
            var toTargetDirection = _currentTarget.Transform.position - transform.position;
            toTargetDirection.y = 0;
            return toTargetDirection.normalized;
        }

        public void UpdateRange(float weaponRange)
        {
            _collider.radius = weaponRange;
        }

        private void Update()
        {
            if (_targetIsExist)
            {
                if (_currentTarget.IsDead)
                {
                    SetTarget(null);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == _targetMark)
            {
                var target = other.GetComponent<IDamageable>();
                if (target == null || _currentTarget == target || HasEnvironmentBetween(target.Transform.position))
                {
                    return;
                }

                if (_currentTarget == null || _currentTarget.IsDead)
                {
                    SetTarget(target);
                    return;
                }

                TryUpdateNearestTarget(target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == _targetMark)
            {
                SetTarget(null);
            }
        }

        private bool HasEnvironmentBetween(Vector3 targetPosition)
        {
            var ownerPosition = transform.position;
            var direction = targetPosition - ownerPosition;
            var layerMask = 1 << 8;

            return Physics.Raycast(ownerPosition, direction.normalized, out var hit, direction.magnitude, layerMask);
        }

        private void SetTarget(IDamageable target)
        {
            _currentTarget = target;
            _targetIsExist = target != null;
        }

        private void TryUpdateNearestTarget(IDamageable target)
        {
            var position = transform.position;
            var distanceToCurrentTarget = Vector3.Distance(position, _currentTarget.Transform.position);
            var distanceToOtherTarget = Vector3.Distance(position, target.Transform.position);

            if (distanceToOtherTarget < distanceToCurrentTarget)
            {
                SetTarget(target);
            }
        }
    }
}