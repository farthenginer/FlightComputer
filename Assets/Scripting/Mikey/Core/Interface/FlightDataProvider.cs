public class FlightDataProvider : IFlightDataProvider
{
    public FlightData Current { get; private set; }

    public FlightDataProvider()
    {
        Current = new FlightData
        {
            Pitch = .5f,
            Roll = .2f,
            Heading = 180f
        };
    }
}