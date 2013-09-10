IF NOT EXISTS (SELECT * FROM [User] WHERE [Email] = 'heinz95729@gmail.com')
BEGIN
  INSERT INTO [User] ([Id], [Email]) VALUES (1, 'heinz95729@gmail.com')
  INSERT INTO [webpages_Membership] ([UserId], [CreateDate], [IsConfirmed], [PasswordFailuresSinceLastSuccess], [Password], [PasswordSalt])
  VALUES (1, GETUTCDATE(), 1, 0, 'AEKbR35glEFmtJWVqCJU7HozJk0Bvt4I3jK1iUqngPZBMd/obcg18whl4WfeTUtmsg==', '')
END
GO

IF NOT EXISTS (SELECT * FROM [User] WHERE [Email] = 'larry@outlook.com')
BEGIN
  INSERT INTO [User] ([Id], [Email]) VALUES (2, 'larry@outlook.com')
  INSERT INTO [webpages_Membership] ([UserId], [CreateDate], [IsConfirmed], [PasswordFailuresSinceLastSuccess], [Password], [PasswordSalt])
  VALUES (2, GETUTCDATE(), 1, 0, 'AEKbR35glEFmtJWVqCJU7HozJk0Bvt4I3jK1iUqngPZBMd/obcg18whl4WfeTUtmsg==', '')
END
GO

IF NOT EXISTS (SELECT * FROM [User] WHERE [Email] = 'moe@gmail.com')
BEGIN
  INSERT INTO [User] ([Id], [Email]) VALUES (3, 'moe@gmail.com')
  INSERT INTO [webpages_Membership] ([UserId], [CreateDate], [IsConfirmed], [PasswordFailuresSinceLastSuccess], [Password], [PasswordSalt])
  VALUES (3, GETUTCDATE(), 1, 0, 'AEKbR35glEFmtJWVqCJU7HozJk0Bvt4I3jK1iUqngPZBMd/obcg18whl4WfeTUtmsg==', '')
END
GO

DECLARE @RoleId INT
IF NOT EXISTS (SELECT * FROM [webpages_Roles] WHERE [RoleName] = 'Admin')
BEGIN
  INSERT INTO [webpages_Roles] (RoleName) VALUES ('Admin')
  SET @RoleId = SCOPE_IDENTITY()
  INSERT INTO [webpages_UsersInRoles] (UserId, RoleId) VALUES (1, @RoleId)
END
GO