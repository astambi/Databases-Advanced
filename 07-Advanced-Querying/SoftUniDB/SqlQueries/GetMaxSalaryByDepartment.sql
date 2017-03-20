USE SoftUni

SELECT 
   d.DepartmentID, d.Name,
   MAX(e.Salary) AS MaxSalary
FROM Employees AS e
JOIN Departments as d on e.DepartmentID = d.DepartmentID
GROUP BY d.DepartmentID, d.Name
HAVING MAX(e.Salary) < 30000 OR MAX(e.Salary) > 70000