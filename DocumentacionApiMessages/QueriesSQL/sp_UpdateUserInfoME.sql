CREATE OR ALTER PROCEDURE USU.UpdateUserInfoME
    @UserId INT,
    @FullName NVARCHAR(100),
    @PhoneNumber NVARCHAR(20),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE USU.UserInfoME
    SET
        FullName = @FullName,
        PhoneNumber = @PhoneNumber,
        Email = @Email
    WHERE UserId = @UserId;

    -- Retornar el número de filas afectadas
    SELECT @@ROWCOUNT;
END




