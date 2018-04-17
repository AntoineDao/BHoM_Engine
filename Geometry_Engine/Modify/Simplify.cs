﻿using BH.oM.Geometry;
using System.Collections.Generic;

namespace BH.Engine.Geometry
{
    public static partial class Modify
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Polyline Simplify(this Polyline polyline, double distanceTolerance = Tolerance.Distance, double angleTolerance = Tolerance.Angle)
        {
            bool isClosed = polyline.IsClosed(distanceTolerance);
            List<Point> ctrlPts = polyline.DiscontinuityPoints(distanceTolerance, angleTolerance);
            List<Point> newPts = new List<Point>(ctrlPts);
            for (int i = 1; i < ctrlPts.Count - 1; i++)
            {
                List<Point> checkPoints = new List<Point>() { ctrlPts[i - 1], ctrlPts[i] }.CullDuplicates(distanceTolerance);
                if (checkPoints.Count == 1)
                {
                    ctrlPts.RemoveRange(i - 1, 2);
                    ctrlPts.Insert(i - 1, checkPoints[0]);
                    i--;
                }
            }
            if (ctrlPts.Count < 2) return polyline;
            return new Polyline { ControlPoints = ctrlPts };
        }
    }
}