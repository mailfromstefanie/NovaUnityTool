# WAT IS STEFSTOOLS – SCENE ANALYZER?
Een **één-klik scene-inspector** voor Unity (werkt ook uitstekend voor VRChat-projecten).  
De tool maakt één **JSON-bestand** met alles wat een helper of AI nodig heeft om jouw scene te begrijpen – zodat je geen stapels screenshots of lange verhalen meer hoeft te sturen.

---

## WAAROM JSON?
- Zeer leesbaar voor AI en scripts.
- Compact en makkelijk te delen.
- Bevat alle info in één bestand, geen losse CSV/TXT die niemand gebruikt.

---

## VOOR WIE IS HET?
- **Beginners:** klik één knop, kies waar het rapport wordt opgeslagen.  
- **Helpers / Technical Artists:** volledig gestructureerd beeld van de scene in één bestand.  
- **VRChat-makers:** extra checks (VRC-detectie + Unity LTS-advies).

---

## HOE GEBRUIKEN (EASY MODE – STANDAARD)
1. Open de scene die je wilt analyseren.  
2. Ga in Unity naar: **STEFSTOOLS → Scene Analyzer → Open Scene Analyzer**.  
3. Klik op **"Analyseer & Bewaar Rapport Als…"** en kies een locatie.  
4. Klaar! (Er wordt ook een kopie opgeslagen in `Assets/STEFSTOOLS/STEFSLOGS/`.)

**Easy Mode** is super eenvoudig: één knop, geen afleiding.

---

## PRO MODE (VOOR GEVORDERDEN)
Extra knoppen:
- **Analyze Scene — Make Report (JSON):** maakt direct een rapport in de logs-map.
- **Analyze & Save Report As…:** analyse + zelf opslaglocatie kiezen.
- **See What Changed Since Last Report (Diff JSON):** laat zien wat er is veranderd sinds het vorige rapport (incl. ScriptChanges).
- **Save Latest Report As…:** kopie van de laatste baseline opslaan op een zelfgekozen locatie.
- **Open Logs Folder**, Easy/Pro handleiding.

---

## WAT STAAT ER IN HET JSON-RAPPORT?

### **Projectinformatie**
- Unity-versie, render pipeline, build target, quality level.
- VRChat-detectie (controleert VRC-pakketten in `Packages/manifest.json`).
- Korte LTS-compatibiliteitsmelding.
- Lichtinstellingen (aantal lightmaps, realtime/baked GI-info).

### **Per GameObject**
- Identiteit & status: pad, naam, parent, actief/niet-actief, tag, layer.
- Vlaggen & prefab-info: staticFlags, prefabStatus (incl. overrides), ontbrekende scripts.
- Transform: positie, rotatie, schaal.
- Performance: vertex/triangle-telling (MeshFilter + SkinnedMeshRenderer).
- Lijsten: componenten, scripts, materials, vrcComponents (detectie van VRC/Udon-achtige types).

### **Renderer & Materials**
- Renderer: enabled, lightmapIndex, lightmapScaleOffset, lightProbeUsage, reflectionProbeUsage.
- Per materiaal: shader, shaderKeywords, textures (naam, property, breedte/hoogte/formaat, importer-info: compressie/mipmaps/sRGB).

### **ScriptBindings (debug-focus)**
Per MonoBehaviour:
- Enabled-status.
- Geserialiseerde velden met waarden.
- Primitives/enums/strings/vectors als tekst.
- Object-referenties → scene-pad of asset-pad.
- Arrays volledig opgelijst.
- UnityEvents (bijv. Button.onClick) met target pad/type + methode.

### **scriptsCatalog (volledige broncode)**
- Per gebruikt script: typeName, fileName, assetPath, code.
- Als een script uit een precompiled package komt: melding dat broncode niet beschikbaar is.

### **Diff JSON**
- Toegevoegde / verwijderde object-paden.
- Gewijzigde velden (actief/tag/layer/transform/components/materials/verts/tris).
- scriptChanges: fieldChanges, referenceChanges, eventChanges.

---

## VRCHAT-SPECIFIEKE FUNCTIES
- Detecteert VRC aan de hand van bekende pakketnamen in `manifest.json`.
- Geeft LTS-advies: “OK voor VRChat LTS” of “Check VCC release notes”.

---

## WAAR WORDEN BESTANDEN OPGESLAGEN?
- **Standaard:** `Assets/STEFSTOOLS/STEFSLOGS/`
- **Easy Mode:** jij kiest de locatie, + kopie in logs-map.
- **Laatste baseline:** `STEF_LATEST.json` (voor Diff-functie).

---

## PERFORMANCE & PRIVACY
- Alleen in de Editor, read-only; verandert niets aan de scene.
- Snelheid hangt af van scene-grootte (veel meshes/materials = iets langer).
- Let op: bevat scriptbroncode; deel alleen met vertrouwde mensen.
- Toekomst: schakeloptie “Include script code: Ja/Nee”.

---

## MENU-OVERZICHT (HUIDIGE VERSIE)
**STEFSTOOLS**
- **Scene Analyzer**
  - Open Scene Analyzer (Easy Mode)
  - Analyze & Save Report As…
  - Analyze Scene — Make Report (JSON) (Pro)
  - See What Changed Since Last Report (Diff JSON) (Pro)
  - Save Latest Report As… (Pro)
  - Open Logs Folder (Pro)
- **Setup**
  - Generate Package Skeleton (mappen, README, asmdef, handleidingen, icon-slot)

---

## ICON
Plaats op:  
`Assets/STEFSTOOLS/Editor/stefstools_icon.png` (32×32, transparant)

---

## ROADMAP – TOOLS DIE ERBIJ KOMEN IN HET STEFSTOOLS-MENU
- Sun & Moon Lighting Setter
- FBX Material Wizard
- VRChat Validators
- Optimalisatie-helpers
- Export-instellingen (privacy)
- Packaging & Branding
