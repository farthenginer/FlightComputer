using UnityEngine;

public class FlightComputer : MonoBehaviour
{
    public static FlightComputer data;
	
	public IFlightDataProvider provider;
	
	void Awake()
	{
	    if(data == null)
		{
		    data = this;
		}
	}
	
	void Start()
	{
	    Initialize();
	}
	
	void Initialize()
	{
		provider = new FlightDataProvider();
	}
}
