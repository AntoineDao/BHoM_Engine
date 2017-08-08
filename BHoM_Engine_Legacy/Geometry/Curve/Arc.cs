﻿using BH.oM.Base;
using BH.oM.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Engine.Geometry
{
    public static class XArc
    {
        public static void CreateNurbForm(this Arc arc)
        {
            double[] centre = arc.Centre;
            double[] P1 = CollectionUtils.SubArray<double>(arc.ControlPointVector, 0, arc.Dimensions + 1);
            double[] P2 = CollectionUtils.SubArray<double>(arc.ControlPointVector, (arc.Dimensions + 1) * 2, arc.Dimensions + 1);

            double[] V1 = ArrayUtils.Sub(P1, centre);
            double[] V2 = ArrayUtils.Sub(P2, centre);

            double[] Normal = ArrayUtils.CrossProduct(V1, V2);

            double[] T1 = ArrayUtils.CrossProduct(V1, Normal);
            double[] T2 = ArrayUtils.CrossProduct(V2, Normal);

            double[] cP2 = ArrayUtils.Intersect(P1, T1, P2, T2);

            double w2 = arc.Radius() / ArrayUtils.Length(ArrayUtils.Sub(cP2, centre));

            double arcAngle = ArrayUtils.Angle(V1, V2);

            arc.SetWeights(new double[] { 1, w2, 1 });
            arc.SetKnots(new double[] { 0, 0, 0, arcAngle, arcAngle, arcAngle });
            arc.SetDegree(2);

            Array.Copy(cP2, 0,arc.ControlPointVector, arc.Dimensions + 1, arc.Dimensions + 1);
        }

        public static double Length(this Arc arc)
        {
            return arc.ArcAngle() * arc.Radius();            
        }

        public static double ArcAngle(this Arc arc)
        {
            if (!arc.IsNurbForm) arc.CreateNurbForm();
            return arc.Knots.Length > 0 ? arc.Knots[arc.Knots.Length - 1] : 0;
        }


        public static double Radius(this Arc arc)
        {
            return ArrayUtils.Length(ArrayUtils.Sub(arc.Centre(), CollectionUtils.SubArray<double>(arc.ControlPointVector, 0, 4)));
        }
    }
}