﻿CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Role] NVARCHAR(50) NOT NULL, 
    [Username] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(50) NOT NULL, 
    [FacebookId] NVARCHAR(50) NULL, 
    [TwitterId] NVARCHAR(50) NULL, 
    [GoogleId] NVARCHAR(50) NULL, 
    [SignUpDate] DATETIME NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [LastSignInDate] DATETIME NOT NULL
)
