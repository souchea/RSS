/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


-- UserCategory
ALTER TABLE FeedCategory
ADD CONSTRAINT FK_UserCategory 
FOREIGN KEY (UserId)
REFERENCES [User](Id)

--UserFeedSource
ALTER TABLE FeedSource
ADD CONSTRAINT FK_UserFeedSource
FOREIGN KEY (UserId)
REFERENCES [User](Id)

--CategoryFeedSource
ALTER TABLE FeedSource
ADD CONSTRAINT FK_CategoryFeedSource
FOREIGN KEY (CategoryId)
REFERENCES [FeedCategory](Id)

