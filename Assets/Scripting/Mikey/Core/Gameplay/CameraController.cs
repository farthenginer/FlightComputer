/*
    25.07.2026 - 00:05 Created by Omer Faruk Simsek
*/

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private void Update()
    {
        if (!Input.GetMouseButton(2))
            return;

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 movement = new Vector3(x, y, 0);
        transform.position += -movement * _movementSpeed * Time.deltaTime;
    }
}
