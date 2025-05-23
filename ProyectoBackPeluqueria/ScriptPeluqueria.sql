
/****** Object:  UserDefinedTableType [dbo].[Tipo_CompraDetalles]    Script Date: 12/03/2025 9:29:10 ******/
CREATE TYPE [dbo].[Tipo_CompraDetalles] AS TABLE(
	[ProductoId] [int] NULL,
	[Cantidad] [int] NULL,
	CHECK (([Cantidad]>(0)))
)
GO
/****** Object:  Table [dbo].[RolesUsuario]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolesUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Apellidos] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Contrasena] [nvarchar](100) NOT NULL,
	[ColorAvatar] [nvarchar](100) NULL,
	[Imagen] [nvarchar](100) NULL,
	[IdRolUsuario] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vista_Usuarios]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vista_Usuarios] AS
SELECT u.Id, u.Nombre, u.Apellidos, u.Telefono, u.Email, u.ColorAvatar, u.Imagen, r.Nombre AS Rol
FROM Usuarios u
JOIN RolesUsuario r ON u.IdRolUsuario = r.Id;
GO
/****** Object:  Table [dbo].[Servicios]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[DuracionMinutos] [int] NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservas]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[ServicioId] [int] NOT NULL,
	[FechaHoraInicio] [datetime] NOT NULL,
	[FechaHoraFin] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vista_Reservas]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vista_Reservas] AS
SELECT r.Id AS ReservaID, 
       u.Nombre + ' ' + u.Apellidos AS Cliente, 
       s.Nombre AS Servicio, 
       r.FechaHoraInicio, 
       r.FechaHoraFin
FROM Reservas r
JOIN Usuarios u ON r.ClienteId = u.Id
JOIN Servicios s ON r.ServicioId = s.Id;
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Stock] [int] NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Imagen] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompraDetalles]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompraDetalles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompraId] [int] NOT NULL,
	[ProductoId] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[PrecioUnitario] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vista_Compras]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vista_Compras] AS
SELECT 
    c.Id AS CompraID, 
    u.Nombre + ' ' + u.Apellidos AS Cliente, 
    c.Fecha, 
    p.Nombre AS Producto, 
    cd.Cantidad, 
    cd.PrecioUnitario, 
    (cd.Cantidad * cd.PrecioUnitario) AS PrecioTotal
FROM Compras c
JOIN Usuarios u ON c.ClienteId = u.Id
JOIN CompraDetalles cd ON c.Id = cd.CompraId
JOIN Productos p ON cd.ProductoId = p.Id;
GO
/****** Object:  Table [dbo].[HorariosDisponibles]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HorariosDisponibles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[HoraInicio] [time](7) NOT NULL,
	[Disponible] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 

INSERT [dbo].[Productos] ([Id], [Nombre], [Stock], [Precio], [Imagen]) VALUES (1, N'Shampoo Hidratante', 50, CAST(15.00 AS Decimal(10, 2)), N'shampoo.jpg')
INSERT [dbo].[Productos] ([Id], [Nombre], [Stock], [Precio], [Imagen]) VALUES (2, N'Acondicionador Nutritivo', 30, CAST(18.00 AS Decimal(10, 2)), N'acondicionador.jpg')
INSERT [dbo].[Productos] ([Id], [Nombre], [Stock], [Precio], [Imagen]) VALUES (3, N'Tinte Rojo Intenso', 20, CAST(25.00 AS Decimal(10, 2)), N'tinte_rojo.jpg')
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
SET IDENTITY_INSERT [dbo].[RolesUsuario] ON 

INSERT [dbo].[RolesUsuario] ([Id], [Nombre]) VALUES (1, N'Cliente')
INSERT [dbo].[RolesUsuario] ([Id], [Nombre]) VALUES (2, N'Peluquero')
SET IDENTITY_INSERT [dbo].[RolesUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Servicios] ON 

INSERT [dbo].[Servicios] ([Id], [Nombre], [Descripcion], [DuracionMinutos], [Precio]) VALUES (1, N'Corte de Cabello', N'Corte profesional para caballeros, incluye lavado y peinado.', 30, CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[Servicios] ([Id], [Nombre], [Descripcion], [DuracionMinutos], [Precio]) VALUES (2, N'Tinte', N'Aplicación de tinte permanente o semipermanente para cambiar el color del cabello.', 90, CAST(50.00 AS Decimal(10, 2)))
INSERT [dbo].[Servicios] ([Id], [Nombre], [Descripcion], [DuracionMinutos], [Precio]) VALUES (3, N'Manicure', N'Cuidado y embellecimiento de las uñas de las manos, incluye limado, esmalte y masaje.', 45, CAST(25.00 AS Decimal(10, 2)))
INSERT [dbo].[Servicios] ([Id], [Nombre], [Descripcion], [DuracionMinutos], [Precio]) VALUES (4, N'Pedicure', N'Tratamiento completo para los pies, incluye exfoliación, corte de uñas y esmaltado.', 60, CAST(30.00 AS Decimal(10, 2)))
INSERT [dbo].[Servicios] ([Id], [Nombre], [Descripcion], [DuracionMinutos], [Precio]) VALUES (5, N'Depilación con Cera', N'Eliminación de vello corporal con cera caliente o fría, ideal para piel suave y duradera.', 40, CAST(20.00 AS Decimal(10, 2)))
INSERT [dbo].[Servicios] ([Id], [Nombre], [Descripcion], [DuracionMinutos], [Precio]) VALUES (6, N'Masaje Relajante', N'Masaje terapéutico para aliviar el estrés y mejorar la circulación sanguínea.', 60, CAST(45.00 AS Decimal(10, 2)))
INSERT [dbo].[Servicios] ([Id], [Nombre], [Descripcion], [DuracionMinutos], [Precio]) VALUES (7, N'Alisado Permanente', N'Tratamiento químico para alisar el cabello de manera duradera.', 120, CAST(80.00 AS Decimal(10, 2)))
INSERT [dbo].[Servicios] ([Id], [Nombre], [Descripcion], [DuracionMinutos], [Precio]) VALUES (8, N'Maquillaje Profesional', N'Aplicación de maquillaje para eventos especiales o sesiones fotográficas.', 60, CAST(55.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Servicios] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__RolesUsu__75E3EFCF61047B59]    Script Date: 12/03/2025 9:29:10 ******/
ALTER TABLE [dbo].[RolesUsuario] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuarios__4EC50480648A5C8C]    Script Date: 12/03/2025 9:29:10 ******/
ALTER TABLE [dbo].[Usuarios] ADD UNIQUE NONCLUSTERED 
(
	[Telefono] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuarios__A9D10534375ED098]    Script Date: 12/03/2025 9:29:10 ******/
ALTER TABLE [dbo].[Usuarios] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Compras] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[HorariosDisponibles] ADD  DEFAULT ((1)) FOR [Disponible]
GO
ALTER TABLE [dbo].[CompraDetalles]  WITH CHECK ADD  CONSTRAINT [FK_CompraDetalles_Compra] FOREIGN KEY([CompraId])
REFERENCES [dbo].[Compras] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompraDetalles] CHECK CONSTRAINT [FK_CompraDetalles_Compra]
GO
ALTER TABLE [dbo].[CompraDetalles]  WITH CHECK ADD  CONSTRAINT [FK_CompraDetalles_Producto] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Productos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CompraDetalles] CHECK CONSTRAINT [FK_CompraDetalles_Producto]
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD  CONSTRAINT [FK_Compras_Usuarios] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Compras] CHECK CONSTRAINT [FK_Compras_Usuarios]
GO
ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reservas_Servicios] FOREIGN KEY([ServicioId])
REFERENCES [dbo].[Servicios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_Reservas_Servicios]
GO
ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reservas_Usuarios] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_Reservas_Usuarios]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([IdRolUsuario])
REFERENCES [dbo].[RolesUsuario] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
ALTER TABLE [dbo].[CompraDetalles]  WITH CHECK ADD CHECK  (([Cantidad]>(0)))
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD CHECK  (([Precio]>=(0)))
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD CHECK  (([Stock]>=(0)))
GO
ALTER TABLE [dbo].[Servicios]  WITH CHECK ADD CHECK  (([DuracionMinutos]>(0)))
GO
ALTER TABLE [dbo].[Servicios]  WITH CHECK ADD CHECK  (([Precio]>=(0)))
GO
/****** Object:  StoredProcedure [dbo].[AgregarDisponibilidad]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[AgregarDisponibilidad]
    @Fecha DATE
AS
BEGIN
    SET NOCOUNT ON;
    -- Verificar si el día ya tiene horarios generados
    IF EXISTS (SELECT 1 FROM HorariosDisponibles WHERE Fecha = @Fecha)
    BEGIN
        RAISERROR ('El día ya tiene horarios disponibles.', 16, 1);
        RETURN;
    END
    -- Insertar horarios desde 09:00 AM hasta 07:00 PM en intervalos de 30 minutos
    DECLARE @HoraInicio TIME = '09:00'
    DECLARE @HoraFin TIME = '19:00'
    WHILE @HoraInicio < @HoraFin
    BEGIN
        INSERT INTO HorariosDisponibles (Fecha, HoraInicio)
        VALUES (@Fecha, @HoraInicio)
        -- Incrementar 30 minutos
        SET @HoraInicio = DATEADD(MINUTE, 30, @HoraInicio)
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertarReservaSimple]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarReservaSimple]
    @ClienteId INT,
    @ServicioId INT,
    @FechaHoraInicio DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DuracionMinutos INT;
    DECLARE @FechaHoraFin DATETIME;

    -- Verificar si el servicio existe y obtener su duración
    SELECT @DuracionMinutos = DuracionMinutos
    FROM Servicios
    WHERE Id = @ServicioId;

    IF @DuracionMinutos IS NULL
    BEGIN
        RAISERROR ('Error: El ServicioId especificado no existe.', 16, 1);
        RETURN;
    END

    -- Calcular FechaHoraFin
    SET @FechaHoraFin = DATEADD(MINUTE, @DuracionMinutos, @FechaHoraInicio);

    -- Insertar la reserva
    INSERT INTO Reservas (ClienteId, ServicioId, FechaHoraInicio, FechaHoraFin)
    VALUES (@ClienteId, @ServicioId, @FechaHoraInicio, @FechaHoraFin);
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDiasDisponibles]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerDiasDisponibles]
AS
	SELECT distinct(Fecha)
	FROM HorariosDisponibles
	WHERE Disponible = 1;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDiasYHorasDisponibles]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerDiasYHorasDisponibles]
    @ServicioId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Duracion INT;
    -- Obtener la duración del servicio
    SELECT @Duracion = DuracionMinutos FROM Servicios WHERE Id = @ServicioId;

    -- Seleccionar todos los días y horas disponibles para el servicio
    SELECT hd.Id, hd.Fecha, hd.HoraInicio
    FROM HorariosDisponibles hd
    WHERE hd.Disponible = 1
      AND NOT EXISTS (
          SELECT 1
          FROM Reservas r
          WHERE r.FechaHoraInicio < CAST(hd.Fecha AS DATETIME) + CAST(DATEADD(MINUTE, @Duracion, hd.HoraInicio) AS DATETIME)
            AND r.FechaHoraFin > CAST(hd.Fecha AS DATETIME) + CAST(hd.HoraInicio AS DATETIME)
      )
    ORDER BY hd.Fecha, hd.HoraInicio;
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDisponibilidad]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[ObtenerDisponibilidad]
    @Fecha DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT d.Fecha, 
           DATEADD(SECOND, 0, CAST(d.Fecha AS DATETIME) + CAST(d.HoraInicio AS DATETIME)) AS HoraDisponible
    FROM HorariosDisponibles d
    WHERE d.Disponible = 1
    AND d.Fecha = @Fecha
    AND NOT EXISTS (
        SELECT 1 FROM Reservas r
        WHERE CAST(r.FechaHoraInicio AS DATE) = d.Fecha
        AND (CAST(d.Fecha AS DATETIME) + CAST(d.HoraInicio AS DATETIME)) >= r.FechaHoraInicio
        AND (CAST(d.Fecha AS DATETIME) + CAST(d.HoraInicio AS DATETIME)) < r.FechaHoraFin
    )
    ORDER BY d.Fecha, d.HoraInicio;
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerHorariosDisponiblesPorFecha]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerHorariosDisponiblesPorFecha]
    @ServicioId INT,
    @Fecha DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Duracion INT;
    -- Obtener la duración del servicio
    SELECT @Duracion = DuracionMinutos FROM Servicios WHERE Id = @ServicioId;

    -- Seleccionar horarios disponibles sin solaparse con reservas existentes
    SELECT *
    FROM HorariosDisponibles hd
    WHERE hd.Fecha = @Fecha
      AND hd.Disponible = 1
      AND NOT EXISTS (
          SELECT 1
          FROM Reservas r
          WHERE r.FechaHoraInicio < CAST(hd.Fecha AS DATETIME) + CAST(DATEADD(MINUTE, @Duracion, hd.HoraInicio) AS DATETIME)
            AND r.FechaHoraFin > CAST(hd.Fecha AS DATETIME) + CAST(hd.HoraInicio AS DATETIME)
      )
    ORDER BY hd.HoraInicio;
END;
GO
/****** Object:  StoredProcedure [dbo].[RealizarCompra]    Script Date: 12/03/2025 9:29:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RealizarCompra]
    @ClienteId INT,
    @DetallesCompra Tipo_CompraDetalles READONLY
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @CompraId INT;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insertar la compra (cabecera)
        INSERT INTO Compras (ClienteId) VALUES (@ClienteId);
        SET @CompraId = SCOPE_IDENTITY();

        -- Insertar los productos dentro de la compra con el precio actual
        INSERT INTO CompraDetalles (CompraId, ProductoId, Cantidad, PrecioUnitario)
        SELECT @CompraId, p.Id, dc.Cantidad, p.Precio
        FROM @DetallesCompra dc
        JOIN Productos p ON dc.ProductoId = p.Id;

        -- Actualizar stock
        UPDATE p
        SET p.Stock = p.Stock - dc.Cantidad
        FROM Productos p
        JOIN @DetallesCompra dc ON p.Id = dc.ProductoId
        WHERE p.Stock >= dc.Cantidad;

        -- Verificar stock negativo
        IF EXISTS (SELECT 1 FROM Productos p JOIN @DetallesCompra dc ON p.Id = dc.ProductoId WHERE p.Stock < 0)
        BEGIN
            RAISERROR ('Stock insuficiente para uno o más productos.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        COMMIT TRANSACTION;

        -- Devolver los detalles de la compra
        SELECT cd.CompraId, p.Nombre AS Producto, cd.Cantidad, cd.PrecioUnitario, cd.Cantidad * cd.PrecioUnitario as TotalGastado
        FROM CompraDetalles cd
        JOIN Productos p ON cd.ProductoId = p.Id
        WHERE cd.CompraId = @CompraId;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;




-- Procedimiento para obtener horarios disponibles para un servicio en una fecha específica
IF OBJECT_ID('ObtenerHorariosDisponiblesPorFecha', 'P') IS NOT NULL
    DROP PROCEDURE ObtenerHorariosDisponiblesPorFecha;
GO
USE [master]
GO
ALTER DATABASE [PeluqueriaDB] SET  READ_WRITE 
GO
