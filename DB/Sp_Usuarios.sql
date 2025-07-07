USE PruebaDesarrollador;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_Usuarios]
	
	@opc		Char(10),
	@Id			int = 0,
	@UsuarioId	int = 0,
	@UserId		int = NULL,
	@Nombre		varchar(150) = null,
	@Documento	varchar(150) = null,
	@Email		varchar(150) = null,
	@Clave		varchar(150) = null

AS
BEGIN
declare
@mensaje nvarchar(max),
@estado nvarchar(max),
@identificador   nvarchar(max)


    BEGIN TRY

		IF @opc = 'LISTAR'
		BEGIN
			SELECT UsuarioId, Nombre, Documento, Email FROM Usuarios
			WHERE (@UserId IS NULL OR UsuarioId = @UserId);
			RETURN;
		END
		
		IF @opc = 'ACTUALIZAR'
		BEGIN
			UPDATE Usuarios SET 
				Nombre = ISNULL(@Nombre, Nombre),
				Documento = ISNULL(@Documento, Documento),
				Email = ISNULL(@Email, Email),
				Clave = ISNULL(@Clave, Clave)
			WHERE UsuarioId = @UsuarioId;

			SET @mensaje = 'Actualizacion Exitosa';
			SET @estado = 'ok';
			SET @identificador = @UsuarioId;

			SELECT 
				@mensaje AS mensaje, 
				@estado AS estado,
				CAST(@UsuarioId AS DECIMAL(10, 2)) AS identificador;
		END
		
		IF @opc = 'ELIMINAR'
		BEGIN
			DELETE FROM Usuarios WHERE UsuarioId = @Id;
			set @mensaje = 'Eliminacion Exitosa';
			set @estado = 'ok';
			set @identificador = SCOPE_IDENTITY()
			select @mensaje as mensaje, @estado as estado,CAST(0 AS decimal(10, 2))  as identificador
		END

	END TRY
    BEGIN CATCH
        -- Capturamos errores de SQL Server
        SET @mensaje = ERROR_MESSAGE();
        SET @estado = 'error';
        SET @identificador = NULL;
    END CATCH
END