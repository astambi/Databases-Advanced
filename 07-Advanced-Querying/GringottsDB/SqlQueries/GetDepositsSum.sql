USE Gringotts

SELECT 
   DepositGroup, 
   SUM(DepositAmount) AS DepositsSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander Family'
GROUP BY DepositGroup