/*
    15.07.2026 - 22:24 Created by Omer Faruk Simsek
*/

using UnityEngine;
using UnityEngine.Splines;

public class TurnCoordinatorSystem : MonoBehaviour
{
    #region Core Variables

    [Header("Core Variables")]

    [SerializeField] private GameObject turnedAircraft;

    [SerializeField] private SplineContainer slipSkidSplineContainer;
    [SerializeField] private SplineAnimate slipSkidSplineAnimate;

    [SerializeField] [Range(0, 1)] private float slipSkidSplineTime; // Slid-Skid indicator balance value
                                                                     // 0; the aircraft has banked dangerously to the left.
                                                                     // 0.5; normal value (recommenden)
                                                                     // 1; the aircraft has banked dangerously to the right.

    #endregion

    private void Update()
    {
        Vector2 axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        AircraftRotation(axis.x);

        SlipSkidIndicator(slipSkidSplineTime);
    }

    // Aircraft Z rotation will be between 15 and 35 degrees.

    float smoothRoll;
    [SerializeField] float rotationSpeed = 100f;

    float currentRoll;
    [SerializeField] float rollSpeed = 80f;

    private void AircraftRotation(float horizontal)
    {
        bool turnRateIsDanger = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift); //Demo

        smoothRoll = Mathf.Lerp(smoothRoll, horizontal, 8f * Time.deltaTime);

        if (horizontal != 0)
        {
            if (turnRateIsDanger)
            {
                currentRoll = Mathf.Lerp(currentRoll, horizontal > 0 ? 35 : -35, 5 * Time.deltaTime);
            }
            else
            {
                currentRoll = Mathf.Lerp(currentRoll, horizontal > 0 ? 15 : -15, 5 * Time.deltaTime);
            }
        }
        else
        {
            currentRoll = Mathf.Lerp(currentRoll, 0, 5 * Time.deltaTime);
        }
        turnedAircraft.transform.localRotation = Quaternion.Euler(90,0,-currentRoll);
    }

    [SerializeField] float slipSkidSmoothValue;
    private void SlipSkidIndicator(float value)
    {
        slipSkidSmoothValue = Mathf.Lerp(slipSkidSmoothValue, value, 2 * Time.deltaTime); //Apply the value fluently
        slipSkidSplineAnimate.Container = slipSkidSplineContainer;
        slipSkidSplineAnimate.NormalizedTime = slipSkidSmoothValue;
    }
}
