-- Query 3: Conteggio dei verbali trascritti raggruppati per tipo di violazione
SELECT idviolazione, COUNT(*) AS NumeroVerbali
FROM VERBALE
GROUP BY idviolazione;