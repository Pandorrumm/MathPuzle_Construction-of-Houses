using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathPuzzle
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera = null;

        [Space]
        [SerializeField] private Button zoomInButton = null;
        [SerializeField] private Button zoomOutButton = null;

        [Space]
        [SerializeField] private float zoomStep = 0f;
        [SerializeField] private float minCameraSize = 0f;
        [SerializeField] private float maxCameraSize = 0f;
       
        [Space]
        [SerializeField] private SpriteRenderer mapRenderer = null;

        private Vector3 dragOrigin;

        private float mapMinX;
        private float mapMaxX;
        private float mapMinY;
        private float mapMaxY;

        private void Awake()
        {
            mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
            mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

            mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
            mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;

            zoomInButton.onClick.AddListener(ZoomIn);
            zoomOutButton.onClick.AddListener(ZoomOut);
        }

        private void Update()
        {
            PanCamera();
        }

        private void PanCamera()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
           
            if (Input.GetMouseButton(0))
            {
                Vector3 difference = dragOrigin - mainCamera.ScreenToWorldPoint(Input.mousePosition);

                mainCamera.transform.position = ClampCamera(mainCamera.transform.position + difference);
            }
        }

        public void ZoomIn()
        {
            float newSize = mainCamera.orthographicSize - zoomStep;
            mainCamera.orthographicSize = Mathf.Clamp(newSize, minCameraSize, maxCameraSize);

            mainCamera.transform.position = ClampCamera(mainCamera.transform.position);
        }

        public void ZoomOut()
        {
            float newSize = mainCamera.orthographicSize + zoomStep;
            mainCamera.orthographicSize = Mathf.Clamp(newSize, minCameraSize, maxCameraSize);

            mainCamera.transform.position = ClampCamera(mainCamera.transform.position);
        }

        private Vector3 ClampCamera(Vector3 _tagetPosition)
        {
            float cameraHeight = mainCamera.orthographicSize;
            float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;

            float minX = mapMinX + cameraWidth;
            float maxX = mapMaxX - cameraWidth;

            float minY = mapMinY + cameraHeight;
            float maxY = mapMaxY - cameraHeight;

            float newX = Mathf.Clamp(_tagetPosition.x, minX, maxX);
            float newY = Mathf.Clamp(_tagetPosition.y, minY, maxY);

            return new Vector3(newX, newY, _tagetPosition.z);
        }
    }
}
