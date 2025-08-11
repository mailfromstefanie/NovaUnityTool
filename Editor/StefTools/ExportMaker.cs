#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Text;

namespace StefTools
{
    public static class ExportMaker
    {
        public static void DoExport(string mode) // "new" | "continue" | "close"
        {
            BestandenHulp.EnsureSceneOpen();

            string defaultName = "NovaExport_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".zip";
            string saveTo = EditorUtility.SaveFilePanel(
                "Kies waar je de zip opslaat",
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                defaultName,
                "zip");
            if (string.IsNullOrEmpty(saveTo)) return;

            string workingRoot = Path.Combine(Application.dataPath, "STEFSTOOLS/Latest");
            if (Directory.Exists(workingRoot)) { try { Directory.Delete(workingRoot, true); } catch {} }
            Directory.CreateDirectory(workingRoot);

            File.WriteAllText(Path.Combine(workingRoot, "Projectplan.txt"),
                mode == "new" ? BuildProjectplanEmpty() : LoadExistingProjectplanOrEmpty(),
                Encoding.UTF8);

            File.WriteAllText(Path.Combine(workingRoot, "Werkbriefje.txt"),
                BuildWerkbriefjeTemplate(), Encoding.UTF8);

            File.WriteAllText(Path.Combine(workingRoot, "Scene-overzicht.txt"),
                SceneLezer.BuildSceneReportReadable(), Encoding.UTF8);

            File.WriteAllText(Path.Combine(workingRoot, "Scene-overzicht.json"),
                SceneLezer.BuildSceneReportJson(), Encoding.UTF8);

            string codeDir = Path.Combine(workingRoot, "Scripts-en-Shaders");
            Directory.CreateDirectory(codeDir);
            BestandenHulp.CollectScriptsAndShaders(codeDir);

            File.WriteAllText(Path.Combine(workingRoot, "Zet-in-chat.txt"),
                BuildPromptText(mode), Encoding.UTF8);

            if (File.Exists(saveTo)) File.Delete(saveTo);
            BestandenHulp.ZipWithoutMeta(workingRoot, saveTo);

            EditorUtility.DisplayDialog("Klaar",
                "Bestand gemaakt:\n" + saveTo + "\n\nUpload deze zip in de chat en plak de tekst uit 'Zet-in-chat.txt'.",
                "OK");
            EditorUtility.RevealInFinder(saveTo);
        }

        static string BuildProjectplanEmpty()
        {
            string project = Application.productName;
            string version = PlayerSettings.bundleVersion;
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            var sb = new StringBuilder();
            sb.AppendLine("BEGIN PROJECTPLAN");
            sb.AppendLine("PROJECT: " + project + "  |  VERSIE: v" + version + " - " + date);
            sb.AppendLine();
            sb.AppendLine("SNAPSHOT:");
            sb.AppendLine("- [Door Nova in te vullen]");
            sb.AppendLine();
            sb.AppendLine("WAT DOEN WE NU (3):");
            sb.AppendLine("1) [Door Nova in te vullen]");
            sb.AppendLine("2) [Door Nova in te vullen]");
            sb.AppendLine("3) [Door Nova in te vullen]");
            sb.AppendLine();
            sb.AppendLine("DOEL VAN DIT PROJECT:");
            sb.AppendLine("- [Door Nova in te vullen]");
            sb.AppendLine();
            sb.AppendLine("EXTRA:");
            sb.AppendLine("- [Door Nova in te vullen]");
            sb.AppendLine("END PROJECTPLAN");
            return sb.ToString();
        }

        static string LoadExistingProjectplanOrEmpty()
        {
            string latest = Path.Combine(Application.dataPath, "STEFSTOOLS/Latest/Projectplan.txt");
            if (File.Exists(latest)) return File.ReadAllText(latest, Encoding.UTF8);
            return BuildProjectplanEmpty();
        }

        static string BuildWerkbriefjeTemplate()
        {
            var sb = new StringBuilder();
            sb.AppendLine("WERKBRIEFJE (vul in wat je wilt delen)");
            sb.AppendLine("Veranderd:");
            sb.AppendLine("- [Wat heb je aangepast sinds de vorige keer?]");
            sb.AppendLine();
            sb.AppendLine("Waar loop je vast:");
            sb.AppendLine("- [Wat werkt niet / wat is lastig?]");
            sb.AppendLine();
            sb.AppendLine("Wat moet Nova nu doen:");
            sb.AppendLine("- [Wat wil je dat Nova NU oppakt?]");
            sb.AppendLine();
            sb.AppendLine("Niet aanpassen:");
            sb.AppendLine("- [Wat moet blijven zoals het is?]");
            sb.AppendLine();
            sb.AppendLine("Overig:");
            sb.AppendLine("- [Extra opmerkingen of ideeën]");
            return sb.ToString();
        }

        static string BuildPromptText(string mode)
        {
            string line =
                (mode == "new") ? "Gebruik dit als basis. Dit is mijn eerste export voor Nova."
              : (mode == "continue") ? "Gebruik dit als basis. Dit is mijn laatste export voor Nova (verder werken)."
              : "Gebruik dit als basis. Dit is mijn laatste export voor Nova (sessie afsluiten).";

            var sb = new StringBuilder();
            sb.AppendLine(line);
            sb.AppendLine("(Upload deze zip en plak deze tekst in de chat)");
            sb.AppendLine();
            sb.AppendLine("Belangrijk:");
            sb.AppendLine("- Aan het einde van de sessie geeft Nova jou een nieuwe Projectplan.txt.");
            sb.AppendLine("- Open in Unity: Assets/STEFSTOOLS/Latest/");
            sb.AppendLine("- Vervang daar het oude Projectplan.txt met de nieuwe die Nova jou gaf.");
            return sb.ToString();
        }
    }
}
#endif
