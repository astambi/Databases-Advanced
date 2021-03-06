USE [master]
GO

CREATE DATABASE [Products.CodeFirst]
GO

USE [Products.CodeFirst]
GO

CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Distributor] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Distributor], [Description], [Price]) 
VALUES 
   (1, N'Apple', N'Kaufland', N'Fruit', CAST(2.20 AS Decimal(18, 2))),
   (2, N'Banana', N'Kaufland', N'Fruit', CAST(3.30 AS Decimal(18, 2))),
   (3, N'Red Tomato', N'Kaufland', N'Vegetable', CAST(2.50 AS Decimal(18, 2))),
   (4, N'Carrot', N'Kaufland', N'Vegetable', CAST(2.80 AS Decimal(18, 2))),
   (5, N'Millet', N'Zelen Bio', N'Cereal', CAST(3.30 AS Decimal(18, 2)))

SET IDENTITY_INSERT [dbo].[Products] OFF