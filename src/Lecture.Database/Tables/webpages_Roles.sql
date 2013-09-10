CREATE TABLE [dbo].[webpages_Roles] (
  [RoleId] INT IDENTITY NOT NULL,
  [RoleName] NVARCHAR (256) NOT NULL,
  CONSTRAINT [PK_webpages_Roles] PRIMARY KEY ([RoleId]),
  CONSTRAINT [U_RoleName] UNIQUE ([RoleName])
)