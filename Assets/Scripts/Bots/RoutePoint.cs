using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Bots
{
    public class RoutePoint
    {
        public Vector3 Position { get; set; }
        public bool HasVisited { get; set; }
        public RoutePoint(Vector3 position)
        {
            Position = position;
        }

        public void ResetPoint()
        {
            HasVisited = false;
        }

        public void VisitPoint()
        {
            HasVisited = true;
        }
    }
}
