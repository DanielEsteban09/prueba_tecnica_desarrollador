USE PruebaDesarrollador;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_Pasaje]
    @opc CHAR(20),
    @UsuarioId INT = NULL,
    @TipoPasaje INT = NULL,
    @Cantidad INT = NULL,
    @FechaCompra DATETIME2 = NULL,
    @Codigo NVARCHAR(MAX) = NULL,
    @MedioPago INT = NULL,
    @PasajeId INT = NULL  
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
        @MedioPagoTexto NVARCHAR(50);

    BEGIN TRY
        -- OPC: CREAR
        IF @opc = 'CREAR'
        BEGIN
            IF @UsuarioId IS NULL OR @TipoPasaje IS NULL OR @Cantidad IS NULL OR @MedioPago IS NULL
            BEGIN
                SET @mensaje = 'Faltan parámetros obligatorios';
                SET @estados = 'error';
                SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PasajeId AS DECIMAL(10, 2)) AS identificador;
                RETURN;
            END;

            SET @TipoPasajeTexto = CASE @TipoPasaje
                WHEN 1 THEN 'Pasaje_Individual'
                WHEN 2 THEN 'Pasaje_Multiple'
                WHEN 3 THEN 'Pasaje_Especial'
                ELSE NULL
            END;

            IF @TipoPasajeTexto IS NULL
            BEGIN
                SET @mensaje = 'TipoPasaje no válido.';
                SET @estados = 'error';
                SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PasajeId AS DECIMAL(10, 2)) AS identificador;
                RETURN;
            END;

            SET @MedioPagoTexto = CASE @MedioPago
                WHEN 1 THEN 'Efectivo'
                WHEN 2 THEN 'Transferencia_Bancaria'
                WHEN 3 THEN 'Tarjeta_Debito'
                WHEN 4 THEN 'Tarjeta_Credito'
                ELSE NULL
            END;

            IF @MedioPagoTexto IS NULL
            BEGIN
                SET @mensaje = 'Medio de pago no válido.';
                SET @estados = 'error';
                SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PasajeId AS DECIMAL(10, 2)) AS identificador;
                RETURN;
            END;

            SET @Precio = CASE @TipoPasaje
                WHEN 1 THEN 2800.000
                WHEN 2 THEN 10000.000
                WHEN 3 THEN 1500.000
                ELSE NULL
            END;

            IF @Precio IS NULL
            BEGIN
                SET @mensaje = 'Precio no válido.';
                SET @estados = 'error';
                SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PasajeId AS DECIMAL(10, 2)) AS identificador;
                RETURN;
            END;

            IF @Cantidad <= 0
            BEGIN
                SET @mensaje = 'La cantidad debe ser mayor que cero.';
                SET @estados = 'error';
				SELECT @mensaje AS mensaje, @estados AS estado, CAST(@identificador AS DECIMAL(10, 2)) AS identificador;
                RETURN;
            END;

            IF @FechaCompra IS NULL
                SET @FechaCompra = GETDATE();

            INSERT INTO Pasajes (
                UsuarioId,
                TipoPasaje,
                Precio,
                Cantidad,
                FechaCompra,
                Estado,
                Codigo,
                MedioPago
            )
            VALUES (
                @UsuarioId,
                @TipoPasajeTexto,
                @Precio,
                @Cantidad,
                @FechaCompra,
                @Estado,
                @Codigo,
                @MedioPagoTexto
            );

            SET @identificador = SCOPE_IDENTITY();
            SET @mensaje = 'Registro exitoso';
            SET @estados = 'ok';

			SELECT 
			@mensaje        AS mensaje,
			@estados        AS estado,
			CAST(@identificador AS DECIMAL(10,2)) AS identificador;

            RETURN;
        END

        -- OPC: LISTAR_POR_USUARIO
        IF @opc = 'LISTAR_POR_USUARIO'
        BEGIN
            SELECT 
                PasajeId,
                UsuarioId,
                TipoPasaje,
                Precio,
                Cantidad,
                FechaCompra,
                Estado,
                Codigo,
                MedioPago
            FROM Pasajes
            WHERE UsuarioId = @UsuarioId;

            RETURN;
        END

        -- OPC: ACTUALIZAR_MEDIO_PAGO
        IF @opc = 'ACTUALIZAR_MEDIO_PAGO'
		PRINT 'Entró a ACTUALIZAR_MEDIO_PAGO'
		BEGIN
			IF @PasajeId IS NULL OR @MedioPago IS NULL
			BEGIN
				SET @mensaje = 'Parámetros obligatorios faltantes';
				SET @estados = 'error';
				SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PasajeId AS DECIMAL(10, 2)) AS identificador;
				RETURN;
			END;

			SET @MedioPagoTexto = CASE @MedioPago
				WHEN 1 THEN 'Efectivo'
				WHEN 2 THEN 'Transferencia_Bancaria'
				WHEN 3 THEN 'Tarjeta_Debito'
				WHEN 4 THEN 'Tarjeta_Credito'
				ELSE NULL
			END;

			IF @MedioPagoTexto IS NULL
			BEGIN
				SET @mensaje = 'Medio de pago no válido.';
				SET @estados = 'error';
				SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PasajeId AS DECIMAL(10, 2)) AS identificador;
				RETURN;
			END;

			UPDATE Pasajes
			SET MedioPago = @MedioPagoTexto
			WHERE PasajeId = @PasajeId;

			IF @@ROWCOUNT = 0
			BEGIN
				SET @mensaje = 'No se actualizó ningún registro. Verifique el PasajeId.';
				SET @estados = 'error';
				SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PasajeId AS DECIMAL(10, 2)) AS identificador;
				RETURN;
			END

			SET @mensaje = 'Medio de pago actualizado correctamente';
			SET @estados = 'ok';
			SET @identificador = @PasajeId;
			SELECT @mensaje AS mensaje, @estados AS estado, CAST(@PasajeId AS DECIMAL(10, 2)) AS identificador;
			RETURN;
		END


    END TRY
    BEGIN CATCH
        SET @mensaje = ERROR_MESSAGE();
        SET @estados = 'error';
        SET @identificador = NULL;
        SELECT @mensaje AS mensaje, @estados AS estado, NULL AS identificador;
        RETURN;
    END CATCH
END