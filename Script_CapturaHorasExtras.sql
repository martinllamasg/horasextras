
CREATE TABLE OvertimeEntryModels (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Shift NVARCHAR(50) NOT NULL,
    ReportDate DATE NOT NULL,
    Estado NVARCHAR(20) NOT NULL DEFAULT 'Borrador',
    CONSTRAINT UQ_ReportDate_Shift UNIQUE (ReportDate, Shift)
);

CREATE TABLE OvertimeRows (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Lunes INT NULL,
    Martes INT NULL,
    Miércoles INT NULL,
    Jueves INT NULL,
    Viernes INT NULL,
    Sábado INT NULL,
    Domingo INT NULL,
    Descripcion NVARCHAR(100),
    OvertimeEntryId INT NOT NULL,
    FOREIGN KEY (OvertimeEntryId) REFERENCES OvertimeEntryModels(Id) ON DELETE CASCADE
);
