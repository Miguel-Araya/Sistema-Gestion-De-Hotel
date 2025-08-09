--SP para modificar los datos d las tarifas de las habitaciones
CREATE PROCEDURE sp_update_RoomType
    @RoomTypeId INT,
    @Price INT,
    @Characteristics NVARCHAR(MAX),
    @Description NVARCHAR(MAX),
    @Img NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar si existe el RoomType
    IF EXISTS (SELECT 1 FROM RoomType WHERE RoomTypeId = @RoomTypeId)
    BEGIN
        UPDATE RoomType
        SET
            Price = @Price,
            Characteristics = @Characteristics,
            Description = @Description,
            Image = @Img
        WHERE RoomTypeId = @RoomTypeId;

        -- Devolver �xito
        SELECT 
            0 AS Code, 
            'Tipo de habitaci�n actualizado correctamente.' AS Message;
    END
    ELSE
    BEGIN
        -- Devolver error
        SELECT 
            1 AS Code, 
            'No se encontr� el tipo de habitaci�n con ese ID.' AS Message;
    END
END;
select * from RoomType