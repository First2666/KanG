SET NOCOUNT ON;

-- Insert an Admin user
INSERT INTO Users (Username, Email, PasswordHash, Role, CreatedAt)
VALUES (N'admin', N'admin@kanvue.com', N'admin123', 1, GETUTCDATE());

PRINT 'Admin user created successfully!';
