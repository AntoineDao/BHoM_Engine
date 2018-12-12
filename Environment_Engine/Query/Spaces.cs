﻿using System.Linq;
using System.Collections.Generic;
using BH.oM.Environment.Elements;
using BH.oM.Geometry;
using BH.oM.Base;

namespace BH.Engine.Environment
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static List<Space> Spaces(this List<IBHoMObject> bhomObjects)
        {
            List<Space> spaces = new List<Space>();

            foreach(IBHoMObject obj in bhomObjects)
            {
                if (obj is Space)
                    spaces.Add(obj as Space);
            }

            return spaces;
        }

        /***************************************************/

        public static List<Space> Spaces(this List<List<BuildingElement>> besAsSpace)
        {
            List<Space> spaces = new List<Space>();

            for (int x = 0; x < besAsSpace.Count; x++)
                spaces.Add(besAsSpace[x].Space(x.ToString(), x.ToString()));

            return spaces;
        }

        /***************************************************/

        public static Space Space(this List<BuildingElement> space)
        {
            Point spaceCentre = space.SpaceCentre();
            string xName = spaceCentre.X.ToString().Length > 3 ? spaceCentre.X.ToString().Substring(0, 3) : spaceCentre.X.ToString();
            string yName = spaceCentre.Y.ToString().Length > 3 ? spaceCentre.Y.ToString().Substring(0, 3) : spaceCentre.Y.ToString();
            string zName = spaceCentre.Z.ToString().Length > 3 ? spaceCentre.Z.ToString().Substring(0, 3) : spaceCentre.Z.ToString();
            string spaceName = xName + "-" + yName + "-" + zName;
            return Create.Space(spaceName, spaceName, space.SpaceCentre());
        }

        /***************************************************/

        public static Space Space(this List<BuildingElement> space, string spaceNumber, string spaceName)
        {
            return Create.Space(spaceName, spaceNumber, space.SpaceCentre());
        }

        /***************************************************/

        public static Space Space(this List<BuildingElement> space, int spaceNumber, string spaceName)
        {
            return Space(space, spaceNumber.ToString(), spaceName);
        }

        /***************************************************/

        public static List<List<BuildingElement>> SpacesNotClosed(this List<List<BuildingElement>> spaces)
        {
            List<List<BuildingElement>> spacesNotClosed = new List<List<BuildingElement>>();

            foreach(List<BuildingElement> space in spaces)
            {
                if (!space.IsClosed())
                    spacesNotClosed.Add(space);
            }

            return spacesNotClosed;
        }

        /***************************************************/

        public static List<List<BuildingElement>> ClosedSpaces(this List<List<BuildingElement>> spaces)
        {
            List<List<BuildingElement>> closedSpaces = new List<List<BuildingElement>>();

            foreach(List<BuildingElement> space in spaces)
            {
                if (space.IsClosed())
                    closedSpaces.Add(space);
            }

            return closedSpaces;
        }

        /***************************************************/

       

        /***************************************************/

        public static List<List<BuildingElement>> JoinSpaces(this List<List<BuildingElement>> spaces, List<List<BuildingElement>> spacesExtra)
        {
            spaces.AddRange(spacesExtra);
            return spaces;
        }

        /***************************************************/

        public static List<List<BuildingElement>> AddSpaces(this List<List<BuildingElement>> spaces, List<BuildingElement> space)
        {
            spaces.Add(space);
            return spaces;
        }

        public static List<string> UniqueSpaceNames(this List<BuildingElement> elements)
        {
            List<string> spaceNames = new List<string>();

            foreach (BuildingElement be in elements)
            {
                if (be.CustomData.ContainsKey("Revit_spaceId"))
                    spaceNames.Add(be.CustomData["Revit_spaceId"].ToString());
                if(be.CustomData.ContainsKey("Revit_adjacentSpaceId"))
                    spaceNames.Add(be.CustomData["Revit_adjacentSpaceId"].ToString());
            }

            return spaceNames.Where(x => !x.Equals("-1")).Distinct().ToList();
        }
    }
}
