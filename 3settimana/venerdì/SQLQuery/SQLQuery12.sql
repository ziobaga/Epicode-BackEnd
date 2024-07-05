-- Query 9: Query che visualizzi Data violazione, Importo e decurtamento punti relativi ad una certa data (esempio)
SELECT DataViolazione, Importo, DecurtamentoPunti
FROM VERBALE
WHERE DataViolazione = '2024-07-10';