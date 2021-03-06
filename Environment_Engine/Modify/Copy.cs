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

using System.Collections.Generic;

using BH.oM.Geometry;
using BH.Engine.Geometry;
using BH.oM.Environment.Elements;
using BH.oM.Environment.Interface;
using BH.oM.Architecture.Elements;

namespace BH.Engine.Environment
{
    public static partial class Modify
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Panel Copy(this Panel buildingElementPanel)
        {
            Panel aBuildingElementPanel = buildingElementPanel.GetShallowClone(true) as Panel;
            aBuildingElementPanel.PanelCurve = buildingElementPanel.PanelCurve.IClone();
            return aBuildingElementPanel;
        }

        /***************************************************/

        public static IBuildingObject Copy(this IBuildingObject buildingObject)
        {
            IBuildingObject aBuildingObject = Copy(buildingObject as dynamic);
            return aBuildingObject;
        }

        /***************************************************/

        public static Level Copy(this Level level)
        {
            return level.GetShallowClone(true) as Level;
        }

        /***************************************************/

        public static BuildingElement Copy(this BuildingElement buildingElement)
        {
            BuildingElement aBuildingElement = buildingElement.GetShallowClone(true) as BuildingElement;
            aBuildingElement.PanelCurve = buildingElement.PanelCurve;
            aBuildingElement.Openings = new List<Opening>(buildingElement.Openings);
            aBuildingElement.CustomData = buildingElement.CustomData;
            aBuildingElement.ExtendedProperties = new List<IBHoMExtendedProperties>(buildingElement.ExtendedProperties);
            return aBuildingElement;
        }

        /***************************************************/

        public static Level Copy(this Level level, string name, double elevation)
        {
            Level aLevel = level.Copy();
            aLevel.Name = name;
            aLevel.Elevation = elevation;

            return aLevel;
        }

        /***************************************************/

        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/



        /***************************************************/
    }
}
