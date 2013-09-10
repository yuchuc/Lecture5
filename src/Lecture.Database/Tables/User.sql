﻿CREATE TABLE [dbo].[User] (
  [Id] INT NOT NULL,
  [Email] NVARCHAR (256) NOT NULL,
  [Age] INT NOT NULL DEFAULT 13, 
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
)
GO

CREATE UNIQUE INDEX [IX_Email] ON [User] ([Email])
GO