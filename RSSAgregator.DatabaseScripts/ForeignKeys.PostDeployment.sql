
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

