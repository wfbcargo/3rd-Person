using Assets.Scripts.Bots;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotRoute : MonoBehaviour
{
    public List<Transform> Route;

    private List<RoutePoint> routePoints;
    // Start is called before the first frame update
    void Start()
    {
        routePoints = Route?.Select(x => new RoutePoint(x.position)).ToList() ?? new List<RoutePoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public RoutePoint GetNextPoint()
    {
        if(routePoints == null || routePoints.Count == 0)
        {
            return new RoutePoint(Vector3.zero);
        }

        if(routePoints.All(x => x.HasVisited))
        {
            foreach(var point in routePoints)
            {
                point.ResetPoint();
            }
        }

        var nextPoint = routePoints.FirstOrDefault(x => !x.HasVisited);

        return nextPoint;
    }
}
