using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Examples.State
{
    [RequireComponent(typeof(AudioSource))]
    public class TargetAssigner : MonoBehaviour
    {
        public event Action<Vector3> NewTargetAcquired = delegate { };
 
        [SerializeField] GameObject _targetIndicatorPrefab = null;
        GameObject _targetIndicator;

        Camera _camera = null;
        RaycastHit _hitInfo;

        private void Awake()
        {
            // get references
            _camera = Camera.main;
            // setup the target indicator visual and hide it
            _targetIndicator = Instantiate(_targetIndicatorPrefab, _hitInfo.point, Quaternion.identity);
            _targetIndicator.SetActive(false);
        }

        void Update()
        {
            //set new target
            if (Input.GetMouseButtonDown(0))
            {
                GetNewMouseHit(_camera);
                SetNewTargetPoint(_hitInfo.point);
            }
        }

        public void GetNewMouseHit(Camera camera)
        {
            // get a ray hit point from camera click location
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hitInfo, Mathf.Infinity))
            {
                Debug.Log("Ray hit: " + _hitInfo.transform.name
                    + " at coordinates: " + _hitInfo.point);
            }
        }

        public void SetNewTargetPoint(Vector3 position)
        {
            // handle the target visual
            _targetIndicator.SetActive(true);
            _targetIndicator.transform.position = position;
 
            // send notification
            NewTargetAcquired.Invoke(position);
        }
    }
}

