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
        HeadingWithProvider();
    }
		
	private void HeadingWithProvider()
	{
	    float heading = FlightComputer.data.provider.Current.Heading;
		headingMechanism.transform.localRotation = Quaternion.Euler(0, heading, 0);
	}
}