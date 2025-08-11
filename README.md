# Nova Unity Tool – Blueprint & Scene Export

Doel  
Deze Unity Editor-tool maakt in één klik een ZIP met:
- Projectplan (Blueprint)
- Werkbriefje (korte notities)
- Scene-overzicht (leesbaar en JSON)
- Scripts en shaders die in de scene gebruikt worden
- Een kort tekstje om in de chat te plakken

Zo kan je een AI (zoals Nova) je project meteen laten begrijpen en verder laten bouwen, zonder alles uit te leggen.

## Installeren (korte versie)
1. Kopieer de map `Editor/StefTools/` uit deze repo naar jouw Unity-project:  
   `Assets/Editor/StefTools/`
2. Wacht tot Unity klaar is met compilen.
3. Menu in Unity: **STEFSTOOLS → Nova Export (simpel)**.

## Gebruiken (korte versie)
- **Nieuw project starten**: maakt lege Projectplan.txt, Werkbriefje.txt, Scene-overzicht (txt+json) en Zet-in-chat.txt.  
- **Verder werken aan project**: leest je laatste Projectplan en maakt nieuwe export.  
- **Sessie afsluiten**: maakt pakket klaar voor de volgende chat.

Uitgebreide uitleg staat in `Docs/INSTALL.txt` en `Docs/USAGE.txt`.

## Mapindeling
- `Editor/StefTools/` – de Unity-tool scripts (zet je in `Assets/Editor/StefTools/`)  
- `Docs/` – korte handleidingen (installeren, gebruiken, opnieuw opbouwen)  
- `Blueprints/Starter/` – start-templates (lege blueprint, werkbriefje, projectplan)

## Licentie
Vrij te gebruiken binnen je eigen projecten. Geen garanties.

## Voorbeelden
- [Starter (lege templates)](Blueprints/Starter)
- [Ingevulde voorbeelden](Blueprints/Voorbeelden-Ingevuld)
- [Ballonvaart-voorbeeld](Blueprints/Ballonvaart-Project/Voorbeelden-Ingevuld)
