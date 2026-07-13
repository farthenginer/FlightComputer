/*
    13.07.2026 - 11:34 Created by Omer Faruk Simsek
 */

//The camera can cover a maximum angle of 110 degrees and a minimum angle of 65 degrees along the X-Axis
//The camera can cover a maximum angle of 80 degrees and minimun angle of -80 degrees along the Z-Axis

using UnityEngine;

public class AttitudeController : MonoBehaviour
{
    #region Core Variables
    [Header("Core Variables")]
    //private InputController input;
    #endregion

    #region Camera Variables

    const int maxX = 110;
    const int minX = 65;

    const int maxY = 80;
    const int minY = -80;

    [Header("Camera Configuration")]

    [SerializeField] private Camera attitudeCamera;

    [SerializeField][Range(0, 5)] private float cameraSmoothValue;

    #endregion

    void Update()
    {
        Vector2 axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }
}
