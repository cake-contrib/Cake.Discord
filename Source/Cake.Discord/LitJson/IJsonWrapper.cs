#pragma warning disable SA1124
#region Header

/*
 * IJsonWrapper.cs
 *   Interface that represents a type capable of handling all kinds of JSON
 *   data. This is mainly used when mapping objects through JsonMapper, and
 *   it's implemented by JsonData.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 */

#endregion Header

#pragma warning disable SA1003
#pragma warning disable SA1008
#pragma warning disable SA1009
#pragma warning disable SA1012
#pragma warning disable SA1013
#pragma warning disable SA1115
#pragma warning disable SA1116
#pragma warning disable SA1117
#pragma warning disable SA1119
#pragma warning disable SA1121
#pragma warning disable SA1122
#pragma warning disable SA1128
#pragma warning disable SA1129
#pragma warning disable SA1130
#pragma warning disable SA1200
#pragma warning disable SA1201
#pragma warning disable SA1202
#pragma warning disable SA1204
#pragma warning disable SA1210
#pragma warning disable SA1310
#pragma warning disable SA1311
#pragma warning disable SA1400
#pragma warning disable SA1401
#pragma warning disable SA1402
#pragma warning disable SA1403
#pragma warning disable SA1408
#pragma warning disable SA1413
#pragma warning disable SA1500
#pragma warning disable SA1503
#pragma warning disable SA1504
#pragma warning disable SA1507
#pragma warning disable SA1505
#pragma warning disable SA1508
#pragma warning disable SA1513
#pragma warning disable SA1515
#pragma warning disable SA1516
#pragma warning disable SA1519
#pragma warning disable SA1520
#pragma warning disable SA1600
#pragma warning disable SA1649
namespace Cake.Discord
{
    using System.Collections;
    using System.Collections.Specialized;
    using System;
    using System.Collections.Generic;
    using System.IO;

    namespace LitJson
    {
        internal enum JsonType
        {
            None,

            Object,
            Array,
            String,
            Int,
            Long,
            Double,
            Boolean
        }

        internal interface IJsonWrapper : IList, IOrderedDictionary
        {
            bool IsArray { get; }
            bool IsBoolean { get; }
            bool IsDouble { get; }
            bool IsInt { get; }
            bool IsLong { get; }
            bool IsObject { get; }
            bool IsString { get; }

            bool GetBoolean();

            double GetDouble();

            int GetInt();

            JsonType GetJsonType();

            long GetLong();

            string GetString();

            void SetBoolean(bool val);

            void SetDouble(double val);

            void SetInt(int val);

            void SetJsonType(JsonType type);

            void SetLong(long val);

            void SetString(string val);

            string ToJson();

            void ToJson(JsonWriter writer);
        }
    }
}
#pragma warning restore SA1003
#pragma warning restore SA1008
#pragma warning restore SA1009
#pragma warning restore SA1012
#pragma warning restore SA1013
#pragma warning restore SA1115
#pragma warning restore SA1116
#pragma warning restore SA1117
#pragma warning restore SA1119
#pragma warning restore SA1121
#pragma warning restore SA1122
#pragma warning restore SA1124
#pragma warning restore SA1128
#pragma warning restore SA1129
#pragma warning restore SA1130
#pragma warning restore SA1200
#pragma warning restore SA1201
#pragma warning restore SA1202
#pragma warning restore SA1204
#pragma warning restore SA1210
#pragma warning restore SA1310
#pragma warning restore SA1311
#pragma warning restore SA1400
#pragma warning restore SA1401
#pragma warning restore SA1402
#pragma warning restore SA1403
#pragma warning restore SA1408
#pragma warning restore SA1413
#pragma warning restore SA1500
#pragma warning restore SA1503
#pragma warning restore SA1504
#pragma warning restore SA1507
#pragma warning restore SA1505
#pragma warning restore SA1508
#pragma warning restore SA1513
#pragma warning restore SA1515
#pragma warning restore SA1516
#pragma warning restore SA1519
#pragma warning restore SA1520
#pragma warning restore SA1600
#pragma warning restore SA1649
