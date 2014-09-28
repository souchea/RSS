CREATE TABLE [dbo].[FeedSource]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [UserId] INT NOT NULL, 
    [CategoryId] INT NOT NULL, 
    [Url] NVARCHAR(MAX) NOT NULL, 
    [Public] TINYINT NOT NULL, 
    [CreationDate] DATETIME NOT NULL
)
