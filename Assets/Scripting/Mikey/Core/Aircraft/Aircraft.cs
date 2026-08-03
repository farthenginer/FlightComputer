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
        List<GuestAircraft> inRange = GetAircraftsInRange(detectedAircrafts);

        otherAircraftList = inRange;

        foreach (var aircraft in inRange)
        {
            var level = EvaluateThreatLevel(aircraft);
            var pos = CalculateRadarPosition(aircraft);

            TCASConfiguration.AircraftInfo info = new TCASConfiguration.AircraftInfo
            {
                altitude = aircraft.Altitude,
                action = TCASConfiguration.actionStatus.none,
                threatLevel = level,
                relativePosition = pos
            };

            TCASIndicatorSystem.Instance.UpdateOrCreateNode(aircraft, info);
        }

        // Clean
        TCASIndicatorSystem.Instance.RemoveStaleNodes(inRange);
    }

    [SerializeField] private float radarDisplayRadius = 0.22f;

    private Vector2 CalculateRadarPosition(GuestAircraft aircraft)
    {
        Vector3 worldOffset = aircraft.transform.position - transform.position;
        Vector3 localOffset = transform.InverseTransformDirection(worldOffset);

        float scale = radarDisplayRadius / tcasObject.radarRange;
        Vector2 radarPos = new Vector2(localOffset.x, localOffset.y) * scale;

        Debug.Log($"[CALC] worldOffset: {worldOffset}, radarDisplayRadius: {radarDisplayRadius}, radarRange: {tcasObject.radarRange}, scale: {scale}, radarPos: {radarPos}");

        return radarPos;
    }

    private List<GuestAircraft> GetAircraftsInRange(List<GuestAircraft> _list)
    {
        List<GuestAircraft> result = new List<GuestAircraft>();
        foreach (var aircraft in _list)
        {
            float distance = Vector3.Distance(transform.position, aircraft.transform.position);
            if (distance <= tcasObject.radarRange)
                result.Add(aircraft);
        }
        return result;
    }

    private TCASConfiguration.threatLevel EvaluateThreatLevel(GuestAircraft aircraft)
    {
        float distance = Vector3.Distance(transform.position, aircraft.transform.position);

        if (distance <= tcasObject.collisionThreshold)
            return TCASConfiguration.threatLevel.collisionThreat;
        if (distance <= tcasObject.potentialThreshold)
            return TCASConfiguration.threatLevel.potentialThreat;
        if (distance <= tcasObject.proximateThreshold)
            return TCASConfiguration.threatLevel.proximateThreat;

        return TCASConfiguration.threatLevel.noThreat;
    }

    private IEnumerator DetectionRoutine()
    {
        while (true)
        {
            DetectAircrafts();
            yield return new WaitForSeconds(tcasObject.radarScanRate);
        }
    }

    // Radar giz
    private void OnDrawGizmosSelected()
    {
        if (tcasObject == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, tcasObject.radarRange);
    }
    #endregion
}

