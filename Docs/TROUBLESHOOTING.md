# 🔧 Troubleshooting – Nova Unity Tool

Dit is de **hulpgids** voor als iets niet werkt met de Nova Unity Tool.  
We leggen alles simpel uit, stap voor stap.

---

## 1️⃣ De tool verschijnt niet in Unity
**Mogelijke oorzaken:**
- Het script staat niet in een **`Editor`** map in je Unity-project.
- Unity is nog niet klaar met **compileren** (rechts onderin zie je een zandloper-icoon).

**Oplossing:**
1. Zorg dat alle scripts van de tool in een map staan met de naam `Editor`.
2. Wacht tot Unity klaar is met compileren (geen zandloper meer).
3. Ga naar **menu** → `STEFSTOOLS` → `Nova Export`.

---

## 2️⃣ Er gebeurt niets als ik op een knop klik
**Mogelijke oorzaken:**
- Er is een fout in de console (rode foutmelding).
- Er ontbreekt iets in je scène (bijv. je hebt geen actieve scene geopend).

**Oplossing:**
1. Open **Window → General → Console** in Unity.
2. Kijk of er een rode foutmelding staat.
3. Los de fout op (zie fouttekst of vraag hulp).
4. Zorg dat je een scene geopend hebt voordat je exporteert.

---

## 3️⃣ Ik krijg een foutmelding over `.meta` bestanden
**Goed nieuws:** De Nova Unity Tool **slaat .meta-bestanden automatisch over** in het exportpakket.  
Zie je ze toch? Dan heb je mogelijk handmatig bestanden toegevoegd.

**Oplossing:**
- Verwijder handmatig de `.meta` bestanden uit je zip als ze er nog in zitten.

---

## 4️⃣ Het zipbestand is leeg of mist bestanden
**Mogelijke oorzaken:**
- Je projectmap heeft geen schrijfrechten.
- Een antivirus- of beveiligingsprogramma blokkeert Unity.

**Oplossing:**
1. Controleer of Unity mag schrijven naar de map `Assets/` en je gekozen exportmap.
2. Zet tijdelijk je antivirus uit en probeer opnieuw.

---

## 5️⃣ AI begrijpt mijn project niet na upload
**Mogelijke oorzaken:**
- Je bent vergeten de juiste zin uit **Zet-in-chat.txt** te kopiëren.
- Je hebt een oud exportbestand geüpload.

**Oplossing:**
1. Open het nieuwste zipbestand dat de tool gemaakt heeft.
2. Kopieer de tekst uit `Zet-in-chat.txt` en plak deze als eerste in je nieuwe AI-chat.
3. Upload daarna het zipbestand.

---

## 6️⃣ Nog steeds problemen?
- Probeer Unity opnieuw op te starten.
- Maak een **nieuw project** met `Nieuw project starten` in de tool en test of het dan wel werkt.
- Vraag hulp in de GitHub-issues van deze tool.

💡 **Tip:** Hoe meer info je geeft (Unity-versie, screenshots, foutmeldingen), hoe sneller iemand je kan helpen.

