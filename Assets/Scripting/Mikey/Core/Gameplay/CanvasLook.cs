/*
    28.07.2026 - 15:52 Created by Omer Faruk Simsek
*/

using TMPro;
using UnityEngine;

public class CanvasLook : MonoBehaviour
{
    Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
        GetComponent<Canvas>().worldCamera = _camera;

        MonitorData();
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        transform.rotation = Quaternion.Euler(0, 0, -(transform.root.rotation.z));  
    }    

    private void MonitorData()
    {
        Transform _panel = transform.GetChild(0);
        //labels
        TMP_Text nameLabel = _panel.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text altitudeLabel = _panel.transform.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text differenceLabel = _panel.transform.GetChild(2).GetComponent<TMP_Text>();
        TMP_Text directionLabel = _panel.transform.GetChild(3).GetComponent<TMP_Text>();

        //variables
        GuestAircraftInitData data = transform.root.GetComponent<GuestAircraft>()._data;

        string name = data.aircraftName;
        string direction = data.direction.ToString();

        int altitude = (int)data.altitudeOffset;
        int difference = 5000 - altitude; //demo variable.

        //set
        nameLabel.text = $"Name: {name}";
        directionLabel.text = $"Direction: {direction}";

        altitudeLabel.text = $"Altitude: {altitude}";
        differenceLabel.text = $"Difference: {difference}";
    }
}
