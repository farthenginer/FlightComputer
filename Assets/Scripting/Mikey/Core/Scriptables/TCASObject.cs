using UnityEngine;

[CreateAssetMenu(fileName = "TCAS", menuName = "Aircraft Configuration/TCAS")]
public class TCASObject : ScriptableObject
{
    //Variables
    public float radarRange;
    public float radarScanRate;

    //Thresholds
    public float ThreatThreshold;
    public float proximateThreshold;
    public float potentialThreshold;
    public float collisionThreshold;
}