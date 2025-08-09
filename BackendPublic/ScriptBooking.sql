 -------Booking----------------
--RESERVA

--INSERT INTO Room(RoomTypeId,[IsActice],[RoomNumber],[Status])VALUES(1,1,1,0)
--INSERT INTO Room(RoomTypeId,[IsActice],[RoomNumber],[Status])VALUES(1,1,2,0)
--INSERT INTO Room(RoomTypeId,[IsActice],[RoomNumber],[Status])VALUES(1,1,3,0)
--INSERT INTO Room(RoomTypeId,[IsActice],[RoomNumber],[Status])VALUES(1,1,4,0)
--INSERT INTO Room(RoomTypeId,[IsActice],[RoomNumber],[Status])VALUES(2,1,5,0)
--INSERT INTO Room(RoomTypeId,[IsActice],[RoomNumber],[Status])VALUES(2,1,6,0)
--INSERT INTO Room(RoomTypeId,[IsActice],[RoomNumber],[Status])VALUES(2,1,7,0)
--INSERT INTO Room(RoomTypeId,[IsActice],[RoomNumber],[Status])VALUES(2,1,8,0)
--INSERT INTO Room(RoomTypeId,[IsActice],[RoomNumber],[Status])VALUES(1,1,10,0)
--INSERT INTO Room(RoomTypeId,[IsActice],[RoomNumber],[Status])VALUES(1,1,11,0)
--INSERT INTO [HotelDB].[dbo].[Customer] ([CustomerName], [CustomerLastName], [CustomerEmail], [CardNumber])
--VALUES 
--('John', 'Doe', 'johndoe@example.com', '1234-5678-9876-5432'),
--('Jane', 'Smith', 'janesmith@example.com', '2345-6789-8765-4321'),
--('Michael', 'Johnson', 'michaeljohnson@example.com', '3456-7890-7654-3210'),
--('Emily', 'Davis', 'emilydavis@example.com', '4567-8901-6543-2109'),
--('David', 'Brown', 'davidbrown@example.com', '5678-9012-5432-1098'),
--('Sarah', 'Miller', 'sarahmiller@example.com', '6789-0123-4321-0987'),
--('James', 'Wilson', 'jameswilson@example.com', '7890-1234-3210-9876'),
--('Linda', 'Moore', 'lindamoore@example.com', '8901-2345-2109-8765'),
--('Robert', 'Taylor', 'roberttaylor@example.com', '9012-3456-1098-7654'),
--('Olivia', 'Anderson', 'oliviaanderson@example.com', '0123-4567-9876-6543');

--select * from [Booking]

INSERT INTO [HotelDB].[dbo].[Booking] ([CreationDate], [BookingReferenceNumber], [CheckIn], [CheckOut], [CustomerID], [Transaction], [IsActive],[RoomId])
VALUES
('2025-04-01 10:00:00', 'BR123456', '2025-04-10', '2025-04-15', 1, 2131, 1,1),
( '2025-04-03 14:00:00', 'BR123458', '2025-04-15', '2025-04-20', 3, 123, 1,2),
( '2025-04-04 16:00:00', 'BR123459', '2025-04-20', '2025-04-25', 4, 123213, 1,3),
( '2025-04-05 18:00:00', 'BR123460', '2025-04-25', '2025-04-30', 5, 123, 1,4),
( '2025-04-06 20:00:00', 'BR123461', '2025-04-28', '2025-05-02', 6, 12312, 1,5),
('2025-04-07 22:00:00', 'BR123462', '2025-05-01', '2025-05-05', 7, 213, 1,6),
( '2025-04-08 10:30:00', 'BR123463', '2025-05-03', '2025-05-08', 8,1232131, 1,7)

---------------------SP roomType-----------------------------------------------

CREATE PROCEDURE sp_get_all_RoomType
AS
BEGIN
SELECT RoomTypeId,RoomTypeName
FROM RoomType
END

--EXEC sp_get_all_RoomType


--------------------------SP check_availability -------------------------------------------------------


CREATE PROCEDURE sp_check_availability
    @RoomType INT,
    @StartTime DATETIME,
    @EndTime DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    -- Calcular número de noches
    DECLARE @NumNights INT;
    SET @NumNights = DATEDIFF(DAY, @StartTime, @EndTime);

    -- Sumar promociones activas
    DECLARE @TotalDiscountPercent DECIMAL(10,2);
    SELECT @TotalDiscountPercent = ISNULL(SUM([Percent]), 0)
    FROM Promotion
    WHERE IsActive = 1
      AND GETDATE() BETWEEN StartDate AND EndDate;
    
    -- Verificar si existe una temporada activa
    DECLARE @SeasonPercent DECIMAL(10,2) = 0;
    DECLARE @IsHighSeason BIT = 0;
    DECLARE @HasActiveSeason BIT = 0;
    
    SELECT TOP 1 
        @SeasonPercent = S.[Percent],
        @IsHighSeason = S.IsHigh,
        @HasActiveSeason = 1
    FROM Season S
    WHERE S.IsActive = 1
      AND GETDATE() BETWEEN S.StartDate AND S.EndDate;
    
IF EXISTS (
    SELECT 1
    FROM Room R
    INNER JOIN RoomType RT ON RT.RoomTypeId = R.RoomTypeId
    WHERE R.RoomTypeId = @RoomType AND R.Status !=1
      AND R.RoomId NOT IN (
          SELECT B.RoomId
          FROM Booking B
          WHERE (@StartTime < B.CheckOut AND @EndTime > B.CheckIn)
          AND NOT (
            CAST(@StartTime AS DATE) = CAST(B.CheckOut AS DATE) AND 
            DATEPART(HOUR, @StartTime) >= 12 AND DATEPART(HOUR, B.CheckOut) < 12
          )
      )
)
BEGIN
    DECLARE @SelectedRoomId INT;

    -- Primero obtén el RoomId y guárdalo en la variable
    SELECT TOP 1 
	@SelectedRoomId = R.RoomId
    FROM Room R
    INNER JOIN RoomType RT ON RT.RoomTypeId = R.RoomTypeId
    WHERE R.RoomTypeId = @RoomType AND R.Status !=1
      AND R.RoomId NOT IN (
          SELECT B.RoomId
          FROM Booking B
          WHERE (@StartTime < B.CheckOut AND @EndTime > B.CheckIn)
          AND NOT (
            CAST(@StartTime AS DATE) = CAST(B.CheckOut AS DATE) AND 
            DATEPART(HOUR, @StartTime) >= 12 AND DATEPART(HOUR, B.CheckOut) < 12
          )
      );

    -- Luego haz el UPDATE
    UPDATE Room
    SET Status = 1
    WHERE RoomId = @SelectedRoomId;

    -- Y ahora sí puedes mostrar todos los datos de la habitación seleccionada
    SELECT TOP 1 
        R.RoomNumber, 
        R.RoomId,
        R.RoomTypeId,
        RT.RoomTypeName,
        RT.Description,
        CAST(@NumNights * RT.Price * 
            (CASE 
                WHEN @HasActiveSeason = 1 THEN 
                    (1 + CASE WHEN @IsHighSeason = 1 THEN @SeasonPercent / 100.0 ELSE -@SeasonPercent / 100.0 END)
                ELSE 1.0
            END) *
            (1 - @TotalDiscountPercent / 100.0) AS INT) AS Price,
        RT.Image AS ImgUrl,
        @StartTime AS CheckIn,
        @EndTime AS CheckOut,
        'Available' AS ResultType
    FROM Room R
    INNER JOIN RoomType RT ON RT.RoomTypeId = R.RoomTypeId 
    WHERE R.RoomId = @SelectedRoomId;
END

    ELSE IF EXISTS (
        SELECT 1
        FROM Room R
        INNER JOIN RoomType RT ON RT.RoomTypeId = R.RoomTypeId
        WHERE R.RoomId NOT IN (
              SELECT B.RoomId
              FROM Booking B
              WHERE (@StartTime < B.CheckOut AND @EndTime > B.CheckIn)
              AND NOT (
                -- Permite reservar si el check-in es el mismo día que otro check-out,
                -- pero a partir de las 12:00
                CAST(@StartTime AS DATE) = CAST(B.CheckOut AS DATE) AND 
                DATEPART(HOUR, @StartTime) >= 12 AND DATEPART(HOUR, B.CheckOut) < 12
              )
          )
    )
    BEGIN
        -- Otra habitación de diferente tipo en el mismo rango de fechas
        SELECT TOP 1 
            R.RoomNumber, 
            R.RoomId,
            R.RoomTypeId,
            RT.RoomTypeName,
            RT.Description,
            CAST(@NumNights * RT.Price * 
                (CASE 
                    WHEN @HasActiveSeason = 1 THEN 
                        (1 + CASE WHEN @IsHighSeason = 1 THEN @SeasonPercent / 100.0 ELSE -@SeasonPercent / 100.0 END)
                    ELSE 1.0 -- Sin ajuste de temporada si no hay temporada activa
                END) *
                (1 - @TotalDiscountPercent / 100.0) AS INT) AS Price,
            RT.Image AS ImgUrl,
            @StartTime AS CheckIn,
            @EndTime AS CheckOut,
            'Recommendation' AS ResultType
        FROM Room R
        INNER JOIN RoomType RT ON RT.RoomTypeId = R.RoomTypeId 
        WHERE R.Status !=1 AND R.RoomId NOT IN (
              SELECT B.RoomId
              FROM Booking B
              WHERE (@StartTime < B.CheckOut AND @EndTime > B.CheckIn)
              AND NOT (
                -- Permite reservar si el check-in es el mismo día que otro check-out,
                -- pero a partir de las 12:00
                CAST(@StartTime AS DATE) = CAST(B.CheckOut AS DATE) AND 
                DATEPART(HOUR, @StartTime) >= 12 AND DATEPART(HOUR, B.CheckOut) < 12
              )
          );
    END
    ELSE
    BEGIN
        -- Busca la primera fecha disponible a partir del @EndTime
        DECLARE @AltStart DATETIME = @EndTime;
        DECLARE @AltEnd DATETIME;
        DECLARE @MaxSearchDays INT = 30; -- Máximo de días que puede recomendar
        DECLARE @DaysChecked INT = 0;

        WHILE @DaysChecked < @MaxSearchDays
        BEGIN
            SET @AltEnd = DATEADD(DAY, @NumNights, @AltStart);

            IF EXISTS (
                SELECT 1
                FROM Room R
                INNER JOIN RoomType RT ON RT.RoomTypeId = R.RoomTypeId
                WHERE R.RoomTypeId = @RoomType AND R.Status !=1
                  AND R.RoomId NOT IN (
                      SELECT B.RoomId
                      FROM Booking B
                      WHERE (@AltStart < B.CheckOut AND @AltEnd > B.CheckIn)
                      AND NOT (
                        -- Permite reservar si el check-in es el mismo día que otro check-out,
                        -- pero a partir de las 12:00
                        CAST(@AltStart AS DATE) = CAST(B.CheckOut AS DATE) AND 
                        DATEPART(HOUR, @AltStart) >= 12 AND DATEPART(HOUR, B.CheckOut) < 12
                      )
                  )
            )
            BEGIN
                SELECT TOP 1 
                    R.RoomNumber, 
                    R.RoomId,
                    R.RoomTypeId,
                    RT.RoomTypeName,
                    RT.Description,
                    CAST(@NumNights * RT.Price * 
                        (CASE 
                            WHEN @HasActiveSeason = 1 THEN 
                                (1 + CASE WHEN @IsHighSeason = 1 THEN @SeasonPercent / 100.0 ELSE -@SeasonPercent / 100.0 END)
                            ELSE 1.0 -- Sin ajuste de temporada si no hay temporada activa
                        END) *
                        (1 - @TotalDiscountPercent / 100.0) AS INT) AS Price,
                    RT.Image AS ImgUrl,
                    @AltStart AS CheckIn,
                    @AltEnd AS CheckOut,
                    'AlternativeDates' AS ResultType
                FROM Room R
                INNER JOIN RoomType RT ON RT.RoomTypeId = R.RoomTypeId
                WHERE R.RoomTypeId = @RoomType AND R.Status !=1
                  AND R.RoomId NOT IN (
                      SELECT B.RoomId
                      FROM Booking B
                      WHERE (@AltStart < B.CheckOut AND @AltEnd > B.CheckIn)
                      AND NOT (
                        -- Permite reservar si el check-in es el mismo día que otro check-out,
                        -- pero a partir de las 12:00
                        CAST(@AltStart AS DATE) = CAST(B.CheckOut AS DATE) AND 
                        DATEPART(HOUR, @AltStart) >= 12 AND DATEPART(HOUR, B.CheckOut) < 12
                      )
                  );

                BREAK;
            END

            -- Si no encontró, prueba siguiente día
            SET @AltStart = DATEADD(DAY, 1, @AltStart);
            SET @DaysChecked += 1;
        END
        
        IF @DaysChecked = @MaxSearchDays
        BEGIN
            SELECT 
                NULL AS RoomNumber, 
                NULL AS RoomId,
                @RoomType AS RoomTypeId,
                NULL AS RoomTypeName,
                NULL AS Description,
                NULL AS Price,
                NULL AS ImgUrl,
                NULL AS CheckIn,
                NULL AS CheckOut,
                'NoAvailability' AS ResultType;
        END
    END -- Fin ELSE final (AlternativeDates)
END -- Fin del procedimiento

EXEC [dbo].[sp_check_availability]
			@RoomType =5,
			@StartTime = N'2025-05-19',
			@EndTime = N'2025-05-25'

		delete booking
		
		select * 
		from room

		select * 
		from booking



		update room
		set Status=0 

		select *
		from Season

--	delete from Booking
-----------------------sp reserva--------------------------------------

CREATE PROCEDURE sp_CreateBooking
    @CustomerName nvarchar(max),
    @CustomerLastName nvarchar(max),
    @CustomerEmail nvarchar(max),
    @CardNumber nvarchar(max),
	@RoomID INT,
    @CheckIn datetime2(7),
    @CheckOut datetime2(7),
    @Transaction INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
         ----Iniciar la transacción
        BEGIN TRANSACTION;
        
        DECLARE @CustomerID INT;
        
         ----Verificar si el cliente ya existe (por email)
        SELECT @CustomerID = CustomerID 
        FROM dbo.Customer 
        WHERE CustomerEmail = @CustomerEmail;
        
         ----Si el cliente no existe, insertarlo
        IF @CustomerID IS NULL
        BEGIN
            INSERT INTO dbo.Customer (
                CustomerName,
                CustomerLastName,
                CustomerEmail,
                CardNumber
            )
            VALUES (
                @CustomerName,
                @CustomerLastName,
                @CustomerEmail,
                @CardNumber
            );
            
             ----Obtener el ID del cliente recién insertado
            SET @CustomerID = SCOPE_IDENTITY();
        END;
        
         ----Generar número de referencia único
        DECLARE @BookingReference nvarchar(max);
        SET @BookingReference = 'RES-' + CONVERT(VARCHAR(8), GETDATE(), 112) + 
                               '-' + RIGHT('000000' + CAST(ABS(CHECKSUM(NEWID())) % 1000000 AS VARCHAR(6)), 6);
        
         ----Insertar la reserva
        INSERT INTO dbo.Booking (
            CreationDate,
            BookingReferenceNumber,
            CheckIn,
            CheckOut,
            CustomerID,
            RoomID,
            [Transaction],
            IsActive
        )
        VALUES (
            GETDATE(),                   
            @BookingReference,           
            @CheckIn,
            @CheckOut,
            @CustomerID,                 
            @RoomID,
            @Transaction,
            1                           
        );
         UPDATE ROOM
		  SET STATUS = 0
		  WHERE RoomId=@RoomID ;
         ----Confirmar la transacción
        COMMIT TRANSACTION;
        
         ----Devuelve información sobre la operación exitosa
        SELECT 
            'Success' AS Status,
            @BookingReference AS BookingReference,
			@CustomerName AS Name,
			@CustomerLastName AS LastName,
			@CustomerEmail AS Email
            
    END TRY
    BEGIN CATCH
         ----Si ocurre algún error, hacer rollback
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
         ----Devolver información del error
        SELECT 
            'Error' AS Status,
            ERROR_MESSAGE() AS ErrorMessage,
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_SEVERITY() AS ErrorSeverity,
            ERROR_STATE() AS ErrorState,
            ERROR_PROCEDURE() AS ErrorProcedure,
            ERROR_LINE() AS ErrorLine;
    END CATCH;
END;

create PROCEDURE sp_view_all_bookings
@page int
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @pageSize INT = 6;
    DECLARE @offset INT = (@page - 1) * @pageSize;

    SELECT 
        b.bookingid,
        b.RoomID,
        b.CreationDate,
        b.CheckIn,
        b.CheckOut,
        b.CustomerID,
        b.[Transaction],
        b.BookingReferenceNumber,
        c.CustomerName,
        c.CustomerLastName,
        c.CustomerEmail,
        c.CardNumber,
        rt.RoomTypeName
        
    FROM 
        Booking b
    JOIN 
        Customer c ON b.customerid = c.customerid
    JOIN 
        Room r ON b.roomid = r.roomid
    JOIN
        RoomType rt ON r.roomtypeid = rt.roomtypeid
    WHERE 
        b.IsActive = 1
    ORDER BY 
        b.CheckIn DESC 
   
    OFFSET @offset ROWS
    FETCH NEXT @pageSize ROWS ONLY;
END;

EXEC sp_view_all_bookings @page = 2;
drop PROCEDURE sp_delete_booking
CREATE PROCEDURE sp_delete_booking
@bookingid INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the booking exists
    IF EXISTS (SELECT 1 FROM Booking WHERE BookingID = @bookingid AND IsActive = 1)
    BEGIN
        -- Mark the booking as inactive
        DELETE FROM Booking
        WHERE BookingID = @bookingid;

        SELECT 'Booking deleted successfully.' AS Message;
    END
    ELSE
    BEGIN
        SELECT 'Booking not found or already inactive.' AS Message;
    END
END;

---------Sp actualizar habitación en 0 después de estar desabilitada para evitar concurrencia'------
--CREATE PROCEDURE sp_enable_room
--@RoomId INT
--	AS
--	BEGIN
--		SET NOCOUNT ON;
--		UPDATE Room
--		SET Status = 0
--		WHERE RoomId=@RoomId
--	END;

---------para el job que desocupa las habitaciones----------------------
--UPDATE b
--SET b.IsActive = 0
--FROM Booking b
--WHERE b.CheckOut = CONVERT(date, GETDATE())
--  AND b.IsActive = 1;

CREATE PROCEDURE sp_list_available_rooms
    @RoomType INT = 0, 
    @StartTime DATETIME,
    @EndTime DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Calcular número de noches
    DECLARE @NumNights INT;
    SET @NumNights = DATEDIFF(DAY, @StartTime, @EndTime);

    -- Sumar promociones activas
    DECLARE @TotalDiscountPercent DECIMAL(10,2);
    SELECT @TotalDiscountPercent = ISNULL(SUM([Percent]), 0)
    FROM Promotion
    WHERE IsActive = 1
      AND GETDATE() BETWEEN StartDate AND EndDate;
    
    -- Verificar si existe una temporada activa
    DECLARE @SeasonPercent DECIMAL(10,2) = 0;
    DECLARE @IsHighSeason BIT = 0;
    DECLARE @HasActiveSeason BIT = 0;
    
    SELECT TOP 1 
        @SeasonPercent = S.[Percent],
        @IsHighSeason = S.IsHigh,
        @HasActiveSeason = 1
    FROM Season S
    WHERE S.IsActive = 1
      AND GETDATE() BETWEEN S.StartDate AND S.EndDate;

    -- Consulta principal: devolver todas las habitaciones disponibles
    SELECT 
        R.RoomNumber, 
        RT.RoomTypeName,
        CAST(@NumNights * RT.Price * 
            (CASE 
                WHEN @HasActiveSeason = 1 THEN 
                    (1 + CASE WHEN @IsHighSeason = 1 THEN @SeasonPercent / 100.0 ELSE -@SeasonPercent / 100.0 END)
                ELSE 1.0
            END) *
            (1 - @TotalDiscountPercent / 100.0) AS INT) AS Price

    FROM Room R
    INNER JOIN RoomType RT ON RT.RoomTypeId = R.RoomTypeId
    WHERE R.Status != 1  -- Solo habitaciones no ocupadas/bloqueadas
      AND (@RoomType =0 OR R.RoomTypeId = @RoomType)  -- Filtro opcional por tipo
      AND R.RoomId NOT IN (
          SELECT B.RoomId
          FROM Booking B
          WHERE (@StartTime < B.CheckOut AND @EndTime > B.CheckIn)
          AND NOT (
            -- Permite reservar si el check-in es el mismo día que otro check-out,
            -- pero a partir de las 12:00
            CAST(@StartTime AS DATE) = CAST(B.CheckOut AS DATE) AND 
            DATEPART(HOUR, @StartTime) >= 12 AND DATEPART(HOUR, B.CheckOut) < 12
          )
      )
    ORDER BY RT.RoomTypeName, R.RoomNumber;  -- Ordenar por tipo y número de habitación

END


EXECUTE [dbo].[sp_list_available_rooms] 
   @RoomType =0
  ,@StartTime= '2025-06-22'
  ,@EndTime= '2025-06-25'
GO


select * from room