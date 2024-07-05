-- Query 7: Totale degli importi per ogni anagrafico
SELECT a.Cognome, a.Nome, SUM(v.Importo) AS TotaleImporti
FROM ANAGRAFICA a
JOIN VERBALE v ON a.idanagrafica = v.idanagrafica
GROUP BY a.Cognome, a.Nome;