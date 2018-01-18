﻿using BH.oM.Structural.Properties;

namespace BH.Engine.Structure
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Constraint6DOF Constraint6DOF(string name = "")
        {
            return new Constraint6DOF { Name = name };
        }

        /***************************************************/

        public static Constraint6DOF Constraint6DOF(string name, bool[] fixity, double[] values)
        {
            return new Constraint6DOF
            {
                Name = name,
                TranslationX = (fixity[0]) ? DOFType.Fixed : (values[0] == 0) ? DOFType.Free : DOFType.Spring,
                TranslationY = (fixity[1]) ? DOFType.Fixed : (values[1] == 0) ? DOFType.Free : DOFType.Spring,
                TranslationZ = (fixity[2]) ? DOFType.Fixed : (values[2] == 0) ? DOFType.Free : DOFType.Spring,
                RotationX = (fixity[3]) ? DOFType.Fixed : (values[3] == 0) ? DOFType.Free : DOFType.Spring,
                RotationY = (fixity[4]) ? DOFType.Fixed : (values[4] == 0) ? DOFType.Free : DOFType.Spring,
                RotationZ = (fixity[5]) ? DOFType.Fixed : (values[5] == 0) ? DOFType.Free : DOFType.Spring,

                TranslationalStiffnessX = values[0],
                TranslationalStiffnessY = values[1],
                TranslationalStiffnessZ = values[2],
                RotationalStiffnessX = values[3],
                RotationalStiffnessY = values[4],
                RotationalStiffnessZ = values[5]
            };
        }

        /***************************************************/

        public static Constraint6DOF PinConstraint6DOF(string name = "Pin")
        {
            return new Constraint6DOF
            {
                Name = name,
                TranslationX = DOFType.Fixed,
                TranslationY = DOFType.Fixed,
                TranslationZ = DOFType.Fixed
            };
        }

        /***************************************************/

        public static Constraint6DOF FixConstraint6DOF(string name = "Fix")
        {
            return new Constraint6DOF
            {
                Name = name,
                TranslationX = DOFType.Fixed,
                TranslationY = DOFType.Fixed,
                TranslationZ = DOFType.Fixed,
                RotationX = DOFType.Fixed,
                RotationY = DOFType.Fixed,
                RotationZ = DOFType.Fixed
            };
        }

        /***************************************************/

        public static Constraint6DOF FullReleaseConstraint6DOF(string name = "Release")
        {
            return new Constraint6DOF
            {
                Name = name,
                TranslationX = DOFType.Free,
                TranslationY = DOFType.Free,
                TranslationZ = DOFType.Free,
                RotationX = DOFType.Free,
                RotationY = DOFType.Free,
                RotationZ = DOFType.Free
            };
        }

        /***************************************************/
    }
}