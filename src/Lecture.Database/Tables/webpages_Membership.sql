CREATE TABLE [dbo].[webpages_Membership] (
  [UserId] INT NOT NULL,
  [CreateDate] DATETIME,
  [ConfirmationToken] NVARCHAR (128),
  [IsConfirmed] BIT DEFAULT 0,
  [LastPasswordFailureDate] DATETIME,
  [PasswordFailuresSinceLastSuccess] INT DEFAULT 0 NOT NULL,
  [Password] NVARCHAR (128) NOT NULL,
  [PasswordChangedDate] DATETIME,
  [PasswordSalt] NVARCHAR (128) NOT NULL,
  [PasswordVerificationToken] NVARCHAR (128),
  [PasswordVerificationTokenExpirationDate] DATETIME,
  CONSTRAINT [PK_webpages_Membership] PRIMARY KEY ([UserId]),
  CONSTRAINT [FK_webpages_Membership_User] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
)