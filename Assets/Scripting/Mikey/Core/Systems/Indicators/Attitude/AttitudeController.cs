/*
    13.07.2026 - 11:34 Created by Omer Faruk Simsek
 */


using UnityEngine;

public class AttitudeController : MonoBehaviour
{
    #region Core Variables

    [Header("Core Variables")]
    //private InputController input;
    
    [SerializeField] private GameObject attitudeRotationMechanism;
    [SerializeField] private GameObject attitudeVerticalMechanism;

    #endregion


    void Update()
    {
        Vector2 axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        RotationMechanism(axis);
        VerticalMechanism(axis);
    }

    //Minimum Z position: -.6f;
    //Maximum Z position: .6f;

    float smoothRoll;
    [SerializeField] float rotationSpeed = 100f;

    float currentRoll;
    [SerializeField] float rollSpeed = 80f;

    void RotationMechanism(Vector2 axis)
    {
        smoothRoll = Mathf.Lerp(smoothRoll, axis.x, 8f * Time.deltaTime);

        currentRoll += smoothRoll * rollSpeed * Time.deltaTime;

        attitudeRotationMechanism.transform.localRotation = Quaternion.Euler(0, -currentRoll, 0);
    }

    float smoothInput;

    float currentPitch;
    [SerializeField] float pitchSpeed = 0.5f;
    void VerticalMechanism(Vector2 axis)
    {
        smoothInput = Mathf.Lerp(smoothInput, axis.y, 8f * Time.deltaTime);

        currentPitch += smoothInput * pitchSpeed * Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch, -0.6f, 0.6f);

        attitudeVerticalMechanism.transform.localPosition = Vector3.forward * currentPitch;
    }
}

