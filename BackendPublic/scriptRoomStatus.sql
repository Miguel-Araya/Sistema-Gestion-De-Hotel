

CREATE PROCEDURE [dbo].[sp_getHotelRoomStatusToday]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @today DATE = CONVERT(date, GETDATE());

    SELECT 
        R.RoomNumber,
        RT.RoomTypeName,
        CASE 
            WHEN EXISTS (
                SELECT 1
                FROM Booking B
                WHERE 
                    B.RoomID = R.RoomId
                    AND B.IsActive = 1
                    AND @today BETWEEN CONVERT(date, B.CheckIn) AND CONVERT(date, B.CheckOut)
            )
            THEN 'OCUPADA'
            ELSE 'DISPONIBLE'
        END AS Status
    FROM Room R
    JOIN RoomType RT ON R.RoomTypeId = RT.RoomTypeId
    WHERE R.IsActice = 1
    ORDER BY R.RoomNumber;
END



CREATE PROCEDURE sp_getManageActiveRooms
AS
BEGIN
    SELECT 
        rt.RoomTypeId,
        rt.RoomTypeName,
        r.RoomId,
        r.IsActice,
        r.RoomNumber
    FROM RoomType rt
    INNER JOIN Room r ON r.RoomTypeId = rt.RoomTypeId
    ORDER BY rt.RoomTypeId, r.RoomNumber;
END

select * from Room

exec sp_getManageActiveRooms


drop procedure sp_getManageActiveRooms


CREATE PROCEDURE sp_UpdateRoomActive
    @RoomId INT,
    @IsActice BIT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Room WHERE RoomId = @RoomId)
    BEGIN
        UPDATE Room
        SET IsActice = @IsActice
        WHERE RoomId = @RoomId;

        SELECT 1 AS Resultado;
    END
    ELSE
    BEGIN
        SELECT 0 AS Resultado;
    END
END


exec sp_UpdateRoomActive 1, 0;
drop procedure sp_UpdateRoomActive

select * from Room where RoomId=3