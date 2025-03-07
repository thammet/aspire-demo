USE AspireExample

IF NOT (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Team'))
BEGIN
    CREATE TABLE [dbo].[Team]
    (
        Id        INT PRIMARY KEY IDENTITY(1,1),
        Name VARCHAR(255) NOT NULL,
        City  VARCHAR(255) NOT NULL
    );

    INSERT INTO [dbo].[Team] (Name, City)
    VALUES
        ('Lakers', 'Los Angeles'),
        ('Celtics', 'Boston'),
        ('Warriors', 'San Francisco'),
        ('Heat', 'Miami'),
        ('Knicks', 'New York');
END