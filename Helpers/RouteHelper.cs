namespace dotnet_rideShare.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_rideShare.Interfaces;

public class RouteHelper
{
    private List<CityNode> cities;

    public RouteHelper(List<CityNode> cities)
    {
        this.cities = cities;
    }

    public List<int> FindBestRoute(int departureCityId, int destinationCityId)
    {
        var departureCity = cities.FirstOrDefault(x => x.Id == departureCityId);
        var destinationCity = cities.FirstOrDefault(x => x.Id == destinationCityId);

        if (departureCity == null || destinationCity == null)
        {
            return Enumerable.Empty<CityNode>().ToList();
        }

        // Initialize distances
        foreach (var city in cities)
        {
            city.DistanceFromStart = double.PositiveInfinity;
            city.Previous = null;
        }

        departureCity.DistanceFromStart = 0;

        var unvisitedCities = new List<CityNode>(cities);

        while (unvisitedCities.Any())
        {
            // Find the city with the smallest distance from start
            var currentCity = unvisitedCities.OrderBy(c => c.DistanceFromStart).First();

            // Stop if we reached the destination city
            if (currentCity == destinationCity)
            {
                break;
            }

            unvisitedCities.Remove(currentCity);

            foreach (var neighbor in currentCity.Neighbors)
            {
                var distance = CalculateDistance(currentCity, neighbor);
                var altDistance = currentCity.DistanceFromStart + distance;

                if (altDistance < neighbor.DistanceFromStart)
                {
                    neighbor.DistanceFromStart = altDistance;
                    neighbor.Previous = currentCity;
                }
            }
        }

        // Build the best route
        var bestRoute = new List<int>();
        var currentNode = destinationCity;
        while (currentNode != null)
        {
            bestRoute.Insert(0, currentNode.Id);
            currentNode = currentNode.Previous;
        }

        return bestRoute;
    }

    private double CalculateDistance(CityNode city1, CityNode city2)
    {
        var deltaX = city2.XCoordinate - city1.XCoordinate;
        var deltaY = city2.YCoordinate - city1.YCoordinate;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }
}