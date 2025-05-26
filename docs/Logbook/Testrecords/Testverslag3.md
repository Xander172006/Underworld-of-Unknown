## Testverslag 3: Levensbalk en Combat

**Datum:** 22-05-2025  
**Geteste functionaliteit:**
- Levensbalk UI
- Combat tussen speler en vijand

### Testmethode:
Speler en vijand laten vechten, kijken of levensbalk juist afneemt bij schade.

### Resultaat:
- Levensbalk werkt en staat op de juiste plek.
- Bij schade wordt de levensbalk minder.
- Rode overlay verschijnt bij hit.

### Feedback:
De levensbalk liep soms niet synchroon met de daadwerkelijke health van de speler.

### Aanpassing:
De update van de levensbalk is nu direct gekoppeld aan de health-variabele van de speler, zodat deze altijd klopt.

### Opmerkingen:
- UI-elementen blijven netjes op hun plek, combat voelt responsief.