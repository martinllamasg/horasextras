
CREATE TABLE OvertimeRecords (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(100) NOT NULL,
    WeekDate DATE NOT NULL,
    Days NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    FilePath NVARCHAR(255),
    HoursPerDayJson NVARCHAR(MAX),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
