USE [ACMS]
GO
/****** Object:  Table [dbo].[V_ACSM_USER_TMP]    Script Date: 11/16/2010 09:54:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[V_ACSM_USER_TMP](
	[ID] [nvarchar](100) NOT NULL,
	[NATIVE_NAME] [nvarchar](200) NOT NULL,
	[ENGLISH_NAME] [nvarchar](100) NULL,
	[WORK_ID] [nvarchar](36) NULL,
	[OFFICE_MAIL] [nvarchar](200) NULL,
	[DEPT_ID] [nvarchar](36) NULL,
	[C_DEPT_NAME] [nvarchar](200) NULL,
	[C_DEPT_ABBR] [nvarchar](200) NULL,
	[OFFICE_PHONE] [nvarchar](50) NULL,
	[EXPERIENCE_START_DATE] [datetime] NULL,
	[BIRTHDAY] [datetime] NULL,
	[SEX] [nvarchar](2) NULL,
	[JOB_CNAME] [nvarchar](200) NULL,
	[STATUS] [nvarchar](2) NULL,
	[WORK_END_DATE] [datetime] NULL,
	[COMPANY_CODE] [nvarchar](36) NULL,
	[C_NAME] [nvarchar](120) NULL,
	[createat] [datetime] NULL,
 CONSTRAINT [PK_V_ACSM_USER_TMP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[NATIVE_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[V_ACSM_USER]    Script Date: 11/16/2010 09:54:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[V_ACSM_USER](
	[ID] [nvarchar](100) NOT NULL,
	[NATIVE_NAME] [nvarchar](200) NOT NULL,
	[ENGLISH_NAME] [nvarchar](100) NULL,
	[WORK_ID] [nvarchar](36) NULL,
	[OFFICE_MAIL] [nvarchar](200) NULL,
	[DEPT_ID] [nvarchar](36) NULL,
	[C_DEPT_NAME] [nvarchar](200) NULL,
	[C_DEPT_ABBR] [nvarchar](200) NULL,
	[OFFICE_PHONE] [nvarchar](50) NULL,
	[EXPERIENCE_START_DATE] [datetime] NULL,
	[BIRTHDAY] [datetime] NULL,
	[SEX] [nvarchar](2) NULL,
	[JOB_CNAME] [nvarchar](200) NULL,
	[STATUS] [nvarchar](2) NULL,
	[WORK_END_DATE] [datetime] NULL,
	[COMPANY_CODE] [nvarchar](36) NULL,
	[C_NAME] [nvarchar](120) NULL,
	[createat] [datetime] NULL,
 CONSTRAINT [PK_V_ACSM_USER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[NATIVE_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[UTILfn_Split]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[UTILfn_Split](
 @String nvarchar (4000),
 @Delimiter nvarchar (10)
 )
returns @ValueTable table ([Value] nvarchar(4000))
begin
 declare @NextString nvarchar(4000)
 declare @Pos int
 declare @NextPos int
 declare @CommaCheck nvarchar(1)
 
 --Initialize
 set @NextString = ''
 set @CommaCheck = right(@String,1) 
 
 --Check for trailing Comma, if not exists, INSERT
 --if (@CommaCheck <> @Delimiter )
 set @String = @String + @Delimiter
 
 --Get position of first Comma
 set @Pos = charindex(@Delimiter,@String)
 set @NextPos = 1
 
 --Loop while there is still a comma in the String of levels
 while (@pos <>  0)  
 begin
  set @NextString = substring(@String,1,@Pos - 1)
 
  insert into @ValueTable ( [Value]) Values (@NextString)
 
  set @String = substring(@String,@pos +1,len(@String))
  
  set @NextPos = @Pos
  set @pos  = charindex(@Delimiter,@String)
 end
 
 return
end
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[active] [nchar](1) NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleUserMapping]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleUserMapping](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NOT NULL,
	[unit_id] [int] NOT NULL,
	[emp_id] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_RoleUserMapping_1] PRIMARY KEY CLUSTERED 
(
	[unit_id] ASC,
	[role_id] ASC,
	[emp_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleList]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleList](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](50) NULL,
 CONSTRAINT [PK_RoleList] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JOB_GRADE_GROUP]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JOB_GRADE_GROUP](
	[GROUP_CODE] [int] NOT NULL,
	[GROUP_DESCRIPTION] [nvarchar](100) NULL,
 CONSTRAINT [PK_JOB_GRADE_GROUP] PRIMARY KEY CLUSTERED 
(
	[GROUP_CODE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomFieldValue]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFieldValue](
	[id] [uniqueidentifier] NULL,
	[emp_id] [nvarchar](50) NOT NULL,
	[field_id] [int] NOT NULL,
	[field_value] [nvarchar](200) NULL,
 CONSTRAINT [PK_KeyValue] PRIMARY KEY CLUSTERED 
(
	[emp_id] ASC,
	[field_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomFieldItem]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFieldItem](
	[field_id] [int] NULL,
	[field_item_id] [int] IDENTITY(1,1) NOT NULL,
	[field_item_name] [nvarchar](50) NULL,
	[field_item_text] [nvarchar](50) NULL,
 CONSTRAINT [PK_KeyItem] PRIMARY KEY CLUSTERED 
(
	[field_item_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomField]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomField](
	[activity_id] [uniqueidentifier] NULL,
	[field_id] [int] IDENTITY(1,1) NOT NULL,
	[field_name] [nvarchar](50) NULL,
	[field_control] [nvarchar](50) NULL,
 CONSTRAINT [PK_KeyDefine] PRIMARY KEY CLUSTERED 
(
	[field_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityTeamMember]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityTeamMember](
	[activity_id] [uniqueidentifier] NOT NULL,
	[emp_id] [nvarchar](100) NOT NULL,
	[boss_id] [nvarchar](100) NULL,
	[idno_type] [smallint] NULL,
	[idno] [nvarchar](20) NULL,
	[remark] [nvarchar](max) NULL,
	[check_status] [int] NULL,
 CONSTRAINT [PK_ActivityTeamMember] PRIMARY KEY CLUSTERED 
(
	[activity_id] ASC,
	[emp_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityRegist]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityRegist](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[activity_id] [uniqueidentifier] NOT NULL,
	[emp_id] [nvarchar](100) NOT NULL,
	[regist_by] [nvarchar](100) NULL,
	[idno_type] [int] NULL,
	[idno] [nvarchar](20) NULL,
	[team_name] [nvarchar](100) NULL,
	[ext_people] [int] NULL,
	[createat] [datetime] NULL,
	[check_status] [int] NULL,
 CONSTRAINT [PK_ActivityRegist] PRIMARY KEY CLUSTERED 
(
	[activity_id] ASC,
	[emp_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityGroupLimit]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityGroupLimit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[activity_id] [uniqueidentifier] NOT NULL,
	[emp_id] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ActivityGroupLimit] PRIMARY KEY CLUSTERED 
(
	[activity_id] ASC,
	[emp_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 11/16/2010 09:54:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Activity](
	[sn] [int] IDENTITY(1,1) NOT NULL,
	[id] [uniqueidentifier] NOT NULL,
	[activity_type] [char](1) NULL,
	[activity_info] [ntext] NULL,
	[org_id] [nvarchar](50) NULL,
	[activity_name] [nvarchar](50) NULL,
	[people_type] [nvarchar](50) NULL,
	[activity_startdate] [datetime] NULL,
	[activity_enddate] [datetime] NULL,
	[limit_count] [int] NULL,
	[limit2_count] [int] NULL,
	[team_member_min] [int] NULL,
	[team_member_max] [int] NULL,
	[regist_startdate] [datetime] NULL,
	[regist_deadline] [datetime] NULL,
	[cancelregist_deadline] [datetime] NULL,
	[is_showfile] [nchar](1) NULL,
	[is_showprogress] [nchar](1) NULL,
	[is_showperson_fix1] [nchar](1) NULL,
	[is_showperson_fix2] [nchar](1) NULL,
	[personextcount_min] [int] NULL,
	[personextcount_max] [int] NULL,
	[is_showidno] [nchar](1) NULL,
	[is_showremark] [nchar](1) NULL,
	[remark_name] [nvarchar](50) NULL,
	[is_showteam_fix1] [nchar](1) NULL,
	[is_showteam_fix2] [nchar](1) NULL,
	[teamextcount_min] [int] NULL,
	[teamextcount_max] [int] NULL,
	[is_grouplimit] [nchar](1) NULL,
	[notice] [ntext] NULL,
	[active] [nchar](1) NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[V_ACSM_USER2]    Script Date: 11/16/2010 09:54:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_ACSM_USER2]
AS
SELECT [ID] as [WINDOWS_ID],[ID]+[NATIVE_NAME] as [ID]
      ,[NATIVE_NAME]
      ,[ENGLISH_NAME]
      ,[WORK_ID]
      ,[OFFICE_MAIL]
      ,[DEPT_ID]
      ,[C_DEPT_NAME]
      ,[C_DEPT_ABBR]
      ,[OFFICE_PHONE]
      ,[EXPERIENCE_START_DATE]
      ,[BIRTHDAY]
      ,[SEX]
      ,[JOB_CNAME]
      ,[STATUS]
      ,[WORK_END_DATE]
      ,[COMPANY_CODE]
      ,[C_NAME]
  FROM [ACMS].[dbo].[V_ACSM_USER]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "V_ACSM_USER"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_ACSM_USER2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_ACSM_USER2'
GO
