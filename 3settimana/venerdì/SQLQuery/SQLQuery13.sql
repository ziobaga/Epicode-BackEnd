-- Query 10: Conteggio delle violazioni contestate raggruppate per Nominativo dell’agente di Polizia
SELECT Nominativo_Agente, COUNT(*) AS NumeroViolazioni
FROM VERBALE
GROUP BY Nominativo_Agente;