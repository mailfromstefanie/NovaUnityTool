#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace StefTools
{
    public static class BestandenHulp
    {
        public static void EnsureSceneOpen()
        {
            if (UnityEditor.SceneManagement.EditorSceneManager.sceneCount == 0)
                throw new InvalidOperationException("Open eerst een scene.");
        }

        public static string GetPath(Transform t)
        {
            var stack = new Stack<string>();
            while (t != null) { stack.Push(t.name); t = t.parent; }
            return string.Join("/", stack.ToArray());
        }

        public static void CollectScriptsAndShaders(string targetDir)
        {
            var scripts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var shaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var gos = UnityEngine.Object.FindObjectsOfType<GameObject>(true);
            foreach (var go in gos)
            {
                foreach (var mb in go.GetComponents<MonoBehaviour>())
                {
                    if (!mb) continue;
                    var ms = MonoScript.FromMonoBehaviour(mb);
                    if (!ms) continue;
                    string p = AssetDatabase.GetAssetPath(ms);
                    if (!string.IsNullOrEmpty(p) && File.Exists(p)) scripts.Add(p);
                }

                var r = go.GetComponent<Renderer>();
                if (r)
                {
                    foreach (var m in r.sharedMaterials)
                    {
                        if (!m) continue;
                        var sh = m.shader;
                        if (!sh) continue;
                        string p = AssetDatabase.GetAssetPath(sh);
                        if (!string.IsNullOrEmpty(p) && File.Exists(p)) shaders.Add(p);
                    }
                }
            }

            foreach (var src in scripts)
            {
                try { File.Copy(src, Path.Combine(targetDir, Path.GetFileName(src)), true); }
                catch (Exception ex) { Debug.LogWarning("Kon script niet kopieren: " + src + " // " + ex.Message); }
            }
            foreach (var src in shaders)
            {
                try { File.Copy(src, Path.Combine(targetDir, Path.GetFileName(src)), true); }
                catch (Exception ex) { Debug.LogWarning("Kon shader niet kopieren: " + src + " // " + ex.Message); }
            }
        }

        public static void ZipWithoutMeta(string srcRoot, string zipPath)
        {
            using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (var file in Directory.GetFiles(srcRoot, "*", SearchOption.AllDirectories))
                {
                    if (file.EndsWith(".meta", StringComparison.OrdinalIgnoreCase)) continue;
                    string rel = file.Substring(srcRoot.Length).TrimStart(Path.DirectorySeparatorChar);
                    zip.CreateEntryFromFile(file, rel);
                }
            }
        }

        public static string FormatProp(SerializedProperty p)
        {
            try
            {
                switch (p.propertyType)
                {
                    case SerializedPropertyType.Integer: return p.intValue.ToString();
                    case SerializedPropertyType.Boolean: return p.boolValue.ToString();
                    case SerializedPropertyType.Float:   return p.floatValue.ToString("0.###");
                    case SerializedPropertyType.String:  return string.IsNullOrEmpty(p.stringValue) ? "\"\"" : "\"" + p.stringValue + "\"";
                    case SerializedPropertyType.Color:   return p.colorValue.ToString();
                    case SerializedPropertyType.ObjectReference:
                        if (p.objectReferenceValue == null) return "<null>";
                        var obj = p.objectReferenceValue;
                        string ap = AssetDatabase.GetAssetPath(obj);
                        if (!string.IsNullOrEmpty(ap)) return obj.name + " (Asset:" + ap + ")";
                        if (obj is Component c) return c.GetType().Name + " @ " + GetPath(c.transform);
                        if (obj is GameObject go) return "GameObject " + GetPath(go.transform);
                        return obj.name + " (" + obj.GetType().Name + ")";
                    case SerializedPropertyType.LayerMask: return p.intValue.ToString();
                    case SerializedPropertyType.Enum:
                        if (p.enumDisplayNames != null && p.enumValueIndex >= 0 && p.enumValueIndex < p.enumDisplayNames.Length)
                            return p.enumDisplayNames[p.enumValueIndex];
                        return p.intValue.ToString();
                    case SerializedPropertyType.Vector2:  return p.vector2Value.ToString();
                    case SerializedPropertyType.Vector3:  return p.vector3Value.ToString();
                    case SerializedPropertyType.Vector4:  return p.vector4Value.ToString();
                    case SerializedPropertyType.Rect:     return p.rectValue.ToString();
                    case SerializedPropertyType.Bounds:   return p.boundsValue.ToString();
#if UNITY_2021_1_OR_NEWER
                    case SerializedPropertyType.Quaternion: return p.quaternionValue.eulerAngles.ToString();
#endif
                    default: return "<unsupported>";
                }
            }
            catch { return "<read error>"; }
        }

        public static string SerializedToSimpleJson(SerializedProperty p)
        {
            try
            {
                switch (p.propertyType)
                {
                    case SerializedPropertyType.Integer: return p.intValue.ToString();
                    case SerializedPropertyType.Boolean: return p.boolValue ? "true" : "false";
                    case SerializedPropertyType.Float:   return p.floatValue.ToString("0.###");
                    case SerializedPropertyType.String:  return "\"" + JsonHulp.Escape(p.stringValue ?? "") + "\"";
                    case SerializedPropertyType.Color:   return "\"" + JsonHulp.Escape(p.colorValue.ToString()) + "\"";
                    case SerializedPropertyType.ObjectReference:
                        if (p.objectReferenceValue == null) return "null";
                        var obj = p.objectReferenceValue;
                        string ap = AssetDatabase.GetAssetPath(obj);
                        if (!string.IsNullOrEmpty(ap)) return "\"" + JsonHulp.Escape(obj.name + " (Asset:" + ap + ")") + "\"";
                        if (obj is Component c) return "\"" + JsonHulp.Escape(c.GetType().Name + " @ " + GetPath(c.transform)) + "\"";
                        if (obj is GameObject go) return "\"" + JsonHulp.Escape("GameObject " + GetPath(go.transform)) + "\"";
                        return "\"" + JsonHulp.Escape(obj.name + " (" + obj.GetType().Name + ")") + "\"";
                    case SerializedPropertyType.LayerMask: return p.intValue.ToString();
                    case SerializedPropertyType.Enum:
                        if (p.enumDisplayNames != null && p.enumValueIndex >= 0 && p.enumValueIndex < p.enumDisplayNames.Length)
                            return "\"" + JsonHulp.Escape(p.enumDisplayNames[p.enumValueIndex]) + "\"";
                        return p.intValue.ToString();
                    case SerializedPropertyType.Vector2:  return "\"" + JsonHulp.Escape(p.vector2Value.ToString()) + "\"";
                    case SerializedPropertyType.Vector3:  return "\"" + JsonHulp.Escape(p.vector3Value.ToString()) + "\"";
                    case SerializedPropertyType.Vector4:  return "\"" + JsonHulp.Escape(p.vector4Value.ToString()) + "\"";
                    case SerializedPropertyType.Rect:     return "\"" + JsonHulp.Escape(p.rectValue.ToString()) + "\"";
                    case SerializedPropertyType.Bounds:   return "\"" + JsonHulp.Escape(p.boundsValue.ToString()) + "\"";
#if UNITY_2021_1_OR_NEWER
                    case SerializedPropertyType.Quaternion: return "\"" + JsonHulp.Escape(p.quaternionValue.eulerAngles.ToString()) + "\"";
#endif
                    default: return null;
                }
            }
            catch { return null; }
        }
    }
}
#endif
