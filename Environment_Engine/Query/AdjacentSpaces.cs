﻿using BH.oM.Environmental.Elements;
using System.Collections.Generic;

namespace BH.Engine.Environment
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static List<Space> AdjacentSpaces(this Building building, Space space)
        {
            if (building == null || space == null)
                return null;

            List<Space> aResult = new List<Space>();
            foreach (BuildingElement aBuildingElement in building.BuildingElements)
            {
                if (aBuildingElement.AdjacentSpaces != null && aBuildingElement.AdjacentSpaces.Count > 0)
                {
                    Space aSpace = aBuildingElement.AdjacentSpaces.Find(x => x.BHoM_Guid == space.BHoM_Guid);
                    if (aSpace != null)
                    {
                        foreach(Space aSpace_Temp in aBuildingElement.AdjacentSpaces)
                        {
                            if (aSpace_Temp.BHoM_Guid != aSpace.BHoM_Guid)
                                aResult.Add(aSpace_Temp);
                        }
                    }
                }
            }

            return aResult;
        }

        /***************************************************/
    }
}

