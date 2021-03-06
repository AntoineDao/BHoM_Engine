/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2018, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BHG = BH.oM.Geometry;
using BH.Engine.Geometry;
using BH.oM.Environment.Elements;
using BH.oM.Environment.Properties;

using BH.oM.Base;

namespace BH.Engine.Environment
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static List<Opening> Openings(this List<IBHoMObject> bhomObjects)
        {
            List<Opening> openings = new List<Opening>();

            foreach (IBHoMObject obj in bhomObjects)
            {
                if (obj is Opening)
                    openings.Add(obj as Opening);
            }

            return openings;
        }

        public static List<Opening> OpeningsFromElements(this List<BuildingElement> elements)
        {
            List<Opening> openings = new List<Opening>();

            foreach(BuildingElement be in elements)
            {
                List<Opening> beOpenings = be.Openings;
                foreach(Opening o in beOpenings)
                {
                    Opening opInList = openings.Where(x => x.BHoM_Guid == o.BHoM_Guid).FirstOrDefault();
                    if (opInList == null)
                        openings.Add(o);
                }
            }

            return openings;
        }

        public static List<BuildingElement> Openings(this List<BuildingElement> elements)
        {
            return elements.Where(x => x.ElementProperties() != null && (x.ElementProperties() as ElementProperties) != null && ((x.ElementProperties() as ElementProperties).BuildingElementType == BuildingElementType.Window || (x.ElementProperties() as ElementProperties).BuildingElementType == BuildingElementType.Door)).ToList();
        }
    }
}