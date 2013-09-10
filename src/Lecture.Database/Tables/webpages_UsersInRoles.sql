CREATE TABLE [dbo].[webpages_UsersInRoles] (
  [UserId] INT NOT NULL,
  [RoleId] INT NOT NULL,
  CONSTRAINT [PK_webpages_UsersInRoles] PRIMARY KEY ([UserId], [RoleId]),
  CONSTRAINT [FK_webpages_UsersInRoles_webpages_Roles] FOREIGN KEY ([RoleId]) REFERENCES [webpages_Roles] ([RoleId]),
  CONSTRAINT [FK_webpages_UsersInRoles_User] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
)