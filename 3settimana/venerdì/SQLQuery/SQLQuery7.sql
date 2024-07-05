-- Query 4: Totale dei punti decurtati per ogni anagrafica
SELECT a.Cognome, a.Nome, SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati
FROM ANAGRAFICA a
JOIN VERBALE v ON a.idanagrafica = v.idanagrafica
GROUP BY a.Cognome, a.Nome;