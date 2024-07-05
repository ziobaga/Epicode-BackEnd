-- Query 5: Cognome, Nome, Data violazione, Indirizzo violazione, importo e punti decurtati per tutti gli anagrafici residenti a Palermo
SELECT a.Cognome, a.Nome, v.DataViolazione, v.IndirizzoViolazione, v.Importo, v.DecurtamentoPunti
FROM ANAGRAFICA a
JOIN VERBALE v ON a.idanagrafica = v.idanagrafica
WHERE a.Città = 'Palermo';