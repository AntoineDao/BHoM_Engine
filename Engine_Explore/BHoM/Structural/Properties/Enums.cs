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

namespace Engine_Explore.BHoM.Structural.Properties
{
    /// <summary>Section classification</summary>
    public enum SectionClass
    {
        /// <summary>c1</summary>
        c1 = 0,
        /// <summary>c2</summary>
        c2,
        /// <summary>c3</summary>
        c3,
        /// <summary>c4</summary>
        c4,
        /// <summary>Not known</summary>
        unknown,
    }

    /*******************************************/

    /// <summary>Axis direction for any application (loads, results, geometry
    /// all cartesian coordinate systems follow the right hand rule</summary>
    public enum AxisDirection
    {
        /// <summary>X direction</summary>
        X = 0,
        /// <summary>Y direction</summary>
        Y = 1,
        /// <summary>Z direction</summary>
        Z = 2,
        /// <summary>Clockwise rotation about X-Axis looking in positive X direction</summary>
        XX = 3,
        /// <summary>Clockwise rotation about Y-Axis looking in positive Y direction</summary>
        YY = 4,
        /// <summary>Clockwise rotation about Z-Axis looking in positive Z direction</summary>
        ZZ = 5
    }

    /*******************************************/

    /// <summary>
    /// Enumerator of types of degrees of freedom
    /// </summary>
    public enum DOFType
    {
        /// <summary>Free - all DOF's released</summary>
        Free = 0,
        /// <summary>Fixed - all DOF's blocked</summary>
        Fixed = 1,
        /// <summary>Zero stiffness in the positive direction</summary>
        FixedNegative = 2,
        /// <summary>Zero stiffness in the negative direction</summary>
        FixedPositive = 3,
        /// <summary>Linear spring constant</summary>
        Spring = 4,
        /// <summary>Non-linear, zero stiffnss in positive direction</summary>
        SpringNegative = 5,
        /// <summary>Non-linear, zero stiffness in negative direction</summary>
        SpringPositive = 6,
        /// <summary>Spring stiffness between 0-1 relates to the element to which the DOF applies (e.g. bar end stiffness)</summary>
        SpringRelative = 7,
        /// <summary>As spring negative, but relative stiffness</summary>
        SpringRelativeNegative = 8,
        /// <summary>As spring positive but relative stiffness</summary>
        SpringRelativePositive = 9,
        /// <summary>Non-linear spring model</summary>
        NonLinear = 10,
        /// <summary>Friction model (relative to the load applied)</summary>
        Friction = 11,
        /// <summary>Damped velocities/accelerations</summary>
        Damped = 12,
        /// <summary>Gap model</summary>
        Gap = 13
    }

    /*******************************************/

    /// <summary>Constraint type</summary>
    public enum ConstraintType
    {
        /// <summary>Restraint (e.g. node resraint)</summary>
        Restraint = 0,
        /// <summary>Release (e.g. bar end releases)</summary>
        Release,
        /// <summary>Rigid (e.g. rigid links)</summary>
        Rigid,
        /// <summary>Compatibility (e.g. compatible nodes)</summary>
        Compatibility
    }

    /*******************************************/

    public enum ShapeType
    {
        Rectangle = 0,
        Box = 1,
        Angle = 2,
        ISection = 3,
        Tee = 4,
        Channel = 5,
        Tube = 6,
        Circle = 7,
        Zed = 8,
        Polygon = 9,

        DoubleAngle = 22,
        DoubleISection = 23,
        DoubleChannel = 25,

        //Maybe should move elsewhere
        Cable = 30,
    }
}
