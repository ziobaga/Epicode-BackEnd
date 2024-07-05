-- Query 11: Cognome, Nome, Indirizzo, Data violazione, Importo e punti decurtati per tutte le violazioni che superino il decurtamento di 5 punti
SELECT a.Cognome, a.Nome, v.IndirizzoViolazione, v.DataViolazione, v.Importo, v.DecurtamentoPunti
FROM ANAGRAFICA a
JOIN VERBALE v ON a.idanagrafica = v.idanagrafica
WHERE v.DecurtamentoPunti > 5;