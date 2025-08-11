#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;

namespace StefTools
{
    public class NovaVenster : EditorWindow
    {
        [MenuItem("STEFSTOOLS/Nova Export (simpel)")]
        public static void ShowWindow()
        {
            var w = GetWindow<NovaVenster>("Nova Export");
            w.minSize = new Vector2(600, 620);
        }

        Vector2 _scroll;

        void OnGUI()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            GUILayout.Label("Werk met Nova in 3 stappen", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "1) Kies hieronder wat je wilt doen.\n" +
                "2) De tool maakt 1 zip met alles wat Nova nodig heeft (zonder .meta).\n" +
                "3) Upload die zip in de chat en plak de tekst uit 'Zet-in-chat.txt'.",
                MessageType.Info);

            DrawBlock(
                "Nieuw project starten",
                "Gebruik deze na de allereerste brainstorm. Maakt lege Projectplan.txt, Scene-overzicht (txt+json), Werkbriefje.txt en Zet-in-chat.txt.",
                () => ExportMaker.DoExport("new"));

            DrawBlock(
                "Verder werken aan project",
                "Gebruik deze als je al een Projectplan.txt hebt. Leest de laatste Projectplan.txt (uit 'Assets/STEFSTOOLS/Latest' als die bestaat) en maakt nieuwe export.",
                () => ExportMaker.DoExport("continue"));

            DrawBlock(
                "Sessie afsluiten (klaar voor volgende keer)",
                "Gebruik deze als je stopt met werken. Maakt nieuw pakket met Werkbriefje (sjabloon), vers Scene-overzicht en Zet-in-chat.txt.",
                () => ExportMaker.DoExport("close"));

            EditorGUILayout.HelpBox(
                "Na Nova's antwoord: kopieer de nieuwe 'Projectplan.txt' uit de chat en vervang jouw lokale 'Assets/STEFSTOOLS/Latest/Projectplan.txt'.",
                MessageType.None);

            EditorGUILayout.EndScrollView();
        }

        void DrawBlock(string title, string help, Action run)
        {
            using (new GUILayout.VerticalScope("box"))
            {
                GUILayout.Label(title, EditorStyles.boldLabel);
                EditorGUILayout.LabelField(help, EditorStyles.wordWrappedLabel);
                GUILayout.Space(4);
                if (GUILayout.Button(title + "  Maak zip", GUILayout.Height(32)))
                {
                    try { run(); }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex);
                        EditorUtility.DisplayDialog("Er ging iets mis", ex.Message, "OK");
                    }
                }
            }
        }
    }
}
#endif
