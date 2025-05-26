# Daglog - [22-05-2025] Underworld of Mysteries

_Tijd besteed: [4 uur]_

## Inleiding:

Voor vandaag heb ik dus wat ik gisteren niet kon doen afgemaakt, namelijk de levensbalk van de speler met daarbij de combat logica.
Ook had ik nog wat extra tijd genomen om wat extra assets te maken voor de environment en decoratieve props.

<br>

## **Uitgevoerde taken:**
- Levensbalk UI toegevoegd aan de speler
- Levensbalk Functionaliteit gemaakt
- Combat logica gemaakt voor de speler en vijanden

### Bonus:
- Decoratieve props gemaakt voor de omgeving

<br>

## **Problemen/Oplossingen:**

### Problemen:

- `HealthBar` Ik zat nogal wat vast te lopen met de untiy editor en hoe de UI werkte voor de levensbalk. Ik dacht namelijk dat als ik een main canvas element had dat ik die ook zo moest verschuiven in het level zelf. <br>
Maar dit bleek niet het geval te zijn, omdat de UI elementen altijd op een vaste plek staan in de game. <br>

- `Combat System` De combat logica was vervellend in het begin, omdat ik niet makelijk kon zien wanner ik of de vijand geraakt werd



### Oplossingen:

- Ik moest dus in dit geval voor de levensbalk hem zo positioneren in de canvas, zodat wanneer ik het spel speel de levensbalk ook op de juiste plek linksboven stond.
- Mijn oplossing voor de combat logica kwam uiteindelijk in het brengen van een rode overlay op de entity als die geraakt werd, en dat hielp mij enorm.

<br>

## **Volgende stappen:**

De volgende stappen voor dag 5 is om de Game Over logica te maken met daarbij ook een reset knop om het spel opnieuw te starten.
Ook moeten de vijanden hun code logica nog wat verbeterd worden; Daarbij denk ik aan bijv niet aanvallen als de speler al dood is.

- Game Over menu maken
- Reset knop maken
- Vijanden `NinjaEnemyScript` hun code logica verbeteren
