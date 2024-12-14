CREATE FUNCTION USU.fnGetUserById (@UserId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        UserId, 
        FullName, 
        PhoneNumber, 
        Email
    FROM USU.UserInfoME
    WHERE UserId = @UserId
);
GO

SELECT * 
FROM USU.fnGetUserById(1); -- Busca al usuario con UserId = 1