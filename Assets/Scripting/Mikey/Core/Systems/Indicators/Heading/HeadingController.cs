/*
    14.07.2026 - 22:30 Created by Omer Faruk Simsek
 */

using UnityEngine;

public class HeadingController : MonoBehaviour
{
    #region Core Variables
    
    [Header("Core Variables")]

    [SerializeField] private GameObject headingMechanism;

    [SerializeField] private float rotationSpeed = 100;

    #endregion

    private void Update()
    {
        HeadingWithMouse();
    }
    //Demo
    float currentZ;
    private void HeadingWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        currentZ -= mouseX * rotationSpeed * Time.deltaTime;
        headingMechanism.transform.localRotation = Quaternion.Euler(0, currentZ, 0);
    }
}