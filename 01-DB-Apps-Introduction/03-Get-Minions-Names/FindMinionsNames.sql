SELECT m.Name, m.Age
FROM MinionsVillains AS mv 
JOIN Minions AS m ON m.Id = mv.MinionId
WHERE mv.VillainId = @villainId