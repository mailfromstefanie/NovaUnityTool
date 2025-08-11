#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace StefTools
{
    public static class SceneLezer
    {
        public static string BuildSceneReportReadable()
        {
            string project = Application.productName;
            string version = PlayerSettings.bundleVersion;
            string date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string scene = SceneManager.GetActiveScene().name;

            var sb = new StringBuilder(256 * 1024);
            sb.AppendLine("=== SCENE-OVERZICHT (leesbaar) ===");
            sb.AppendLine("Project: " + project + " | Versie: " + version + " | Datum: " + date);
            sb.AppendLine("Scene: " + scene);
            sb.AppendLine();

            var allGOs = UnityEngine.Object.FindObjectsOfType<GameObject>(true).OrderBy(p => BestandenHulp.GetPath(p.transform));
            foreach (var go in allGOs)
            {
                string path = BestandenHulp.GetPath(go.transform);
                sb.AppendLine("Object: " + path + "  (ActiveSelf=" + go.activeSelf + ", ActiveInHierarchy=" + go.activeInHierarchy + ")");
                sb.AppendLine("  Tag=" + go.tag + ", Layer=" + LayerMask.LayerToName(go.layer));

                foreach (var c in go.GetComponents<Component>())
                {
                    if (!c) { sb.AppendLine("  - Component: <MISSING>"); continue; }
                    var t = c.GetType();
                    sb.AppendLine("  - Component: " + t.Name);

                    if (c is Renderer r)
                    {
                        var mats = r.sharedMaterials;
                        for (int i = 0; i < mats.Length; i++)
                        {
                            var m = mats[i];
                            if (!m) continue;
                            string shName = (m.shader != null ? m.shader.name : "<none>");
                            sb.AppendLine("      Material[" + i + "]: " + m.name + "  (Shader: " + shName + ")");
                        }
                    }

                    if (c is MonoBehaviour mb)
                    {
                        try
                        {
                            var so = new SerializedObject(mb);
                            var it = so.GetIterator();
                            bool enter = true;
                            while (it.NextVisible(enter))
                            {
                                enter = false;
                                if (it.propertyPath == "m_Script") continue;
                                sb.AppendLine("      [" + it.propertyType + "] " + it.propertyPath + " = " + BestandenHulp.FormatProp(it));
                            }
                        }
                        catch (System.Exception ex) { sb.AppendLine("      [SerializedObject-Error] " + ex.Message); }
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static string BuildSceneReportJson()
        {
            var sb = new StringBuilder(512 * 1024);
            sb.Append("{");

            // meta
            sb.Append("\"meta\":{");
            sb.Append("\"project\":\"").Append(JsonHulp.Escape(Application.productName)).Append("\",");
            sb.Append("\"version\":\"").Append(JsonHulp.Escape(PlayerSettings.bundleVersion)).Append("\",");
            sb.Append("\"scene\":\"").Append(JsonHulp.Escape(SceneManager.GetActiveScene().name)).Append("\",");
            sb.Append("\"exported_at\":\"").Append(System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")).Append("\"");
            sb.Append("},");

            // objects
            var objects = UnityEngine.Object.FindObjectsOfType<GameObject>(true).OrderBy(p => BestandenHulp.GetPath(p.transform)).ToArray();
            sb.Append("\"objects\":[");
            for (int i = 0; i < objects.Length; i++)
            {
                var go = objects[i];
                if (i > 0) sb.Append(",");
                sb.Append("{");
                sb.Append("\"path\":\"").Append(JsonHulp.Escape(BestandenHulp.GetPath(go.transform))).Append("\",");
                sb.Append("\"activeSelf\":").Append(go.activeSelf ? "true" : "false").Append(",");
                sb.Append("\"activeInHierarchy\":").Append(go.activeInHierarchy ? "true" : "false").Append(",");
                sb.Append("\"tag\":\"").Append(JsonHulp.Escape(go.tag)).Append("\",");
                sb.Append("\"layer\":\"").Append(JsonHulp.Escape(LayerMask.LayerToName(go.layer))).Append("\"");
                sb.Append("}");
            }
            sb.Append("],");

            // components
            sb.Append("\"components\":[");
            bool firstComp = true;
            foreach (var go in objects)
            {
                foreach (var c in go.GetComponents<Component>())
                {
                    if (!c) continue;
                    if (!firstComp) sb.Append(",");
                    firstComp = false;

                    sb.Append("{");
                    sb.Append("\"object\":\"").Append(JsonHulp.Escape(BestandenHulp.GetPath(go.transform))).Append("\",");
                    sb.Append("\"type\":\"").Append(JsonHulp.Escape(c.GetType().Name)).Append("\"");

                    if (c is Renderer r)
                    {
                        var mats = r.sharedMaterials;
                        var matNames = new List<string>();
                        var shaderNames = new List<string>();
                        foreach (var m in mats)
                        {
                            if (m) { matNames.Add(m.name); shaderNames.Add(m.shader ? m.shader.name : "<none>"); }
                        }
                        sb.Append(",\"materials\":").Append(JsonHulp.ToJsonArray(matNames));
                        sb.Append(",\"shaders\":").Append(JsonHulp.ToJsonArray(shaderNames));
                    }

                    try
                    {
                        var so = new SerializedObject(c);
                        var it = so.GetIterator();
                        bool enter = true;
                        sb.Append(",\"fields\":{");
                        bool firstField = true;
                        while (it.NextVisible(enter))
                        {
                            enter = false;
                            if (it.propertyPath == "m_Script") continue;
                            string val = BestandenHulp.SerializedToSimpleJson(it);
                            if (val == null) continue;
                            if (!firstField) sb.Append(",");
                            firstField = false;
                            sb.Append("\"").Append(JsonHulp.Escape(it.propertyPath)).Append("\":").Append(val);
                        }
                        sb.Append("}");
                    }
                    catch
                    {
                        sb.Append(",\"fields\":{}");
                    }

                    sb.Append("}");
                }
            }
            sb.Append("]");

            sb.Append("}");
            return sb.ToString();
        }
    }
}
#endif
