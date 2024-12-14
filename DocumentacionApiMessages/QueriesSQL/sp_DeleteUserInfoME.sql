CREATE PROCEDURE USU.DeleteUserInfoME
    @UserId INT
AS
BEGIN
    DELETE FROM USU.UserInfoME WHERE UserId = @UserId;
    
    IF @@ROWCOUNT > 0
        RETURN 1; -- Registros eliminados
    ELSE
        RETURN 0; -- No se eliminó ningún registro
END
GO