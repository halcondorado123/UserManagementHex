CREATE VIEW USU.vwUserInfoME
AS
SELECT 
    UserId,         
    FullName,       
    PhoneNumber,    
    Email           
FROM USU.UserInfoME;
GO


SELECT * FROM USU.vwUserInfoME