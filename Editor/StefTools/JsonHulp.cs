#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;

namespace StefTools
{
    public static class JsonHulp
    {
        public static string Escape(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        public static string ToJsonArray(IEnumerable<string> items)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            bool first = true;
            foreach (var it in items)
            {
                if (!first) sb.Append(",");
                first = false;
                sb.Append("\"").Append(Escape(it)).Append("\"");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
#endif
