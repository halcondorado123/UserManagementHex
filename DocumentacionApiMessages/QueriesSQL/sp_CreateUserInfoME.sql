CREATE PROCEDURE USU.CreateUserInfoME
    @FullName NVARCHAR(100),
    @PhoneNumber NVARCHAR(15),
    @Email NVARCHAR(60)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validación: Verificar si alguno de los parámetros es NULL o vacío
    IF (@FullName IS NULL OR LTRIM(RTRIM(@FullName)) = '')
        RAISERROR ('El campo FullName no puede estar vacío o nulo.', 16, 1);

    IF (@PhoneNumber IS NULL OR LTRIM(RTRIM(@PhoneNumber)) = '')
        RAISERROR ('El campo PhoneNumber no puede estar vacío o nulo.', 16, 1);

    IF (@Email IS NULL OR LTRIM(RTRIM(@Email)) = '')
        RAISERROR ('El campo Email no puede estar vacío o nulo.', 16, 1);

    -- Si pasa todas las validaciones, realizar la inserción
    INSERT INTO USU.UserInfoME (FullName, PhoneNumber, Email)
    VALUES (@FullName, @PhoneNumber, @Email);

    -- Opcional: Devuelve el ID del usuario recién creado
    SELECT SCOPE_IDENTITY() AS NewUserId;
END;
GO


