CREATE TABLE [dbo].[FeedCategory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Name] VARCHAR(50) NOT NULL, 
    [UserId] INT NOT NULL, 
    [Public] TINYINT NOT NULL, 
    [CreationDate] DATETIME NOT NULL
)
