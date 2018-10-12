
# VR
## 1. Fullscale und freier Teleport.
### Beschreibung : 
Naiver Navigations Ansatz, man soll sich selber im raum zurechtfinden, keine shortcuts nichts. 

### Erkenntnisse: 
Es ist insbesondere schwer sich auf den Treppen einen genauen Punkt auszusuchen und dort hinzukommen. 

Recht präzise auf kleinen Raum, aber extrem mühselig wenn größere Abschnitte begehbar seien sollen.

## 2. Fullscale und Teleport nur mit "festen Punkten" Waypoints
### Beschreibung: 
Statt ganz naiv und frei, gibt es jetzt kleine Punkte zu denen man sich hinteleportieren kann.

Erstes Problem, war wie stelle ich diese dar, wenn sie hinter Objekten sind? (Im Raum)?

-> Lösung: Größere Colldier drum herum, wenn anvisiert werden Teile der Wand ausgeblendet.

### Erkenntnisse: 
Nicht mehr ganz naiver Navigationsansatz, insbesondere hoch/runter geht sehr viel schneller und zuverlässiger.

Es ist aber sehr schwer in ein spezifisches Stockwerk zu kommen, denn man geht eher nur 1 hoch/runter, wenn man außen einen Zusätzlichen Punkt hätte, müsste man auch immer in ein Stockwerk rein, dann wieder raus in die Welt, um wieder in ein Stockwerk rein zu kommen..

## 3. Miniature Scale (Minimap mit Waypoints)
### Beschreibung: 
Statt an der richtigen Größe des Models zu hantieren, hat man ein kleines Minimap Model welches man benützt zum teleportieren.

### Erkenntnisse: 
Definitiv eine große Bereicherung, es ist sehr angenehm in egal welches Stockwerk sich zu teleportieren. 

Als ganze Teleportation erlaubt war, war es recht fummelig - deswegen lieber mit Waypoints.

Allerdings gibt es ein Performance Bottleneck,man braucht definitiv unterschiedliche LODs für Fullscale und Minimap Model.

