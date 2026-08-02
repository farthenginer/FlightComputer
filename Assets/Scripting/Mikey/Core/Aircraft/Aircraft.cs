/*
    28.07.2026 - 16:59 Created by Omer Faruk Simsek
*/

//Aircraft Core Script from Mikey

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
    #region Core

    private void Start()
    {
        StartCoroutine(DetectionRoutine()); //Start TCAS detection.
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    #endregion

    #region TCAS System

    [SerializeField] private List<GuestAircraft> otherAircraftList;
    [SerializeField] private TCASObject tcasObject;

    private void DetectAircrafts()
    {
        List<GuestAircraft> detectedAircrafts = FindObjectsByType<GuestAircraft>(FindObjectsSortMode.None).ToList();
        List<GuestAircraft> TCASThreats = GetCollisionThreats(detectedAircrafts);

        otherAircraftList = detectedAircrafts;
    }
    private List<GuestAircraft> GetCollisionThreats(List<GuestAircraft> _list)
    {
        List<GuestAircraft> result = new List<GuestAircraft>();
        foreach (var aircraft in _list)
        {
            // used vector3 for 2D & 3D
            float distance = Vector3.Distance(transform.position, aircraft.transform.position);
            
            if (distance <= tcasObject.radarRange)
            {
                result.Add(aircraft);
            }
        }

        Debug.Log($"G_Aircraft Results: {result.Count}");
        return result;
    }

    private IEnumerator DetectionRoutine()
    {
        while (true)
        {
            DetectAircrafts();
            yield return tcasObject.radarScanRate;
        }
    }

    #endregion
}

