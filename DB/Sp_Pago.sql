USE PruebaDesarrollador;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_Pago]
    @opc CHAR(30),
    @UsuarioId INT = NULL,
    @TipoPasaje INT = NULL,
    @Cantidad INT = NULL,
    @FechaCompra DATETIME2 = NULL,
    @Codigo NVARCHAR(MAX) = NULL,  -- Código QR en base64
    @MedioPago INT = NULL,
    @PasajeId INT = NULL,
    @ReferenciaTransaccion NVARCHAR(100) = NULL,
	@PagoId INT = NULL  
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @mensaje NVARCHAR(MAX),
        @estados NVARCHAR(10),
        @identificador NVARCHAR(MAX),
        @Precio DECIMAL(10,3),
        @Estado NVARCHAR(50) = 'En_Proceso',
        @TipoPasajeTexto NVARCHAR(50),
        @MedioPagoTexto NVARCHAR(50),
        @Resultado NVARCHAR(20),
        @FechaActual DATETIME = GETDATE();

    BEGIN TRY

        -- REGISTRAR_PAGO
		IF @opc = 'REGISTRAR_PAGO'
		BEGIN
			IF @PasajeId IS NULL
			BEGIN
				SET @mensaje = 'Debe enviar el PasajeId';
				SET @estados = 'error';
				SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PagoId AS DECIMAL(10, 2)) AS identificador;
				RETURN;
			END;

			-- Obtener medio de pago y estado actual del pasaje
			SELECT 
				@MedioPagoTexto = MedioPago,
				@Estado = Estado
			FROM Pasajes 
			WHERE PasajeId = @PasajeId;

			IF @MedioPagoTexto IS NULL
			BEGIN
				SET @mensaje = 'No se encontró el pasaje especificado';
				SET @estados = 'error';
				SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PagoId AS DECIMAL(10, 2)) AS identificador;
				RETURN;
			END;

			-- Validar que el pasaje esté en estado En_Proceso
			IF @Estado <> 'En_Proceso'
			BEGIN
				SET @mensaje = 'Solo se puede registrar pago para pasajes en estado En_Proceso';
				SET @estados = 'error';
				SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PagoId AS DECIMAL(10, 2)) AS identificador;
				RETURN;
			END;

			-- Determinar resultado del pago
			SET @Resultado = CASE 
								WHEN @ReferenciaTransaccion IS NULL THEN 'Rechazado' 
								ELSE 'Aprobado' 
							 END;

			-- Registrar el pago
			INSERT INTO Pago (
				PasajeId, Medio, Fecha, ReferenciaTransaccion, Resultado
			)
			VALUES (
				@PasajeId, @MedioPagoTexto, @FechaActual, @ReferenciaTransaccion, @Resultado
			);

			SET @identificador = SCOPE_IDENTITY();

			-- Si el pago fue aprobado, actualizar el campo Codigo y Estado del pasaje
			IF @Resultado = 'Aprobado' AND @Codigo IS NOT NULL
			BEGIN
				UPDATE Pasajes
				SET Codigo = @Codigo,
					Estado = 'Vigente'
				WHERE PasajeId = @PasajeId;
			END;

			SET @mensaje = 'Pago registrado correctamente';
			SET @estados = 'ok';

			SELECT 
			@mensaje        AS mensaje,
			@estados        AS estado,
			CAST(@identificador AS DECIMAL(10,2)) AS identificador;

			--SELECT @mensaje AS mensaje, @estados AS estado, @identificador AS identificador;
			RETURN;
		END;

		IF @opc = 'LISTAR_PAGOS_POR_USUARIO'
		BEGIN
			IF @UsuarioId IS NULL
			BEGIN
				SELECT 'Debe proporcionar un UsuarioId válido.' AS mensaje, 'error' AS estado;
				RETURN;
			END;

			SELECT 
				pg.PagoId,
				pg.PasajeId,
				p.UsuarioId,
				pg.Medio,
				pg.Fecha,
				pg.ReferenciaTransaccion,
				pg.Resultado
			FROM Pago pg
			INNER JOIN Pasajes p ON pg.PasajeId = p.PasajeId
			WHERE p.UsuarioId = @UsuarioId;
        
			RETURN;
		END

	END TRY
    BEGIN CATCH
        SET @mensaje = ERROR_MESSAGE();
        SET @estados = 'error';
        SELECT @mensaje AS mensaje, @estados AS estado, NULL AS identificador;
    END CATCH
END;