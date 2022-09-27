using UnityEngine;

namespace Common
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        private Vector3 _offset;
        
        void Start()
        {
            _offset = _target.transform.position - transform.position;
            transform.LookAt(_target.transform);
        }

        private void LateUpdate()
        {
            if (_target != null)
            {
                transform.position = _target.transform.position -  _offset;
            }
        }
    }
}
