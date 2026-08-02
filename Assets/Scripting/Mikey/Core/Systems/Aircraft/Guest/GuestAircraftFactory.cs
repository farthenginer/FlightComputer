/*
    27.07.2026 - 16:43 Created by Omer Faruk Simsek
*/

using UnityEngine;

public class GuestAircraftFactory : MonoBehaviour
{
    [SerializeField] private GameObject aircraftPrefab;

    public GuestAircraft Spawn(GuestAircraftInitData data, Vector2 position)
    {
        GuestAircraft aircraft = Instantiate(aircraftPrefab, position, CalculateRotation(data)).GetComponent<GuestAircraft>(); //instantiate and casting

        aircraft.Initialize(data); //init
        return aircraft;
    }

    public Quaternion CalculateRotation(GuestAircraftInitData data)
    {
        int ZRotation = 0; //default Z

        switch (data.direction)
        {
            case GameManager.directions.south:
                ZRotation = 180;
                break;
            case GameManager.directions.north:
                ZRotation = 0;
                break;
            case GameManager.directions.west:
                ZRotation = 90;
                break;
            case GameManager.directions.east:
                ZRotation = 270;
                break;
            case GameManager.directions.southwest:
                ZRotation = 135;
                break;
            case GameManager.directions.northwest:
                ZRotation = 45;
                break;
            case GameManager.directions.southeast:
                ZRotation = 225;
                break;
            case GameManager.directions.northeast:
                ZRotation = 315;
                break;
        }
        return Quaternion.Euler(0, 0, ZRotation);
    }
}
