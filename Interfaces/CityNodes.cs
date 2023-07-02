namespace dotnet_rideShare.Interfaces;
public class CityNode
{
    public int Id { get; set; }
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public List<CityNode> Neighbors { get; set; } = new List<CityNode>();
    public double DistanceFromStart { get; set; } = double.PositiveInfinity;
    public CityNode? Previous { get; set; }
}