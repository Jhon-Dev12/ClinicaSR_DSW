USE BDSeg
GO

CREATE OR ALTER PROCEDURE USP_Usuario_LOGIN
    @Username VARCHAR(50),
    @Contrasenia VARCHAR(255) 
AS
BEGIN
    DECLARE @PasswordHash VARCHAR(255);
    
    -- IMPORTANTE: Convertimos @Contrasenia a VARCHAR antes del HASHBYTES
    SET @PasswordHash = CONVERT(VARCHAR(255), HASHBYTES('SHA2_512', @Contrasenia), 2);

    SELECT 
        ID_Usuario, Username, Nombres, Apellidos, Rol, Estado, Img_Perfil, Correo
    FROM Usuario 
    WHERE Username = @Username 
      AND UPPER(Contrasenia) = UPPER(@PasswordHash)
      AND Estado = 'ACTIVO';
END
GO

CREATE OR ALTER PROCEDURE USP_Usuario_Registrar
    @Username VARCHAR(50),
    @Contrasenia NVARCHAR(255),
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(100),
    @DNI VARCHAR(8),
    @Telefono VARCHAR(15),
    @Img_Perfil VARCHAR(200),
    @Correo VARCHAR(100),
    @Rol VARCHAR(20)
AS
BEGIN
    INSERT INTO Usuario (
        Username, Contrasenia, Nombres, Apellidos, DNI, 
        Telefono, Img_Perfil, Correo, Rol, Estado
    )
    VALUES (
        @Username, CONVERT(VARCHAR(255), HASHBYTES('SHA2_512', @Contrasenia), 2),
        @Nombres, @Apellidos, @DNI, @Telefono, @Img_Perfil, @Correo, @Rol, 'ACTIVO'
    );
END
GO

CREATE OR ALTER PROCEDURE USP_Listar_Usuarios
AS
BEGIN
	SELECT * FROM Usuario 
END
GO

EXEC USP_Usuario_Registrar 
    @Username = 'recep2', 
    @Contrasenia = '1234', 
    @Nombres = 'Carlos', 
    @Apellidos = 'Sánchez Pérez', 
    @DNI = '77889900', 
    @Telefono = '955444333', 
    @Img_Perfil = null, 
    @Correo = 'csanchez@clinica.com', 
    @Rol = 'ADMINISTRADOR';

