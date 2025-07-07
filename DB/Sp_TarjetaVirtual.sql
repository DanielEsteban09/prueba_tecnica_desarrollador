USE PruebaDesarrollador;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_TarjetaVirtual]
	
	@opc			Char(10),
	@UserId			int = 0,
	@Numero			varchar(150) = null,
	@Saldo			numeric(10,3) = 0,
	@EstadoTarjeta	varchar(80) = null,
	@UsuarioId		int = 0

AS
BEGIN
declare
@mensaje nvarchar(max),
@estado nvarchar(max),
@identificador   nvarchar(max)

	BEGIN TRY

		IF @opc = 'LISTAR'
        BEGIN
            SELECT
                TarjetaVirtualId, Numero, Saldo, FechaEmision, FechaUltimaRecarga, UsuarioId, EstadoTarjeta
            FROM dbo.TarjetaVirtual
            WHERE UsuarioId = @UserId;
            RETURN;
        END
		
		IF @opc = 'CREAR'
		BEGIN
			INSERT INTO TarjetaVirtual (Numero, Saldo, FechaUltimaRecarga, UsuarioId, EstadoTarjeta)
			VALUES (@Numero, @Saldo, SYSDATETIME(), @UsuarioId, @EstadoTarjeta);

			SET @mensaje = 'Registro Exitoso';
			SET @estado = 'ok';
			SET @identificador = SCOPE_IDENTITY();

			SELECT 
				@mensaje AS mensaje,
				@estado AS estado,
				CAST(@identificador AS DECIMAL(10, 2)) AS identificador;
		END

	END TRY
    BEGIN CATCH
        SET @mensaje = ERROR_MESSAGE();
        SET @estado = 'error';
        SET @identificador = NULL;
    END CATCH

END
GO
