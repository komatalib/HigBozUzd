IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'PhoneBook')
BEGIN
    CREATE DATABASE PhoneBook;
END;

USE [master]
GO
/****** Object:  Database [PhoneBook]    Script Date: 2023-09-13 10:10:13 ******/

ALTER DATABASE [PhoneBook] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PhoneBook].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PhoneBook] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PhoneBook] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PhoneBook] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PhoneBook] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PhoneBook] SET ARITHABORT OFF 
GO
ALTER DATABASE [PhoneBook] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PhoneBook] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PhoneBook] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PhoneBook] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PhoneBook] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PhoneBook] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PhoneBook] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PhoneBook] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PhoneBook] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PhoneBook] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PhoneBook] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PhoneBook] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PhoneBook] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PhoneBook] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PhoneBook] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PhoneBook] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PhoneBook] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PhoneBook] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PhoneBook] SET  MULTI_USER 
GO
ALTER DATABASE [PhoneBook] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PhoneBook] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PhoneBook] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PhoneBook] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PhoneBook] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PhoneBook] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PhoneBook] SET QUERY_STORE = ON
GO
ALTER DATABASE [PhoneBook] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200)
GO
USE [PhoneBook]
GO
/****** Object:  Table [dbo].[phoneBookData]    Script Date: 2023-09-13 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phoneBookData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name Surname] [varchar](40) NULL,
	[Phone Nr.] [varchar](15) NULL,
	[Birthday ] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[CheckAndOperateOnTable]    Script Date: 2023-09-13 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CheckAndOperateOnTable]
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'phoneBoookData')
    BEGIN
        PRINT 'Tables exist';
        
    END
    ELSE
    BEGIN
        PRINT 'Table not exist';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteData]    Script Date: 2023-09-13 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteData]
	
	@contactId VARCHAR(100)
AS
BEGIN
	
	DELETE FROM phoneBookData WHERE ID = @contactId;

	
END
GO
/****** Object:  StoredProcedure [dbo].[InsertPhoneBookData]    Script Date: 2023-09-13 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertPhoneBookData]
    @vardas_pavarde VARCHAR(40),
    @telefonas VARCHAR(15),
    @gimimo_metai DATE
AS
BEGIN
    INSERT INTO phoneBookData ([Name Surname], [Phone Nr.], [Birthday])
    VALUES (@vardas_pavarde, @telefonas, @gimimo_metai);
END;
GO
/****** Object:  StoredProcedure [dbo].[SaveUpdateData]    Script Date: 2023-09-13 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveUpdateData]
	
	@contactId VARCHAR(100),
	@vardas_pavarde VARCHAR(40),
    @telefonas VARCHAR(15),
    @gimimo_metai DATE
AS
BEGIN
	
	UPDATE phoneBookData
SET [Name Surname] = @vardas_pavarde, [Phone Nr.] = @telefonas, [Birthday] = @gimimo_metai
WHERE ID=@contactId;
	
	
END
GO
/****** Object:  StoredProcedure [dbo].[SelectPhoneBookData]    Script Date: 2023-09-13 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectPhoneBookData]
AS

BEGIN
    SELECT 
        ID, 
        [Name Surname],
		[Phone Nr.], 
		[Birthday]
    FROM 
        phoneBookData;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateFormLoadData]    Script Date: 2023-09-13 10:10:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateFormLoadData] 
	
	@contactId VARCHAR(100)
AS
BEGIN
	

	
	SELECT [Name Surname], [Phone Nr.], [Birthday] FROM phoneBookData WHERE ID=@contactId;

END
GO
USE [master]
GO
ALTER DATABASE [PhoneBook] SET  READ_WRITE 
GO
