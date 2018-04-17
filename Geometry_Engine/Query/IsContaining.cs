﻿using BH.oM.Geometry;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BH.Engine.Geometry
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods - BoundingBox              ****/
        /***************************************************/

        public static bool IsContaining(this BoundingBox box1, BoundingBox box2)
        {
            return (box1.Min.X <= box2.Min.X && box1.Min.Y <= box2.Min.Y && box1.Min.Z <= box2.Min.Z && box1.Max.X >= box2.Max.X && box1.Max.Y >= box2.Max.Y && box1.Max.Z >= box2.Max.Z);
        }

        /***************************************************/

        public static bool IIsContaining(this BoundingBox box, Point pt)
        {
            Point max = box.Max;
            Point min = box.Min;

            return (pt.X <= max.X && pt.X >= min.X && pt.Y <= max.Y && pt.Y >= min.Y && pt.Z <= max.Z && pt.Z >= min.Z);
        }

        /***************************************************/

        public static bool IsContaining(this BoundingBox box, IGeometry geometry)
        {
            return box.IsContaining(geometry.IBounds());
        }


        /***************************************************/
        /**** Public Methods - Curve / points           ****/
        /***************************************************/

        public static bool IsContaining(this Arc curve, List<Point> points, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            if (!curve.IsClosed(tolerance)) return false;
            Circle circle = new Circle { Centre = curve.Centre(), Radius = curve.Radius(), Normal = curve.FitPlane().Normal };
            return circle.IsContaining(points, acceptOnEdge, tolerance);
        }

        /***************************************************/

        public static bool IsContaining(this Circle curve, List<Point> points, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            Plane p = new Plane { Origin = curve.Centre, Normal = curve.Normal };
            foreach (Point pt in points)
            {
                if (Math.Abs(pt.Distance(p)) > tolerance) return false;
                if ((acceptOnEdge && pt.Distance(curve.Centre) - curve.Radius - tolerance > 0) || (!acceptOnEdge && pt.Distance(curve.Centre) - curve.Radius + tolerance >= 0)) return false;
            }
            return true;
        }

        /***************************************************/

        public static bool IsContaining(this Line curve1, List<Point> points, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            return false;
        }

        /***************************************************/

        public static bool IsContaining(this NurbCurve curve1, List<Point> points, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            throw new NotImplementedException();
        }

        /***************************************************/

        public static bool IsContaining(this Polyline curve, List<Point> points, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            // Todo:
            // - to be replaced with a general method for a nurbs curve?
            // - this is very problematic for edge cases (cutting line going through a sharp corner, to be superseded?

            Plane p = curve.FitPlane(tolerance);
            if (curve.IsClosed(tolerance))
            {
                double sqTol = tolerance * tolerance;
                foreach (Point pt in points)
                {
                    if (pt.IsInPlane(p, tolerance))
                    {
                        List<Point> intersects = curve.LineIntersections(new Line { Start = pt, End = p.Origin }, true, tolerance); // what if the points are in exactly same spot?
                        if ((pt.ClosestPoint(intersects).SquareDistance(pt) <= sqTol))
                        {
                            if (acceptOnEdge) continue;
                            else return false;
                        }
                        intersects.Add(pt);
                        intersects = intersects.SortCollinear(tolerance);
                        intersects = intersects.CullDuplicates(tolerance);
                        for (int j = 0; j < intersects.Count; j++)
                        {
                            if (j % 2 == 0 && intersects[j] == pt) return false;
                        }
                    }
                    else return false;
                }
                return true;
            }
            return false;
        }

        /***************************************************/

        public static bool IsContaining(this PolyCurve curve, List<Point> points, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            // Todo:
            // - to be replaced with a general method for a nurbs curve?
            // - this is very problematic for edge cases (cutting line going through a sharp corner, to be superseded?

            Plane p = curve.FitPlane(tolerance);
            if (curve.IsClosed(tolerance))
            {
                double sqTol = tolerance * tolerance;
                foreach (Point pt in points)
                {
                    if (pt.IsInPlane(p, tolerance))
                    {
                        List<Point> intersects = curve.LineIntersections(new Line { Start = pt, End = p.Origin }, true, tolerance); // what if the points are in exactly same spot?
                        if ((pt.ClosestPoint(intersects).SquareDistance(pt) <= sqTol))
                        {
                            if (acceptOnEdge) continue;
                            else return false;
                        }
                        intersects.Add(pt);
                        intersects = intersects.SortCollinear(tolerance);
                        intersects = intersects.CullDuplicates(tolerance);
                        for (int j = 0; j < intersects.Count; j++)
                        {
                            if (j % 2 == 0 && intersects[j] == pt) return false;
                        }
                    }
                    else return false;
                }
                return true;
            }
            return false;
        }


        /***************************************************/
        /**** Public Methods - Curve / curve            ****/
        /***************************************************/

        public static bool IsContaining(this Arc curve1, ICurve curve2, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            if (!curve1.IsClosed(tolerance)) return false;
            Circle circle = new Circle { Centre = curve1.Centre(), Radius = curve1.Radius(), Normal = curve1.FitPlane().Normal };
            return circle.IsContaining(curve2);
        }

        /***************************************************/

        public static bool IsContaining(this Circle curve1, ICurve curve2, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            if (curve2 is Line || curve2 is Polyline) return curve1.IsContaining(curve2.IControlPoints(), acceptOnEdge, tolerance);

            List<Point> iPts = curve1.ICurvePlanarIntersections(curve2, tolerance);
            if (!acceptOnEdge && iPts.Count > 0) return false;

            List<double> cParams = new List<double> { 0, 1 };
            foreach (Point iPt in iPts)
            {
                cParams.Add(curve2.IParameterAtPoint(iPt, tolerance));
            }
            cParams.Sort();

            for (int i = 0; i < cParams.Count - 1; i++)
            {
                iPts.Add(curve2.IPointAtParameter((cParams[i] + cParams[i + 1]) * 0.5));
            }
            return curve1.IsContaining(iPts, acceptOnEdge, tolerance);
        }

        /***************************************************/

        public static bool IsContaining(this Line curve1, ICurve curve2, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            return false;
        }

        /***************************************************/

        public static bool IsContaining(this NurbCurve curve1, ICurve curve2, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            throw new NotImplementedException();
        }

        /***************************************************/

        public static bool IsContaining(this Polyline curve1, ICurve curve2, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            if (!curve1.IsClosed(tolerance)) return false;

            List<Point> iPts = curve1.ICurvePlanarIntersections(curve2, tolerance);
            if (!acceptOnEdge && iPts.Count > 0) return false;

            List<double> cParams = new List<double> { 0, 1 };
            foreach (Point iPt in iPts)
            {
                cParams.Add(curve2.IParameterAtPoint(iPt, tolerance));
            }
            cParams.Sort();

            for (int i = 0; i < cParams.Count - 1; i++)
            {
                iPts.Add(curve2.IPointAtParameter((cParams[i] + cParams[i + 1]) * 0.5));
            }
            return curve1.IsContaining(iPts, acceptOnEdge, tolerance);
        }

        /***************************************************/

        public static bool IsContaining(this PolyCurve curve1, ICurve curve2, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            if (!curve1.IsClosed(tolerance)) return false;

            List<Point> iPts = curve1.ICurvePlanarIntersections(curve2, tolerance);
            if (!acceptOnEdge && iPts.Count > 0) return false;

            List<double> cParams = new List<double> { 0, 1 };
            foreach (Point iPt in iPts)
            {
                cParams.Add(curve2.IParameterAtPoint(iPt, tolerance));
            }
            cParams.Sort();

            for (int i = 0; i < cParams.Count - 1; i++)
            {
                iPts.Add(curve2.IPointAtParameter((cParams[i] + cParams[i + 1]) * 0.5));
            }
            return curve1.IsContaining(iPts, acceptOnEdge, tolerance);
        }


        /***************************************************/
        /**** Public Methods - Interfaces               ****/
        /***************************************************/

        public static bool IIsContaining(this ICurve curve, List<Point> points, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            return IsContaining(curve as dynamic, points, acceptOnEdge, tolerance);
        }

        /***************************************************/

        public static bool IIsContaining(this ICurve curve1, ICurve curve2, bool acceptOnEdge = true, double tolerance = Tolerance.Distance)
        {
            return IsContaining(curve1 as dynamic, curve2 as dynamic, acceptOnEdge, tolerance);
        }

        /***************************************************/
    }
}
