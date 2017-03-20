CREATE PROCEDURE usp_CountBooksByAuthor (@firstName NVARCHAR(MAX), @lastName NVARCHAR(MAX))
AS
BEGIN
   SELECT COUNT(*)
   FROM Authors AS a
   LEFT JOIN Books AS b ON a.Id = b.AuthorId
   WHERE a.FirstName = @firstName AND a.LastName = @lastName
END

--EXEC usp_CountBooksByAuthor "Amanda", "Rice"