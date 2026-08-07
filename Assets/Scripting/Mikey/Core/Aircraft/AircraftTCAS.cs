/*
    07.08.2026 - 14:40 Created by Omer Faruk Simsek
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AircraftTCAS : MonoBehaviour
{
    private Dictionary<GuestAircraft, TCASConfiguration.threatLevel> previousThreats = new();
    
    [SerializeField] private List<GuestAircraft> otherAircraftList;
    [SerializeField] private TCASObject tcasObject;

    [SerializeField] private float alertRepeatDelay = 8f;

    private TCASConfiguration.threatLevel currentThreat = TCASConfiguration.threatLevel.noThreat;
    private float lastVoiceTime;

    #region Radar

    public void StartDetection()
    {
        StartCoroutine(DetectionRoutine());
    }

    private void DetectAircrafts()
    {
        TCASConfiguration.threatLevel highestThreat = TCASConfiguration.threatLevel.noThreat;

        List<GuestAircraft> detectedAircrafts = FindObjectsByType<GuestAircraft>(FindObjectsSortMode.None).ToList();
        List<GuestAircraft> inRange = GetAircraftsInRange(detectedAircrafts);

        otherAircraftList = inRange;

        foreach (var aircraft in inRange)
        {
            var level = EvaluateThreatLevel(aircraft);
            var pos = CalculateRadarPosition(aircraft);

            if ((int)level > (int)highestThreat)
                highestThreat = level;

            TCASConfiguration.AircraftInfo info = new TCASConfiguration.AircraftInfo
            {
                altitude = aircraft.Altitude,
                action = TCASConfiguration.actionStatus.none,
                threatLevel = level,
                relativePosition = pos
            };

            TCASIndicatorSystem.Instance.UpdateOrCreateNode(aircraft, info);
        }
        UpdateThreat(highestThreat);

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

        //Debug.Log($"[CALC] worldOffset: {worldOffset}, radarDisplayRadius: {radarDisplayRadius}, radarRange: {tcasObject.radarRange}, scale: {scale}, radarPos: {radarPos}");

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

        if (Mathf.Abs(aircraft.Altitude - 0) > tcasObject.threatAltitudeThreshold) //min threat altitude
            return TCASConfiguration.threatLevel.noThreat;

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
    private void OnDrawGizmos()
    {
        if (tcasObject == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, tcasObject.radarRange);
    }

    #endregion

    #region Voice
    public void UpdateThreat(TCASConfiguration.threatLevel newThreat)
    {
        //pass
        if (newThreat == currentThreat && currentThreat != TCASConfiguration.threatLevel.collisionThreat)
            return;

        //cooldown
        if (Time.time - lastVoiceTime < alertRepeatDelay)
            return;

        var previous = currentThreat;
        currentThreat = newThreat;
        lastVoiceTime = Time.time;

        PlayTransition(previous, newThreat);
    }

    private void PlayTransition(
        TCASConfiguration.threatLevel previous,
        TCASConfiguration.threatLevel current)
    {
        switch (current)
        {
            case TCASConfiguration.threatLevel.proximateThreat:
                AudioEngine.Instance.PlayTCAS(AudioPool.TCAS_Sounds.traffic);
                break;

            case TCASConfiguration.threatLevel.potentialThreat:
                AudioEngine.Instance.PlayTCAS(AudioPool.TCAS_Sounds.climb);
                break;

            case TCASConfiguration.threatLevel.collisionThreat:
                AudioEngine.Instance.PlayTCAS(AudioPool.TCAS_Sounds.climbNow);
                alertRepeatDelay = 1.5f;
                break;

            case TCASConfiguration.threatLevel.noThreat:

                if (previous != TCASConfiguration.threatLevel.noThreat)
                    AudioEngine.Instance.PlayTCAS(AudioPool.TCAS_Sounds.clearOfConflict);
                break;
        }
    }
    #endregion
}
