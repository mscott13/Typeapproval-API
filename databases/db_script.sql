USE [master]
GO
/****** Object:  Database [SLW_Database]    Script Date: 12/3/2018 1:29:58 AM ******/
CREATE DATABASE [SLW_Database]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SLW_Database', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\SLW_Database.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SLW_Database_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\SLW_Database_log.ldf' , SIZE = 18240KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SLW_Database] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SLW_Database].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SLW_Database] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SLW_Database] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SLW_Database] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SLW_Database] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SLW_Database] SET ARITHABORT OFF 
GO
ALTER DATABASE [SLW_Database] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SLW_Database] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SLW_Database] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SLW_Database] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SLW_Database] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SLW_Database] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SLW_Database] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SLW_Database] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SLW_Database] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SLW_Database] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SLW_Database] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SLW_Database] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SLW_Database] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SLW_Database] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SLW_Database] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SLW_Database] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SLW_Database] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SLW_Database] SET RECOVERY FULL 
GO
ALTER DATABASE [SLW_Database] SET  MULTI_USER 
GO
ALTER DATABASE [SLW_Database] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SLW_Database] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SLW_Database] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SLW_Database] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SLW_Database] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SLW_Database] SET QUERY_STORE = OFF
GO
USE [SLW_Database]
GO
/****** Object:  Table [dbo].[slw_client_company]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_client_company](
	[username] [varchar](50) NULL,
	[company] [varchar](500) NULL,
	[clientId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_form_freq]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_form_freq](
	[application_id] [varchar](100) NULL,
	[sequence] [int] NOT NULL,
	[lower_freq] [varchar](100) NULL,
	[upper_freq] [varchar](100) NULL,
	[power] [varchar](100) NULL,
	[tolerance] [varchar](100) NULL,
	[emmision_desig] [varchar](300) NULL,
	[freq_type] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[sequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_form_step1]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_form_step1](
	[application_id] [varchar](100) NULL,
	[applicant_name] [varchar](300) NULL,
	[applicant_tel] [varchar](300) NULL,
	[applicant_address] [varchar](400) NULL,
	[applicant_fax] [varchar](50) NULL,
	[applicant_city_town] [varchar](300) NULL,
	[applicant_contact_person] [varchar](300) NULL,
	[applicant_nationality] [varchar](300) NULL,
	[manufacturer_name] [varchar](300) NULL,
	[manufacturer_tel] [varchar](300) NULL,
	[manufacturer_address] [varchar](300) NULL,
	[manufacturer_fax] [varchar](300) NULL,
	[manufacturer_contact_person] [varchar](300) NULL,
	[provider_name] [varchar](300) NULL,
	[provider_telephone] [varchar](300) NULL,
	[provider_address] [varchar](300) NULL,
	[provider_fax] [varchar](300) NULL,
	[provider_contact_person] [varchar](300) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_form_step2]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_form_step2](
	[application_id] [varchar](100) NULL,
	[equipment_type] [varchar](300) NULL,
	[equipment_description] [varchar](max) NULL,
	[product_identifiation] [varchar](300) NULL,
	[ref#] [varchar](300) NULL,
	[make] [varchar](300) NULL,
	[software] [varchar](300) NULL,
	[type_of_equipment] [varchar](100) NULL,
	[other] [varchar](300) NULL,
	[antenna_type] [varchar](300) NULL,
	[antenna_gain] [varchar](300) NULL,
	[channel] [varchar](300) NULL,
	[separation] [varchar](300) NULL,
	[aspect] [varchar](300) NULL,
	[compatibility] [varchar](300) NULL,
	[security] [varchar](300) NULL,
	[equipment_comm_type] [varchar](300) NULL,
	[fee_code] [varchar](300) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_forms]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_forms](
	[username] [varchar](50) NOT NULL,
	[application_id] [varchar](100) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[status] [varchar](100) NOT NULL,
	[last_updated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[application_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_notification_types]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_notification_types](
	[types] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[types] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_user_keys]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_user_keys](
	[user_id] [int] NULL,
	[access_key] [varchar](500) NOT NULL,
	[last_detected_activity] [datetime] NOT NULL,
	[max_inactivity_mins] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_user_notifications]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_user_notifications](
	[notification_id] [varchar](200) NOT NULL,
	[received_date] [datetime] NULL,
	[read_date] [datetime] NULL,
	[type] [varchar](100) NULL,
	[target_user] [varchar](50) NULL,
	[status_read] [bit] NULL,
	[message] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[notification_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_user_types]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_user_types](
	[user_type] [int] NOT NULL,
	[description] [varchar](300) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[user_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_users]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_users](
	[username] [varchar](50) NOT NULL,
	[first_name] [varchar](100) NOT NULL,
	[last_name] [varchar](100) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[user_type] [int] NOT NULL,
	[last_password_change_date] [datetime] NOT NULL,
	[last_login_date] [datetime] NOT NULL,
	[hash] [varchar](500) NOT NULL,
	[password_reset_required] [bit] NOT NULL,
	[user_id] [int] NOT NULL,
	[email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[slw_client_company] ([username], [company], [clientId]) VALUES (N'gwallcot', N'Digicel (Jamaica) Limited', 7519)
INSERT [dbo].[slw_client_company] ([username], [company], [clientId]) VALUES (N'client_test', N'Digicel (Jamaica) Limited', 7523)
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'bb8b35a3-3c5c-4a45-844d-85315f2c5bf6', 1, N'100', N'150', N'10', N'1', N'LLC', N'R')
INSERT [dbo].[slw_form_step1] ([application_id], [applicant_name], [applicant_tel], [applicant_address], [applicant_fax], [applicant_city_town], [applicant_contact_person], [applicant_nationality], [manufacturer_name], [manufacturer_tel], [manufacturer_address], [manufacturer_fax], [manufacturer_contact_person], [provider_name], [provider_telephone], [provider_address], [provider_fax], [provider_contact_person]) VALUES (N'bb8b35a3-3c5c-4a45-844d-85315f2c5bf6', N'Digicel (Jamaica) Limited', N'876-511-5000', N'14 Ocean Boulevard', N'unavailable', N'unavailable', N'unavailable', N'JMC', N'', N'', N'', N'', N'', N'Spectrum Management Authority Provider', N'5947897', N'1e Tavern, Papine, Kingston Jamaica', N'5587498', N'Mark Scott')
INSERT [dbo].[slw_form_step2] ([application_id], [equipment_type], [equipment_description], [product_identifiation], [ref#], [make], [software], [type_of_equipment], [other], [antenna_type], [antenna_gain], [channel], [separation], [aspect], [compatibility], [security], [equipment_comm_type], [fee_code]) VALUES (N'bb8b35a3-3c5c-4a45-844d-85315f2c5bf6', N'Arris high powered router', N'Arris high powered router able to deliver signal over 10 miles', N'ars0119', N'774415898', N'Arris', N'v1.23', N'Other', N'High Powered Transciever', N'ATDI Parabolic ITU R-1245', N'182', N'105', N'109', N'16:91', N'802.11 b/g/n/a', N'wpa/psk v2.1', N'Radio Systems', N'Radio Interface Equipment')
INSERT [dbo].[slw_forms] ([username], [application_id], [created_date], [status], [last_updated]) VALUES (N'gwallcot', N'bb8b35a3-3c5c-4a45-844d-85315f2c5bf6', CAST(N'2018-12-02T21:33:23.803' AS DateTime), N'incomplete', CAST(N'2018-12-02T21:44:08.510' AS DateTime))
INSERT [dbo].[slw_notification_types] ([types]) VALUES (N'GENERAL')
INSERT [dbo].[slw_notification_types] ([types]) VALUES (N'TYPE_APPROVAL')
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (221020181, N'isqvR/NS1khqwzjTdKu0RZrX7q2bYu2WbXNjb3R0', CAST(N'2018-11-25T11:30:15.273' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (21120182, N'GuPnFMdY1kjmXiXN5jQqSZIQtgpb/CY0Z3dhbGxjb3Q=', CAST(N'2018-12-02T21:28:59.197' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (21220183, N'YWxvN79Y1kg+kfFnqLxkSLVsa2ma/++uY2xpZW50X3Rlc3Q=', CAST(N'2018-12-02T20:32:41.157' AS DateTime), 45)
INSERT [dbo].[slw_user_types] ([user_type], [description]) VALUES (0, N'Client')
INSERT [dbo].[slw_user_types] ([user_type], [description]) VALUES (1, N'Staff')
INSERT [dbo].[slw_user_types] ([user_type], [description]) VALUES (9, N'Administrator')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'client_test', N'client', N'test', CAST(N'2018-12-02T20:32:01.970' AS DateTime), 0, CAST(N'2018-12-02T20:32:01.970' AS DateTime), CAST(N'2018-12-02T21:28:59.197' AS DateTime), N'LblOPX1/zvSo+kTf8gx069Wjvb2i9c4Ftm+xCe2unWX7ivoWhx117Q==', 0, 21220183, N'client_test@hotmail.com')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'gwallcot', N'Greg', N'Wallcott', CAST(N'2018-11-02T15:39:05.553' AS DateTime), 0, CAST(N'2018-11-02T15:39:05.553' AS DateTime), CAST(N'2018-12-02T21:28:59.197' AS DateTime), N'0znccmWeo6to/Fu8pQ/A6RbNqys4zRq3PQuC7MfyDr3PIiSSrF5URA==', 0, 21120182, N'gregwallcott55@yahoo.com')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'mscott', N'Mark', N'Scott', CAST(N'2018-10-22T12:42:04.193' AS DateTime), 9, CAST(N'2018-10-22T12:42:04.193' AS DateTime), CAST(N'2018-12-02T21:28:59.197' AS DateTime), N'XShVy5NIENgEuFenoXnEIFDXE7tirAXKtEuz0agkXV8bpckO4zkx1A==', 0, 221020181, N'a.markscott13@gmail.com')
/****** Object:  Index [UQ__SLW_USER__B9BE370E1ED998B2]    Script Date: 12/3/2018 1:29:58 AM ******/
ALTER TABLE [dbo].[slw_users] ADD UNIQUE NONCLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[slw_forms] ADD  DEFAULT (getdate()) FOR [last_updated]
GO
ALTER TABLE [dbo].[slw_client_company]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[slw_users] ([username])
GO
ALTER TABLE [dbo].[slw_form_freq]  WITH CHECK ADD FOREIGN KEY([application_id])
REFERENCES [dbo].[slw_forms] ([application_id])
GO
ALTER TABLE [dbo].[slw_form_step1]  WITH CHECK ADD FOREIGN KEY([application_id])
REFERENCES [dbo].[slw_forms] ([application_id])
GO
ALTER TABLE [dbo].[slw_form_step2]  WITH CHECK ADD FOREIGN KEY([application_id])
REFERENCES [dbo].[slw_forms] ([application_id])
GO
ALTER TABLE [dbo].[slw_forms]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[slw_users] ([username])
GO
ALTER TABLE [dbo].[slw_user_keys]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[slw_users] ([user_id])
GO
ALTER TABLE [dbo].[slw_user_notifications]  WITH CHECK ADD FOREIGN KEY([target_user])
REFERENCES [dbo].[slw_users] ([username])
GO
ALTER TABLE [dbo].[slw_user_notifications]  WITH CHECK ADD FOREIGN KEY([type])
REFERENCES [dbo].[slw_notification_types] ([types])
GO
ALTER TABLE [dbo].[slw_users]  WITH CHECK ADD FOREIGN KEY([user_type])
REFERENCES [dbo].[slw_user_types] ([user_type])
GO
/****** Object:  StoredProcedure [dbo].[sp_checkUserExist]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_checkUserExist]
(
	@username varchar(50)
)
as
begin
	select COUNT(*) as count from SLW_USERS where username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_clearAllForms]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_clearAllForms]
as
begin
	delete from slw_form_freq
	delete from slw_form_step2
	delete from slw_form_step1
	delete from slw_forms
end
GO
/****** Object:  StoredProcedure [dbo].[sp_createUser]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_createUser]
(
	@username varchar(50),
	@first_name varchar(50),
	@last_name varchar(50),
	@created_date datetime,
	@user_type integer,
	@last_password_change_date datetime,
	@last_login_date datetime,
	@hash varchar(500),
	@password_reset_required bit,
	@email varchar(100),
	@company varchar(500),
	@clientId integer
)
as
begin
	declare @count_single integer
	declare @count_all integer
	
	set @count_single = (select COUNT(username) from SLW_USERS where username=@username);
	set @count_all = (select COUNT(username) from SLW_USERS)
	set @count_all = @count_all + 1
	
	if(@count_single = 0)
	begin
		declare @day integer
		declare @month integer
		declare @year integer
		declare @user_id_str varchar(60)
		declare @user_id_int integer
		
		set @day = DAY(GETDATE())
		set @month = MONTH(GETDATE())
		set @year = YEAR(GETDATE())
		
		
		set @user_id_str = CAST(@day as varchar(10)) + CAST(@month as varchar(10)) + CAST(@year as varchar(10)) + CAST(@count_all as varchar(10))
		set @user_id_int = CAST(@user_id_str as integer)
		
		
		insert into SLW_USERS values(@username, @first_name, @last_name, @created_date, @user_type, @last_password_change_date, @last_login_date, @hash, @password_reset_required, @user_id_int, @email)
		declare @adminType integer
		
		if(@user_type != 9)
		begin
			insert into SLW_CLIENT_COMPANY values (@username, @company, @clientId)
		end
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteFreq]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_deleteFreq]
(
	@application_id varchar(50),
	@sequence integer
)
as
begin
	delete from slw_form_freq where application_id=@application_id and sequence=@sequence
end
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteUser]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_deleteUser]
(
	@username varchar(50)
)
as
begin
	declare @user_id integer
	set @user_id = (select USER_ID from SLW_USERS where username=@username)
	
	delete from SLW_USER_KEYS where user_id=@user_id
	delete from SLW_CLIENT_COMPANY where username=@username
	delete from SLW_USERS where username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getAssignedCompany]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getAssignedCompany]
(
	@username varchar(50)
)
as
begin
	select company, clientId from slw_client_company where username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getClientDetail]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getClientDetail]
(
	@clientId integer
)
as
begin
	select clientId, clientType, ccNum, clientCountry1, clientStreet1 as address, nationality, clientFaxNum, clientTelNum, clientCompany  from [ASMSGenericMaster].[dbo].[client] where clientId=@clientId 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getClientDetails]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getClientDetails]
(
	@clientCompany varchar(300)
)
as
begin
	select clientId, clientType, ccNum, clientCountry1, clientStreet1 as address, nationality, clientFaxNum, clientTelNum, clientCompany  from [ASMSGenericMaster].[dbo].[client] where clientCompany like @clientCompany+'%' 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getKeyDetail]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getKeyDetail] 
(
	@key varchar(500)
)
as
begin
	select slw_user_keys.user_id, username, access_key, last_detected_activity, max_inactivity_mins from SLW_USER_KEYS 
	inner join slw_users on slw_user_keys.user_id=slw_users.user_id where access_key=@key
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getManufacturerDetail]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getManufacturerDetail]
(
	@manufacturer varchar(200)
)
as
begin
	SELECT top 1 * from [ASMSGenericMaster].[dbo].[tbl_TypeApproval] where Dealer = @manufacturer
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getManufacturers]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getManufacturers]
(
	@query varchar(200)
)
as
begin
	if(@query = '')
	begin
		SELECT Dealer from [ASMSGenericMaster].[dbo].[tbl_TypeApproval]
	end
	else
	begin
		SELECT Dealer from [ASMSGenericMaster].[dbo].[tbl_TypeApproval] where Dealer like @query+'%' group by dealer order by dealer asc 
	end
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getModels]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getModels]
(
	@query varchar(200)
)
as
begin
	SELECT Model from [ASMSGenericMaster].[dbo].[tbl_TypeApproval] where Model like @query+'%'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getTypeApprovalDetails]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getTypeApprovalDetails]
(
	@Dealer varchar (50),
	@Model varchar (50)
)
as
begin 
	 if (@Dealer != '' AND @Model != '')
	 begin 
		SELECT c.clientCompany, TA.Dealer, TA.Model, TA.Description, TA.Address2, TA.Remarks, TA.issueDate, TA.keyTypeApprovalID  
		FROM  [ASMSGenericMaster].[dbo].[tbl_TypeApproval] TA, [ASMSGenericMaster].[dbo].[client] c 
		WHERE TA.fk_Client_ClientID = c.clientID
		AND TA.Dealer = @Dealer
		AND TA.Model = @Model
	 end
	 
	 if (@Dealer = '' AND @Model != '')
	 begin 
		SELECT c.clientCompany, TA.Dealer, TA.Model, TA.Description, TA.Address2, TA.Remarks, TA.issueDate, TA.keyTypeApprovalID  
		FROM  [ASMSGenericMaster].[dbo].[tbl_TypeApproval] TA, [ASMSGenericMaster].[dbo].[client] c 
		WHERE TA.fk_Client_ClientID = c.clientID
		AND TA.Model = @Model
	 end
	 
	 if (@Dealer != '' AND @Model = '')
	 begin 
		SELECT  c.clientCompany, TA.Dealer, TA.Model, TA.Description, TA.Address2, TA.Remarks, TA.issueDate, TA.keyTypeApprovalID  
		FROM  [ASMSGenericMaster].[dbo].[tbl_TypeApproval] TA, [ASMSGenericMaster].[dbo].[client] c 
		WHERE TA.fk_Client_ClientID = c.clientID
		AND TA.Dealer = @Dealer
	 end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getTypeApprovalTableInfo]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getTypeApprovalTableInfo]
(
	@keyTypeApprovalID int
)
as
begin 
	 SELECT LowerFrequency, UpperFrequency, PowerOutput, FrequencyTolerance, EmissionClass  FROM  [ASMSGenericMaster].[dbo].[tbl_TypeApprovalDetail] 
	 WHERE @keyTypeApprovalID = keyTypeApprovalID

end
GO
/****** Object:  StoredProcedure [dbo].[sp_getUnreadNotifications]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getUnreadNotifications]
as
begin
	select * from SLW_USER_NOTIFICATIONS where status_read = 1;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getUserCredentials]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getUserCredentials]
(
	@username varchar(50)
)
as
begin
	select hash, user_type, password_reset_required, (upper(left(lower(first_name), 1))+LOWER(SUBSTRING(first_name,2,LEN(first_name)))+' '+upper(left(lower(last_name), 1))+LOWER(SUBSTRING(last_name,2,LEN(last_name)))) as name from SLW_USERS where username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getUserTypeByKey]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getUserTypeByKey]
(
	@key varchar(500)
)
as
begin
	declare @user_id integer
	set @user_id = (select user_id from SLW_USER_KEYS where access_key=@key)
	
	select user_type from SLW_USERS where user_id=@user_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getUserTypes]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_getUserTypes]
as
begin
	select * from slw_user_types order by user_type asc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_newKey]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_newKey]
(
	@username varchar(50),
	@access_key varchar(500),
	@last_detected_activity datetime,
	@max_inactivity_mins integer
)
as
begin
	declare @count_key integer
	declare @user_id integer
	set @user_id = 0
	
	set @user_id = (select user_id from SLW_USERS where username=@username)
	set @count_key = (select COUNT(access_key) from SLW_USER_KEYS where user_id = @user_id)
	
	if(@user_id !=0)
	begin
		if(@count_key = 0)
			begin
				insert into SLW_USER_KEYS values(@user_id, @access_key, @last_detected_activity, @max_inactivity_mins)
				update SLW_USERS set last_login_date = getdate()
			end
			else
			begin
				update SLW_USER_KEYS set access_key=@access_key, last_detected_activity =@last_detected_activity, max_inactivity_mins=@max_inactivity_mins where user_id=@user_id
				update SLW_USERS set last_login_date = @last_detected_activity
			end
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_newMessage]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_newMessage]
(
	@notification_id varchar(200),
	@received_date datetime,
	@type varchar(100),
	@target_user varchar(50),
	@status_read bit,
	@message varchar(max)
)
as
begin
	insert into SLW_USER_NOTIFICATIONS (notification_id, received_date, read_date, type, target_user, status_read, message) values (@notification_id, @received_date, (select cast('1753-1-1' as datetime)),  @type, @target_user, @status_read, @message)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_newUserType]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_newUserType]
(
	@user_type integer,
	@description varchar(300)
)
as
begin
	declare @count integer
	set @count = (select count(*) from slw_user_types where user_type=@user_type)
	
	if(@count=0)
	begin
		insert into slw_user_types values(@user_type, @description)
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_resetPassword]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_resetPassword]
(
	@username varchar(50)
)
as
begin
	update SLW_USERS set password_reset_required = 1 where username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_saveFormDetails]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_saveFormDetails]
(
	@applicationId varchar(50),
	@username varchar(50),
	@applicant_name varchar(300),
	@applicant_tel varchar(300),
	@applicant_address varchar(300),
	@applicant_fax varchar(300),
	@applicant_city_town varchar(300),
	@applicant_contact_person varchar(300),
	@applicant_nationality varchar(300),
	@manufacturer_name varchar(300),
	@manufacturer_tel varchar(300),
	@manufacturer_address varchar(300),
	@manufacturer_fax varchar(300),
	@manufacturer_contact_person varchar(300),
	@provider_name varchar(300),
	@provider_telephone varchar(300),
	@provider_address varchar(300),
	@provider_fax varchar(300),
	@provider_contact_person varchar(300),
	@equipment_type varchar(300),
	@equipment_description varchar(300),
	@product_identification varchar(300),
	@ref# varchar(300),
	@make varchar(300),
	@software varchar(300),
	@type_of_equipment varchar(100),
	@other varchar(300),
	@antenna_type varchar(300),
	@antenna_gain varchar(300),
	@channel varchar(300),
	@separation varchar(300),
	@aspect varchar(300),
	@compatibility varchar(300),
	@security varchar(300),
	@equipment_comm_type varchar(300),
	@fee_code varchar(300),
	@status varchar(100)
)
as
begin
	declare @application_exist integer
	set @application_exist = (select count(application_id) from slw_forms where application_id=@applicationId)

	if(@application_exist=0)
	begin

		insert into slw_forms(username, application_id, created_date, status, last_updated)
					   values(@username, @applicationId, getdate(), @status, getdate())

		insert into slw_form_step1 (application_id, applicant_name, applicant_tel, applicant_address,
									applicant_fax, applicant_city_town, applicant_contact_person,
									applicant_nationality, manufacturer_name, manufacturer_tel, 
									manufacturer_address, manufacturer_fax, manufacturer_contact_person, provider_name, provider_telephone, provider_address, provider_fax, provider_contact_person)

									values(@applicationId, @applicant_name, @applicant_tel, @applicant_address,
										   @applicant_fax, @applicant_city_town, @applicant_contact_person,
										   @applicant_nationality, @manufacturer_name, @manufacturer_tel, 
										   @manufacturer_address, @manufacturer_fax, @manufacturer_contact_person, @provider_name, @provider_telephone, @provider_address, @provider_fax, @provider_contact_person)

		insert into slw_form_step2 (application_id, equipment_type, equipment_description, product_identifiation, ref#,
									make, software, type_of_equipment, other, antenna_type, antenna_gain, channel, separation, aspect,
									compatibility, security, equipment_comm_type, fee_code)

									values(@applicationId, @equipment_type, @equipment_description, @product_identification, @ref#,
										   @make, @software, @type_of_equipment, @other, @antenna_type, @antenna_gain, @channel, @separation, @aspect,
										   @compatibility, @security, @equipment_comm_type, @fee_code)
	end
	else
	begin
		update slw_forms set last_updated=getdate(), status=@status where application_id=@applicationId

		update slw_form_step1 set manufacturer_name=@manufacturer_name, manufacturer_tel=@manufacturer_tel, manufacturer_address=@manufacturer_address,
								  manufacturer_fax=@manufacturer_fax, manufacturer_contact_person=@manufacturer_contact_person
								  where application_id=@applicationId

		update slw_form_step2 set equipment_type=@equipment_type, equipment_description=@equipment_description, product_identifiation=@product_identification,
								  ref#=@ref#, make=@make, software=@software, type_of_equipment=@type_of_equipment, other=@other,
								  antenna_type=@antenna_type, antenna_gain=@antenna_gain, channel = @channel, separation=@separation, aspect=@aspect,
								  compatibility=@compatibility, security=@security, equipment_comm_type=@equipment_comm_type, fee_code=@fee_code 
								  where application_id=@applicationId
	end
end


GO
/****** Object:  StoredProcedure [dbo].[sp_saveFrequencyDetails]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_saveFrequencyDetails]
(
	@application_id varchar(50),
	@sequence integer,
	@lower_freq varchar(100),
	@upper_freq varchar(100),
	@power varchar(100),
	@tolerance varchar(100),
	@emmision_desig varchar(300),
	@freq_type varchar(50)
)
as
begin
	declare @sequenceExist integer
	set @sequenceExist = (select count(sequence) from slw_form_freq where application_id=@application_id)

	if(@sequenceExist = 0)
	begin
		insert into slw_form_freq (application_id, sequence, lower_freq, upper_freq, power, tolerance, emmision_desig, freq_type)
							values(@application_id, @sequence, @lower_freq, @upper_freq, @power, @tolerance, @emmision_desig, @freq_type)
	end
	else
	begin
		update slw_form_freq set lower_freq=@lower_freq, upper_freq=@upper_freq, power=@power, tolerance=@tolerance, emmision_desig=@emmision_desig,
								 freq_type=@freq_type where application_id=@application_id and sequence=@sequence
	end
end


GO
/****** Object:  StoredProcedure [dbo].[sp_updatePassword]    Script Date: 12/3/2018 1:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_updatePassword]
(
	@username varchar(50),
	@hash varchar(500)
)
as
begin
	update SLW_USERS set hash=@hash where username=@username
end
GO
USE [master]
GO
ALTER DATABASE [SLW_Database] SET  READ_WRITE 
GO
