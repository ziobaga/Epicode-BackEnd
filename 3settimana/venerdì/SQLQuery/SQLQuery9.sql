-- Query 6: Cognome, Nome, Indirizzo, Data violazione, importo e punti decurtati per le violazioni fatte tra il febbraio 2009 e luglio 2009 (esempio)
SELECT a.Cognome, a.Nome, v.IndirizzoViolazione, v.DataViolazione, v.Importo, v.DecurtamentoPunti
FROM ANAGRAFICA a
JOIN VERBALE v ON a.idanagrafica = v.idanagrafica
WHERE v.DataViolazione BETWEEN '2009-02-01' AND '2009-07-31';