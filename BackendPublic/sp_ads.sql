
--SP obtener ads
CREATE PROCEDURE [dbo].[sp_GetAdminAds]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        AdID,
        [Name],
        StartDate,
        EndDate,
        IsActive,
        Img,
        ImgUrl
    FROM 
        [dbo].[Ad];
END

exec sp_GetAdminAds;
drop procedure sp_GetAdminAds;

CREATE PROCEDURE [dbo].[sp_GetAdById]
    @AdID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        AdID,
        [Name],
        StartDate,
        EndDate,
        IsActive,
        Img,
        ImgUrl
    FROM 
        [dbo].[Ad]
    WHERE 
        AdID = @AdID;
END

exec sp_GetAdById 2;

CREATE PROCEDURE [dbo].[sp_UpdateAd]
    @AdID INT,
    @Name NVARCHAR(MAX),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @IsActive BIT,
    @Img NVARCHAR(MAX),
    @ImgUrl NVARCHAR(MAX)
AS
BEGIN

    UPDATE [dbo].[Ad]
    SET 
        [Name] = @Name,
        StartDate = @StartDate,
        EndDate = @EndDate,
        IsActive = @IsActive,
        Img = @Img,
        ImgUrl = @ImgUrl
    WHERE 
        AdID = @AdID;
END

drop procedure sp_UpdateAd

CREATE PROCEDURE [dbo].[sp_DeleteAd]
    @AdID INT
AS
BEGIN

    DELETE FROM [dbo].[Ad]
    WHERE AdID = @AdID;
END

drop procedure sp_DeleteAd;

CREATE PROCEDURE [dbo].[sp_CreateAd]
    @Name NVARCHAR(MAX),
    @StartDate DATETIME,
    @EndDate DATETIME,
    @IsActive BIT,
    @Img NVARCHAR(MAX),
    @ImgUrl NVARCHAR(MAX)
AS
BEGIN

    INSERT INTO [dbo].[Ad] (
        [Name],
        [StartDate],
        [EndDate],
        [IsActive],
        [Img],
        [ImgUrl]
    )
    VALUES (
        @Name,
        @StartDate,
        @EndDate,
        @IsActive,
        @Img,
        @ImgUrl
    );
	
	SELECT SCOPE_IDENTITY();
END

