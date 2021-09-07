using System;
using UnityEngine;

namespace PlatformerTP
{
    public class CameraGestion : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private Vector3 offsets;
        [SerializeField] private float smoother = 3f;
        private Flipper _flipper;
        private bool _isFlip;
        private void Awake()
        {
            _flipper = FindObjectOfType<Flipper>();
        }

        private void LateUpdate()
        {
            if (_flipper.flip && !_isFlip)
            {
                _isFlip = true;
                offsets = new Vector2(offsets.x - 2, offsets.y);
            }
            else if (!_flipper.flip && _isFlip)
            {
                _isFlip = false;
                offsets = new Vector2(offsets.x + 2, offsets.y);
            }
            
            var position = target.position + offsets;
            var smooth = Vector3.Lerp(transform.position, position, smoother * Time.deltaTime);
            
            transform.position = smooth;
        }
    }
}