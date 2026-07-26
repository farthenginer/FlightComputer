/*
    25.07.2026 - 00:05 Created by Omer Faruk Simsek
*/

using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    #region Core Variables
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 20f;

    private Vector3 dragOrigin;
    Camera cam;
    
    #endregion

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        DragCamera();
        ZoomCamera();
    }

    private void DragCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            difference.z = 0f; //Camera z clamp
            cam.transform.position += difference;
        }
    }

    private void ZoomCamera()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll == 0)
            return;
        cam.orthographicSize -= scroll * moveSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }
}

