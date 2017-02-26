CREATE PROCEDURE usp_GetOlder (@id int)
AS
BEGIN
  UPDATE Minions
  SET Age += 1
  WHERE Id = @id
END