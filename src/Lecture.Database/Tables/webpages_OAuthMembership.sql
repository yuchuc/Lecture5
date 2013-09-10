CREATE TABLE [dbo].[webpages_OAuthMembership] (
  [Provider] NVARCHAR (30) NOT NULL,
  [ProviderUserId] NVARCHAR (100) NOT NULL,
  [UserId] INT NOT NULL,
  CONSTRAINT [PK_webpages_OAuthMembership] PRIMARY KEY ([Provider], [ProviderUserId]),
  CONSTRAINT [FK_webpages_OAuthMembership_User] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
)