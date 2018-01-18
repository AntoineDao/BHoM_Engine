﻿using BH.oM.Base;
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
            pack2.Add(new EnumRepresentationConvention(BsonType.String));
            ConventionRegistry.Register("Enum Conventions", pack2, x => x.GetType().IsEnum);

            // Register additional serialisers
            try
            {
                BsonSerializer.RegisterSerializer(typeof(object), new BH_ObjectSerializer());
                BsonSerializer.RegisterSerializer(typeof(System.Drawing.Color), new ColourSerializer());
                BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
                BsonSerializer.RegisterSerializer(typeof(CustomObject), new CustomObjectSerializer());
                BsonDefaults.DynamicDocumentSerializer = new CustomObjectSerializer();
            }
            catch (Exception)
            {
                Console.WriteLine("Problem with initialisation of the Bson Serializer");
            }
            
            // Register class maps
            foreach (Type type in BH.Engine.Reflection.Query.BHoMTypeList())
            {
                if (!type.IsGenericType && !BsonClassMap.IsClassMapRegistered(type))
                    RegisterClassMap(type);
            }
            RegisterClassMap(typeof(System.Drawing.Color));

            m_TypesRegistered = true;
        }


        /*******************************************/

        private static void RegisterClassMap(Type type)
        {
            BsonClassMap cm = new BsonClassMap(type);
            cm.AutoMap();
            cm.SetDiscriminator(type.FullName);
            cm.SetDiscriminatorIsRequired(true);
            cm.SetIgnoreExtraElements(true);   // It would have been nice to use cm.MapExtraElementsProperty("CustomData") but it doesn't work for inherited properties
            BsonClassMap.RegisterClassMap(cm);
        }


        /*******************************************/
        /**** Private Fields                    ****/
        /*******************************************/

        private static bool m_TypesRegistered = false;


        /*******************************************/
    }
}