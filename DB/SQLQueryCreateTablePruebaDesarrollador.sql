CREATE DATABASE PruebaDesarrollador;
GO

USE PruebaDesarrollador;
GO


CREATE TABLE Usuarios (
	UsuarioId INT NOT NULL IDENTITY(1,1),
	Nombre NVARCHAR(150) NOT NULL,
	Documento NVARCHAR(150) NOT NULL,
	Email NVARCHAR(150) NOT NULL,
	Clave NVARCHAR(150), --este campo lo encriptamos en el Back-End
	CONSTRAINT PK_Usuarios PRIMARY KEY (UsuarioId)
);

GO

CREATE TABLE TarjetaVirtual (
	TarjetaVirtualId INT NOT NULL IDENTITY(1,1),
	Numero NVARCHAR(150) NOT NULL,
	Saldo NUMERIC(10,3) NOT NULL,
	FechaEmision DATETIME2 NOT NULL CONSTRAINT DF_TarjetaVirtual_FechaEmision DEFAULT SYSUTCDATETIME(),
	FechaUltimaRecarga DATETIME2,
	UsuarioId INT NOT NULL,
	EstadoTarjeta NVARCHAR(80) NULL,
	CONSTRAINT PK_TarjetaVirtual PRIMARY KEY (TarjetaVirtualId),
	CONSTRAINT FK_TarjetaVirtual_Usuarios_UsuarioId FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE
);

GO

CREATE TABLE Pasajes (
	PasajeId INT NOT NULL IDENTITY(1,1),
	TipoPasaje NVARCHAR(50) NOT NULL,
	Precio NUMERIC(10,3) NOT NULL,
	Cantidad INT NOT NULL,
	--Uso el tipo de dato DATETIME2 porque es mas preciso en las fechas
	FechaCompra DATETIME2 NOT NULL CONSTRAINT DF_Pasajes_FechaCompra DEFAULT SYSUTCDATETIME(),
	Estado NVARCHAR(50),
	Codigo NVARCHAR(MAX),
	MedioPago NVARCHAR(50),
	UsuarioId INT,
	CONSTRAINT PK_Pasaje PRIMARY KEY (PasajeId),
	CONSTRAINT FK_Pasajes_Usuarios_UsuarioId FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE
);

GO

CREATE TABLE Pago (
	PagoId INT NOT NULL IDENTITY(1,1),
	Medio NVARCHAR(50) NOT NULL,
	Fecha DATETIME2 NOT NULL CONSTRAINT DF_Pago_Fecha DEFAULT SYSUTCDATETIME(),
	Resultado NVARCHAR(150) NOT NULL,
	ReferenciaTransaccion INT NOT NULL,
	PasajeId INT NOT NULL,
	CONSTRAINT PK_Pago PRIMARY KEY (PagoId),
	CONSTRAINT FK_Pago_Pasajes_PasajeId FOREIGN KEY (PasajeId) REFERENCES Pasajes(PasajeId) ON DELETE CASCADE
);