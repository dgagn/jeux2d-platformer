using System;
using UnityEngine;

namespace PlatformerTP
{
    public class Crosshair: MonoBehaviour
    {
        public GameObject crosshair;
        private Vector3 _positionSouris;
        private Camera _camera;
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            _positionSouris = _camera.ScreenToWorldPoint(Input.mousePosition);
            crosshair.transform.position = new Vector3(_positionSouris.x, _positionSouris.y);
            Cursor.visible = Time.timeScale == 0;
        }
    }
}