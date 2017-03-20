USE Gringotts

SELECT 
   DepositGroup, 
   SUM(DepositAmount) AS DepositsSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander Family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY DepositsSum DESC