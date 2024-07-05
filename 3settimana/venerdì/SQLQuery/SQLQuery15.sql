-- Query 12: Cognome, Nome, Indirizzo, Data violazione, Importo e punti decurtati per tutte le violazioni che superino l’importo di 400 euro
SELECT a.Cognome, a.Nome, v.IndirizzoViolazione, v.DataViolazione, v.Importo, v.DecurtamentoPunti
FROM ANAGRAFICA a
JOIN VERBALE v ON a.idanagrafica = v.idanagrafica
WHERE v.Importo > 400;