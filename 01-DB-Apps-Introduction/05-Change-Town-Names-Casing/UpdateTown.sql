UPDATE Towns
SET Name = UPPER(Name)
WHERE Name = @name