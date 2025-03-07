USE AspireExample

IF NOT (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Player'))
BEGIN
    CREATE TABLE [dbo].[Player]
    (
        Id        INT PRIMARY KEY IDENTITY(1,1),
        FirstName VARCHAR(255) NOT NULL,
        LastName  VARCHAR(255) NOT NULL
    );

    INSERT INTO [dbo].[Player] (FirstName, LastName)
    VALUES
        ('LeBron', 'James'),
        ('Michael', 'Jordan'),
        ('Stephen', 'Curry'),
        ('Dwyane', 'Wade'),
        ('Charles', 'Barkley');
END