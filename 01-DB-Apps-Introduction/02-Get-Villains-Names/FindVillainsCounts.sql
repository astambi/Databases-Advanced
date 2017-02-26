SELECT v.Id, v.Name, COUNT(mv.MinionId) AS NumberOfMinions
FROM Villains AS v
JOIN MinionsVillains AS mv ON mv.VillainId = v.Id
GROUP BY v.Id, v.Name
HAVING COUNT(mv.MinionId) > 3
ORDER BY NumberOfMinions DESC