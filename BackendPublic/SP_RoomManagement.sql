
CREATE PROCEDURE sp_get_all_RoomType_Info 
	@RoomTypeName NVARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [RoomTypeId]
      ,[RoomTypeName]
      ,[Price]
      ,[Characteristics]
      ,[description]
      ,[Image]
  FROM [dbo].[RoomType]
  WHERE RoomTypeName = @RoomTypeName
END
GO
