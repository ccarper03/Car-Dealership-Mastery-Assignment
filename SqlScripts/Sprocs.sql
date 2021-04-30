USE GuildCars
GO

--Cars Repository Procedures
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllCars')
      DROP PROCEDURE SelectAllCars
GO

CREATE PROCEDURE SelectAllCars
AS
BEGIN
	SELECT CarId, ModelYear, IsNew, IsFeatured, IsSold, UnitsInStock, Mileage, VIN, BodyColorId,
	BodyStyleId, TransmissionId, MakeId, ModelId, InteriorColorId, SalePrice, MSRP, IMGFilePath, VehicleDetails
	FROM Cars
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllNewCars')
      DROP PROCEDURE SelectAllNewCars
GO

CREATE PROCEDURE SelectAllNewCars
AS
BEGIN
	SELECT CarId, ModelYear, IsNew, IsFeatured, IsSold, UnitsInStock, Mileage, VIN, BodyColorId,
	BodyStyleId, TransmissionId, MakeId, ModelId, InteriorColorId, SalePrice, MSRP, IMGFilePath, VehicleDetails
	FROM Cars
	WHERE IsNew = 'true'
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllUsedCars')
      DROP PROCEDURE SelectAllUsedCars
GO

CREATE PROCEDURE SelectAllUsedCars
AS
BEGIN
	SELECT CarId, ModelYear, IsNew, IsFeatured, IsSold, UnitsInStock, Mileage, VIN, BodyColorId,
	BodyStyleId, TransmissionId, MakeId, ModelId, InteriorColorId, SalePrice, MSRP, IMGFilePath, VehicleDetails
	FROM Cars
	WHERE IsNew = 'false'
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllUnsoldCars')
      DROP PROCEDURE SelectAllUnsoldCars
GO

CREATE PROCEDURE SelectAllUnsoldCars
AS
BEGIN
	SELECT CarId, ModelYear, IsNew, IsFeatured, IsSold, UnitsInStock, Mileage, VIN, BodyColorId,
	BodyStyleId, TransmissionId, MakeId, ModelId, InteriorColorId, SalePrice, MSRP, IMGFilePath, VehicleDetails
	FROM Cars
	WHERE IsSold = 'false'
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllSoldCars')
      DROP PROCEDURE SelectAllSoldCars
GO

CREATE PROCEDURE SelectAllSoldCars
AS
BEGIN
	SELECT CarId, ModelYear, IsNew, IsFeatured, IsSold, UnitsInStock, Mileage, VIN, BodyColorId,
	BodyStyleId, TransmissionId, MakeId, ModelId, InteriorColorId, SalePrice, MSRP, IMGFilePath, VehicleDetails
	FROM Cars
	WHERE IsSold = 'true'
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectCarById')
      DROP PROCEDURE SelectCarById
GO

CREATE PROCEDURE SelectCarById (
	@CarId INT
) AS
BEGIN
	SELECT CarId, ModelYear, IsNew, IsFeatured, IsSold, UnitsInStock, Mileage, VIN, BodyColorId,
	BodyStyleId, TransmissionId, MakeId, ModelId, InteriorColorId, SalePrice, MSRP, IMGFilePath, VehicleDetails
	FROM Cars
	WHERE CarId = @CarId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarDelete')
      DROP PROCEDURE CarDelete
GO

CREATE PROCEDURE CarDelete (
	@CarId int
) AS
BEGIN
	DELETE FROM Cars
	WHERE CarId = @CarId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarInsert')
      DROP PROCEDURE CarInsert
GO

CREATE PROCEDURE CarInsert (
	@CarId INT OUTPUT,
	@ModelYear DateTime,
	@IsNew BIT,
	@IsFeatured BIT,
	@IsSold BIT,
	@UnitsInStock INT,
	@Mileage VARCHAR(7),
	@VIN VARCHAR(17),
	@BodyColorId INT,
	@BodyStyleId INT,
	@TransmissionId INT,
	@MakeId INT,
	@ModelId INT,
	@InteriorColorId INT,
	@SalePrice DECIMAL(7,2),
	@MSRP DECIMAL(7,2),
	@IMGFilePath NVARCHAR(MAX),
	@VehicleDetails NVARCHAR(400)
) AS
BEGIN
	INSERT INTO Cars(ModelYear, IsNew, IsFeatured, IsSold, UnitsInStock, Mileage, VIN, BodyColorId,
	BodyStyleId, TransmissionId, MakeId, ModelId, InteriorColorId, SalePrice, MSRP, IMGFilePath, VehicleDetails )
	VALUES (@ModelYear, @IsNew, @IsFeatured, @IsSold, @UnitsInStock, @Mileage, @VIN, @BodyColorId,
	@BodyStyleId, @TransmissionId, @MakeId, @ModelId, @InteriorColorId, @SalePrice, @MSRP, @IMGFilePath, @VehicleDetails)

	SET @CarId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CarUpdate')
      DROP PROCEDURE CarUpdate
GO

CREATE PROCEDURE CarUpdate (
	@CarId INT,
	@ModelYear DATETIME,
	@IsNew BIT,
	@IsFeatured BIT,
	@IsSold BIT,
	@UnitsInStock INT,
	@Mileage VARCHAR(7),
	@VIN VARCHAR(17),
	@BodyColorId INT,
	@BodyStyleId INT,
	@TransmissionId INT,
	@MakeId INT,
	@ModelId INT,
	@InteriorColorId INT,
	@SalePrice DECIMAL(7,2),
	@MSRP DECIMAL(7,2),
	@IMGFilePath NVARCHAR(MAX),
	@VehicleDetails NVARCHAR(400)
) AS
BEGIN
	UPDATE Cars SET 
		ModelYear =	@ModelYear,
		IsNew = @IsNew,
		IsFeatured = @IsFeatured,
		IsSold = @IsSold,
		UnitsInStock = @UnitsInStock,
		Mileage = @Mileage,
		VIN = @VIN,
		BodyColorId = @BodyColorId,
		BodyStyleId = @BodyStyleId,
		TransmissionId = @TransmissionId,
		MakeId = @MakeId,
		ModelId =	@ModelId,
		InteriorColorId = @InteriorColorId,
		SalePrice = @SalePrice,
		MSRP = @MSRP,
		IMGFilePath = @IMGFilePath,
		VehicleDetails = @VehicleDetails
	WHERE CarId = @CarId
END
GO

--Body Style Repository Procedures

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllBodyStyles')
      DROP PROCEDURE SelectAllBodyStyles
GO

CREATE PROCEDURE SelectAllBodyStyles
AS
BEGIN
	SELECT BodyStyleId, BodyStyleType
	FROM BodyStyle
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectBodyStyleById')
      DROP PROCEDURE SelectBodyStyleById
GO

CREATE PROCEDURE SelectBodyStyleById(

@BodyStyleId INT

)AS
BEGIN
	SELECT BodyStyleId, BodyStyleType
	FROM BodyStyle
	WHERE BodyStyleId = @BodyStyleId 
END

GO

--Color Repository Procedures

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllColors')
      DROP PROCEDURE SelectAllColors
GO

CREATE PROCEDURE SelectAllColors
AS
BEGIN
	SELECT ColorId, ColorName
	FROM Color
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectColorById')
      DROP PROCEDURE SelectColorById
GO

CREATE PROCEDURE SelectColorById(

@ColorId INT

)AS
BEGIN
	SELECT ColorId, ColorName
	FROM Color
	WHERE ColorId = @ColorId 
END

GO

--Featured Listing Procedure

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectFeaturedCars')
      DROP PROCEDURE SelectFeaturedCars
GO

CREATE PROCEDURE SelectFeaturedCars(

@isFeatured BIT

)AS
BEGIN
	SELECT CarId, IMGFilePath, ModelYear, SalePrice, c.MakeId, c.ModelId, m.MakeName, mo.ModelName 
	FROM Cars c
		INNER JOIN Make m ON c.MakeId = m.MakeId
		INNER JOIN Model mo ON mo.MakeId = m.MakeId
	WHERE IsFeatured = @isFeatured
	ORDER BY CarId
END

GO

--Make Procedures

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllMakes')
      DROP PROCEDURE SelectAllMakes
GO

CREATE PROCEDURE SelectAllMakes
AS
BEGIN
	SELECT *
	FROM Make
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectMakeById')
      DROP PROCEDURE SelectMakeById
GO

CREATE PROCEDURE SelectMakeById(

@MakeId INT

)AS
BEGIN
	SELECT *
	FROM Make
	WHERE MakeId = @MakeId 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeInsert')
      DROP PROCEDURE MakeInsert
GO

CREATE PROCEDURE MakeInsert (
	@MakeId INT OUTPUT,
	@MakeName VARCHAR(15),
	@DateAdded DATETIME2,
	@AddedBy NVARCHAR(256)
	
) AS
BEGIN
	INSERT INTO Make(MakeName, DateAdded, AddedBy)
	VALUES (@MakeName, @DateAdded, @AddedBy)

	SET @MakeId = SCOPE_IDENTITY();
END

GO

--Model Procedures

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllModels')
      DROP PROCEDURE SelectAllModels
GO

CREATE PROCEDURE SelectAllModels
AS
BEGIN
	SELECT *
	FROM Model
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectModelById')
      DROP PROCEDURE SelectModelById
GO

CREATE PROCEDURE SelectModelById(

@ModelId INT

)AS
BEGIN
	SELECT *
	FROM Model
	WHERE ModelId = @ModelId 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectModelByMakeId')
      DROP PROCEDURE SelectModelByMakeId
GO

CREATE PROCEDURE SelectModelByMakeId(

@MakeId INT

)AS
BEGIN
	SELECT *
	FROM Model
	WHERE MakeId = @MakeId 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelInsert')
      DROP PROCEDURE ModelInsert
GO

CREATE PROCEDURE ModelInsert (
	@ModelId INT OUTPUT,
	@ModelName VARCHAR(30),
	@DateAdded DATETIME2,
	@MakeId INT,
	@AddedBy NVARCHAR(256)
	
) AS
BEGIN
	INSERT INTO Model(ModelName, DateAdded, MakeId, AddedBy)
	VALUES (@ModelName, @DateAdded, @MakeId, @AddedBy)

	SET @ModelId = SCOPE_IDENTITY();
END

GO

--Transmission Procedures

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectTransmissionById')
      DROP PROCEDURE SelectTransmissionById
GO

CREATE PROCEDURE SelectTransmissionById(

@TransmissionId INT

)AS
BEGIN
	SELECT TransmissionId, TransmissionType
	FROM Transmission
	WHERE TransmissionId = @TransmissionId 
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllTransmissions')
      DROP PROCEDURE SelectAllTransmissions
GO

CREATE PROCEDURE SelectAllTransmissions
AS
BEGIN
	SELECT TransmissionId, TransmissionType
	FROM Transmission
END

GO

--Customer Contact Procedures

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectCustomerContactById')
      DROP PROCEDURE SelectCustomerContactById
GO

CREATE PROCEDURE SelectCustomerContactById (
	@ContactId INT
) AS
BEGIN
	SELECT ContactId, Email, Phone, MessageBody, ContactName
	FROM CustomerContact
	WHERE ContactId = @ContactId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllCustomerContacts')
      DROP PROCEDURE SelectAllCustomerContacts
GO

CREATE PROCEDURE SelectAllCustomerContacts
AS
BEGIN
	SELECT ContactId, Email, Phone, MessageBody, ContactName
	FROM CustomerContact
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CustomerContactInsert')
      DROP PROCEDURE CustomerContactInsert
GO

CREATE PROCEDURE CustomerContactInsert (
	@ContactId INT OUTPUT,
	@ContactName VARCHAR(40),
	@Phone NVARCHAR(15),
	@Email NVARCHAR(50),
	@MessageBody NVARCHAR(400)
	
) AS
BEGIN
	INSERT INTO CustomerContact(ContactName, Phone, Email, MessageBody)
	VALUES (@ContactName, @Phone, @Email, @MessageBody)

	SET @ContactId = SCOPE_IDENTITY();
END

GO

--Purchase Log Procedures

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'PurchaseLogInsert')
      DROP PROCEDURE PurchaseLogInsert
GO

CREATE PROCEDURE PurchaseLogInsert (
	@PurchaseLogId INT OUTPUT,
	@PurchaserName VARCHAR(40),
	@PurchaseType NVARCHAR(16),
	@Email NVARCHAR(50),
	@CarId INT,
	@AddressOne NVARCHAR(50),
	@AddressTwo NVARCHAR(50),
	@City NVARCHAR(30),
	@Phone NVARCHAR(15),
	@ZipCode VARCHAR(5),
	@SalesPersonId NVARCHAR(128),
	@DateSold DATETIME2,
	@PurchasePrice DECIMAL(10, 2)
) AS
BEGIN
	INSERT INTO PurchaseLog(PurchaserName, Email, Phone, AddressOne, AddressTwo,
	 City, ZipCode, CarId, PurchasePrice, PurchaseType, SalesPersonId, DateSold)
	VALUES (@PurchaserName, @Email, @Phone, @AddressOne, @AddressTwo,
	 @City, @ZipCode, @CarId, @PurchasePrice, @PurchaseType, @SalesPersonId, @DateSold)

	SET @PurchaseLogId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllPurchaseLogs')
      DROP PROCEDURE SelectAllPurchaseLogs
GO

CREATE PROCEDURE SelectAllPurchaseLogs
AS
BEGIN
	SELECT *
	FROM PurchaseLog
END

GO

-- Specials Procedures

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllSpecials')
      DROP PROCEDURE SelectAllSpecials
GO

CREATE PROCEDURE SelectAllSpecials
AS
BEGIN
	SELECT *
	FROM Specials
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialInsert')
      DROP PROCEDURE SpecialInsert

GO

CREATE PROCEDURE SpecialInsert (
	@SpecialId INT OUTPUT,
	@Title NVARCHAR(100),
	@SpecialDetails NVARCHAR(400)
) AS
BEGIN
	INSERT INTO Specials(SpecialDetails, Title)
	VALUES (@SpecialDetails, @Title)

	SET @SpecialId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialDelete')
      DROP PROCEDURE SpecialDelete
GO

CREATE PROCEDURE SpecialDelete (
	@SpecialId int
) AS
BEGIN
	DELETE FROM Specials
	WHERE SpecialsId = @SpecialId
END

GO

-- Reports Repository procedures

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllSalesReports')
      DROP PROCEDURE SelectAllSalesReports
GO

CREATE PROCEDURE SelectAllSalesReports
AS
BEGIN
	SELECT a.UserName As "UserName", a.Id AS "UserId",  SUM(p.PurchasePrice) AS "TotalSales", COUNT(p.SalesPersonId) AS "TotalCarsSold"

	FROM AspNetUsers a
		INNER JOIN PurchaseLog p ON p.SalesPersonId = a.UserName
		Group By UserName, Id
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllInventoryForReport')
      DROP PROCEDURE SelectAllInventoryForReport
GO

CREATE PROCEDURE SelectAllInventoryForReport
AS
BEGIN
	SELECT c.ModelYear AS "Year", c.IsNew AS "IsNew", c.MSRP * c.UnitsInStock AS "StockValue", c.UnitsInStock AS "UnitsInStock",
	mk.MakeName AS "Make", md.ModelName AS "Model"
	
	FROM Cars c
		INNER JOIN Make mk ON mk.MakeId = c.MakeId
		INNER JOIN Model md ON md.ModelId = c.ModelId
		Group By ModelYear, IsNew, MakeName, ModelName, MSRP, UnitsInStock
END

GO

-- User Repository Procedures

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllUsers')
      DROP PROCEDURE SelectAllUsers
GO

CREATE PROCEDURE SelectAllUsers
AS
BEGIN
	SELECT *
	FROM AspNetUsers
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectUserById')
      DROP PROCEDURE SelectUserById
GO

CREATE PROCEDURE SelectUserById (
	@UserId NVARCHAR(128)
) AS
BEGIN
	SELECT *
	FROM AspNetUsers
	WHERE Id = @UserId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectUserByUserName')
      DROP PROCEDURE SelectUserByUserName
GO

CREATE PROCEDURE SelectUserByUserName (
	@UserName NVARCHAR(256)
) AS
BEGIN
	SELECT *
	FROM AspNetUsers
	WHERE Username = @UserName
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectUsersByRole')
      DROP PROCEDURE SelectUsersByRole
GO

CREATE PROCEDURE SelectUsersByRole (
	@Role NVARCHAR(max)
) AS
BEGIN
	SELECT *
	FROM AspNetUsers
	WHERE UserRole = @Role
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'UserInsert')
      DROP PROCEDURE UserInsert

GO

CREATE PROCEDURE UserInsert (
	@UserId NVARCHAR(128) OUTPUT,
	@UserName NVARCHAR(256),
	@UserRole NVARCHAR(MAX),
	@Email NVARCHAR(256),
	@EmailConfirmed BIT,
	@PasswordHash NVARCHAR(MAX),
	@SecurityStamp NVARCHAR(MAX),
	@PhoneNumber NVARCHAR(MAX),
	@PhoneNumberConfirmed BIT,
	@TwoFactorEnabled BIT,
	@LockoutEndDateUtc DATETIME,
	@LockoutEnabled BIT,
	@AccessFailedCount INT,
	@FirstName NVARCHAR(MAX),
	@LastName NVARCHAR(MAX)
) AS
BEGIN
	INSERT INTO AspNetUsers(Id, UserName, UserRole, Email, EmailConfirmed, PasswordHash, SecurityStamp,
	PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount,FirstName, LastName)
	VALUES (@UserId, @UserName, @UserRole, @Email, @EmailConfirmed, @PasswordHash, @SecurityStamp,
	@PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEndDateUtc, @LockoutEnabled, @AccessFailedCount, @FirstName, @LastName)

END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'UserUpdate')
      DROP PROCEDURE UserUpdate
GO

CREATE PROCEDURE UserUpdate (
	@UserId NVARCHAR(128),
	@UserName NVARCHAR(256),
	@UserRole NVARCHAR(MAX),
	@Email NVARCHAR(256),
	@EmailConfirmed BIT,
	@PasswordHash NVARCHAR(MAX),
	@SecurityStamp NVARCHAR(MAX),
	@PhoneNumber NVARCHAR(MAX),
	@PhoneNumberConfirmed BIT,
	@TwoFactorEnabled BIT,
	@LockoutEndDateUtc DATETIME,
	@LockoutEnabled BIT,
	@AccessFailedCount INT,
	@FirstName NVARCHAR(MAX),
	@LastName NVARCHAR(MAX)
) AS
BEGIN
	UPDATE AspNetUsers SET 
		UserName = @UserName,
		UserRole = @UserRole,
		Email = @Email,
		EmailConfirmed = @EmailConfirmed,
		PasswordHash = @PasswordHash,
		SecurityStamp = @SecurityStamp,
		PhoneNumber = @PhoneNumber,
		PhoneNumberConfirmed = @PhoneNumberConfirmed,
		TwoFactorEnabled = @TwoFactorEnabled,
		LockoutEndDateUtc = @LockoutEndDateUtc,
		LockoutEnabled = @LockoutEnabled,
		AccessFailedCount =	@AccessFailedCount,
		FirstName = @FirstName,
		LastName = @LastName

	WHERE Id = @UserId
END

GO

