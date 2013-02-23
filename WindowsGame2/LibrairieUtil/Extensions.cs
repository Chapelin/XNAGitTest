using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTRPG_case
{
    public static class Extensions
    {
        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }

        public static string Tostring<T>(this T[] v)
        {
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < v.Length; i++)
            {
                ret.Append(v[i].ToString());
            }
            return ret.ToString();
        }
    }
}
