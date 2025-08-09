--update facilities
USE [HotelDB]
GO
-------Add facilidad--------------
CREATE PROCEDURE sp_addFaclity
@PageContent NVARCHAR(MAX),
    @ImagePath NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
        DECLARE @NewPageID INT;
        DECLARE @NewImageID INT;
        
        -- Insertar en tabla Page
        INSERT INTO dbo.Page (PageTitle, PageContent)
        VALUES ('Facilidades', @PageContent);
        
        -- Obtener el ID del Page recién insertado
        SET @NewPageID = SCOPE_IDENTITY();
        
        -- Insertar en tabla Image
        INSERT INTO dbo.Image (ImagePath)
        VALUES (@ImagePath);
        
        -- Obtener el ID de la Image recién insertada
        SET @NewImageID = SCOPE_IDENTITY();
        
        -- Insertar en tabla PageImage para relacionar Page e Image
        INSERT INTO dbo.PageImage (PageID, ImageID)
        VALUES (@NewPageID, @NewImageID);
        
        COMMIT TRANSACTION;
        
        -- Opcional: Retornar los IDs creados
        SELECT @NewPageID AS NewPageID, @NewImageID AS NewImageID;
        
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH

END;


---Delete facilidad
CREATE PROCEDURE [dbo].[sp_deleteFacility]
    @PageID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar si la página existe
    IF NOT EXISTS (SELECT 1 FROM Page WHERE PageID = @PageID)
    BEGIN
        SELECT 
            1 AS CodigoResultado,
            'Facilidad no encontrada.' AS Mensaje;
        RETURN;
    END

    -- Tabla temporal para guardar las imágenes relacionadas con la página
    DECLARE @ImageIDs TABLE (ImageID INT);

    INSERT INTO @ImageIDs (ImageID)
    SELECT ImageID
    FROM PageImage
    WHERE PageID = @PageID;

    -- Eliminar relaciones en PageImage
    DELETE FROM PageImage
    WHERE PageID = @PageID;

    -- Eliminar la página
    DELETE FROM Page
    WHERE PageID = @PageID;

    -- Eliminar las imágenes que ya no están relacionadas con ninguna otra página
    DELETE FROM Image
    WHERE PageImageID IN (
        SELECT ImageID
        FROM @ImageIDs
        WHERE ImageID NOT IN (
            SELECT ImageID FROM PageImage
        )
    );

    -- Devolver resultado exitoso
    SELECT 
        0 AS CodigoResultado,
        'Facilidad eliminada correctamente.' AS Mensaje;
END;
GO


 
CREATE PROCEDURE sp_updateFacility
    @PageID INT,
    @PageContent NVARCHAR(MAX),
    @ImagePath NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
        DECLARE @ExistingImageID INT;

        -- Obtener el ImageID relacionado con ese PageID desde PageImage
        SELECT TOP 1 @ExistingImageID = ImageID
        FROM dbo.PageImage
        WHERE PageID = @PageID;

        -- Actualizar Page
        UPDATE dbo.Page 
        SET PageContent = @PageContent
        WHERE PageID = @PageID;

        -- Si hay una imagen relacionada, actualizarla
        IF @ExistingImageID IS NOT NULL
        BEGIN
            UPDATE dbo.Image 
            SET ImagePath = @ImagePath
            WHERE PageImageID = @ExistingImageID;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH
END;

EXEC sp_updateFacility 
    @PageID = 2051,
    @PageContent = 'Mi contenido nuevo',
    @ImagePath = 'https://res.cloudinary.com/dcqwzf9fg/image/upload/v1743139397/samples/dessert-on-a-plate.jpg'

 insert into Page  (PageTitle,PageContent) values ('Facilidades','Sumérgete en un oasis de tranquilidad en nuestra exclusiva piscina rodeada de exuberante vegetación tropical. Diseñada para brindar una experiencia relajante, esta piscina cuenta con fuentes de agua que añaden un toque de serenidad al ambiente. El área de la piscina está cuidadosamente decorada con palmeras y plantas exóticas, creando un entorno natural y fresco.')
 insert into Page  (PageTitle,PageContent) values ('Facilidades','Para aquellos que disfrutan preparando sus propias comidas, nuestro hotel ofrece una cocina totalmente equipada para huéspedes. Este espacio cuenta con todo lo necesario para que puedas cocinar con comodidad, incluyendo estufa, horno, refrigerador, utensilios de cocina, vajilla y una zona de comedor.')
 insert into Page  (PageTitle,PageContent) values ('Facilidades','Facilidades	Nuestro hotel cuenta con un parqueo verde, un espacio diseñado para ofrecer comodidad a nuestros huéspedes mientras cuidamos el medio ambiente. Ubicado en una zona rodeada de naturaleza, este estacionamiento combina áreas pavimentadas con superficies permeables que permiten la filtración del agua, reduciendo el impacto ambiental.	')

 insert into [Image]  (ImagePath) values ('https://dynamic-media-cdn.tripadvisor.com/media/photo-o/01/22/b5/f0/piscina.jpg?w=1900&h=1400&s=1')
 insert into [Image]  (ImagePath) values ('https://dynamic-media-cdn.tripadvisor.com/media/photo-o/01/22/b5/f0/piscina.jpg?w=1900&h=1400&s=1')
 insert into [Image]  (ImagePath) values ('https://media-cdn.tripadvisor.com/media/photo-s/1c/50/ee/77/acceso-para-cualquier.jpg')
 
 insert into PageImage (PageID,ImageID) VALUES (2062,2046)
 insert into PageImage (PageID,ImageID) VALUES (2063,2047)
 insert into PageImage (PageID,ImageID) VALUES (2064,2048)
 insert into PageImage (PageID,ImageID) VALUES (2054,2035)
 insert into PageImage (PageID,ImageID) VALUES (2055,2036)
 insert into PageImage (PageID,ImageID) VALUES (2056,2037)

 select * from Page
 SELECT * FROM Image
 select * from PageImage
 delete from Page

 CREATE PROCEDURE sp_GetPageWithImagesAboutUs
 @PageTitle NVARCHAR(255)
 AS
 BEGIN
   SELECT 
       p.PageID, p.PageTitle, p.PageContent, i.ImagePath,i.PageImageID
   FROM Page p
   LEFT JOIN PageImage pi ON p.PageID = pi.PageID
   LEFT JOIN Image i ON pi.ImageID = i.PageImageID
    WHERE p.PageTitle = @PageTitle
 END
EXEC GetPageWithImagesAboutUs @PageTitle = 'Sobre nosotros';

create procedure sp_DeleteImagePageAboutUs
@ImageID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the image exists
    IF EXISTS (SELECT 1 FROM Image WHERE PageImageID = @ImageID)
    BEGIN
        -- Delete the image
        DELETE FROM Image
        WHERE PageImageID = @ImageID;

        SELECT 'Image deleted successfully.' AS Message;
    END
    ELSE
    BEGIN
        SELECT 'Image not found.' AS Message;
    END
END;
EXEC DeleteImagePageAboutUs @ImageID = 5;
create procedure sp_InsertImagePageAboutUs
@ImagePath NVARCHAR(255),
@PageID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Insert the new image
    INSERT INTO Image (ImagePath)
    VALUES (@ImagePath);

    DECLARE @NewImageID INT = SCOPE_IDENTITY();

    -- Associate the image with the page
    INSERT INTO PageImage (PageID, ImageID)
    VALUES (@PageID, @NewImageID);

    SELECT 'Image inserted successfully.' AS Message;
END;
drop procedure sp_UpdateTextAboutUs
create PROCEDURE sp_UpdateTextAboutUs
@PageID INT,
@PageContent NVARCHAR(MAX)
AS
BEGIN
    -- Update the page content
    UPDATE Page
    SET PageContent = @PageContent
    WHERE PageID = @PageID;
END;