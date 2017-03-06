USE [Products.CodeFirst]

--ALTER TABLE Products
--ADD Quantity FLOAT NULL, Weight FLOAT NULL;

UPDATE Products
SET Quantity = 0
WHERE Quantity IS NULL;

ALTER TABLE Products
ALTER COLUMN Quantity FLOAT NOT NULL;

UPDATE Products
SET Weight = 0
WHERE Weight IS NULL;

ALTER TABLE Products
ALTER COLUMN Weight FLOAT NOT NULL;