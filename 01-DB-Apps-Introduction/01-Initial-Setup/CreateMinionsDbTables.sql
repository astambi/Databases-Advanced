--CREATE DATABASE MinionsDB
--NB! do not use GO! Does not work in VS & Judge, only in SSMS

USE MinionsDB

CREATE TABLE Towns(
  Id int IDENTITY,
  Name nvarchar(50) NOT NULL UNIQUE,
  CountryName nvarchar(50),
  CONSTRAINT PK_Towns PRIMARY KEY (Id)
)

CREATE TABLE Minions(
  Id int IDENTITY,
  Name nvarchar(50) NOT NULL UNIQUE, 
  Age int NOT NULL, 
  TownId int NOT NULL,
  CONSTRAINT PK_Minions PRIMARY KEY (Id),
  CONSTRAINT FK_Minions_Towns FOREIGN KEY (TownId) REFERENCES Towns(Id)
)

CREATE TABLE Villains(
  Id int IDENTITY,
  Name nvarchar(50) NOT NULL UNIQUE,
  EvilnessFactor varchar(15) CHECK (EvilnessFactor IN ('good', 'bad', 'evil', 'super evil')),
  CONSTRAINT PK_Villains PRIMARY KEY (Id)
)

CREATE TABLE MinionsVillains(
  MinionId int NOT NULL,
  VillainId int NOT NULL,
  CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId),
  CONSTRAINT FK_MinionsVillains_Minions FOREIGN KEY (MinionId) REFERENCES Minions(Id),
  CONSTRAINT FK_MinionsVillains_Villains FOREIGN KEY (VillainId) REFERENCES Villains(Id)
)

INSERT INTO Towns (Name, CountryName) 
VALUES
  ('Munich', 'Germany'), 
  ('Frankfurt', 'Germany'),
  ('Paris', 'France'), 
  ('Arles', 'France'),
  ('Barcelona', 'Spain'),
  ('Lisbon', 'Portugal'), 
  ('Sintra', 'Portugal'),
  ('Florence', 'Italy'), 
  ('Rome', 'Italy')

INSERT INTO Minions (Name, Age, TownId)
VALUES
  ('Tom', 18, 1),
  ('Bob', 19, 2),
  ('Mary', 20, 3),
  ('Peter', 21, 4),
  ('Elsa', 22, 3),
  ('Ida', 23, 5),
  ('Steve', 24, 6)

INSERT INTO Villains (Name, EvilnessFactor)
VALUES
  ('Ole', 'good'),
  ('Evil Devil', 'super evil'),
  ('Sorceress', 'bad'),
  ('Magician', 'good'),
  ('Chaos', 'bad'),
  ('Cute Villain', 'good')

INSERT INTO MinionsVillains (MinionId, VillainId)
VALUES
  (1, 1),
  (1, 2),
  (1, 3),
  (1, 4),
  (1, 5),
  (2, 1),
  (2, 2),
  (2, 3),
  (2, 4),
  (2, 5),
  (3, 1),
  (3, 3),
  (3, 4),
  (3, 5),
  (4, 1),
  (4, 3),
  (4, 4),
  (5, 1),
  (5, 3),
  (5, 4),
  (6, 1),
  (6, 4),
  (7, 1),
  (7, 3)