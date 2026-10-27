using System.Collections.Generic;
using UnityEngine;

public interface IFlightDataProvider
{
    FlightData Current { get; }
}

public struct FlightData
{
    public float Pitch, Roll, Heading;
	public float AttitudeMeters;
	public float AirSpeedMS, VerticalSpeedMS;
	
	public float AngleOfAttack, GForce;
	public float TurnRate, Slip;
	
	public Vector3 MainPosition;
	public List<AircraftData> Nodes;
	
	//public bool GearDown;
	
	//public float Flaps;
}

public struct AircraftData
{
    public Vector3 MainPosition;
    public float altitudeOffset;
    public GameManager.directions direction;
}