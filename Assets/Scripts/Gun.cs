using UnityEngine;

namespace PlatformerTP
{
    public class Gun : MonoBehaviour
    {
        private Camera _camera;

        private void Awake() 
            => _camera = Camera.main;

        private void Update()
        {
            var mousePos = Input.mousePosition;
            var gunPos = _camera.WorldToScreenPoint(transform.position);
            mousePos.x -= gunPos.x;
            mousePos.y -= gunPos.y;

            var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            if (_camera.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
                transform.rotation = Quaternion.Euler(new Vector3(180f, 0, -angle));
            else
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
