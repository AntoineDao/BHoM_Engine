﻿using BH.oM.Geometry;
using System;
using System.Linq;

namespace BH.Engine.Geometry
{
    public static partial class Modify
    {
        /***************************************************/
        /**** Public Methods - Vectors                  ****/
        /***************************************************/

        public static Point Scale(this Point pt, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(pt, scaleMatrix);
        }

        /***************************************************/

        public static Vector Scale(this Vector vector, double rad, Vector axis)
        {
            // using Rodrigues' rotation formula
            axis = axis.Normalise();

            return vector * Math.Cos(rad) + axis.CrossProduct(vector) * Math.Sin(rad) + axis * (axis * vector) * (1 - Math.Cos(rad));
        }

        /***************************************************/

        public static Plane Scale(this Plane plane, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(plane, scaleMatrix);
        }


        /***************************************************/
        /**** Public Methods - Curves                   ****/
        /***************************************************/

        public static Arc Scale(this Arc arc, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(arc, scaleMatrix);
        }

        /***************************************************/

        public static Circle Scale(this Circle circle, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(circle, scaleMatrix);
        }

        /***************************************************/

        public static Line Scale(this Line line, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(line, scaleMatrix);
        }

        /***************************************************/

        public static NurbCurve Scale(this NurbCurve curve, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(curve, scaleMatrix);
        }


        /***************************************************/

        public static PolyCurve Scale(this PolyCurve curve, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(curve, scaleMatrix);
        }

        /***************************************************/

        public static Polyline Scale(this Polyline curve, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(curve, scaleMatrix);
        }


        /***************************************************/
        /**** Public Methods - Surfaces                 ****/
        /***************************************************/

        public static Extrusion Scale(this Extrusion surface, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(surface, scaleMatrix);
        }

        /***************************************************/

        public static Loft Scale(this Loft surface, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(surface, scaleMatrix);
        }

        /***************************************************/

        public static NurbSurface Scale(this NurbSurface surface, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(surface, scaleMatrix);
        }

        /***************************************************/

        public static Pipe Scale(this Pipe surface, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(surface, scaleMatrix);
        }

        /***************************************************/

        public static PolySurface Scale(this PolySurface surface, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(surface, scaleMatrix);
        }


        /***************************************************/
        /**** Public Methods - Others                   ****/
        /***************************************************/

        public static Mesh Scale(this Mesh mesh, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(mesh, scaleMatrix);
        }

        /***************************************************/

        public static CompositeGeometry Scale(this CompositeGeometry group, Point origin, Vector scaleVector)
        {
            TransformMatrix scaleMatrix = Create.ScaleMatrix(origin, scaleVector);
            return Transform(group, scaleMatrix);
        }


        /***************************************************/
        /**** Public Methods - Interfaces               ****/
        /***************************************************/

        public static IBHoMGeometry IScale(this IBHoMGeometry geometry, Point origin, Vector scaleVector)
        {
            return Scale(geometry as dynamic, origin, scaleVector);
        }

        /***************************************************/

        public static ICurve IScale(this ICurve geometry, Point origin, Vector scaleVector)
        {
            return Scale(geometry as dynamic, origin, scaleVector);
        }

        /***************************************************/

        public static ISurface IScale(this ISurface geometry, Point origin, Vector scaleVector)
        {
            return Scale(geometry as dynamic, origin, scaleVector);
        }
    }
}