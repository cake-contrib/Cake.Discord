#pragma warning disable SA1124
#region Header
/*
 * JsonMockWrapper.cs
 *   Mock object implementing IJsonWrapper, to facilitate actions like
 *   skipping data more efficiently.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 */
#endregion

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
    using System;
    using System.Collections;
    using System.Collections.Specialized;


    namespace LitJson
    {
        internal class JsonMockWrapper : IJsonWrapper
        {
            public bool IsArray
            {
                get { return false; }
            }

            public bool IsBoolean
            {
                get { return false; }
            }

            public bool IsDouble
            {
                get { return false; }
            }

            public bool IsInt
            {
                get { return false; }
            }

            public bool IsLong
            {
                get { return false; }
            }

            public bool IsObject
            {
                get { return false; }
            }

            public bool IsString
            {
                get { return false; }
            }

            public bool GetBoolean()
            {
                return false;
            }

            public double GetDouble()
            {
                return 0.0;
            }

            public int GetInt()
            {
                return 0;
            }

            public JsonType GetJsonType()
            {
                return JsonType.None;
            }

            public long GetLong()
            {
                return 0L;
            }

            public string GetString()
            {
                return "";
            }

            public void SetBoolean(bool val)
            {
            }

            public void SetDouble(double val)
            {
            }

            public void SetInt(int val)
            {
            }

            public void SetJsonType(JsonType type)
            {
            }

            public void SetLong(long val)
            {
            }

            public void SetString(string val)
            {
            }

            public string ToJson()
            {
                return "";
            }

            public void ToJson(JsonWriter writer)
            {
            }


            bool IList.IsFixedSize
            {
                get { return true; }
            }

            bool IList.IsReadOnly
            {
                get { return true; }
            }

            object IList.this[int index]
            {
                get { return null; }
                set { }
            }

            int IList.Add(object value)
            {
                return 0;
            }

            void IList.Clear()
            {
            }

            bool IList.Contains(object value)
            {
                return false;
            }

            int IList.IndexOf(object value)
            {
                return -1;
            }

            void IList.Insert(int i, object v)
            {
            }

            void IList.Remove(object value)
            {
            }

            void IList.RemoveAt(int index)
            {
            }


            int ICollection.Count
            {
                get { return 0; }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return null; }
            }

            void ICollection.CopyTo(Array array, int index)
            {
            }


            IEnumerator IEnumerable.GetEnumerator()
            {
                return null;
            }


            bool IDictionary.IsFixedSize
            {
                get { return true; }
            }

            bool IDictionary.IsReadOnly
            {
                get { return true; }
            }

            ICollection IDictionary.Keys
            {
                get { return null; }
            }

            ICollection IDictionary.Values
            {
                get { return null; }
            }

            object IDictionary.this[object key]
            {
                get { return null; }
                set { }
            }

            void IDictionary.Add(object k, object v)
            {
            }

            void IDictionary.Clear()
            {
            }

            bool IDictionary.Contains(object key)
            {
                return false;
            }

            void IDictionary.Remove(object key)
            {
            }

            IDictionaryEnumerator IDictionary.GetEnumerator()
            {
                return null;
            }


            object IOrderedDictionary.this[int idx]
            {
                get { return null; }
                set { }
            }

            IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
            {
                return null;
            }

            void IOrderedDictionary.Insert(int i, object k, object v)
            {
            }

            void IOrderedDictionary.RemoveAt(int i)
            {
            }
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
