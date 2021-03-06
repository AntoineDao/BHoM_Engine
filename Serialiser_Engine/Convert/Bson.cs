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

using BH.oM.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using MongoDB.Bson.Serialization.Conventions;
using BH.Engine.Serialiser.BsonSerializers;
using BH.Engine.Serialiser.MemberMapConventions;
using BH.Engine.Serialiser.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using System.Diagnostics;
using BH.Engine.Serialiser.Objects.MemberMapConventions;
using System.Reflection;

namespace BH.Engine.Serialiser
{
    public static partial class Convert
    {
        /*******************************************/
        /**** Public Methods                    ****/
        /*******************************************/

        public static BsonDocument ToBson(this object obj)
        {
            if (!m_TypesRegistered)
                RegisterTypes();

            if (obj is string)
            {
                BsonDocument document;
                BsonDocument.TryParse(obj as string, out document);
                return document;
            }
            else
                return obj.ToBsonDocument();
        }

        /*******************************************/

        public static object FromBson(BsonDocument bson)
        {
            if (!m_TypesRegistered)
                RegisterTypes();

            bson.Remove("_id");
            object obj = BsonSerializer.Deserialize(bson, typeof(object));
            if (obj is ExpandoObject)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>(obj as ExpandoObject);
                CustomObject co = new CustomObject();
                if (dic.ContainsKey("Name"))
                {
                    co.Name = dic["Name"] as string;
                    dic.Remove("Name");
                }   
                if (dic.ContainsKey("Tags"))
                {
                    co.Tags = new HashSet<string>(((List<object>)dic["Tags"]).Cast<string>());
                    dic.Remove("Tags");
                }
                if (dic.ContainsKey("BHoM_Guid"))
                {
                    co.BHoM_Guid = (Guid)dic["BHoM_Guid"];
                    dic.Remove("BHoM_Guid");
                }
                co.CustomData = dic;
                return co;
            }
            else
                return obj;
        }


        /*******************************************/
        /**** Private Methods                   ****/
        /*******************************************/

        static Convert()
        {
            RegisterTypes();
        }

        /*******************************************/

        private static void RegisterTypes()
        {
            // Define the conventions   
            var pack = new ConventionPack();
            pack.Add(new ImmutableBHoMClassMapConvention());
            pack.Add(new ImmutableBHoMCreatorMapConvention());
            pack.Add(new BHoMDictionaryConvention());
            ConventionRegistry.Register("BHoM Conventions", pack, x => x is object);

            var pack2 = new ConventionPack();
            pack2.Add(new BHoMEnumConvention());
            ConventionRegistry.Register("Enum Conventions", pack2, x => x.GetType().IsEnum);

            // Register additional serialisers
            try
            {
                BsonSerializer.RegisterSerializer(typeof(object), new BH_ObjectSerializer());
                BsonSerializer.RegisterSerializer(typeof(System.Drawing.Color), new ColourSerializer());
                BsonSerializer.RegisterSerializer(typeof(MethodBase), new MethodBaseSerializer());
                BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
                BsonSerializer.RegisterSerializer(typeof(CustomObject), new CustomObjectSerializer());
                BsonSerializer.RegisterSerializer(typeof(Enum), new EnumSerializer());

                var typeSerializer = new TypeSerializer();
                BsonSerializer.RegisterSerializer(typeof(Type), typeSerializer);
                BsonSerializer.RegisterSerializer(Type.GetType("System.RuntimeType"), typeSerializer);

                BsonDefaults.DynamicDocumentSerializer = new CustomObjectSerializer();

                BsonSerializer.RegisterDiscriminatorConvention(typeof(object), new GenericDiscriminatorConvention());
            }
            catch (Exception)
            {
                Debug.WriteLine("Problem with initialisation of the Bson Serializer");
            }

            // Register class maps
            MethodInfo method = typeof(BH.Engine.Serialiser.Convert).GetMethod("CreateEnumSerializer", BindingFlags.NonPublic | BindingFlags.Static);
            foreach (Type type in BH.Engine.Reflection.Query.BHoMTypeList())
            {
                if (!BsonClassMap.IsClassMapRegistered(type))
                {
                    if (type.IsEnum)
                    {
                        MethodInfo generic = method.MakeGenericMethod(type);
                        generic.Invoke(null, null);
                    } 
                    else if (!type.IsGenericType)
                        RegisterClassMap(type);
                    
                }
                    
            }
            RegisterClassMap(typeof(System.Drawing.Color));
            RegisterClassMap(typeof(MethodInfo));
            RegisterClassMap(typeof(ConstructorInfo));

            m_TypesRegistered = true;
        }


        /*******************************************/

        private static void RegisterClassMap(Type type)
        {
            try
            {
                BsonClassMap cm = new BsonClassMap(type);
                cm.AutoMap();
                cm.SetDiscriminator(type.FullName);
                cm.SetDiscriminatorIsRequired(true);
                cm.SetIgnoreExtraElements(false);   // It would have been nice to use cm.MapExtraElementsProperty("CustomData") but it doesn't work for inherited properties
                BsonClassMap.RegisterClassMap(cm);

                BsonSerializer.RegisterDiscriminatorConvention(type, new GenericDiscriminatorConvention());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        /*******************************************/

        private static void CreateEnumSerializer<T>() where T : struct
        {
            try
            {
                BsonSerializer.RegisterSerializer(typeof(T), new EnumSerializer<T>(BsonType.String));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        /*******************************************/
        /**** Private Fields                    ****/
        /*******************************************/

        private static bool m_TypesRegistered = false;


        /*******************************************/
    }
}
