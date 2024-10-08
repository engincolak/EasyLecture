USE [staj]
GO
/****** Object:  Table [dbo].[Lecture]    Script Date: 12.09.2024 10:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lecture](
	[LctID] [int] IDENTITY(1,1) NOT NULL,
	[LctName] [varchar](100) NULL,
	[LctisActive] [bit] NULL,
 CONSTRAINT [PK_Lecture] PRIMARY KEY CLUSTERED 
(
	[LctID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12.09.2024 10:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RolID] [int] NOT NULL,
	[RolName] [nvarchar](50) NULL,
	[RolIsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[RolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermission]    Script Date: 12.09.2024 10:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermission](
	[RpiId] [int] IDENTITY(1,1) NOT NULL,
	[RpiRolId] [int] NULL,
	[RpiService] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[RpiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 12.09.2024 10:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StdID] [int] IDENTITY(1,1) NOT NULL,
	[StdName] [varchar](75) NULL,
	[StdUseId] [int] NOT NULL,
	[StdIsActive] [bit] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentLecture]    Script Date: 12.09.2024 10:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentLecture](
	[StlLctId] [int] NOT NULL,
	[StlStdId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StlLctId] ASC,
	[StlStdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 12.09.2024 10:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[TcrID] [int] IDENTITY(1,1) NOT NULL,
	[TcrName] [varchar](75) NULL,
	[TcrUseID] [int] NOT NULL,
	[TcrIsActive] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12.09.2024 10:46:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UseID] [int] IDENTITY(1,1) NOT NULL,
	[UseMail] [nvarchar](255) NOT NULL,
	[UsePassword] [nvarchar](255) NOT NULL,
	[UseRolId] [int] NOT NULL,
	[UseIsActive] [bit] NOT NULL,
	[UseToken] [varchar](max) NULL,
 CONSTRAINT [PK__Users__1788CCAC98132C5C] PRIMARY KEY CLUSTERED 
(
	[UseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lecture] ADD  CONSTRAINT [DF_Lecture_isActive]  DEFAULT ((1)) FOR [LctisActive]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_IsActive]  DEFAULT ((1)) FOR [StdIsActive]
GO
ALTER TABLE [dbo].[Teacher] ADD  CONSTRAINT [DF_Teacher_IsActive]  DEFAULT ((1)) FOR [TcrIsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [UseIsActive]
GO
ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermission_Role] FOREIGN KEY([RpiRolId])
REFERENCES [dbo].[Role] ([RolID])
GO
ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_Role]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_User] FOREIGN KEY([StdUseId])
REFERENCES [dbo].[Users] ([UseID])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_User]
GO
ALTER TABLE [dbo].[StudentLecture]  WITH CHECK ADD  CONSTRAINT [FK_StudentLecture_Lecture] FOREIGN KEY([StlLctId])
REFERENCES [dbo].[Lecture] ([LctID])
GO
ALTER TABLE [dbo].[StudentLecture] CHECK CONSTRAINT [FK_StudentLecture_Lecture]
GO
ALTER TABLE [dbo].[StudentLecture]  WITH CHECK ADD  CONSTRAINT [FK_StudentLecture_Student] FOREIGN KEY([StlStdId])
REFERENCES [dbo].[Student] ([StdID])
GO
ALTER TABLE [dbo].[StudentLecture] CHECK CONSTRAINT [FK_StudentLecture_Student]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_User] FOREIGN KEY([TcrUseID])
REFERENCES [dbo].[Users] ([UseID])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_User]
GO
