---- Insertar pÃ¡ginas
 INSERT INTO Page (PageTitle, PageContent)
 VALUES ('Inicio', 'En Brisas del Mar, te invitamos a descubrir un paraíso de tranquilidad y belleza natural. Ubicado frente a las aguas cristalinas del océano, nuestro hotel combina elegancia y comodidad para ofrecerte una experiencia inolvidable. Desde el momento en que llegas, te envuelve la brisa marina y la cálida hospitalidad de nuestro equipo. Contamos con habitaciones diseñadas para el descanso, cada una con vistas espectaculares y todas las comodidades que necesitas para una estancia placentera. Relájate en nuestra piscina al aire libre, disfruta de exquisitos platillos en nuestro restaurante con gastronomía local e internacional, o explora las maravillas que nos rodean, desde playas de arena dorada hasta aventuras acuáticas. Ya sea que busques un escape romántico, unas vacaciones familiares o un retiro de relajación, Brisas del Mar es el destino perfecto. Déjanos ser parte de tus mejores recuerdos y permítenos brindarte una experiencia única donde el mar, la naturaleza y el confort se unen en armonía.
 ¡Reserva hoy y comienza a vivir la magia de Brisas del Mar!');

 INSERT INTO Page (PageTitle, PageContent)
 VALUES ('Sobre nosotros', 'En Hotel Brisas del Mar, nos especializamos en ofrecer una experiencia Ãºnica y acogedora para nuestros huÃ©spedes. Ubicado en Puerto viejo, LimÃ³n, nuestro hotel combina confort, elegancia y hospitalidad excepcional para garantizar una estadÃ­a inolvidable. Desde el momento en que cruzas nuestras puertas, te recibimos con un ambiente cÃ¡lido y un servicio personalizado. Nuestras habitaciones estÃ¡n diseÃ±adas para brindar el mÃ¡ximo confort, con comodidades modernas y detalles que hacen la diferencia. AdemÃ¡s, ofrecemos una amplia gama de servicios, incluyendo restaurante de alta cocina, spa, piscina y acceso a las principales atracciones de la zona. Ya sea que viajes por negocios o placer, nuestro compromiso es hacer que tu estancia sea inigualable. Â¡Esperamos darte la bienvenida pronto y hacer de tu visita una experiencia inolvidable!.');

 INSERT INTO Page (PageTitle, PageContent)
 VALUES ('Contáctenos', 'Teléfonos: 2222-7070 / 2222-7171; Apartado Postal: 41001; Correo electrónico: info@brisasdelmar.com; Facebook: brisasdelmar.facebook.com; Instagram: @brisasdelmarig');

 INSERT INTO Page (PageTitle, PageContent)
 VALUES ('Como llegar?', 'Estamos ubicados en Manzanillo, Limón. Para llegar al hotel puedes buscarnos en Google Maps o Waze como Hotel Brisas del Mar');

-- -- Insertar imÃ¡genes
-- INSERT INTO Image (ImagePath)
-- VALUES ('/images/banner_principal.jpg');

-- INSERT INTO Image (ImagePath)
-- VALUES ('/images/equipo.jpg');

-- INSERT INTO Image (ImagePath)
-- VALUES ('/images/logo.png');

-- INSERT INTO Image (ImagePath)
-- VALUES ('/images/contacto.jpg');

-- -- Insertar relaciones en PageImages
-- INSERT INTO PageImage (PageID, ImageID)
-- VALUES (1, 1); -- PÃ¡gina inicio tiene banner principal

-- INSERT INTO PageImage (PageID, ImageID)
-- VALUES (1, 3); -- PÃ¡gina inicio tiene logo

-- INSERT INTO PageImage (PageID, ImageID)
-- VALUES (2, 2); -- PÃ¡gina sobre nosotros tiene foto del equipo

-- INSERT INTO PageImage (PageID, ImageID)
-- VALUES (2, 3); -- PÃ¡gina sobre nosotros tiene logo

-- INSERT INTO PageImage (PageID, ImageID)
-- VALUES (3, 4); -- PÃ¡gina contacto tiene imagen de contacto

-----------------Stored procedure---------------
 --CREATE PROCEDURE GetPagesWithImages
 --AS
 --BEGIN
 --   SELECT 
 --       p.PageID, p.PageTitle, p.PageContent, i.ImagePath
 --   FROM Page p
 --   LEFT JOIN PageImage pi ON p.PageID = pi.PageID
 --   LEFT JOIN Image i ON pi.ImageID = i.PageImageID
 --END

EXEC GetPagesWithImages


 --CREATE PROCEDURE GetMainAds
 --AS
 --BEGIN
 --	Select AdID, Name, Img, ImgUrl 
 --	from [dbo].[Ad]
 --	Where IsActive = 1
 --	AND (GETDATE() BETWEEN [StartDate] AND [EndDate])
 --END

--exec GetMainAds

-- Insertar anuncio de Coca-Cola (con fecha de inicio desde hoy)
--INSERT INTO [dbo].[Ad] ([Name], [StartDate], [EndDate], [IsActive], [Img], [ImgUrl])
--VALUES 
--('Coca-Cola Promoción Verano', 
-- GETDATE(), -- Fecha de inicio desde hoy
-- DATEADD(MONTH, 3, GETDATE()), -- Fecha de fin dentro de 3 meses
-- 1, 
-- 'https://res.cloudinary.com/dl2vh2h4h/image/upload/v1742841420/cld-sample-2.jpg', 
-- 'https://www.coca-cola.com');

---- Insertar anuncio de McDonald's (con fecha de inicio desde hoy)
INSERT INTO [dbo].[Ad] ([Name], [StartDate], [EndDate], [IsActive], [Img], [ImgUrl])
VALUES 
('McDonalds Promoción Big Mac', 
 GETDATE(), -- Fecha de inicio desde hoy
 DATEADD(MONTH, 3, GETDATE()), -- Fecha de fin dentro de 3 meses
 1, 
 'https://res.cloudinary.com/dl2vh2h4h/image/upload/v1742841420/cld-sample-5.jpg', 
 'https://www.mcdonalds.com');

---- Insertar anuncio de KFC (con fecha de inicio desde hoy)
INSERT INTO [dbo].[Ad] ([Name], [StartDate], [EndDate], [IsActive], [Img], [ImgUrl])
VALUES 
('KFC Oferta Especial Combo Familiar', 
 GETDATE(), -- Fecha de inicio desde hoy
 DATEADD(MONTH, 3, GETDATE()), -- Fecha de fin dentro de 3 meses
 1, 
 'https://res.cloudinary.com/dl2vh2h4h/image/upload/v1742841420/cld-sample-4.jpg', 
 'https://www.kfc.com');

CREATE PROCEDURE GetMainPromotions
AS
BEGIN
SELECT [PromotionID],[PromotionName],[Img]
FROM [dbo].[Promotion]
WHERE IsActive = 1
AND (GETDATE() BETWEEN [StartDate] AND [EndDate])
END

--EXEC GetMainPromotions

-- --insertar ejemplos para promociones
 INSERT INTO [dbo].[Promotion] ([PromotionName], [StartDate], [EndDate], [IsActive], [Percent], [Img])
 VALUES 
 ('Coca-Cola Promoción Verano', 
 GETDATE(), -- Fecha de inicio desde hoy
 DATEADD(MONTH, 3, GETDATE()), -- Fecha de fin dentro de 3 meses
 1, 
 15, -- Descuento aleatorio
 'https://res.cloudinary.com/dl2vh2h4h/image/upload/v1742841420/cld-sample-2.jpg');

 INSERT INTO [dbo].[Promotion] ([PromotionName], [StartDate], [EndDate], [IsActive], [Percent], [Img])
 VALUES 
 ('McDonalds Promoción Big Mac', 
 GETDATE(), -- Fecha de inicio desde hoy
 DATEADD(MONTH, 3, GETDATE()), -- Fecha de fin dentro de 3 meses
 1, 
 20, -- Descuento aleatorio
 'https://res.cloudinary.com/dl2vh2h4h/image/upload/v1742841420/cld-sample-5.jpg');

 INSERT INTO [dbo].[Promotion] ([PromotionName], [StartDate], [EndDate], [IsActive], [Percent], [Img])
 VALUES 
 ('KFC Oferta Especial Combo Familiar', 
 GETDATE(), -- Fecha de inicio desde hoy
 DATEADD(MONTH, 3, GETDATE()), -- Fecha de fin dentro de 3 meses
 1, 
 25, -- Descuento aleatorio
 'https://res.cloudinary.com/dl2vh2h4h/image/upload/v1742841420/cld-sample-4.jpg');

CREATE PROCEDURE sp_get_facilities
    @PageTitle NVARCHAR(50)
AS
BEGIN
    SELECT 
        p.PageID,
        p.PageTitle,
        p.PageContent,
        i.ImagePath 
    FROM [dbo].[Page] p
    JOIN [dbo].[PageImage] pi ON p.PageID = pi.PageId
    JOIN [dbo].[Image] i ON pi.ImageId = i.PageImageId
    WHERE p.PageTitle = @PageTitle; 
END;

--exec sp_get_facilities 'Facilidades';

--exec sp_get_facilities 'Facilidades'

 --insert into Page  (PageTitle,PageContent) values ('Facilidades','Sumérgete en un oasis de tranquilidad en nuestra exclusiva piscina rodeada de exuberante vegetación tropical. Diseñada para brindar una experiencia relajante, esta piscina cuenta con fuentes de agua que añaden un toque de serenidad al ambiente. El área de la piscina está cuidadosamente decorada con palmeras y plantas exóticas, creando un entorno natural y fresco.')
 --insert into Page  (PageTitle,PageContent) values ('Facilidades','Para aquellos que disfrutan preparando sus propias comidas, nuestro hotel ofrece una cocina totalmente equipada para huéspedes. Este espacio cuenta con todo lo necesario para que puedas cocinar con comodidad, incluyendo estufa, horno, refrigerador, utensilios de cocina, vajilla y una zona de comedor.')
 --insert into Page  (PageTitle,PageContent) values ('Facilidades','Facilidades	Nuestro hotel cuenta con un parqueo verde, un espacio diseñado para ofrecer comodidad a nuestros huéspedes mientras cuidamos el medio ambiente. Ubicado en una zona rodeada de naturaleza, este estacionamiento combina áreas pavimentadas con superficies permeables que permiten la filtración del agua, reduciendo el impacto ambiental.	')

 --insert into [Image]  (ImagePath) values ('https://dynamic-media-cdn.tripadvisor.com/media/photo-o/01/22/b5/f0/piscina.jpg?w=1900&h=1400&s=1')
 --insert into [Image]  (ImagePath) values ('https://dynamic-media-cdn.tripadvisor.com/media/photo-o/01/22/b5/f0/piscina.jpg?w=1900&h=1400&s=1')
 --insert into [Image]  (ImagePath) values ('https://media-cdn.tripadvisor.com/media/photo-s/1c/50/ee/77/acceso-para-cualquier.jpg')
 
 insert into PageImage (PageID,ImageID) VALUES (4,5)
 insert into PageImage (PageID,ImageID) VALUES (5,6)
 insert into PageImage (PageID,ImageID) VALUES (6,7)
-- INSERT IMAGES 

 INSERT INTO [dbo].[Image]
           ([ImagePath])
     VALUES
           ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131527/areadepiscina_uqh9fc.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131523/alrededores_y4fgsv.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131523/areadepiscina5_pkemkt.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131524/restaurante2_hqnsan.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131524/restaurante_pjaza3.webp'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131525/salas_qymi1s.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131524/habitacion_f3zkjf.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131524/habitacion2_k65csg.webp'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131526/areadepiscina3_lvq2xt.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131524/areadepiscina2_qddrnc.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131522/areadepiscina4_iietjt.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743135093/areadepiscina6_boqwvc.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743136634/restaurante3_da8mb5.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743136631/alrededores8_p4tsam.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743136631/alrededores9_o1xp5q.jpg'),
 		   ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1743136632/areadepiscina7_f8oz0e.jpg'),
         ('https://res.cloudinary.com/dgcrjewoy/image/upload/v1743458859/HotelHome_tlxv2b.jpg')


-- INSERT IMAGES+PAGES - este puede variar el orden en caso de que ya hayan agregado imágenes previamente, pues los IDs de Image serían distintos

 --INSERT INTO [dbo].[PageImage]
 --          ([PageID]
 --          ,[ImageID])
 --    VALUES
 --          (2,5), (2,6), (2,7), (2,8), (2,9), (2,10), (2,11), (2,12), (2,13), (2,14), (2,15), (2,16), (2,17), (2,18), (2,19), (2,20), (1,21)

----------------------------------------


--Imagen para Contact
-- INSERT INTO [dbo].[Image] ([ImagePath]) VALUES ('https://res.cloudinary.com/dqmusg1pu/image/upload/v1744483188/istockphoto-2057973065-1024x1024_uwxx2c.jpg') 
-- INSERT INTO [dbo].[PageImage] ([PageID],[ImageID]) VALUES (3,22)

-- ------------------INSERTS ROOMRATE PAGE------------------
-- insert into Page  (PageTitle,PageContent) values ('RoomRate','')
-- insert into Page  (PageTitle,PageContent) values ('RoomRate','')
-- insert into Page  (PageTitle,PageContent) values ('RoomRate','')

 --insert into [Image]  (ImagePath) values ('https://dynamic-media-cdn.tripadvisor.com/media/photo-o/01/22/b5/f0/piscina.jpg?w=1900&h=1400&s=1')
 --insert into [Image]  (ImagePath) values ('https://dynamic-media-cdn.tripadvisor.com/media/photo-o/01/22/b5/f0/piscina.jpg?w=1900&h=1400&s=1')
 --insert into [Image]  (ImagePath) values ('https://media-cdn.tripadvisor.com/media/photo-s/1c/50/ee/77/acceso-para-cualquier.jpg')


---------------------Insert in RoomType--------------------------

--RESERVA
--Use ~ para separar cada caracteristica

 INSERT INTO RoomType(RoomTypeName, Price,Characteristics,description, Image)VALUES('Normal', 500,'La habitación cuanta con dos camas~ una matrimonial y una individual','Perfecto para que nos venga a visitar con un precio más cómodo, pero igual de agradable', 'https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131523/alrededores_y4fgsv.jpg')
 INSERT INTO RoomType(RoomTypeName, Price,Characteristics,description, Image)VALUES('Premium', 1000,'La habitación cuanta con dos camas matrimoniales~ un Jacuzzi','Perfecto para que nos venga a visitar con la mayor comodidad', 'https://res.cloudinary.com/dqmusg1pu/image/upload/v1743131527/areadepiscina_uqh9fc.jpg')


---------------------SP ROOMRATE PAGE----------------------
--Insert season
INSERT INTO Season 
    (SeasonName, StartDate, EndDate, [Percent], IsActive, IsHigh)
 VALUES 
    ('Temporada Alta', '2025-01-01', '2025-07-31', 15, 1, 1);

     INSERT INTO Season 
    (SeasonName, StartDate, EndDate, [Percent], IsActive, IsHigh)
 VALUES 
    ('Temporada Baja', '2025-08-01', '2025-12-31', 15, 1, 0);

 --CREATE PROCEDURE sp_get_RoomType_season
 --As
 --BEGIN
 --	SELECT 
 --		rt.RoomTypeId,
 --		rt.RoomTypeName,
 --		CAST(rt.Price * (1 + CASE WHEN s.IsHigh = 1 THEN s.[Percent] / 100.0 ELSE -s.[Percent] / 100.0 END)AS INT)AS Price,
 --		rt.Characteristics,
 --		rt.description,
 --		rt.Image
 --	FROM 
 --		RoomType rt
 --	INNER JOIN 
 --		Season s
 --		ON GETDATE() BETWEEN s.StartDate AND s.EndDate
 --	WHERE 
 --		s.IsActive = 1;
 --END

--Exec sp_get_RoomType_season


---------HOME PAGE UPDATE---------

 ------Este tipo de tabla le permite al procedimiento almacenado recibir múltiples strings
 --CREATE TYPE dbo.StringList AS TABLE
 --(
 --    Value NVARCHAR(MAX)
 --);
-- ---------------

 --CREATE PROCEDURE UpdatePageWithImagesByTitle
 --    @PageTitle NVARCHAR(255),
 --    @PageContent NVARCHAR(MAX),
 --    @ImagePaths dbo.StringList READONLY
 --AS
 --BEGIN
 --    SET NOCOUNT ON;

 --    DECLARE @PageID INT;

 --    -- Obtener el ID de la página a actualizar
 --    SELECT @PageID = PageID FROM Page WHERE PageTitle = @PageTitle;

 --    IF @PageID IS NULL
 --    BEGIN
 --        -- No se encontró la página
 --        SELECT 0 AS Resultado;
 --        RETURN;
 --    END

 --    -- Actualizar el contenido de la página
 --    UPDATE Page
 --    SET PageContent = @PageContent
 --    WHERE PageID = @PageID;

 --    -- Eliminar relaciones anteriores en PageImage
 --    DELETE FROM PageImage WHERE PageID = @PageID;

 --    -- Agregar nuevas imágenes y sus relaciones
 --    DECLARE @ImageID INT;
 --    DECLARE @Path NVARCHAR(MAX);

 --    DECLARE image_cursor CURSOR FOR
 --        SELECT Value FROM @ImagePaths;

 --    OPEN image_cursor;
 --    FETCH NEXT FROM image_cursor INTO @Path;

 --    WHILE @@FETCH_STATUS = 0
 --    BEGIN
 --        INSERT INTO Image (ImagePath) VALUES (@Path);
 --        SET @ImageID = SCOPE_IDENTITY();

 --        INSERT INTO PageImage (PageID, ImageID) VALUES (@PageID, @ImageID);

 --        FETCH NEXT FROM image_cursor INTO @Path;
 --    END

 --    CLOSE image_cursor;
 --    DEALLOCATE image_cursor;

 --    -- Todo salió bien
 --    SELECT 1 AS Resultado;
 --END;
