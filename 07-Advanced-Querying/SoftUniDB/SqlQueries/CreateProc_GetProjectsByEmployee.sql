CREATE PROCEDURE usp_GetProjectsByEmployee (@firstName VARCHAR(MAX), @lastName VARCHAR(MAX))
AS
BEGIN
   SELECT p.Name, p.Description, p.StartDate
   FROM Employees AS e
   LEFT JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
   LEFT JOIN Projects AS p ON ep.ProjectID = p.ProjectID
   WHERE e.FirstName = @firstName AND e.LastName = @lastName
END

--EXEC usp_GetProjectsByEmployee "Ruth", "Ellerbrock"