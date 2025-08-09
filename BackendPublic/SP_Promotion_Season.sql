------------SEASON PROCEDURES-------------

CREATE PROCEDURE GetAllCompleteSeasons
AS
BEGIN
    SELECT 
        SeasonID,
        SeasonName,
        StartDate,
        EndDate,
        [Percent],
        IsActive,
        IsHigh
    FROM dbo.Season;
END;

CREATE PROCEDURE GetCompleteSeasonById
    @SeasonID INT
AS
BEGIN
    SELECT 
        SeasonID,
        SeasonName,
        StartDate,
        EndDate,
        [Percent],
        IsActive,
        IsHigh
    FROM Season
    WHERE SeasonID = @SeasonID;
END;


CREATE PROCEDURE CreateSeason
    @SeasonName NVARCHAR(100),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @Percent DECIMAL(10, 2),
    @IsActive BIT,
    @IsHigh BIT
AS
BEGIN
    INSERT INTO Season (SeasonName, StartDate, EndDate, [Percent], IsActive, IsHigh)
    VALUES (@SeasonName, @StartDate, @EndDate, @Percent, @IsActive, @IsHigh);
END;


CREATE PROCEDURE UpdateSeason
    @SeasonID INT,
    @SeasonName NVARCHAR(100),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @Percent DECIMAL(10, 2),
    @IsActive BIT,
    @IsHigh BIT
AS
BEGIN
    UPDATE Season
    SET 
        SeasonName = @SeasonName,
        StartDate = @StartDate,
        EndDate = @EndDate,
        [Percent] = @Percent,
        IsActive = @IsActive,
        IsHigh = @IsHigh
    WHERE SeasonID = @SeasonID;
END;

CREATE PROCEDURE DeleteSeason
    @SeasonID INT
AS
BEGIN
    DELETE FROM Season
    WHERE SeasonID = @SeasonID;
END;


------------PROMOTION PROCEDURES-------------

-- Obtener todos
CREATE PROCEDURE GetAllPromotions
AS
BEGIN
    SELECT * FROM Promotion;
END;

-- Obtener por ID
CREATE PROCEDURE GetPromotionById
    @PromotionID INT
AS
BEGIN
    SELECT * FROM Promotion WHERE PromotionID = @PromotionID;
END;

-- Crear promoción
CREATE PROCEDURE CreatePromotion
    @PromotionName NVARCHAR(255),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @IsActive BIT,
    @Percent INT,
    @Img NVARCHAR(255)
AS
BEGIN
    INSERT INTO Promotion (PromotionName, StartDate, EndDate, IsActive, [Percent], Img)
    VALUES (@PromotionName, @StartDate, @EndDate, @IsActive, @Percent, @Img);

    SELECT SCOPE_IDENTITY();
END;

-- Actualizar promoción
CREATE PROCEDURE UpdatePromotion
    @PromotionID INT,
    @PromotionName NVARCHAR(255),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @IsActive BIT,
    @Percent INT,
    @Img NVARCHAR(255)
AS
BEGIN
    UPDATE Promotion
    SET PromotionName = @PromotionName,
        StartDate = @StartDate,
        EndDate = @EndDate,
        IsActive = @IsActive,
        [Percent] = @Percent,
        Img = @Img
    WHERE PromotionID = @PromotionID;
END;

-- Eliminar promoción
CREATE PROCEDURE DeletePromotion
    @PromotionID INT
AS
BEGIN
    DELETE FROM Promotion WHERE PromotionID = @PromotionID;
END;
