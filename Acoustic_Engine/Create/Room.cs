﻿using BH.oM.Acoustic;
using BH.oM.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace BH.Engine.Acoustic
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Room Room(List<Point> points, double area, double volume)
        {
            return new Room()
            {
                Area = area,
                Volume = volume,
                Samples = points.Select(x => Create.Receiver(x)).ToList()
            };
        }

        /***************************************************/

        public static Room Room(List<Receiver> receivers, double area, double volume)
        {
            return new Room()
            {
                Area = area,
                Volume = volume,
                Samples = receivers
            };
        }

        /***************************************************/
    }
}