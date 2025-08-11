# Nova Unity Tool – Blueprint & Scene Export

Doel
----
Unity Editor tool die in één keer alle nuttige project-informatie exporteert:
- Projectplan (Blueprint)
- Werkbriefje (korte notities voor de volgende sessie)
- Scene-overzicht (leesbaar en JSON)
- Scripts en shaders die in de scene gebruikt worden
- Een korte tekst om in een AI-chat te plakken

Zo kan een AI het project direct begrijpen en verder helpen, zonder dat je zelf alles hoeft uit te leggen.

Mapindeling
-----------
- Editor/StefTools/ : C# Editor-scripts voor de tool (kopieer later naar Assets/Editor/StefTools/ in je Unity-project)
- Docs/             : Handleidingen (installeren, gebruiken, opnieuw opbouwen)
- Blueprints/Starter: Startdocumenten (Blueprint, Werkbriefje, Projectplan) als basis

Snel starten (korte versie)
---------------------------
1) Open je Unity-project.
2) Kopieer de map `Editor/StefTools/` uit deze repo naar `Assets/Editor/StefTools/`.
3) In Unity menu: STFSTOOLS → Nova Export (simpel).
4) Gebruik:
   - Nieuw project starten
   - Verder werken aan project
   - Sessie afsluiten
5) De tool maakt één zip met duidelijke Nederlandse bestandsnamen en een tekstje om in de chat te plakken.

Meer uitleg staat in `Docs/INSTALL.txt` en `Docs/USAGE.txt`.

Licentie
--------
Vrij te gebruiken binnen jouw eigen projecten. Geen garanties.
