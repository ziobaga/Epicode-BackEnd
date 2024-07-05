-- Query 2: Conteggio dei verbali trascritti raggruppati per anagrafica
SELECT idanagrafica, COUNT(*) AS NumeroVerbali
FROM VERBALE
GROUP BY idanagrafica;