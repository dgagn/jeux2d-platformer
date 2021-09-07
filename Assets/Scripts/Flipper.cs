using UnityEngine;

namespace PlatformerTP
{
    public class Flipper : MonoBehaviour
    {
        private Camera _camera;
        public bool flip;
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            var playerTransform = transform;
            var playerRotation = playerTransform.rotation;

            if ((!(mousePos.x < transform.position.x) || flip) &&
                (!(mousePos.x > transform.position.x) || !flip)) return;
            flip = !flip;
            transform.Rotate(new Vector3(0, 180f, 0));
        }
    }
}