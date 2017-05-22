﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alea;
using Alea.Parallel;

using BHoM.Geometry;
using BHoM.Acoustic;


namespace AcousticSPI_Engine
{
    public static class DirectSound
    {
        #region Methods

        /// <summary>
        /// Performs a Direct Sound calculation with obstacles check.
        /// </summary>
        /// <param name="sources">BHoM acoustic Speaker</param>
        /// <param name="targets">BHoM acoustic Receivers</param>
        /// <param name="surfaces">BHoM acoustic Panel</param>
        /// <returns>Returns a list of BHoM Acoustic Rays</returns>
        public static List<Ray> Solve(List<Speaker> sources, List<Receiver> targets, List<Panel> surfaces)
        {
            List<Ray> rays = new List<Ray>();
            /*
            // GPU accelerated ######################################   ##
            Gpu.Default.For(0, sources.Count,
                i =>
                {
                    for (int j = 0; j < targets.Count; i++)
                    {
                        List<Point> rayPts = new List<Point>() { sources[i].Position, targets[j].Position };
                        Polyline path = new Polyline(rayPts);
                        Ray ray = new Ray(path, "S" + i.ToString(), "R" + j.ToString());
                        //Ray ray = new Ray(path, sources[i].SpeakerID, targets[j].ReceiverID);
                        rays.Add(ray);
                    }
                });*/

            // CPU accelerated ######################################   ##
            Parallel.For(0, sources.Count,
                i =>
                {
                    for (int j = 0; j < targets.Count; j++)
                    {
                        List<Point> rayPts = new List<Point>();
                        rayPts.Add(sources[i].Position);
                        rayPts.Add(targets[j].Position);
                        Polyline path = new Polyline(rayPts);
                        Ray ray = new Ray(path, "S" + i.ToString(), "R" + j.ToString());
                        //Ray ray = new Ray(path, sources[i].SpeakerID, targets[j].ReceiverID);
                        rays.Add(ray);
                    }
                });
            if (surfaces.Count == 0)
                return rays;
            else
                return Utils.CheckObstacles(rays, surfaces);

            /*
            for (int i = 0; i < sources.Count; i++)
            {
                for (int j = 0; j < targets.Count; i++)
                {
                    List<Point> rayPts = new List<Point>() { sources[i].Position, targets[j].Position };
                    Polyline path = new Polyline(rayPts);
                    Ray ray = new Ray(path, "S"+i.ToString(), "R"+j.ToString());
                    //Ray ray = new Ray(path, sources[i].SpeakerID, targets[j].ReceiverID);
                    rays.Add(ray);
                }
            }
            return Utils.CheckObstacles(rays, surfaces);*/
        }

        #endregion
    }
}
