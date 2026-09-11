SET NOCOUNT ON;

-- Insert a test user
IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = 1)
BEGIN
    SET IDENTITY_INSERT Users ON;
    INSERT INTO Users (Id, Username, Email, PasswordHash, Role, CreatedAt)
    VALUES (1, N'testuser', N'test@example.com', N'hash', 0, GETUTCDATE());
    SET IDENTITY_INSERT Users OFF;
END

-- Insert a test trip plan
IF NOT EXISTS (SELECT 1 FROM TripPlans WHERE Id = 1)
BEGIN
    SET IDENTITY_INSERT TripPlans ON;
    INSERT INTO TripPlans (Id, UserId, PlanName, StartDate, EndDate, MemberCount, CreatedAt)
    VALUES (1, 1, N'ทริปเที่ยวกาญจนบุรีของฉัน', GETUTCDATE(), DATEADD(day, 2, GETUTCDATE()), 2, GETUTCDATE());
    SET IDENTITY_INSERT TripPlans OFF;
END

PRINT 'User and TripPlan created successfully!';
