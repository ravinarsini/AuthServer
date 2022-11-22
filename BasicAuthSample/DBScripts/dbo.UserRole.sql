CREATE TABLE dbo.UserRole
(
	UserId INT NOT NULL,
	RoleId INT NOT NULL,

	CONSTRAINT PK_UserRole PRIMARY KEY(UserId, RoleId),
	CONSTRAINT FK_UserRole_Login FOREIGN KEY(UserId) REFERENCES dbo.Login(Id),
	CONSTRAINT FK_UserRole_Role FOREIGN KEY(RoleId) REFERENCES dbo.Role(Id)
)