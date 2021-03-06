﻿/*
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

using BH.oM.Environment.Properties;
using BH.oM.Environment.Materials;

namespace BH.Engine.Environment
{
    public static partial class Create
    {
        public static MaterialPropertiesTransparent TransparentMaterialProperties(string description = "", double conductivity = 0.0, double specificHeat = 0.0, double density = 0.0, double vapourDiffusionFactor = 0.0, double solarTransmittance = 0.0, double solarReflectanceExternal = 0.0, double solarReflectanceInternal = 0.0, double lightTransmittance = 0.0, double lightReflectanceExternal = 0.0, double lightReflectanceInternal = 0.0, double emissivityExternal = 0.0, double emissivityInternal = 0.0)
        {
            return new MaterialPropertiesTransparent
            {
                Description = description,
                Conductivity = conductivity,
                SpecificHeat = specificHeat,
                Density = density,
                VapourDiffusionFactor = vapourDiffusionFactor,
                SolarTransmittance = solarTransmittance,
                SolarReflectanceExternal = solarReflectanceExternal,
                SolarReflectanceInternal = solarReflectanceInternal,
                LightTransmittance = lightTransmittance,
                LightReflectanceExternal = lightReflectanceExternal,
                LightReflectanceInternal = lightReflectanceInternal,
                EmissivityInternal = emissivityInternal,
                EmissivityExternal = emissivityExternal,
            };
        }
    }
}
