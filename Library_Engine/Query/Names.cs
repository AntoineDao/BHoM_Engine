﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Engine.Library
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static List<string> Names(string libraryName)
        {
            return Library(libraryName).Select(x => x.Name).ToList();
        }

        /***************************************************/

        public static List<string> Names(string libraryName, string subSection)
        {
            return SubSection(libraryName, subSection).Select(x => x.Name).ToList();
        }

        /***************************************************/

        public static List<string> LibraryNames()
        {
            return LibraryStrings().Select(x => x.Key).ToList();
        }

        /***************************************************/
    }
}