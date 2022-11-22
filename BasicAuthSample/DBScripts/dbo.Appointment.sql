CREATE TABLE dbo.Appointment
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	UserId INT NOT NULL,
	DoctorId INT NOT NULL,
	AppointmentDate DATETIME NOT NULL,
	CreatedBy INT NOT NULL,
	CreatedDate DATETIME NOT NULL,
	ModifiedBy INT NOT NULL,
	ModifiedDate DATETIME NOT NULL,

	CONSTRAINT FK_Appointment_Login FOREIGN KEY (UserId) REFERENCES dbo.Login(Id),
	CONSTRAINT FK_Appointment_Login_Created FOREIGN KEY (CreatedBy) REFERENCES dbo.Login(Id),
	CONSTRAINT FK_Appointment_Login_Modified FOREIGN KEY (ModifiedBy) REFERENCES dbo.Login(Id),
	CONSTRAINT FK_Appointment_Login_Doctor FOREIGN KEY (DoctorId) REFERENCES dbo.Login(Id),
)