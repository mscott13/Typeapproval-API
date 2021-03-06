USE [master]
GO
/****** Object:  Database [SLW_DATABASE]    Script Date: 1/28/2019 12:59:03 AM ******/
CREATE DATABASE [SLW_DATABASE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SLW_DATABSE', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\SLW_DATABSE.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SLW_DATABSE_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\SLW_DATABSE_log.ldf' , SIZE = 10176KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SLW_DATABASE] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SLW_DATABASE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SLW_DATABASE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET ARITHABORT OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SLW_DATABASE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SLW_DATABASE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SLW_DATABASE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SLW_DATABASE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET RECOVERY FULL 
GO
ALTER DATABASE [SLW_DATABASE] SET  MULTI_USER 
GO
ALTER DATABASE [SLW_DATABASE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SLW_DATABASE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SLW_DATABASE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SLW_DATABASE] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SLW_DATABASE] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SLW_DATABASE] SET QUERY_STORE = OFF
GO
USE [SLW_DATABASE]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_getCurrentAppStatus]    Script Date: 1/28/2019 12:59:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[fn_getCurrentAppStatus]
(
	@app_id varchar(100)
)
returns varchar(100)
as
begin
	declare @result varchar(100)
	set @result = (select status from slw_forms where application_id=@app_id)
	
	if(@result is null)
	begin
		set @result = 'not_applicable'
	end
	return @result
end
GO
/****** Object:  Table [dbo].[certificates]    Script Date: 1/28/2019 12:59:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[certificates](
	[sequence] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NULL,
	[application_id] [varchar](100) NULL,
	[approval_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[sequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_activity_types]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_activity_types](
	[type] [varchar](100) NOT NULL,
	[type_description] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_application_files]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_application_files](
	[file_id] [varchar](300) NULL,
	[filename] [varchar](500) NULL,
	[created_date] [datetime] NULL,
	[path] [varchar](500) NULL,
	[purpose] [varchar](1000) NULL,
	[application_id] [varchar](50) NULL,
	[username] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_assigned_company]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_assigned_company](
	[username] [varchar](50) NULL,
	[company] [varchar](500) NULL,
	[clientId] [int] NULL,
	[source] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_client_companies]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_client_companies](
	[client_id] [int] NOT NULL,
	[name] [varchar](500) NULL,
	[telephone] [varchar](300) NULL,
	[address] [varchar](500) NULL,
	[fax] [varchar](300) NULL,
	[cityTown] [varchar](300) NULL,
	[contactPerson] [varchar](300) NULL,
	[nationality] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[client_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_email_settings]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_email_settings](
	[email] [varchar](300) NOT NULL,
	[last_accessed] [datetime] NULL,
	[company_name] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_form_freq]    Script Date: 1/28/2019 12:59:04 AM ******/
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
	[freq_type] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_form_step1]    Script Date: 1/28/2019 12:59:04 AM ******/
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
	[manufacturer_contact_person] [varchar](300) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_form_step2]    Script Date: 1/28/2019 12:59:04 AM ******/
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
	[additional_info] [varchar](max) NULL,
	[name_of_test] [varchar](500) NULL,
	[country] [varchar](500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_forms]    Script Date: 1/28/2019 12:59:04 AM ******/
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
	[category] [varchar](300) NULL,
	[licensed_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[application_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_manufacturers]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_manufacturers](
	[manufacturer_id] [int] NOT NULL,
	[dealer] [varchar](500) NULL,
	[address] [varchar](500) NULL,
	[telephone] [varchar](300) NULL,
	[fax] [varchar](300) NULL,
	[contact_person] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[manufacturer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_notification_types]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  Table [dbo].[slw_ongoing_tasks]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_ongoing_tasks](
	[application_id] [varchar](100) NULL,
	[created_date] [datetime] NULL,
	[assigned_to] [varchar](50) NULL,
	[submitted_by] [varchar](50) NULL,
	[submitted_by_username] [varchar](50) NULL,
	[date_assigned] [datetime] NULL,
	[status] [varchar](100) NULL,
	[assigned_by] [varchar](100) NULL,
	[assigned_by_username] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_unassigned_tasks]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_unassigned_tasks](
	[application_id] [varchar](100) NULL,
	[created_date] [datetime] NULL,
	[submitted_by] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_user_activity]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slw_user_activity](
	[username] [varchar](50) NULL,
	[sequence] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](100) NULL,
	[created_date] [datetime] NULL,
	[description] [varchar](max) NULL,
	[extras] [varchar](300) NULL,
	[priority] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[sequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slw_user_keys]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  Table [dbo].[slw_user_notifications]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  Table [dbo].[slw_user_types]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  Table [dbo].[slw_users]    Script Date: 1/28/2019 12:59:04 AM ******/
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
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Account', N'Login / Logout / Account Updates')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Approval', N'An application that received approval by the SMA internal staff.')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Cancellation', N'A cancelled application that is not completed.')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Change Password', N'Update password')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Create Account', N'Create a new user account')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Delete Account', N'Delete an existing user account')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Error', N'Represents all errors that occurred during program execution')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Login', N'User login attempt')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Move Ongoing', N'Move unassigned task to ongoing')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Move Unassigned', N'Move ongoing task to unassigned category')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'New Application', N'Represents a newly created application that is not completed')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'New Ongoing', N'New ongoing task')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'New Unassigned', N'New unassigned task')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Reject Unassigned', N'Mark application as rejected and remove it from the administrator''s application listing')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Reset Password', N'Marks an account with a flag to indicate password reset is required on the next login')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Set Email', N'Update email to use for sending notifications to engineers/clients')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Submission', N'A completed application that has all the required data.')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Task Assignment', N'Unassigned task assigned to a new engineer')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Task Reassignment', N'Application is reassigned to a new engineer')
INSERT [dbo].[slw_activity_types] ([type], [type_description]) VALUES (N'Update', N'Data added, removed or added to an incomplete application')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'0d8e6198-fbf1-4eae-9e50-074fa90cb345', N'rptjmccertradiodetail.pdf', CAST(N'2019-01-23T13:31:12.057' AS DateTime), N'C:\Users\asms-accpac-2\Desktop\Git repositories\Typeapproval-API\WebService\applications\jpublic\100000\technical_specifications\rptjmccertradiodetail.pdf', N'TECHNICAL_SPECIFICATION', N'100000', N'jpublic')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'f89d5e4e-1604-484c-a8f3-cebd1d64f2b3', N'MonthlyDefferedIncomeReport20180401.pdf', CAST(N'2019-01-23T13:31:12.073' AS DateTime), N'C:\Users\asms-accpac-2\Desktop\Git repositories\Typeapproval-API\WebService\applications\jpublic\100000\test_report\MonthlyDefferedIncomeReport20180401.pdf', N'TEST_REPORT', N'100000', N'jpublic')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'c16a3177-f5a5-4804-bae7-147d8cdac1f3', N'MonthlyDefferedIncomeSummaryReport20180401.pdf', CAST(N'2019-01-23T13:31:12.083' AS DateTime), N'C:\Users\asms-accpac-2\Desktop\Git repositories\Typeapproval-API\WebService\applications\jpublic\100000\test_report\MonthlyDefferedIncomeSummaryReport20180401.pdf', N'TEST_REPORT', N'100000', N'jpublic')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'c4d60b1b-54fd-492d-bb3f-4bb395af19da', N'DefferedincomeReportFor1 2018.pdf', CAST(N'2019-01-23T13:31:12.093' AS DateTime), N'C:\Users\asms-accpac-2\Desktop\Git repositories\Typeapproval-API\WebService\applications\jpublic\100000\accreditation\DefferedincomeReportFor1 2018.pdf', N'ACCREDITATION', N'100000', N'jpublic')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'7b6581d4-52a2-4f08-99b8-5a1ec7422f39', N'Techical Specifications.pdf', CAST(N'2019-01-24T12:49:35.433' AS DateTime), N'C:\Users\asms-accpac-2\Desktop\Git repositories\Typeapproval-API\WebService\applications\jdoe\100002\technical_specifications\Techical Specifications.pdf', N'TECHNICAL_SPECIFICATION', N'100002', N'jdoe')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'66d3215e-8171-46c6-9169-24421f3c1e73', N'Techical Specifications.pdf', CAST(N'2019-01-24T12:49:35.437' AS DateTime), N'C:\Users\asms-accpac-2\Desktop\Git repositories\Typeapproval-API\WebService\applications\jdoe\100002\test_report\Techical Specifications.pdf', N'TEST_REPORT', N'100002', N'jdoe')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'9ddf3ec8-7c7b-4390-a3fd-cfd89c89f662', N'Test Report 1.pdf', CAST(N'2019-01-24T12:49:35.457' AS DateTime), N'C:\Users\asms-accpac-2\Desktop\Git repositories\Typeapproval-API\WebService\applications\jdoe\100002\test_report\Test Report 1.pdf', N'TEST_REPORT', N'100002', N'jdoe')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'43bc93ae-f556-4a83-b777-a3ed99ae81e3', N'FCC Certificate.pdf', CAST(N'2019-01-24T12:49:35.460' AS DateTime), N'C:\Users\asms-accpac-2\Desktop\Git repositories\Typeapproval-API\WebService\applications\jdoe\100002\accreditation\FCC Certificate.pdf', N'ACCREDITATION', N'100002', N'jdoe')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'6c97bcb6-73f4-4436-b047-aa8db3e5b23d', N'dummy.pdf', CAST(N'2019-01-27T12:13:15.860' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100007\technical_specifications\dummy.pdf', N'TECHNICAL_SPECIFICATION', N'100007', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'9d4ed733-199c-4de4-a491-5155d0e2bcc1', N'pdf-test.pdf', CAST(N'2019-01-27T12:13:15.910' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100007\test_report\pdf-test.pdf', N'TEST_REPORT', N'100007', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'c3e5f872-45a8-454a-84df-e050e353a486', N'pdf-test0v2.pdf', CAST(N'2019-01-27T12:13:15.930' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100007\accreditation\pdf-test0v2.pdf', N'ACCREDITATION', N'100007', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'31c29acc-0dd5-4f9f-ad96-5b2cb2c599f5', N'pdf-test.pdf', CAST(N'2019-01-27T12:15:09.120' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100004\technical_specifications\pdf-test.pdf', N'TECHNICAL_SPECIFICATION', N'100004', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'd6cb4e0c-1234-4aac-b4e3-feb0856cde61', N'dummy.pdf', CAST(N'2019-01-27T12:15:09.200' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100004\test_report\dummy.pdf', N'TEST_REPORT', N'100004', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'ee4afe5c-438f-47e5-a3fc-8ad86f0f9e0f', N'dummy.pdf', CAST(N'2019-01-27T12:15:09.273' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100004\accreditation\dummy.pdf', N'ACCREDITATION', N'100004', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'8f4aa1fd-2163-44a7-9dcb-c18f86f6628c', N'dummy.pdf', CAST(N'2019-01-27T12:16:01.100' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100004\technical_specifications\dummy.pdf', N'TECHNICAL_SPECIFICATION', N'100004', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'35dbf28c-fb59-4ecc-8249-16c006053078', N'pdf-test.pdf', CAST(N'2019-01-27T12:16:01.107' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100004\test_report\pdf-test.pdf', N'TEST_REPORT', N'100004', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'a3ff95f2-0aee-4cbe-8642-cccae559717c', N'pdf-test0v2.pdf', CAST(N'2019-01-27T12:16:01.110' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100004\accreditation\pdf-test0v2.pdf', N'ACCREDITATION', N'100004', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'81c49609-5962-4e28-aca8-4d94df011122', N'dummy.pdf', CAST(N'2019-01-27T23:57:04.980' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100001\technical_specifications\dummy.pdf', N'TECHNICAL_SPECIFICATION', N'100001', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'3c4c4505-dd12-4ee9-9942-6e8c9d5ad669', N'pdf-test0v2.pdf', CAST(N'2019-01-27T23:57:05.010' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100001\technical_specifications\pdf-test0v2.pdf', N'TECHNICAL_SPECIFICATION', N'100001', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'b81aa5c6-661d-4131-b066-1fed12664f98', N'dummy.pdf', CAST(N'2019-01-27T23:57:05.013' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100001\test_report\dummy.pdf', N'TEST_REPORT', N'100001', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'7445535a-4e28-4b16-9dad-03c016b244c2', N'pdf-test.pdf', CAST(N'2019-01-27T23:57:05.013' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100001\test_report\pdf-test.pdf', N'TEST_REPORT', N'100001', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'09b5cf87-27f3-4e82-818d-2b5601c9792e', N'pdf-test0v2.pdf', CAST(N'2019-01-27T23:57:05.020' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100001\test_report\pdf-test0v2.pdf', N'TEST_REPORT', N'100001', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'b0a29ad6-0d29-4068-a606-36013c7e70f2', N'pdf-test.pdf', CAST(N'2019-01-27T23:57:05.023' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100001\accreditation\pdf-test.pdf', N'ACCREDITATION', N'100001', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'cf7bd6a0-b172-4569-bdb5-d5a143d92996', N'pdf-test0v2.pdf', CAST(N'2019-01-27T23:57:05.030' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100001\accreditation\pdf-test0v2.pdf', N'ACCREDITATION', N'100001', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'6a2102b4-d6f1-43a5-b812-69d167dbd638', N'dummy.pdf', CAST(N'2019-01-28T00:48:40.363' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100005\technical_specifications\dummy.pdf', N'TECHNICAL_SPECIFICATION', N'100005', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'c8277923-6d96-459a-ae07-1d30b46a585b', N'pdf-test0v2.pdf', CAST(N'2019-01-28T00:48:40.367' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100005\technical_specifications\pdf-test0v2.pdf', N'TECHNICAL_SPECIFICATION', N'100005', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'e5fa7679-3bc0-442d-a2e4-3d75b23f7db8', N'pdf-test.pdf', CAST(N'2019-01-28T00:48:40.370' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100005\test_report\pdf-test.pdf', N'TEST_REPORT', N'100005', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'56521fda-f45e-4f13-a6fc-c3a482b8c2d2', N'pdf-test0v2.pdf', CAST(N'2019-01-28T00:48:40.373' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100005\test_report\pdf-test0v2.pdf', N'TEST_REPORT', N'100005', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'b62c20e2-ae4b-4d7e-a0d7-66e43feb08dc', N'pdf-test0v2.pdf', CAST(N'2019-01-28T00:48:40.377' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100005\accreditation\pdf-test0v2.pdf', N'ACCREDITATION', N'100005', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'e618bdd3-d630-4b79-89ce-cc42b9f41e69', N'pdf-test.pdf', CAST(N'2019-01-28T00:48:40.380' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100005\accreditation\pdf-test.pdf', N'ACCREDITATION', N'100005', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'124a1d89-134b-44ee-b792-a85a0361dad7', N'dummy.pdf', CAST(N'2019-01-28T00:48:40.383' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100005\letter of authorization\dummy.pdf', N'LETTER_OF_AUTHORIZATION', N'100005', N'client')
INSERT [dbo].[slw_application_files] ([file_id], [filename], [created_date], [path], [purpose], [application_id], [username]) VALUES (N'9bae4b4c-d3fe-45c8-b7b7-ce17b3fc2e87', N'pdf-test0v2.pdf', CAST(N'2019-01-28T00:48:40.387' AS DateTime), N'C:\Users\Mark Scott\Desktop\Git Repositories\Typeapproval-API\WebService\applications\client\100005\user manual\pdf-test0v2.pdf', N'USER_MANUAL', N'100005', N'client')
INSERT [dbo].[slw_assigned_company] ([username], [company], [clientId], [source]) VALUES (N'client', N'3G Wireless LLC', 7880, N'Asms')
INSERT [dbo].[slw_assigned_company] ([username], [company], [clientId], [source]) VALUES (N'admin', N'Spectrum Management Authority', 11698, N'Asms')
INSERT [dbo].[slw_assigned_company] ([username], [company], [clientId], [source]) VALUES (N'EngineerTest', N'Spectrum Management Authority, Jamaica', 0, N'Asms')
INSERT [dbo].[slw_assigned_company] ([username], [company], [clientId], [source]) VALUES (N'cgriffith', N'Spectrum Management Authority, Jamaica', 0, N'Asms')
INSERT [dbo].[slw_assigned_company] ([username], [company], [clientId], [source]) VALUES (N'jpublic', N'Wireless National', 15009, N'Local')
INSERT [dbo].[slw_assigned_company] ([username], [company], [clientId], [source]) VALUES (N'jdoe', N'Wireless Approval Inc', 15010, N'Local')
INSERT [dbo].[slw_assigned_company] ([username], [company], [clientId], [source]) VALUES (N'kchin', N'UL Japan Inc.', 8854, N'Asms')
INSERT [dbo].[slw_assigned_company] ([username], [company], [clientId], [source]) VALUES (N'jstreet', N'Wireless Approvers Inc', 15011, N'Local')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15000, N'efefw', N'efef', N'efefef', N'fef', N'', N'efwfe', N'0')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15001, N'ffefefefe', N'ee', N'feffw', N'efef', N'', N'fwef', N'0')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15002, N'Spectrum Management Authority Jamaica', N'8765424698', N'1e tavern, papine kingston, jamiaca', N'1144785665', N'', N'Junior Black', N'0')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15003, N'efwef', N'fewfe', N'efwef', N'fewf', N'', N'efw', N'0')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15004, N'June Systems', N'18765555', N'1600 Amphitheatre Parkway, Mountain View, CA', N'11122222222', N'', N'', N'0')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15005, N'University of technology, Jamaica', N'1876-554-9877', N'11 harbour street, Kingston, Jamaica', N'1876-662-1477', N'', N'', N'0')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15006, N'"Central 96.3 FM Jamaica", "Adoration Entertainment"s', N'ewfwef', N'efef', N'', N'', N'', N'')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15007, N'Wireless International', N'15613134596', N'123 Boulevard CA 23456 USA', N'15613134597', N'', N'John Public', N'')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15008, N'Test company', N'876-554-4478', N'Shaw park heights, ocho rios', N'876-554-4478', N'', N'', N'')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15009, N'Wireless National', N'15613347689', N'1234 Cedar Boulevard, CA 5467, USA', N'15613347680', N'', N'John Public', N'')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15010, N'Wireless Approval Inc', N'1-561-674-5321', N'234 Allerdale Avenue, CA 5671, USA', N'1-561-674-5322', N'', N'John Doe', N'')
INSERT [dbo].[slw_client_companies] ([client_id], [name], [telephone], [address], [fax], [cityTown], [contactPerson], [nationality]) VALUES (15011, N'Wireless Approvers Inc', N'15616784534', N'234 North Avenue, CA 56543, USA', N'15616784538', N'', N'John Street', N'')
INSERT [dbo].[slw_email_settings] ([email], [last_accessed], [company_name]) VALUES (N'jbiggs@sma.gov.jm', CAST(N'2019-01-25T11:13:31.793' AS DateTime), N'Spectrum Management Authority')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100000', 1, N'2480', N'2490', N'0.101', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100000', 2, N'1200', N'1240', N'0.324', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100000', 3, N'500', N'501', N'0.234', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100001', 1, N'3322', N'5400', N'', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100002', 1, N'2400', N'2480', N'0.123', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100002', 2, N'2483', N'2489', N'0.234', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100003', 1, N'2400', N'2480', N'0.112', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100003', 2, N'2483', N'2500', N'0.342', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100004', 1, N'22', N'222', N'', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100005', 1, N'45445', N'54545', N'', N'', N'', N'T')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100006', 1, N'2400', N'2480', N'0.101', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100006', 2, N'1500', N'1550', N'0.324', N'', N'', N'')
INSERT [dbo].[slw_form_freq] ([application_id], [sequence], [lower_freq], [upper_freq], [power], [tolerance], [emmision_desig], [freq_type]) VALUES (N'100007', 1, N'111', N'2222', N'', N'', N'', N'')
INSERT [dbo].[slw_form_step1] ([application_id], [applicant_name], [applicant_tel], [applicant_address], [applicant_fax], [applicant_city_town], [applicant_contact_person], [applicant_nationality], [manufacturer_name], [manufacturer_tel], [manufacturer_address], [manufacturer_fax], [manufacturer_contact_person]) VALUES (N'100000', N'Wireless National', N'15613347689', N'1234 Cedar Boulevard, CA 5467, USA', N'15613347680', N'unavailable', N'John Public', N'unavailable', N'Apple, Inc.', N'5414105915', N'1 Infinite Loop, Cupertino CA 95014-2084  USA', N'541-719-8195', N'Joseph Biggs')
INSERT [dbo].[slw_form_step1] ([application_id], [applicant_name], [applicant_tel], [applicant_address], [applicant_fax], [applicant_city_town], [applicant_contact_person], [applicant_nationality], [manufacturer_name], [manufacturer_tel], [manufacturer_address], [manufacturer_fax], [manufacturer_contact_person]) VALUES (N'100001', N'3G Wireless LLC', N'410-969-3508', N'7763 Old Telegraph Road, Ste 10 Severn', N'1876-555-4471', N'Kingstown', N'Kia johnson', N'Jamaica', N'Acer Incorporated', N'6516475930', N'8F, 88, Sec 1, Hsin Tai Wu Rd  Hsichih, Taipei Hsien, 221  Taiwan', N'', N'')
INSERT [dbo].[slw_form_step1] ([application_id], [applicant_name], [applicant_tel], [applicant_address], [applicant_fax], [applicant_city_town], [applicant_contact_person], [applicant_nationality], [manufacturer_name], [manufacturer_tel], [manufacturer_address], [manufacturer_fax], [manufacturer_contact_person]) VALUES (N'100002', N'Wireless Approval Inc', N'1-561-674-5321', N'234 Allerdale Avenue, CA 5671, USA', N'1-561-674-5322', N'unavailable', N'John Doe', N'unavailable', N'Apple, Inc.', N'5414105915', N'1 Infinite Loop, Cupertino CA 95014-2084  USA', N'5414105916', N'Steve Jobs')
INSERT [dbo].[slw_form_step1] ([application_id], [applicant_name], [applicant_tel], [applicant_address], [applicant_fax], [applicant_city_town], [applicant_contact_person], [applicant_nationality], [manufacturer_name], [manufacturer_tel], [manufacturer_address], [manufacturer_fax], [manufacturer_contact_person]) VALUES (N'100003', N'UL Japan Inc.', N'81596248116', N'unavailable', N'81596248095', N'unavailable', N'unavailable', N'J', N'Samsung Electronics Co., Ltd.', N'6516475930', N'129, Samsung-ro, Yeongtong-gu Suwon-si, Gyeonggi-do,  443-742, Korea', N'541-719-8195', N'Jason Gordon')
INSERT [dbo].[slw_form_step1] ([application_id], [applicant_name], [applicant_tel], [applicant_address], [applicant_fax], [applicant_city_town], [applicant_contact_person], [applicant_nationality], [manufacturer_name], [manufacturer_tel], [manufacturer_address], [manufacturer_fax], [manufacturer_contact_person]) VALUES (N'100004', N'3G Wireless LLC', N'410-969-3508', N'7763 Old Telegraph Road, Ste 10 Severn', N'1876-555-4471', N'Kingstown', N'Kia johnson', N'Jamaica', N'Ability Enterprise Co., Ltd.', N'+886-2-8601-3788', N'4F., No.8, Ln. 7, Wuquan Rd.  Wugu Dist.  New Taipei City, 24886  Taiwan', N'+886-2-8601-3789', N'')
INSERT [dbo].[slw_form_step1] ([application_id], [applicant_name], [applicant_tel], [applicant_address], [applicant_fax], [applicant_city_town], [applicant_contact_person], [applicant_nationality], [manufacturer_name], [manufacturer_tel], [manufacturer_address], [manufacturer_fax], [manufacturer_contact_person]) VALUES (N'100005', N'3G Wireless LLC', N'410-969-3508', N'7763 Old Telegraph Road, Ste 10 Severn', N'efweffef', N'unavailablefewfefefwe', N'unavailablewdwd', N'USA            ewfefef     ', N'Ability Enterprise Co., Ltd.', N'+886-2-8601-3788', N'4F., No.8, Ln. 7, Wuquan Rd.  Wugu Dist.  New Taipei City, 24886  Taiwan', N'+886-2-8601-3789', N'')
INSERT [dbo].[slw_form_step1] ([application_id], [applicant_name], [applicant_tel], [applicant_address], [applicant_fax], [applicant_city_town], [applicant_contact_person], [applicant_nationality], [manufacturer_name], [manufacturer_tel], [manufacturer_address], [manufacturer_fax], [manufacturer_contact_person]) VALUES (N'100006', N'Wireless Approvers Inc', N'15616784534', N'234 North Avenue, CA 56543, USA', N'15616784538', N'unavailable', N'John Street', N'American', N'Apple, Inc.', N'5414105915', N'1 Infinite Loop, Cupertino CA 95014-2084  USA', N'541-719-8195', N'Steve Jobs')
INSERT [dbo].[slw_form_step1] ([application_id], [applicant_name], [applicant_tel], [applicant_address], [applicant_fax], [applicant_city_town], [applicant_contact_person], [applicant_nationality], [manufacturer_name], [manufacturer_tel], [manufacturer_address], [manufacturer_fax], [manufacturer_contact_person]) VALUES (N'100007', N'3G Wireless LLC', N'410-969-3508', N'7763 Old Telegraph Road, Ste 10 Severn', N'unavailable', N'unavailable', N'unavailable', N'USA                 ', N'3M Interamerica', N'999-999-9999', N'20-24 Barbados Avenue', N'', N'')
INSERT [dbo].[slw_form_step1] ([application_id], [applicant_name], [applicant_tel], [applicant_address], [applicant_fax], [applicant_city_town], [applicant_contact_person], [applicant_nationality], [manufacturer_name], [manufacturer_tel], [manufacturer_address], [manufacturer_fax], [manufacturer_contact_person]) VALUES (N'100008', N'3G Wireless LLC', N'410-969-3508', N'7763 Old Telegraph Road, Ste 10 Severn', N'unavailable', N'unavailable', N'unavailable', N'USA                 ', N'Ability Enterprise Co., Ltd.', N'+886-2-8601-3788', N'4F., No.8, Ln. 7, Wuquan Rd.  Wugu Dist.  New Taipei City, 24886  Taiwan', N'+886-2-8601-3789', N'')
INSERT [dbo].[slw_form_step2] ([application_id], [equipment_type], [equipment_description], [product_identifiation], [ref#], [make], [software], [type_of_equipment], [other], [antenna_type], [antenna_gain], [channel], [separation], [additional_info], [name_of_test], [country]) VALUES (N'100000', N'Mobile Phone', N'Mobile Phone', N'A1901XII', N'', N'Apple', N'', N'Transceiver', N'', N'Integral', N'', N'', N'', N'FCC ID: BCGA1901', N'Federal Communications Commission ', N'Korea')
INSERT [dbo].[slw_form_step2] ([application_id], [equipment_type], [equipment_description], [product_identifiation], [ref#], [make], [software], [type_of_equipment], [other], [antenna_type], [antenna_gain], [channel], [separation], [additional_info], [name_of_test], [country]) VALUES (N'100001', N'Test equipment', N'Test equipment used for arbitrary purposes..', N'TEST-MODEL', N'', N'Test make', N'', N'Transmitter', N'', N'ATDI Parabolic ITU R-1428', N'', N'', N'', N'FCC test device lllL', N'Spectrum Management Authority', N'Jamaica')
INSERT [dbo].[slw_form_step2] ([application_id], [equipment_type], [equipment_description], [product_identifiation], [ref#], [make], [software], [type_of_equipment], [other], [antenna_type], [antenna_gain], [channel], [separation], [additional_info], [name_of_test], [country]) VALUES (N'100002', N'Mobile Phone', N'Mobile Phone', N'ABC1902', N'', N'iPhone', N'', N'Transceiver', N'', N'Integral', N'', N'', N'', N'FCC ID: BCGABC1902', N'Federal Communications Commission', N'Japan')
INSERT [dbo].[slw_form_step2] ([application_id], [equipment_type], [equipment_description], [product_identifiation], [ref#], [make], [software], [type_of_equipment], [other], [antenna_type], [antenna_gain], [channel], [separation], [additional_info], [name_of_test], [country]) VALUES (N'100003', N'Mobile Phone', N'Mobile Phone', N'SM-900C', N'', N'Samsung', N'', N'Transceiver', N'', N'Integral', N'', N'', N'', N'FCC ID: ACLSM-900C', N'Federal Communications Commission', N'Japan')
INSERT [dbo].[slw_form_step2] ([application_id], [equipment_type], [equipment_description], [product_identifiation], [ref#], [make], [software], [type_of_equipment], [other], [antenna_type], [antenna_gain], [channel], [separation], [additional_info], [name_of_test], [country]) VALUES (N'100004', N'efwfef', N'wfefef', N'efwef', N'', N'ewfwe', N'', N'Receiver', N'', N'fwefef', N'wef', N'', N'', N'efwefefwef', N'wefwefeffeffffwffwwefef', N'wefefewfefefwefefwefe')
INSERT [dbo].[slw_form_step2] ([application_id], [equipment_type], [equipment_description], [product_identifiation], [ref#], [make], [software], [type_of_equipment], [other], [antenna_type], [antenna_gain], [channel], [separation], [additional_info], [name_of_test], [country]) VALUES (N'100005', N'fygoyuy', N'gfifyguh', N'yug7uuhu', N'', N'ughuhui9uhuh', N'', N'Transmitter', N'', N'fghntyujku', N'', N'', N'', N'tu,itukyufikuyk', N'kutuyktukytku', N'uuyktuykyuk')
INSERT [dbo].[slw_form_step2] ([application_id], [equipment_type], [equipment_description], [product_identifiation], [ref#], [make], [software], [type_of_equipment], [other], [antenna_type], [antenna_gain], [channel], [separation], [additional_info], [name_of_test], [country]) VALUES (N'100006', N'Mobile Phone', N'Mobile Phone', N'A1904EX', N'', N'iPhone', N'', N'Transceiver', N'', N'Integral', N'', N'', N'', N'FCC ID: BCG-A1904EX', N'FCC', N'Korea')
INSERT [dbo].[slw_form_step2] ([application_id], [equipment_type], [equipment_description], [product_identifiation], [ref#], [make], [software], [type_of_equipment], [other], [antenna_type], [antenna_gain], [channel], [separation], [additional_info], [name_of_test], [country]) VALUES (N'100007', N'Wireless router', N'fe4fwerfew', N'fefwefewfefwe', N'', N'fefewf', N'', N'Receiver', N'', N'wdqwdwd', N'', N'', N'', N'dwdqwdqwdwdqwd', N'efwefef', N'ewfefefefef')
INSERT [dbo].[slw_form_step2] ([application_id], [equipment_type], [equipment_description], [product_identifiation], [ref#], [make], [software], [type_of_equipment], [other], [antenna_type], [antenna_gain], [channel], [separation], [additional_info], [name_of_test], [country]) VALUES (N'100008', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'')
INSERT [dbo].[slw_forms] ([username], [application_id], [created_date], [status], [last_updated], [category], [licensed_date]) VALUES (N'jpublic', N'100000', CAST(N'2019-01-23T13:31:11.523' AS DateTime), N'LICENSED', CAST(N'2019-01-23T13:31:11.523' AS DateTime), N'TYPE_APPROVAL', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[slw_forms] ([username], [application_id], [created_date], [status], [last_updated], [category], [licensed_date]) VALUES (N'client', N'100001', CAST(N'2019-01-23T15:59:54.180' AS DateTime), N'SUBMITTED', CAST(N'2019-01-27T23:55:59.573' AS DateTime), N'TYPE_APPROVAL', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[slw_forms] ([username], [application_id], [created_date], [status], [last_updated], [category], [licensed_date]) VALUES (N'jdoe', N'100002', CAST(N'2019-01-24T12:17:27.867' AS DateTime), N'LICENSED', CAST(N'2019-01-24T12:49:35.407' AS DateTime), N'TYPE_APPROVAL', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[slw_forms] ([username], [application_id], [created_date], [status], [last_updated], [category], [licensed_date]) VALUES (N'kchin', N'100003', CAST(N'2019-01-24T16:31:06.660' AS DateTime), N'RESUBMIT', CAST(N'2019-01-25T10:47:31.617' AS DateTime), N'TYPE_APPROVAL', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[slw_forms] ([username], [application_id], [created_date], [status], [last_updated], [category], [licensed_date]) VALUES (N'client', N'100004', CAST(N'2019-01-24T20:21:00.520' AS DateTime), N'SUBMITTED', CAST(N'2019-01-27T12:16:01.077' AS DateTime), N'TYPE_APPROVAL', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[slw_forms] ([username], [application_id], [created_date], [status], [last_updated], [category], [licensed_date]) VALUES (N'client', N'100005', CAST(N'2019-01-24T23:04:54.030' AS DateTime), N'SUBMITTED', CAST(N'2019-01-28T00:48:40.333' AS DateTime), N'TYPE_APPROVAL', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[slw_forms] ([username], [application_id], [created_date], [status], [last_updated], [category], [licensed_date]) VALUES (N'jstreet', N'100006', CAST(N'2019-01-25T11:32:03.373' AS DateTime), N'INCOMPLETE', CAST(N'2019-01-25T11:52:15.427' AS DateTime), N'TYPE_APPROVAL', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[slw_forms] ([username], [application_id], [created_date], [status], [last_updated], [category], [licensed_date]) VALUES (N'client', N'100007', CAST(N'2019-01-27T12:04:50.370' AS DateTime), N'SUBMITTED', CAST(N'2019-01-27T12:13:15.643' AS DateTime), N'TYPE_APPROVAL', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[slw_forms] ([username], [application_id], [created_date], [status], [last_updated], [category], [licensed_date]) VALUES (N'client', N'100008', CAST(N'2019-01-28T00:01:18.983' AS DateTime), N'INCOMPLETE', CAST(N'2019-01-28T00:01:18.983' AS DateTime), N'TYPE_APPROVAL', CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[slw_manufacturers] ([manufacturer_id], [dealer], [address], [telephone], [fax], [contact_person]) VALUES (15000, N'3M Inter Americas', N'efwe', N'efwf', N'wfewe', N'ewf')
INSERT [dbo].[slw_manufacturers] ([manufacturer_id], [dealer], [address], [telephone], [fax], [contact_person]) VALUES (15001, N'fewfef', N'fewfe', N'efwfe', N'fewff', N'wef')
INSERT [dbo].[slw_manufacturers] ([manufacturer_id], [dealer], [address], [telephone], [fax], [contact_person]) VALUES (15002, N'fefwef', N'efwef', N'fewf', N'fewfe', N'wef')
INSERT [dbo].[slw_manufacturers] ([manufacturer_id], [dealer], [address], [telephone], [fax], [contact_person]) VALUES (15003, N'SpringsZ', N'fewfewf', N'fefew', N'efwf', N'efwf')
INSERT [dbo].[slw_manufacturers] ([manufacturer_id], [dealer], [address], [telephone], [fax], [contact_person]) VALUES (15004, N'Test manufacturer', N'test address', N'187656544', N'', N'')
INSERT [dbo].[slw_notification_types] ([types]) VALUES (N'GENERAL')
INSERT [dbo].[slw_notification_types] ([types]) VALUES (N'TYPE_APPROVAL')
INSERT [dbo].[slw_ongoing_tasks] ([application_id], [created_date], [assigned_to], [submitted_by], [submitted_by_username], [date_assigned], [status], [assigned_by], [assigned_by_username]) VALUES (N'100000', CAST(N'2019-01-23T13:31:11.377' AS DateTime), N'cgriffith', N'John Public', N'jpublic', CAST(N'2019-01-24T10:07:09.060' AS DateTime), N'LICENSED', N'Task Admin', N'TaskAdmin')
INSERT [dbo].[slw_ongoing_tasks] ([application_id], [created_date], [assigned_to], [submitted_by], [submitted_by_username], [date_assigned], [status], [assigned_by], [assigned_by_username]) VALUES (N'100002', CAST(N'2019-01-24T12:49:35.423' AS DateTime), N'cgriffith', N'John Doe', N'jdoe', CAST(N'2019-01-24T12:50:26.963' AS DateTime), N'LICENSED', N'Philmore Trowers', N'ptrowers')
INSERT [dbo].[slw_unassigned_tasks] ([application_id], [created_date], [submitted_by]) VALUES (N'100007', CAST(N'2019-01-27T12:13:15.653' AS DateTime), N'client')
INSERT [dbo].[slw_unassigned_tasks] ([application_id], [created_date], [submitted_by]) VALUES (N'100004', CAST(N'2019-01-27T12:15:08.967' AS DateTime), N'client')
INSERT [dbo].[slw_unassigned_tasks] ([application_id], [created_date], [submitted_by]) VALUES (N'100004', CAST(N'2019-01-27T12:16:01.087' AS DateTime), N'client')
INSERT [dbo].[slw_unassigned_tasks] ([application_id], [created_date], [submitted_by]) VALUES (N'100001', CAST(N'2019-01-27T23:55:59.940' AS DateTime), N'client')
INSERT [dbo].[slw_unassigned_tasks] ([application_id], [created_date], [submitted_by]) VALUES (N'100005', CAST(N'2019-01-28T00:48:40.343' AS DateTime), N'client')
SET IDENTITY_INSERT [dbo].[slw_user_activity] ON 

INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 16926, N'Login', CAST(N'2019-01-23T13:04:49.800' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 16927, N'Set Email', CAST(N'2019-01-23T13:05:34.717' AS DateTime), N'jbiggs@sma.gov.jm', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jpublic', 16928, N'Create Account', CAST(N'2019-01-23T13:21:33.033' AS DateTime), N'', N'', 0)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jpublic', 16929, N'Login', CAST(N'2019-01-23T13:21:53.157' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jpublic', 16930, N'Submission', CAST(N'2019-01-23T13:31:12.210' AS DateTime), N'100000', N'SUBMITTED', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16931, N'Login', CAST(N'2019-01-23T13:36:17.587' AS DateTime), N'incorrect credentials. login failed', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16932, N'Login', CAST(N'2019-01-23T13:36:21.397' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16933, N'New Ongoing', CAST(N'2019-01-23T13:36:43.210' AS DateTime), N'100000', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16934, N'Move Unassigned', CAST(N'2019-01-23T13:43:44.880' AS DateTime), N'100000', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16935, N'New Ongoing', CAST(N'2019-01-23T13:43:51.120' AS DateTime), N'100000', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16936, N'Move Unassigned', CAST(N'2019-01-23T13:44:56.160' AS DateTime), N'100000', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16937, N'New Ongoing', CAST(N'2019-01-23T13:45:02.243' AS DateTime), N'100000', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16938, N'Move Unassigned', CAST(N'2019-01-23T13:46:34.373' AS DateTime), N'100000', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16939, N'New Ongoing', CAST(N'2019-01-23T13:46:39.370' AS DateTime), N'100000', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 16940, N'Login', CAST(N'2019-01-23T13:48:32.843' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16941, N'Login', CAST(N'2019-01-23T13:49:10.477' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jpublic', 16942, N'Login', CAST(N'2019-01-23T14:04:10.703' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jpublic', 16943, N'Login', CAST(N'2019-01-23T14:12:24.637' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 16944, N'Login', CAST(N'2019-01-23T14:29:40.987' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jpublic', 16945, N'Login', CAST(N'2019-01-23T14:38:50.990' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16946, N'Login', CAST(N'2019-01-23T14:40:38.437' AS DateTime), N'incorrect credentials. login failed', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16947, N'Login', CAST(N'2019-01-23T14:40:42.480' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16948, N'Login', CAST(N'2019-01-23T14:41:32.517' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 16949, N'Login', CAST(N'2019-01-23T15:57:51.863' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 16950, N'Login', CAST(N'2019-01-23T15:58:14.500' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 16951, N'New Application', CAST(N'2019-01-23T15:59:54.193' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 16952, N'Update', CAST(N'2019-01-23T16:00:49.697' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 16953, N'Login', CAST(N'2019-01-23T16:40:56.960' AS DateTime), N'incorrect credentials. login failed', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 16954, N'Login', CAST(N'2019-01-23T16:41:02.847' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 16955, N'Login', CAST(N'2019-01-23T16:41:24.843' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16956, N'Login', CAST(N'2019-01-23T17:33:00.910' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16957, N'Move Unassigned', CAST(N'2019-01-23T17:33:20.340' AS DateTime), N'100000', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16958, N'Login', CAST(N'2019-01-23T17:35:00.890' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16959, N'Login', CAST(N'2019-01-23T17:41:03.117' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16960, N'New Ongoing', CAST(N'2019-01-23T17:41:13.917' AS DateTime), N'100000', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16961, N'Move Unassigned', CAST(N'2019-01-23T17:44:50.230' AS DateTime), N'100000', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16962, N'New Ongoing', CAST(N'2019-01-23T17:45:02.063' AS DateTime), N'100000', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16963, N'Move Unassigned', CAST(N'2019-01-23T17:46:24.280' AS DateTime), N'100000', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16964, N'New Ongoing', CAST(N'2019-01-23T17:46:34.953' AS DateTime), N'100000', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16965, N'Move Unassigned', CAST(N'2019-01-23T17:48:53.323' AS DateTime), N'100000', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16966, N'New Ongoing', CAST(N'2019-01-23T17:49:05.943' AS DateTime), N'100000', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 16967, N'Login', CAST(N'2019-01-24T09:24:36.797' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16968, N'Login', CAST(N'2019-01-24T09:57:31.497' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16969, N'Move Unassigned', CAST(N'2019-01-24T09:57:59.130' AS DateTime), N'100000', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16970, N'Login', CAST(N'2019-01-24T09:59:17.727' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16971, N'Set Email', CAST(N'2019-01-24T10:02:43.290' AS DateTime), N'jbiggs@sma.gov.jm', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16972, N'Login', CAST(N'2019-01-24T10:06:45.687' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16973, N'Login', CAST(N'2019-01-24T10:06:59.867' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'TaskAdmin', 16974, N'New Ongoing', CAST(N'2019-01-24T10:07:09.077' AS DateTime), N'100000', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jpublic', 16975, N'Login', CAST(N'2019-01-24T10:38:00.120' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16976, N'Login', CAST(N'2019-01-24T10:50:38.750' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 16977, N'Create Account', CAST(N'2019-01-24T10:52:06.460' AS DateTime), N'', N'', 0)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16978, N'Login', CAST(N'2019-01-24T10:54:24.077' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16979, N'Change Password', CAST(N'2019-01-24T10:54:46.297' AS DateTime), N'', N'', 0)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 16980, N'Login', CAST(N'2019-01-24T10:55:18.720' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jpublic', 16981, N'Login', CAST(N'2019-01-24T11:01:05.670' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jpublic', 16982, N'Login', CAST(N'2019-01-24T11:03:52.793' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 16983, N'Create Account', CAST(N'2019-01-24T12:11:47.730' AS DateTime), N'', N'', 0)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 16984, N'Login', CAST(N'2019-01-24T12:12:01.627' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 16985, N'New Application', CAST(N'2019-01-24T12:17:27.873' AS DateTime), N'100002', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 16986, N'Login', CAST(N'2019-01-24T12:17:48.170' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 16987, N'Submission', CAST(N'2019-01-24T12:30:01.793' AS DateTime), N'100002', N'SUBMITTED', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 16988, N'Login', CAST(N'2019-01-24T12:38:58.693' AS DateTime), N'incorrect credentials. login failed', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 16989, N'Login', CAST(N'2019-01-24T12:39:09.583' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 16990, N'Login', CAST(N'2019-01-24T12:41:28.357' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 16991, N'Login', CAST(N'2019-01-24T12:42:40.220' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 16992, N'New Ongoing', CAST(N'2019-01-24T12:43:49.817' AS DateTime), N'100002', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 16993, N'Login', CAST(N'2019-01-24T12:44:43.043' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 16994, N'Login', CAST(N'2019-01-24T12:45:11.607' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 16995, N'Login', CAST(N'2019-01-24T12:46:00.000' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 16996, N'Submission', CAST(N'2019-01-24T12:49:35.423' AS DateTime), N'100002', N'SUBMITTED', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 16997, N'Login', CAST(N'2019-01-24T12:50:05.143' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 16998, N'New Ongoing', CAST(N'2019-01-24T12:50:26.973' AS DateTime), N'100002', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 16999, N'Login', CAST(N'2019-01-24T12:52:27.157' AS DateTime), N'incorrect credentials. login failed', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 17000, N'Login', CAST(N'2019-01-24T12:52:33.867' AS DateTime), N'incorrect credentials. login failed', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 17001, N'Login', CAST(N'2019-01-24T12:52:44.357' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 17002, N'Login', CAST(N'2019-01-24T13:10:10.193' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 17003, N'Login', CAST(N'2019-01-24T13:17:02.033' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 17004, N'Login', CAST(N'2019-01-24T13:29:10.983' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17005, N'Create Account', CAST(N'2019-01-24T16:19:06.927' AS DateTime), N'', N'', 0)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17006, N'Login', CAST(N'2019-01-24T16:20:31.287' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17007, N'New Application', CAST(N'2019-01-24T16:31:06.663' AS DateTime), N'100003', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17008, N'Login', CAST(N'2019-01-24T16:31:32.023' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17009, N'Update', CAST(N'2019-01-24T16:34:37.157' AS DateTime), N'100003', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17010, N'Login', CAST(N'2019-01-24T16:35:04.530' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17011, N'Submission', CAST(N'2019-01-24T16:40:55.160' AS DateTime), N'100003', N'SUBMITTED', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 17012, N'Login', CAST(N'2019-01-24T16:59:48.517' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 17013, N'New Ongoing', CAST(N'2019-01-24T17:01:02.280' AS DateTime), N'100003', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17014, N'Login', CAST(N'2019-01-24T17:01:27.803' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 17015, N'Login', CAST(N'2019-01-24T17:02:08.707' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17016, N'Login', CAST(N'2019-01-24T17:05:01.230' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17017, N'Update', CAST(N'2019-01-24T17:05:53.303' AS DateTime), N'100003', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17018, N'Submission', CAST(N'2019-01-24T17:12:06.447' AS DateTime), N'100003', N'SUBMITTED', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 17019, N'Login', CAST(N'2019-01-24T17:12:32.730' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 17020, N'New Ongoing', CAST(N'2019-01-24T17:16:53.230' AS DateTime), N'100003', N'cgriffith', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 17021, N'Login', CAST(N'2019-01-24T17:17:05.410' AS DateTime), N'incorrect credentials. login failed', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 17022, N'Login', CAST(N'2019-01-24T17:17:13.103' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jdoe', 17023, N'Login', CAST(N'2019-01-24T17:19:31.007' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'cgriffith', 17024, N'Login', CAST(N'2019-01-24T17:21:25.590' AS DateTime), N'login successful', N'', 1)
GO
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 17025, N'Login', CAST(N'2019-01-24T17:22:27.170' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 17026, N'Login', CAST(N'2019-01-24T17:23:10.977' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17027, N'Login', CAST(N'2019-01-24T20:18:30.937' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17028, N'New Application', CAST(N'2019-01-24T20:21:00.523' AS DateTime), N'100004', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17029, N'Update', CAST(N'2019-01-24T20:25:47.910' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17030, N'Update', CAST(N'2019-01-24T20:27:02.430' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17031, N'Login', CAST(N'2019-01-24T21:00:06.890' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17032, N'Update', CAST(N'2019-01-24T21:13:50.733' AS DateTime), N'100004', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17033, N'Update', CAST(N'2019-01-24T21:13:56.047' AS DateTime), N'100004', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17034, N'Login', CAST(N'2019-01-24T21:15:30.830' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17035, N'Update', CAST(N'2019-01-24T21:15:44.717' AS DateTime), N'100004', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17036, N'Login', CAST(N'2019-01-24T21:16:03.180' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17037, N'Login', CAST(N'2019-01-24T21:42:40.947' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 17038, N'Login', CAST(N'2019-01-24T21:47:44.110' AS DateTime), N'incorrect credentials. login failed', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17039, N'Login', CAST(N'2019-01-24T21:48:11.920' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17040, N'Login', CAST(N'2019-01-24T21:55:35.557' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17041, N'Login', CAST(N'2019-01-24T21:59:59.470' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17042, N'Login', CAST(N'2019-01-24T22:06:48.550' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17043, N'Login', CAST(N'2019-01-24T22:37:20.493' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17044, N'Login', CAST(N'2019-01-24T22:41:18.113' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17045, N'Login', CAST(N'2019-01-24T22:44:21.490' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17046, N'Login', CAST(N'2019-01-24T23:03:47.897' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17047, N'New Application', CAST(N'2019-01-24T23:04:54.040' AS DateTime), N'100005', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17048, N'Login', CAST(N'2019-01-24T23:14:23.747' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17049, N'Update', CAST(N'2019-01-24T23:15:44.613' AS DateTime), N'100005', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17050, N'Update', CAST(N'2019-01-24T23:18:24.890' AS DateTime), N'100005', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17051, N'Update', CAST(N'2019-01-24T23:48:37.230' AS DateTime), N'100005', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'admin', 17052, N'Login', CAST(N'2019-01-25T00:38:48.903' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 17053, N'Login', CAST(N'2019-01-25T10:44:46.257' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17054, N'Login', CAST(N'2019-01-25T10:45:33.660' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17055, N'Submission', CAST(N'2019-01-25T10:47:31.723' AS DateTime), N'100003', N'SUBMITTED', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 17056, N'Login', CAST(N'2019-01-25T10:48:17.557' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jstreet', 17057, N'Create Account', CAST(N'2019-01-25T11:13:31.790' AS DateTime), N'', N'', 0)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jstreet', 17058, N'Login', CAST(N'2019-01-25T11:15:03.740' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jstreet', 17059, N'New Application', CAST(N'2019-01-25T11:32:03.377' AS DateTime), N'100006', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jstreet', 17060, N'Login', CAST(N'2019-01-25T11:32:20.677' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'jstreet', 17061, N'Update', CAST(N'2019-01-25T11:52:15.440' AS DateTime), N'100006', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptowers', 17062, N'Login', CAST(N'2019-01-25T11:52:35.757' AS DateTime), N'invalid user', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptowers', 17063, N'Login', CAST(N'2019-01-25T11:52:41.593' AS DateTime), N'invalid user', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 17064, N'Login', CAST(N'2019-01-25T11:52:46.643' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17065, N'Login', CAST(N'2019-01-25T11:57:50.220' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17066, N'Login', CAST(N'2019-01-25T11:58:29.610' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'ptrowers', 17067, N'Login', CAST(N'2019-01-25T12:00:25.760' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'kchin', 17068, N'Login', CAST(N'2019-01-25T12:01:03.343' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17069, N'Login', CAST(N'2019-01-25T16:16:19.150' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17070, N'Login', CAST(N'2019-01-26T20:42:55.020' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17071, N'Update', CAST(N'2019-01-26T20:50:34.670' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17072, N'Update', CAST(N'2019-01-26T20:56:44.580' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17073, N'Update', CAST(N'2019-01-26T21:17:52.523' AS DateTime), N'100004', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 17074, N'Login', CAST(N'2019-01-26T21:57:55.190' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18070, N'Login', CAST(N'2019-01-27T02:02:09.163' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18071, N'Login', CAST(N'2019-01-27T02:11:19.800' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18072, N'Login', CAST(N'2019-01-27T11:48:24.667' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18073, N'Login', CAST(N'2019-01-27T11:50:44.823' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18074, N'Update', CAST(N'2019-01-27T11:59:31.760' AS DateTime), N'100005', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18075, N'New Application', CAST(N'2019-01-27T12:04:50.410' AS DateTime), N'100007', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18076, N'Update', CAST(N'2019-01-27T12:07:33.177' AS DateTime), N'100005', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18077, N'Update', CAST(N'2019-01-27T12:09:55.463' AS DateTime), N'100007', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18078, N'Update', CAST(N'2019-01-27T12:09:56.623' AS DateTime), N'100007', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18079, N'Update', CAST(N'2019-01-27T12:09:57.750' AS DateTime), N'100007', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18080, N'Update', CAST(N'2019-01-27T12:11:25.347' AS DateTime), N'100007', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18081, N'Update', CAST(N'2019-01-27T12:11:48.533' AS DateTime), N'100007', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18082, N'Update', CAST(N'2019-01-27T12:11:51.727' AS DateTime), N'100007', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18083, N'Submission', CAST(N'2019-01-27T12:13:15.850' AS DateTime), N'100007', N'SUBMITTED', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18084, N'Submission', CAST(N'2019-01-27T12:15:09.083' AS DateTime), N'100004', N'SUBMITTED', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18085, N'Submission', CAST(N'2019-01-27T12:16:01.093' AS DateTime), N'100004', N'SUBMITTED', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18086, N'Login', CAST(N'2019-01-27T12:23:06.690' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18087, N'Login', CAST(N'2019-01-27T12:26:28.053' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18088, N'Update', CAST(N'2019-01-27T12:43:04.960' AS DateTime), N'100005', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18089, N'Login', CAST(N'2019-01-27T13:39:32.500' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18090, N'Update', CAST(N'2019-01-27T13:40:39.217' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 18091, N'Update', CAST(N'2019-01-27T13:41:18.037' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 19070, N'Login', CAST(N'2019-01-27T15:15:13.280' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 20070, N'Login', CAST(N'2019-01-27T17:21:04.060' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 20071, N'Login', CAST(N'2019-01-27T17:34:24.060' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 20072, N'Update', CAST(N'2019-01-27T19:47:00.390' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21070, N'Login', CAST(N'2019-01-27T22:26:40.530' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21071, N'Login', CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'login successful', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21072, N'Update', CAST(N'2019-01-27T22:28:15.590' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21073, N'Update', CAST(N'2019-01-27T22:42:57.837' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21074, N'Update', CAST(N'2019-01-27T22:43:00.273' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21075, N'Update', CAST(N'2019-01-27T22:43:00.610' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21076, N'Update', CAST(N'2019-01-27T22:43:00.817' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21077, N'Update', CAST(N'2019-01-27T22:43:01.003' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21078, N'Update', CAST(N'2019-01-27T22:43:01.503' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21079, N'Update', CAST(N'2019-01-27T22:43:01.537' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21080, N'Update', CAST(N'2019-01-27T22:43:01.570' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21081, N'Update', CAST(N'2019-01-27T22:43:01.610' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21082, N'Update', CAST(N'2019-01-27T22:43:01.633' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21083, N'Update', CAST(N'2019-01-27T22:43:01.790' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21084, N'Update', CAST(N'2019-01-27T22:43:01.883' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21085, N'Update', CAST(N'2019-01-27T22:43:01.920' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21086, N'Update', CAST(N'2019-01-27T22:43:01.957' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21087, N'Update', CAST(N'2019-01-27T22:43:01.987' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21088, N'Update', CAST(N'2019-01-27T22:43:02.050' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21089, N'Update', CAST(N'2019-01-27T22:43:02.090' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21090, N'Update', CAST(N'2019-01-27T22:43:02.133' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21091, N'Update', CAST(N'2019-01-27T22:43:02.173' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21092, N'Update', CAST(N'2019-01-27T22:43:02.210' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21093, N'Update', CAST(N'2019-01-27T22:43:02.290' AS DateTime), N'100001', N'', 1)
GO
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21094, N'Update', CAST(N'2019-01-27T22:43:02.330' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21095, N'Update', CAST(N'2019-01-27T22:43:02.383' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21096, N'Update', CAST(N'2019-01-27T22:43:02.420' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21097, N'Update', CAST(N'2019-01-27T22:43:02.450' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21098, N'Update', CAST(N'2019-01-27T22:43:02.480' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21099, N'Update', CAST(N'2019-01-27T22:43:02.530' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21100, N'Update', CAST(N'2019-01-27T22:43:02.573' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21101, N'Update', CAST(N'2019-01-27T22:43:02.623' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21102, N'Update', CAST(N'2019-01-27T22:43:02.660' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21103, N'Update', CAST(N'2019-01-27T22:43:02.687' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21104, N'Update', CAST(N'2019-01-27T22:43:02.713' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21105, N'Update', CAST(N'2019-01-27T22:43:02.767' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21106, N'Update', CAST(N'2019-01-27T22:43:02.817' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21107, N'Update', CAST(N'2019-01-27T22:43:02.860' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21108, N'Update', CAST(N'2019-01-27T22:43:02.910' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21109, N'Update', CAST(N'2019-01-27T22:43:03.080' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21110, N'Update', CAST(N'2019-01-27T22:43:05.507' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21111, N'Update', CAST(N'2019-01-27T22:43:06.003' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21112, N'Update', CAST(N'2019-01-27T22:43:06.070' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21113, N'Update', CAST(N'2019-01-27T22:43:06.093' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21114, N'Update', CAST(N'2019-01-27T22:43:06.140' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21115, N'Update', CAST(N'2019-01-27T22:43:06.200' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21116, N'Update', CAST(N'2019-01-27T22:43:06.257' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21117, N'Update', CAST(N'2019-01-27T22:43:06.297' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21118, N'Update', CAST(N'2019-01-27T22:43:06.320' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21119, N'Update', CAST(N'2019-01-27T22:43:06.343' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21120, N'Update', CAST(N'2019-01-27T22:43:06.370' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21121, N'Update', CAST(N'2019-01-27T22:43:06.423' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21122, N'Update', CAST(N'2019-01-27T22:43:06.500' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21123, N'Update', CAST(N'2019-01-27T22:43:06.547' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21124, N'Update', CAST(N'2019-01-27T22:43:06.573' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21125, N'Update', CAST(N'2019-01-27T22:43:06.600' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21126, N'Update', CAST(N'2019-01-27T22:43:06.657' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21127, N'Update', CAST(N'2019-01-27T22:43:06.707' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21128, N'Update', CAST(N'2019-01-27T22:43:06.773' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21129, N'Update', CAST(N'2019-01-27T22:43:06.810' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21130, N'Update', CAST(N'2019-01-27T22:43:06.840' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21131, N'Update', CAST(N'2019-01-27T22:43:06.867' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21132, N'Update', CAST(N'2019-01-27T22:43:06.893' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21133, N'Update', CAST(N'2019-01-27T22:43:06.920' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21134, N'Update', CAST(N'2019-01-27T22:43:06.953' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21135, N'Update', CAST(N'2019-01-27T22:43:07.003' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21136, N'Update', CAST(N'2019-01-27T22:43:07.037' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21137, N'Update', CAST(N'2019-01-27T22:43:07.060' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21138, N'Update', CAST(N'2019-01-27T22:43:07.083' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21139, N'Update', CAST(N'2019-01-27T22:43:07.117' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21140, N'Update', CAST(N'2019-01-27T22:43:07.147' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21141, N'Update', CAST(N'2019-01-27T22:43:07.167' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21142, N'Update', CAST(N'2019-01-27T22:43:07.203' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21143, N'Update', CAST(N'2019-01-27T22:43:07.257' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21144, N'Update', CAST(N'2019-01-27T22:43:07.277' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21145, N'Update', CAST(N'2019-01-27T22:43:07.297' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21146, N'Update', CAST(N'2019-01-27T22:43:12.370' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21147, N'Update', CAST(N'2019-01-27T22:43:12.557' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21148, N'Update', CAST(N'2019-01-27T22:43:12.703' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21149, N'Update', CAST(N'2019-01-27T22:43:13.860' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21150, N'Update', CAST(N'2019-01-27T22:43:14.060' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21151, N'Update', CAST(N'2019-01-27T22:43:14.230' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21152, N'Update', CAST(N'2019-01-27T22:43:23.587' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21153, N'Update', CAST(N'2019-01-27T22:43:24.083' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21154, N'Update', CAST(N'2019-01-27T22:43:24.120' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21155, N'Update', CAST(N'2019-01-27T22:43:24.157' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21156, N'Update', CAST(N'2019-01-27T22:43:24.223' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21157, N'Update', CAST(N'2019-01-27T22:43:24.253' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21158, N'Update', CAST(N'2019-01-27T22:43:24.287' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21159, N'Update', CAST(N'2019-01-27T22:43:24.310' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21160, N'Update', CAST(N'2019-01-27T22:43:24.330' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21161, N'Update', CAST(N'2019-01-27T22:43:24.377' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21162, N'Update', CAST(N'2019-01-27T22:43:24.420' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21163, N'Update', CAST(N'2019-01-27T22:43:24.450' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21164, N'Update', CAST(N'2019-01-27T22:43:24.470' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21165, N'Update', CAST(N'2019-01-27T22:43:24.493' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21166, N'Update', CAST(N'2019-01-27T22:43:24.517' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21167, N'Update', CAST(N'2019-01-27T22:43:24.550' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21168, N'Update', CAST(N'2019-01-27T22:43:24.583' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21169, N'Update', CAST(N'2019-01-27T22:43:24.620' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21170, N'Update', CAST(N'2019-01-27T22:43:24.650' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21171, N'Update', CAST(N'2019-01-27T22:43:24.677' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21172, N'Update', CAST(N'2019-01-27T22:43:24.713' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21173, N'Update', CAST(N'2019-01-27T22:43:24.743' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21174, N'Update', CAST(N'2019-01-27T22:43:24.780' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21175, N'Update', CAST(N'2019-01-27T22:43:24.810' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21176, N'Update', CAST(N'2019-01-27T22:43:24.843' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21177, N'Update', CAST(N'2019-01-27T22:43:24.913' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21178, N'Update', CAST(N'2019-01-27T22:43:24.940' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21179, N'Update', CAST(N'2019-01-27T22:43:24.963' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21180, N'Update', CAST(N'2019-01-27T22:43:24.990' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21181, N'Update', CAST(N'2019-01-27T22:43:25.020' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21182, N'Update', CAST(N'2019-01-27T22:43:25.047' AS DateTime), N'100001', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21183, N'Submission', CAST(N'2019-01-27T23:56:00.860' AS DateTime), N'100001', N'SUBMITTED', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21184, N'New Application', CAST(N'2019-01-28T00:01:18.987' AS DateTime), N'100008', N'', 1)
INSERT [dbo].[slw_user_activity] ([username], [sequence], [type], [created_date], [description], [extras], [priority]) VALUES (N'client', 21185, N'Submission', CAST(N'2019-01-28T00:48:40.357' AS DateTime), N'100005', N'SUBMITTED', 1)
SET IDENTITY_INSERT [dbo].[slw_user_activity] OFF
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (1, N'UUkKdNCE1kgsaKJlmvI3Qrd7+OR9Ia14Y2xpZW50', CAST(N'2019-01-27T22:26:55.440' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (2, N'+x6AYYeC1kiRcCTpr5uXTpRMCXMdOIlzYWRtaW4=', CAST(N'2019-01-25T00:38:48.750' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (4, N'CvyP5U2B1kjcYPTVACTaQ53YhDtecLhpRW5naW5lZXJUZXN0', CAST(N'2019-01-23T11:14:48.340' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (5, N'9VXslg2C1kjEoj1sS1lARqbhrF5NLhaVVGFza0FkbWlu', CAST(N'2019-01-24T10:06:59.777' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (7, N'7Z48iRWC1kh+1kGUd0YaTr0gJckwlnBianB1YmxpYw==', CAST(N'2019-01-24T11:03:52.787' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (3, N'eNhWR0qC1kherwlqCctlR57Pno9vPnUYY2dyaWZmaXRo', CAST(N'2019-01-24T17:21:25.557' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (8, N'K138meaC1kgnzQCgBUNvRoCG5BDBQVuJcHRyb3dlcnM=', CAST(N'2019-01-25T12:00:25.703' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (9, N'wzUMA0qC1kj3+G8F/xW1S7cvO73CFyKvamRvZQ==', CAST(N'2019-01-24T17:19:30.983' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (11, N'01ibreKC1khqf70BRq0KSY3xVnhhqCHmanN0cmVldA==', CAST(N'2019-01-25T11:32:20.637' AS DateTime), 45)
INSERT [dbo].[slw_user_keys] ([user_id], [access_key], [last_detected_activity], [max_inactivity_mins]) VALUES (10, N's5tksOaC1kj/SaxC839pRrTcsqF65pHMa2NoaW4=', CAST(N'2019-01-25T12:01:03.297' AS DateTime), 45)
INSERT [dbo].[slw_user_types] ([user_type], [description]) VALUES (0, N'Client')
INSERT [dbo].[slw_user_types] ([user_type], [description]) VALUES (1, N'Engineer')
INSERT [dbo].[slw_user_types] ([user_type], [description]) VALUES (9, N'Administrator')
INSERT [dbo].[slw_user_types] ([user_type], [description]) VALUES (10, N'System Administrator')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'admin', N'Mark', N'Scott', CAST(N'2019-01-23T10:42:21.057' AS DateTime), 10, CAST(N'2019-01-23T10:42:21.057' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'Xjk1c3TKpcVPJ+I9cJ6tQJT1C7mUrs4vcVyKiNX8uQ0elN7r4hzNyA==', 0, 2, N'a.markscott13@gmail.com')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'cgriffith', N'Carlos', N'Griffith', CAST(N'2019-01-23T10:48:50.673' AS DateTime), 1, CAST(N'2019-01-23T10:48:50.673' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'svFcLLFKpRlvhmbtpRL9uJVDmd8Qx6zbMRE1RwVofS4XKwmOltN7oA==', 1, 3, N'cgriffith@sma.gov.jm')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'client', N'Mark', N'Scott', CAST(N'2019-01-23T10:21:18.460' AS DateTime), 0, CAST(N'2019-01-23T10:21:18.460' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'GlhtmVWmXRd3AihUZ0tOKfVFpzCPG66Nz9F9dGB7hIZSUPN0B2UKIg==', 0, 1, N'a.markscott13@gmail.com')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'EngineerTest', N'Engineer', N'Test', CAST(N'2019-01-23T11:00:31.593' AS DateTime), 1, CAST(N'2019-01-23T11:00:31.593' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'JGKRx18KveJkG84Y0i7Z6yHQv9EUc1vm3+A5LJI73YO3PeWkdIcJAg==', 1, 4, N'a.markscott123@gmail.com')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'jdoe', N'John', N'Doe', CAST(N'2019-01-24T12:11:47.680' AS DateTime), 0, CAST(N'2019-01-24T12:11:47.680' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'MGmqPQsBr5K3BJIhMwKCJGJn6pdmJ5bAIA/VEdu07hJxB9yERaQKiQ==', 0, 9, N'john.doe@Wapproval.com')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'jpublic', N'John', N'Public', CAST(N'2019-01-23T13:21:32.837' AS DateTime), 0, CAST(N'2019-01-23T13:21:32.837' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'DYKuIe6TsNxrwp+1IIxo6X3SqL5+xD9knMc+TBajpMnJTMh3/nLwUw==', 0, 7, N'cgriffith@sma.gov.jm')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'jstreet', N'John', N'Street', CAST(N'2019-01-25T11:13:31.610' AS DateTime), 0, CAST(N'2019-01-25T11:13:31.610' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'5p9VuGiPAcbXVCSTWkHwxsDtVphkiorxOxAGGnQP9t0Eqod31qWT7w==', 0, 11, N'cgriffith@sma.gov.jm')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'kchin', N'Kia', N'Chin', CAST(N'2019-01-24T16:19:06.880' AS DateTime), 0, CAST(N'2019-01-24T16:19:06.880' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'hse3vKrVZqvG0SgGrp/jrG5qWI8Kv4tF49YBJOrVltPK4gdmSzk14A==', 0, 10, N'cgriffith@sma.gov.jm')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'ptrowers', N'Philmore', N'Trowers', CAST(N'2019-01-24T10:52:06.450' AS DateTime), 9, CAST(N'2019-01-24T10:52:06.450' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'jrcs9aFuMHz+m2Iew0WyD4yxmUIxRE5IKMZZ3WAENzwuSqXogwk7uw==', 0, 8, N'ptrowers@sma.gov.jm')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'TaskAdmin', N'Task', N'Admin', CAST(N'2019-01-23T11:09:11.190' AS DateTime), 10, CAST(N'2019-01-23T11:09:11.190' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'MXDvrjCN/2XLFh7QsSi09j4BDx7iis9/G/uSx3HN5NTHIcEmpQWg+g==', 1, 5, N'a.markscott13@gmail.com')
INSERT [dbo].[slw_users] ([username], [first_name], [last_name], [created_date], [user_type], [last_password_change_date], [last_login_date], [hash], [password_reset_required], [user_id], [email]) VALUES (N'TaskAdmin2', N'TaskAdmin', N'2', CAST(N'2019-01-23T11:10:57.350' AS DateTime), 0, CAST(N'2019-01-23T11:10:57.350' AS DateTime), CAST(N'2019-01-27T22:26:55.440' AS DateTime), N'aRAzJhodxASEs3qjmDfojb2EdfmOAL4gm3e6b1ZsrepEpsX9HOlPlw==', 0, 6, N'a.markscott13@gmail.com')
/****** Object:  Index [UQ__slw_user__B9BE370E30F848ED]    Script Date: 1/28/2019 12:59:04 AM ******/
ALTER TABLE [dbo].[slw_users] ADD UNIQUE NONCLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[slw_forms] ADD  DEFAULT (getdate()) FOR [last_updated]
GO
ALTER TABLE [dbo].[slw_assigned_company]  WITH CHECK ADD FOREIGN KEY([username])
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
ALTER TABLE [dbo].[slw_ongoing_tasks]  WITH CHECK ADD FOREIGN KEY([application_id])
REFERENCES [dbo].[slw_forms] ([application_id])
GO
ALTER TABLE [dbo].[slw_ongoing_tasks]  WITH CHECK ADD FOREIGN KEY([assigned_to])
REFERENCES [dbo].[slw_users] ([username])
GO
ALTER TABLE [dbo].[slw_unassigned_tasks]  WITH CHECK ADD FOREIGN KEY([application_id])
REFERENCES [dbo].[slw_forms] ([application_id])
GO
ALTER TABLE [dbo].[slw_unassigned_tasks]  WITH CHECK ADD FOREIGN KEY([submitted_by])
REFERENCES [dbo].[slw_users] ([username])
GO
ALTER TABLE [dbo].[slw_user_activity]  WITH CHECK ADD FOREIGN KEY([type])
REFERENCES [dbo].[slw_activity_types] ([type])
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
/****** Object:  StoredProcedure [dbo].[sp_addFile]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_addFile]
(
	@file_id varchar(300),
	@filename varchar(500),
	@created_date datetime,
	@path varchar(500),
	@application_id varchar(50),
	@username varchar(50),
	@purpose varchar(100)
)
as
begin
	insert into slw_application_files (file_id,filename, created_date, path, application_id, username, purpose) values(@file_id, @filename, @created_date, @path, @application_id,  @username, @purpose)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_checkLocalClientExist]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_checkLocalClientExist] 
(
	@client_id int
)
as
begin
	select count(*) as count  from slw_client_companies  where client_id=@client_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_checkTaskExist]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_checkTaskExist]
(
	@applicationId varchar(100)
)
as
begin
	declare @count integer

	set @count = (select count(*) from slw_unassigned_tasks where application_id=@applicationId)

	if(@count>0)
	begin
		select @count as exist
	end
	else
	begin
		set @count = (select count(*) as count from slw_ongoing_tasks where application_id=@applicationId)
		select @count as exist
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_checkUserExist]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_clearAllForms]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_clearAllForms]
as
begin
delete from slw_ongoing_tasks
delete from slw_unassigned_tasks
delete from slw_application_files
delete from slw_form_freq
delete from slw_form_step2
delete from slw_form_step1
delete from slw_forms
end
GO
/****** Object:  StoredProcedure [dbo].[sp_clearUserActivities]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_clearUserActivities]
(
	@username varchar(50)
)
as
begin
	if(lower(@username) ='all')
	begin
		delete from slw_user_activity
	end
	else
	begin
		delete from slw_user_avtivity where username=@username
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_createUser]    Script Date: 1/28/2019 12:59:04 AM ******/
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
	@clientId integer,
	@source varchar(100)
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
		set @user_id_int = (select count (*) from slw_users) + 1
		
		
		insert into SLW_USERS values(@username, @first_name, @last_name, @created_date, @user_type, @last_password_change_date, @last_login_date, @hash, @password_reset_required, @user_id_int, @email)
		declare @adminType integer
		
		if(@user_type != 9)
		begin
			insert into [slw_assigned_company] values (@username, @company, @clientId, @source)
		end
		select @clientId as client_id
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteAllFreqs]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_deleteAllFreqs]
(
	@application_id varchar(50)
)
as
begin
	delete from slw_form_freq where application_id=@application_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteFreq]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_deleteOngoingTask]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_deleteOngoingTask]
(
	@application_id varchar(100)
)
as
begin
	delete from slw_ongoing_tasks where application_id=@application_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteUnassignedTask]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_deleteUnassignedTask]
(
	@application_id varchar(100)
)
as
begin
	delete from slw_unassigned_tasks where application_id=@application_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_deleteUser]    Script Date: 1/28/2019 12:59:04 AM ******/
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
	
	
	
	delete from slw_form_step2
	delete from slw_form_step1
	delete from slw_form_freq
	delete from slw_user_activity
	delete from slw_forms

	delete from SLW_USER_KEYS where user_id=@user_id
	delete from SLW_CLIENT_COMPANY where username=@username
	delete from SLW_USERS where username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getAllSavedApplications]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getAllSavedApplications]
(
	@username varchar(50)
)
as
begin
	select application_id, created_date, last_updated from slw_forms where username=@username and status='incomplete'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getAllUserActivities]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getAllUserActivities]
as
begin
	select * from slw_user_activity order by created_date desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getAllUsernames]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_getAllUsernames]
as
begin
	select username from slw_users
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getApplicationCounters]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getApplicationCounters]
(
	@username varchar(50)
)
as
begin
	declare @licensed_app integer
	declare @pending_approval integer 
	declare @incomplete integer

	set @licensed_app = (select count(*) from slw_forms where status='LICENSED' and username=@username)
	set @pending_approval = (select count (*) from slw_forms where status = 'PENDING' and username=@username or status='INVOICED' and username=@username)
	set @incomplete = (select count(*) from slw_forms where status = 'INCOMPLETE' and username=@username)

	select @licensed_app as licensed_apps, @pending_approval as pending_apps, @incomplete as incomplete_apps
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getApplicationFiles]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getApplicationFiles]
(
	@application_id varchar(100)
)
as
begin
	select * from slw_application_files WHERE application_id=@application_id
end   
GO
/****** Object:  StoredProcedure [dbo].[sp_getApplicationIdsForUser]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getApplicationIdsForUser]
(
	@username varchar(50)
)
as
begin
	select application_id from slw_forms where username=@username and status != 'INCOMPLETE' and status != 'RESUBMIT' and status != 'REJECTED'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getASMSApplication]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getASMSApplication]
(
	@manufacturer varchar(300),
	@model varchar(300)
)
as
begin
	select  Status, Model, Dealer as Manufacturer, Description from ASMSGenericMaster.dbo.tbl_TypeApproval where Model=@model and Dealer=@manufacturer 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getAssignedCompany]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getAssignedCompany]
(
	@username varchar(50)
)
as
begin
	select company, clientId, source from slw_assigned_company where username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getAssignedTasks]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getAssignedTasks]
(
	@username varchar(50)
)
as
begin
	select slw_ongoing_tasks.application_id, slw_ongoing_tasks.created_date, submitted_by, submitted_by_username, date_assigned, assigned_by, status from slw_ongoing_tasks  where slw_ongoing_tasks.assigned_to=@username order by created_date desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getCertificateMain]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getCertificateMain]
(
	@application_id varchar(50)
)
as
begin
	select manufacturer_name, manufacturer_address, product_identifiation, equipment_description from slw_form_step1
	inner join slw_form_step2 on slw_form_step1.application_id=slw_form_step2.application_id where slw_form_step1.application_id=@application_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getClientDetail]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_getClientDetails]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getClientDetails]
(
	@clientCompany varchar(300)
)
as
begin
	if(@clientCompany = '')
	begin
	select clientId, clientType, ccNum, clientCountry1, clientStreet1 as address, nationality, clientFaxNum, clientTelNum, clientCompany  from [ASMSGenericMaster].[dbo].[client] where clientType!='Border Coordination' and clientCompany!=''  order by clientCompany asc
	end
	else
	begin
		select clientId, clientType, ccNum, clientCountry1, clientStreet1 as address, nationality, clientFaxNum, clientTelNum, clientCompany  from [ASMSGenericMaster].[dbo].[client] where clientCompany like @clientCompany+'%' and clientType!='Border Coordination' and clientCompany!='' order by clientCompany asc
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getClientUsers]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getClientUsers]
as
begin
	select UPPER(LEFT(first_name,1))+LOWER(SUBSTRING(first_name,2,LEN(first_name))) as first_name, UPPER(LEFT(last_name,1))+LOWER(SUBSTRING(last_name,2,LEN(last_name))) as last_name, username from slw_users where user_type = 0
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getCurrentEmailSettings]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getCurrentEmailSettings]
as
begin
	select distinct * from slw_email_settings
	update slw_email_settings set last_accessed = getdate()
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getCurrentManufacturerModels]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getCurrentManufacturerModels]
(
	@username varchar(50)
)
as
begin
	select  a.manufacturer_name, a.application_id, b.product_identifiation, main.status from slw_form_step1 a
		   inner join
		   slw_form_step2 b on a.application_id = b.application_id  inner join  slw_forms main on a.application_id=main.application_id where main.username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getFilePath]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getFilePath]
(
	@file_id varchar(300)
)
as
begin
	select filename, path from slw_application_files where  file_id=@file_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getFilesByType]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getFilesByType]
(
	@purpose varchar(100),
	@application_id varchar(50)
)
as
begin
	select file_id, filename, created_date, path, purpose, application_id, username from slw_application_files where purpose=@purpose and application_id=@application_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getForm]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getForm]
(
	@application_id varchar(100)
)
as
begin
	select  a.applicant_name, a.applicant_tel, a.applicant_address, a.applicant_fax, a.applicant_city_town, a.applicant_contact_person, a.applicant_nationality,
		   a.manufacturer_name, a.manufacturer_tel, a.manufacturer_address, a.manufacturer_fax, a.manufacturer_contact_person, b.equipment_type, b.equipment_description , b.product_identifiation, b.ref#, b.make, b.software, b.type_of_equipment, b.other, b.antenna_type, b.antenna_gain, b.channel,
		   b.separation, b.additional_info, b.name_of_test, b.country, main.category, dbo.fn_getCurrentAppStatus(b.application_id) as current_status, b.name_of_test, b.country from slw_form_step1 a
		   inner join
		   slw_form_step2 b on a.application_id = b.application_id  inner join  slw_forms main on a.application_id=main.application_id 
		   where b.application_id=@application_id
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getFrequencies]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getFrequencies] 
(
	@application_id varchar(100)
)
as
begin
	select sequence, lower_freq, upper_freq, power, tolerance, emmision_desig, freq_type from slw_form_freq where application_id=@application_id order by sequence asc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getKeyDetail]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_getLicensedApplications]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getLicensedApplications]
(
	@username varchar(50)
)
as
begin
	select main.application_id, step1.manufacturer_name as manufacturer, step2.product_identifiation as model, main.created_date, main.licensed_date, main.username as author, main.category from slw_forms  main inner join
	slw_form_step2 step2 on step2.application_id = main.application_id inner join slw_form_step1 step1 on main.application_id=step1.application_id  where username=@username and main.status = 'LICENSED'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getLocalClientCompany]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getLocalClientCompany]
(
	@client_id varchar(300)
)
as
begin
	if(@client_id = '')
	begin
	select * from slw_client_companies
	end
	else
	begin
	select * from slw_client_companies where client_id=@client_id
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getLocalManufacturers]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getLocalManufacturers] 
(
	@manufacturer_id int
)
as
begin
	if(@manufacturer_id  = '')
	begin
		select * from slw_manufacturers order by dealer desc
	end
	else
	begin
		select * from slw_manufacturers where manufacturer_id=@manufacturer_id order by dealer desc
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getMake]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_getMake]
(
	@query varchar(200)
)
as
begin
	SELECT Manufacturer from [ASMSGenericMaster].[dbo].[tbl_TypeApproval] where Manufacturer like @query+'%'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getManufacturerDetail]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_getManufacturers]    Script Date: 1/28/2019 12:59:04 AM ******/
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

	select * from (select Dealer, Address2, TelNum, FaxNum, '' as contact_person from [ASMSGenericMaster].[dbo].[tbl_TypeApproval] union all select dealer, address, telephone, fax, contact_person from slw_manufacturers) x order by dealer asc
	end
	else
	begin
	select * from (select Dealer, Address2, TelNum, FaxNum, '' as contact_person from [ASMSGenericMaster].[dbo].[tbl_TypeApproval] union all select dealer, address, telephone, fax, contact_person from slw_manufacturers) x where Dealer like @query+'%' group by Dealer, Address2, TelNum, FaxNum, contact_person order by dealer asc
		
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getModels]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_getNewAppId]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getNewAppId]
as
begin
	declare @count integer
	declare @base integer

	set @base = 100000
	set @count = (select count(*) from slw_forms)
	
	select (@base + @count) as id 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getOngoingTasks]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getOngoingTasks]
as
begin
	select application_id, slw_ongoing_tasks.created_date, 
	UPPER(LEFT(first_name,1))+LOWER(SUBSTRING(first_name,2,LEN(first_name))) +' '+ UPPER(LEFT(last_name,1))+LOWER(SUBSTRING(last_name,2,LEN(last_name))) as assigned_to, date_assigned, submitted_by, submitted_by_username, dbo.fn_getCurrentAppStatus(application_id) as status, assigned_by from slw_ongoing_tasks inner join slw_users on slw_users.username = slw_ongoing_tasks.assigned_to order by created_date desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getPendingApprovals]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getPendingApprovals]
(
	@username varchar(50)
)
as
begin
	select main.application_id, step1.manufacturer_name as manufacturer, step2.product_identifiation as model, main.created_date, main.licensed_date, main.username as author, main.category from slw_forms  main inner join
	slw_form_step2 step2 on step2.application_id = main.application_id inner join slw_form_step1 step1 on main.application_id=step1.application_id  where username=@username and main.status = 'PENDING' OR username=@username and main.status = 'INVOICED'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getRecentActivities]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getRecentActivities] 
(
	@username varchar(50)
)
as
begin
	select main.application_id, step1.manufacturer_name as manufacturer, step2.product_identifiation as model, main.created_date, main.licensed_date, main.username as author, main.category, main.status from slw_forms  main inner join
	slw_form_step2 step2 on step2.application_id = main.application_id inner join slw_form_step1 step1 on main.application_id=step1.application_id  where username=@username order by application_id desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getRecentDocs]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getRecentDocs]
(
	@username varchar(50),
	@days_range int
)
as
begin
	select application_id, created_date, status, last_updated, dbo.fn_getCurrentAppStatus(application_id) as current_status from slw_forms where datediff(day, last_updated, getdate())<@days_range and username=@username and status='completed'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getRemarks]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getRemarks]
(
	@query varchar(200)
)
as
begin
	SELECT Remarks from [ASMSGenericMaster].[dbo].[tbl_TypeApproval] where Remarks like '%'+@query+'%'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getSavedApplications]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getSavedApplications]
(
	@username varchar(50),
	@category varchar(100)
)
as
begin
	select application_id, created_date, last_updated from slw_forms where username=@username and status='incomplete' and category=@category
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getSingleOngoingTask]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getSingleOngoingTask]
(
	@application_id varchar(100)
)
as
begin
	select * from slw_ongoing_tasks where application_id=@application_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getSingleUnassignedTask]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getSingleUnassignedTask]
(
	@application_id varchar(100)
)
as
begin
	select application_id, slw_unassigned_tasks.created_date, 
	UPPER(LEFT(first_name,1))+LOWER(SUBSTRING(first_name,2,LEN(first_name))) +' '+ UPPER(LEFT(last_name,1))+LOWER(SUBSTRING(last_name,2,LEN(last_name))) as submitted_by, username
	 from slw_unassigned_tasks inner join slw_users on slw_users.username=slw_unassigned_tasks.submitted_by where slw_unassigned_tasks.application_id=@application_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getSMACertificateDetails]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getSMACertificateDetails]
(
	@approval_id varchar(100)
)
as
begin
	select LowerFrequency, UpperFrequency, PowerOutput, FrequencyTolerance, EmissionClass from [ASMSGenericMaster].[dbo].[tbl_TypeApprovalDetail] where keyTypeApprovalID=@approval_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getSMACertificateMain]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getSMACertificateMain]
(
	@approval_id varchar(100)
)
as
begin
	select Dealer, Address2, Model, Description, Remarks from [ASMSGenericMaster].[dbo].[tbl_TypeApproval]  where keyTypeApprovalID=@approval_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getSMAEngineers]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getSMAEngineers]
as
begin
	SELECT username, UPPER(LEFT(first_name,1))+LOWER(SUBSTRING(first_name,2,LEN(first_name))) +' '+ UPPER(LEFT(last_name,1))+LOWER(SUBSTRING(last_name,2,LEN(last_name))) as name, email FROM slw_users where user_type = 1
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getTypeApprovalDetails]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getTypeApprovalDetails]
(
	@Dealer varchar (300),
	@Model varchar (300),
	@make varchar(300),
	@remarks varchar(300)
)
as
begin 
	 if (@Dealer != '' AND @Model != '')
	 begin 
		SELECT c.clientCompany, TA.keyTypeApprovalID, TA.Dealer, TA.Model, TA.Description, TA.Address2, TA.Remarks, TA.issueDate, TA.keyTypeApprovalID  
		FROM  [ASMSGenericMaster].[dbo].[tbl_TypeApproval] TA, [ASMSGenericMaster].[dbo].[client] c 
		WHERE TA.fk_Client_ClientID = c.clientID
		AND TA.Dealer = @Dealer
		AND TA.Model = @Model
	 end
	 
	 if (@Dealer = '' AND @Model != '')
	 begin 
		SELECT c.clientCompany, TA.keyTypeApprovalID, TA.Dealer, TA.Model, TA.Description, TA.Address2, TA.Remarks, TA.issueDate, TA.keyTypeApprovalID  
		FROM  [ASMSGenericMaster].[dbo].[tbl_TypeApproval] TA, [ASMSGenericMaster].[dbo].[client] c 
		WHERE TA.fk_Client_ClientID = c.clientID
		AND TA.Model = @Model
	 end
	 
	 if (@Dealer != '' AND @Model = '')
	 begin 
		SELECT  c.clientCompany, TA.keyTypeApprovalID, TA.Dealer, TA.Model, TA.Description, TA.Address2, TA.Remarks, TA.issueDate, TA.keyTypeApprovalID  
		FROM  [ASMSGenericMaster].[dbo].[tbl_TypeApproval] TA, [ASMSGenericMaster].[dbo].[client] c 
		WHERE TA.fk_Client_ClientID = c.clientID
		AND TA.Dealer = @Dealer
	 end

	 if(@make !='')
	 begin
		SELECT  c.clientCompany, TA.keyTypeApprovalID, TA.Dealer, TA.Model, TA.Description, TA.Address2, TA.Remarks, TA.issueDate, TA.keyTypeApprovalID  
		FROM  [ASMSGenericMaster].[dbo].[tbl_TypeApproval] TA, [ASMSGenericMaster].[dbo].[client] c 
		WHERE TA.fk_Client_ClientID = c.clientID
		AND TA.Manufacturer = @make
	 end

	 if(@remarks !='')
	 begin
		SELECT  c.clientCompany, TA.keyTypeApprovalID, TA.Dealer, TA.Model, TA.Description, TA.Address2, TA.Remarks, TA.issueDate, TA.keyTypeApprovalID  
		FROM  [ASMSGenericMaster].[dbo].[tbl_TypeApproval] TA, [ASMSGenericMaster].[dbo].[client] c 
		WHERE TA.fk_Client_ClientID = c.clientID
		AND TA.Remarks = @remarks
	 end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getTypeApprovalTableInfo]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_getUnassignedTasks]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getUnassignedTasks]
as
begin
	select application_id, slw_unassigned_tasks.created_date, 
	UPPER(LEFT(first_name,1))+LOWER(SUBSTRING(first_name,2,LEN(first_name))) +' '+ UPPER(LEFT(last_name,1))+LOWER(SUBSTRING(last_name,2,LEN(last_name))) as submitted_by, username  from slw_unassigned_tasks inner join slw_users on slw_users.username = slw_unassigned_tasks.submitted_by order by created_date desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getUnreadNotifications]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_getUserActivity]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getUserActivity]
(
	@username varchar(50),
	@priority int
)
as
begin
	
	if(@username = '*all')
	begin
		select username, sequence, type, created_date, description, extras, priority, dbo.fn_getCurrentAppStatus(description) as current_status from slw_user_activity order by created_date desc
	end
	else if(@username = '*preview')
	begin
	select top 15 username, sequence, type, created_date, description, extras, priority, dbo.fn_getCurrentAppStatus(description) as current_status from slw_user_activity  order by created_date desc
	end
	else
	begin
	select username, sequence, type, created_date, description, extras, priority, dbo.fn_getCurrentAppStatus(description) as current_status from slw_user_activity where  username=@username order by created_date desc
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getUserActivityByRange]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_getUserActivityByRange]
(
	@username varchar(50),
	@priority int,
	@date_start datetime,
	@date_end datetime
)
as
begin
	select * from slw_user_activity where priority = @priority and created_date >= @date_start and created_date <= @date_end order by created_date desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getUserCredentials]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_getUserDetails]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_getUserDetails]
(
	@username varchar(50)
)
as
begin
	if(@username = '*all')
	begin
		select username, first_name, last_name, UPPER(LEFT(first_name,1))+LOWER(SUBSTRING(first_name,2,LEN(first_name))) +' '+ UPPER(LEFT(last_name,1))+LOWER(SUBSTRING(last_name,2,LEN(last_name))) as fullname,
	 description as user_type, email, created_date, ISNULL(last_detected_activity, '1990-01-17 23:02:16.683') as last_detected_activity from slw_users full outer join slw_user_types on slw_users.user_type=slw_user_types.user_type full outer join slw_user_keys on slw_users.user_id = slw_user_keys.user_id
	where username is not null order by created_date desc
	end
	else
	begin
		select username, first_name, last_name, UPPER(LEFT(first_name,1))+LOWER(SUBSTRING(first_name,2,LEN(first_name))) +' '+ UPPER(LEFT(last_name,1))+LOWER(SUBSTRING(last_name,2,LEN(last_name))) as fullname,
	 description as user_type, email, created_date, ISNULL(last_detected_activity, '1990-01-17 23:02:16.683') as last_detected_activity  from slw_users full outer join  slw_user_types on slw_users.user_type=slw_user_types.user_type full outer join slw_user_keys on slw_users.user_id = slw_user_keys.user_id  where username=@username 
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_getUserTypeByKey]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_getUserTypes]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_newCertificate]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_newCertificate]
(
	@username varchar(50),
	@application_id varchar(100)
)
as
begin
	insert into certificates (username, application_id, approval_date) values(@username, @application_id, getdate())
end
GO
/****** Object:  StoredProcedure [dbo].[sp_newClientCompany]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_newClientCompany]
(
	@name varchar(500),
	@telephone varchar(300),
	@address varchar(500),
	@fax varchar(300),
	@cityTown varchar(300),
	@contactPerson varchar(300),
	@nationality varchar(300)
)
as
begin
	declare @id int
	set @id = (select count(*) from slw_client_companies) + 15000
	insert into slw_client_companies (client_id, name, telephone, address, fax, cityTown, contactPerson, nationality) values(@id, @name, @telephone, @address, @fax, @cityTown, @contactPerson, @nationality)
	select @id as client_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_newKey]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_newManufacturer]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_newManufacturer]
(
	@dealer varchar(500),
	@address varchar(500),
	@telephone varchar(300),
	@fax varchar(300),
	@contact_person varchar(500)
)
as
begin
	declare @id int
	set @id = (select count(*) from slw_manufacturers) + 15000
	insert into slw_manufacturers (manufacturer_id, dealer, address, telephone, fax, contact_person) values (@id, @dealer, @address, @telephone, @fax, @contact_person)
	select * from slw_manufacturers where manufacturer_id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_newMessage]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_newOngoingTask]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_newOngoingTask]
(
	@application_id varchar(100),
	@assigned_to varchar(50),
	@submitted_by_username varchar(50),
	@created_date datetime,
	@assigned_by varchar(100),
	@assigned_by_username varchar(100)
)
as
begin
    declare @name as varchar(300)
    set @name = (select UPPER(LEFT(first_name,1))+LOWER(SUBSTRING(first_name,2,LEN(first_name))) +' '+ UPPER(LEFT(last_name,1))+LOWER(SUBSTRING(last_name,2,LEN(last_name))) from slw_users where username=@submitted_by_username)
	insert into slw_ongoing_tasks (application_id, created_date, assigned_to, submitted_by, submitted_by_username, date_assigned, status, assigned_by, assigned_by_username) values (@application_id, @created_date, @assigned_to, @name, @submitted_by_username, getdate(), dbo.fn_getCurrentAppStatus(@application_id), @assigned_by, @assigned_by_username)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_newUnassignedTask]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_newUnassignedTask]
(
	@application_id varchar(100),
	@submitted_by varchar(50),
	@created_date datetime
)
as
begin
	insert into slw_unassigned_tasks (application_id, created_date, submitted_by) values(@application_id, @created_date, @submitted_by)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_newUserActivity]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_newUserActivity]
(
	@username varchar(50),
	@type varchar(100),
	@description varchar(max),
	@extras varchar(300),
	@priority int
)
as
begin
	insert into slw_user_activity(username, type, created_date, description, extras, priority) values(@username, @type, getdate(), @description, @extras, @priority)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_newUserType]    Script Date: 1/28/2019 12:59:04 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_removeFileReference]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_removeFileReference]
(
	@file_id varchar(100)
)
as
begin
	delete from slw_application_files where file_id=@file_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_resetPassword]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_resetPassword]
(
	@username varchar(50),
	@hash varchar(max)
)
as
begin
	update SLW_USERS set password_reset_required = 1, [hash]=@hash where username=@username
end
GO
/****** Object:  StoredProcedure [dbo].[sp_saveFormDetails]    Script Date: 1/28/2019 12:59:04 AM ******/
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
	@additional_info varchar(max),
	@name_of_test varchar(500),
	@country varchar(100),
	@status varchar(100),
	@category varchar(300)
)
as
begin
	declare @application_exist integer
	set @application_exist = (select count(application_id) from slw_forms where application_id=@applicationId)

	if(@application_exist=0)
	begin

		insert into slw_forms(username, application_id, created_date, status, last_updated, category, licensed_date)
					   values(@username, @applicationId, getdate(), @status, getdate(), @category, '')


		insert into slw_form_step1 (application_id, applicant_name, applicant_tel, applicant_address,
									applicant_fax, applicant_city_town, applicant_contact_person,
									applicant_nationality, manufacturer_name, manufacturer_tel, 
									manufacturer_address, manufacturer_fax, manufacturer_contact_person)

									values(@applicationId, @applicant_name, @applicant_tel, @applicant_address,
										   @applicant_fax, @applicant_city_town, @applicant_contact_person,
										   @applicant_nationality, @manufacturer_name, @manufacturer_tel, 
										   @manufacturer_address, @manufacturer_fax, @manufacturer_contact_person)

		insert into slw_form_step2 (application_id, equipment_type, equipment_description, product_identifiation, ref#,
									make, software, type_of_equipment, other, antenna_type, antenna_gain, channel, separation, additional_info, name_of_test, country)

									values(@applicationId, @equipment_type, @equipment_description, @product_identification, @ref#,
										   @make, @software, @type_of_equipment, @other, @antenna_type, @antenna_gain, @channel, @separation, @additional_info, @name_of_test, @country)

		
	end
	else
	begin
		update slw_forms set last_updated=getdate(), status=@status where application_id=@applicationId

		update slw_form_step1 set applicant_name=@applicant_name, applicant_tel=@applicant_tel, applicant_address=@applicant_address, applicant_fax=@applicant_fax, applicant_city_town=@applicant_city_town, applicant_contact_person=@applicant_contact_person, applicant_nationality=@applicant_nationality,  manufacturer_name=@manufacturer_name, manufacturer_tel=@manufacturer_tel, manufacturer_address=@manufacturer_address,
								  manufacturer_fax=@manufacturer_fax, manufacturer_contact_person=@manufacturer_contact_person
								  where application_id=@applicationId

		update slw_form_step2 set  equipment_type=@equipment_type, equipment_description=@equipment_description, product_identifiation=@product_identification,
								  ref#=@ref#, make=@make, software=@software, type_of_equipment=@type_of_equipment, other=@other,
								  antenna_type=@antenna_type, antenna_gain=@antenna_gain, channel = @channel, separation=@separation, additional_info=@additional_info, name_of_test=@name_of_test, country=@country
								  where application_id=@applicationId
	end
end




GO
/****** Object:  StoredProcedure [dbo].[sp_saveFrequencyDetails]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_saveFrequencyDetails]
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
	insert into slw_form_freq (application_id, sequence, lower_freq, upper_freq, power, tolerance, emmision_desig, freq_type)
							values(@application_id, @sequence, @lower_freq, @upper_freq, @power, @tolerance, @emmision_desig, @freq_type)
end




GO
/****** Object:  StoredProcedure [dbo].[sp_setNewEmailSettings]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_setNewEmailSettings]
(
	@email varchar(300),
	@company_name varchar(300)
)
as
begin
	delete from slw_email_settings
	insert into slw_email_settings (email, last_accessed, company_name) values (@email, getdate(), @company_name)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_updateApplicationStatus]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_updateApplicationStatus]
(
	@application_id varchar(100),
	@status varchar(100)
)
as
begin
	update slw_forms set status=@status where application_id=@application_id
	update slw_ongoing_tasks set status=@status where application_id=@application_id 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_updateAssignedUser]    Script Date: 1/28/2019 12:59:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_updateAssignedUser]
(
	@application_id varchar(100),
	@username varchar(50)
)
as
begin
	update slw_ongoing_tasks set assigned_to=@username, date_assigned=getdate() where application_id=@application_id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_updatePassword]    Script Date: 1/28/2019 12:59:04 AM ******/
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
ALTER DATABASE [SLW_DATABASE] SET  READ_WRITE 
GO
