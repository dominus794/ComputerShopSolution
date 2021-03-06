USE [JustITComputerShop]
GO
/****** Object:  Table [dbo].[Manufacturers]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturers](
	[manufacturer_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED 
(
	[manufacturer_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [AK_Manufacturers] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key of the Manufacturer.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Manufacturers', @level2type=N'COLUMN',@level2name=N'manufacturer_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The name of the manufacturer. e.g Intel' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Manufacturers', @level2type=N'COLUMN',@level2name=N'name'
GO
SET IDENTITY_INSERT [dbo].[Manufacturers] ON
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (2, N'Amd')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (70, N'Compaq')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (4, N'Corsair')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (39, N'Crucial')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (7, N'Dell')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (47, N'Geil')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (53, N'Hewlett Packard')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (1, N'Intel')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (36, N'Kingston')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (52, N'Lenovo')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (72, N'Nintendo')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (45, N'Nvidia')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (54, N'Packard Bell')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (3, N'Samsung')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (5, N'Sandisk')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (15, N'Seagate')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (8, N'Sony')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (56, N'Toshiba')
INSERT [dbo].[Manufacturers] ([manufacturer_id], [name]) VALUES (34, N'Western Digital')
SET IDENTITY_INSERT [dbo].[Manufacturers] OFF
/****** Object:  StoredProcedure [dbo].[GetManufacturerIDByName]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robert Harris
-- Create date: 16.11.12
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetManufacturerIDByName] 
	-- Add the parameters for the stored procedure here
	@manufacturerName nvarchar(50), 
	@manufacturerID int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT @manufacturerID = manufacturer_id 
	FROM Manufacturers 
	WHERE name = @manufacturerName;
END
GO
/****** Object:  StoredProcedure [dbo].[GetManufacturerByID]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robert Harris
-- Create date: 16.11.12
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetManufacturerByID] 
	-- Add the parameters for the stored procedure here
	@manufacturerID int = 0, 
	@manufacturerName nvarchar(50) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT @manufacturerName = name FROM Manufacturers WHERE manufacturer_id = @manufacturerID
END
GO
/****** Object:  Table [dbo].[CPUs]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CPUs](
	[cpu_id] [int] IDENTITY(1,1) NOT NULL,
	[manufacturer_id] [int] NOT NULL,
	[model] [nvarchar](50) NOT NULL,
	[price] [smallmoney] NOT NULL,
	[clock_speed] [decimal](3, 2) NOT NULL,
	[no_of_cores] [nchar](10) NOT NULL,
	[cpu_form_factor] [nchar](10) NOT NULL,
 CONSTRAINT [PK_CPUs] PRIMARY KEY CLUSTERED 
(
	[cpu_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [AK_CPUs_Model] UNIQUE NONCLUSTERED 
(
	[model] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key for CPU.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'COLUMN',@level2name=N'cpu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign Key to identify Manufacturer of this CPU. e.g Intel' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'COLUMN',@level2name=N'manufacturer_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Model name of the CPU. e.g Core2Duo E8500' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'COLUMN',@level2name=N'model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The price of the CPU. e.g 299.99' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'COLUMN',@level2name=N'price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The clock speed of the CPU in Ghz. e.g 3.16' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'COLUMN',@level2name=N'clock_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The number of cores in the CPU.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'COLUMN',@level2name=N'no_of_cores'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The form factor of the CPU e.g Desktop of Laptop' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'COLUMN',@level2name=N'cpu_form_factor'
GO
SET IDENTITY_INSERT [dbo].[CPUs] ON
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (5, 1, N'Core2Duo E8500', 39.9900, CAST(3.16 AS Decimal(3, 2)), N'Dual      ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (13, 2, N'Thunderbird', 5.0000, CAST(1.40 AS Decimal(3, 2)), N'Single    ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (14, 1, N'i7 3770', 259.9900, CAST(3.50 AS Decimal(3, 2)), N'Quad      ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (15, 1, N'i5 3570', 229.9900, CAST(3.40 AS Decimal(3, 2)), N'Quad      ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (16, 2, N'Athlon64 3500', 10.0000, CAST(2.50 AS Decimal(3, 2)), N'Single    ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (18, 2, N'Bulldozer FX 8350', 259.9900, CAST(4.00 AS Decimal(3, 2)), N'Octo      ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (24, 1, N'Pentium II', 0.0500, CAST(1.00 AS Decimal(3, 2)), N'Single    ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (28, 1, N'Pentium III', 20.0000, CAST(1.33 AS Decimal(3, 2)), N'Single    ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (30, 1, N'Core2Quad Q6600', 25.0000, CAST(2.40 AS Decimal(3, 2)), N'Quad      ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (33, 2, N'Duron', 2.5000, CAST(1.00 AS Decimal(3, 2)), N'Single    ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (34, 2, N'E1 1200', 29.9900, CAST(1.40 AS Decimal(3, 2)), N'Dual      ', N'Desktop   ')
INSERT [dbo].[CPUs] ([cpu_id], [manufacturer_id], [model], [price], [clock_speed], [no_of_cores], [cpu_form_factor]) VALUES (35, 1, N'Celeron B830', 29.9900, CAST(1.80 AS Decimal(3, 2)), N'Dual      ', N'Laptop    ')
SET IDENTITY_INSERT [dbo].[CPUs] OFF
/****** Object:  Table [dbo].[RamModules]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RamModules](
	[ram_id] [int] IDENTITY(1,1) NOT NULL,
	[manufacturer_id] [int] NOT NULL,
	[model] [nvarchar](50) NOT NULL,
	[price] [smallmoney] NOT NULL,
	[bus_speed] [smallint] NOT NULL,
	[size] [smallint] NOT NULL,
	[ddr_version] [nchar](4) NOT NULL,
	[ram_form_factor] [nchar](10) NOT NULL,
 CONSTRAINT [PK_RamModules] PRIMARY KEY CLUSTERED 
(
	[ram_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [AK_RamModules] UNIQUE NONCLUSTERED 
(
	[manufacturer_id] ASC,
	[model] ASC,
	[bus_speed] ASC,
	[size] ASC,
	[ddr_version] ASC,
	[ram_form_factor] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key of the Ram Module.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'COLUMN',@level2name=N'ram_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign Key to identify the Manufacturer of this Ram Module.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'COLUMN',@level2name=N'manufacturer_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The name of the Ram module. e.g Dominator' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'COLUMN',@level2name=N'model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The price of the Ram Module. e.g £49.99' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'COLUMN',@level2name=N'price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The bus speed of the Ram Module in Mhz. e.g 400Mhz' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'COLUMN',@level2name=N'bus_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The size of the Ram Module in MB. e.g 2048MB' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'COLUMN',@level2name=N'size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The DDR version of the Ram Module. e.g DDR | DDR2 | DDR3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'COLUMN',@level2name=N'ddr_version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The form factor of the Ram Module. e.g DIMM or SODIMM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'COLUMN',@level2name=N'ram_form_factor'
GO
SET IDENTITY_INSERT [dbo].[RamModules] ON
INSERT [dbo].[RamModules] ([ram_id], [manufacturer_id], [model], [price], [bus_speed], [size], [ddr_version], [ram_form_factor]) VALUES (5, 4, N'Dominator', 19.9900, 1066, 2048, N'DDR2', N'DIMM      ')
INSERT [dbo].[RamModules] ([ram_id], [manufacturer_id], [model], [price], [bus_speed], [size], [ddr_version], [ram_form_factor]) VALUES (17, 4, N'Dominator', 15.9900, 1066, 1024, N'DDR2', N'DIMM      ')
INSERT [dbo].[RamModules] ([ram_id], [manufacturer_id], [model], [price], [bus_speed], [size], [ddr_version], [ram_form_factor]) VALUES (19, 4, N'Dominator', 25.9900, 1066, 2048, N'DDR2', N'SODIMM    ')
INSERT [dbo].[RamModules] ([ram_id], [manufacturer_id], [model], [price], [bus_speed], [size], [ddr_version], [ram_form_factor]) VALUES (20, 4, N'Dominator', 19.9900, 1066, 1024, N'DDR2', N'SODIMM    ')
INSERT [dbo].[RamModules] ([ram_id], [manufacturer_id], [model], [price], [bus_speed], [size], [ddr_version], [ram_form_factor]) VALUES (25, 39, N'Extreme', 29.9900, 1066, 2048, N'DDR3', N'DIMM      ')
INSERT [dbo].[RamModules] ([ram_id], [manufacturer_id], [model], [price], [bus_speed], [size], [ddr_version], [ram_form_factor]) VALUES (28, 47, N'DragonX', 19.9900, 400, 1024, N'DDR ', N'DIMM      ')
INSERT [dbo].[RamModules] ([ram_id], [manufacturer_id], [model], [price], [bus_speed], [size], [ddr_version], [ram_form_factor]) VALUES (31, 36, N'HyperX', 29.9900, 1600, 2048, N'DDR3', N'DIMM      ')
INSERT [dbo].[RamModules] ([ram_id], [manufacturer_id], [model], [price], [bus_speed], [size], [ddr_version], [ram_form_factor]) VALUES (41, 36, N'HyperX', 69.9900, 1600, 4096, N'DDR3', N'DIMM      ')
INSERT [dbo].[RamModules] ([ram_id], [manufacturer_id], [model], [price], [bus_speed], [size], [ddr_version], [ram_form_factor]) VALUES (42, 36, N'HyperX', 19.9900, 1600, 1024, N'DDR3', N'DIMM      ')
INSERT [dbo].[RamModules] ([ram_id], [manufacturer_id], [model], [price], [bus_speed], [size], [ddr_version], [ram_form_factor]) VALUES (43, 39, N'Eco', 19.9900, 1333, 2048, N'DDR3', N'SODIMM    ')
SET IDENTITY_INSERT [dbo].[RamModules] OFF
/****** Object:  Table [dbo].[Monitors]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monitors](
	[monitor_id] [int] IDENTITY(1,1) NOT NULL,
	[manufacturer_id] [int] NOT NULL,
	[model] [nvarchar](50) NOT NULL,
	[price] [smallmoney] NOT NULL,
	[size] [tinyint] NOT NULL,
 CONSTRAINT [PK_Monitors] PRIMARY KEY CLUSTERED 
(
	[monitor_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key of the Monitor.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitors', @level2type=N'COLUMN',@level2name=N'monitor_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign Key to identify the Manufacturer of the Monitor.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitors', @level2type=N'COLUMN',@level2name=N'manufacturer_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The price of the Monitor.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitors', @level2type=N'COLUMN',@level2name=N'price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The size of the Monitor''s display in inches. e.g 24"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitors', @level2type=N'COLUMN',@level2name=N'size'
GO
SET IDENTITY_INSERT [dbo].[Monitors] ON
INSERT [dbo].[Monitors] ([monitor_id], [manufacturer_id], [model], [price], [size]) VALUES (1, 7, N'UltraSharp', 299.9900, 24)
SET IDENTITY_INSERT [dbo].[Monitors] OFF
/****** Object:  Table [dbo].[HDDs]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HDDs](
	[hdd_id] [int] IDENTITY(1,1) NOT NULL,
	[manufacturer_id] [int] NOT NULL,
	[model] [nvarchar](50) NOT NULL,
	[price] [smallmoney] NOT NULL,
	[capacity] [smallint] NOT NULL,
	[speed] [smallint] NOT NULL,
	[hdd_type] [nchar](10) NOT NULL,
	[hdd_form_factor] [nchar](10) NOT NULL,
	[hdd_interface] [nchar](10) NOT NULL,
 CONSTRAINT [PK_HDDs] PRIMARY KEY CLUSTERED 
(
	[hdd_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [AK_HDDs] UNIQUE NONCLUSTERED 
(
	[manufacturer_id] ASC,
	[model] ASC,
	[capacity] ASC,
	[speed] ASC,
	[hdd_type] ASC,
	[hdd_form_factor] ASC,
	[hdd_interface] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key of the HDD.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'COLUMN',@level2name=N'hdd_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign Key to identify Manufacturer of the HDD.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'COLUMN',@level2name=N'manufacturer_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The name of the HDD. e.g Spinpoint' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'COLUMN',@level2name=N'model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The price of the HDD. e.g £99.99' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'COLUMN',@level2name=N'price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The capacity of the HDD in GB. e.g 1024GB' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'COLUMN',@level2name=N'capacity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The type of the HDD. e.g Mechanical or SSD' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'COLUMN',@level2name=N'hdd_type'
GO
SET IDENTITY_INSERT [dbo].[HDDs] ON
INSERT [dbo].[HDDs] ([hdd_id], [manufacturer_id], [model], [price], [capacity], [speed], [hdd_type], [hdd_form_factor], [hdd_interface]) VALUES (10, 5, N'Ultra', 89.9900, 120, 6, N'SSD       ', N'LAPTOP    ', N'SATA      ')
INSERT [dbo].[HDDs] ([hdd_id], [manufacturer_id], [model], [price], [capacity], [speed], [hdd_type], [hdd_form_factor], [hdd_interface]) VALUES (11, 5, N'Ultra', 169.9900, 256, 6, N'SSD       ', N'LAPTOP    ', N'SATA      ')
INSERT [dbo].[HDDs] ([hdd_id], [manufacturer_id], [model], [price], [capacity], [speed], [hdd_type], [hdd_form_factor], [hdd_interface]) VALUES (13, 3, N'Spinpoint', 59.9900, 1024, 6, N'Mechanical', N'DESKTOP   ', N'SATA      ')
INSERT [dbo].[HDDs] ([hdd_id], [manufacturer_id], [model], [price], [capacity], [speed], [hdd_type], [hdd_form_factor], [hdd_interface]) VALUES (15, 3, N'Spinpoint', 99.9900, 2048, 6, N'Mechanical', N'DESKTOP   ', N'SATA      ')
INSERT [dbo].[HDDs] ([hdd_id], [manufacturer_id], [model], [price], [capacity], [speed], [hdd_type], [hdd_form_factor], [hdd_interface]) VALUES (21, 15, N'Barracuda', 109.0000, 2048, 6, N'Mechanical', N'DESKTOP   ', N'SATA      ')
INSERT [dbo].[HDDs] ([hdd_id], [manufacturer_id], [model], [price], [capacity], [speed], [hdd_type], [hdd_form_factor], [hdd_interface]) VALUES (24, 3, N'Spinpoint', 39.9900, 500, 6, N'Mechanical', N'DESKTOP   ', N'SATA      ')
INSERT [dbo].[HDDs] ([hdd_id], [manufacturer_id], [model], [price], [capacity], [speed], [hdd_type], [hdd_form_factor], [hdd_interface]) VALUES (26, 34, N'Green', 64.9900, 2048, 6, N'Mechanical', N'LAPTOP    ', N'SATA      ')
INSERT [dbo].[HDDs] ([hdd_id], [manufacturer_id], [model], [price], [capacity], [speed], [hdd_type], [hdd_form_factor], [hdd_interface]) VALUES (27, 34, N'Green', 49.9900, 500, 6, N'Mechanical', N'DESKTOP   ', N'SATA      ')
SET IDENTITY_INSERT [dbo].[HDDs] OFF
/****** Object:  Table [dbo].[GPUs]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GPUs](
	[gpu_id] [int] IDENTITY(1,1) NOT NULL,
	[manufacturer_id] [int] NOT NULL,
	[model] [nvarchar](50) NOT NULL,
	[price] [smallmoney] NOT NULL,
	[gpu_model] [nchar](10) NOT NULL,
	[gpu_clock_speed] [smallint] NOT NULL,
	[vram_size] [smallint] NOT NULL,
	[vram_clock_speed] [smallint] NOT NULL,
	[vram_type] [nchar](10) NOT NULL,
	[gpu_type] [nchar](10) NOT NULL,
 CONSTRAINT [PK_GPUs] PRIMARY KEY CLUSTERED 
(
	[gpu_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [AK_GPUs] UNIQUE NONCLUSTERED 
(
	[manufacturer_id] ASC,
	[model] ASC,
	[gpu_model] ASC,
	[gpu_clock_speed] ASC,
	[vram_size] ASC,
	[vram_clock_speed] ASC,
	[vram_type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary of the GPU.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'COLUMN',@level2name=N'gpu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign Key to identify Manufacturer of this GPU.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'COLUMN',@level2name=N'manufacturer_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The name of the GPU. e.g Radeon 4870' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'COLUMN',@level2name=N'model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The price of the GPU. e.g £399.99' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'COLUMN',@level2name=N'price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The core clock speed of the GPU in Mhz. e.g 1000Mhz' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'COLUMN',@level2name=N'gpu_clock_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The amount of vram the GPU has in MB. e.g 1024MB' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'COLUMN',@level2name=N'vram_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The clock speed of the Vram in Mhz. e.g 2000Mhz' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'COLUMN',@level2name=N'vram_clock_speed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The type of GPU. e.g Dedicated or Onboard' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'COLUMN',@level2name=N'gpu_type'
GO
SET IDENTITY_INSERT [dbo].[GPUs] ON
INSERT [dbo].[GPUs] ([gpu_id], [manufacturer_id], [model], [price], [gpu_model], [gpu_clock_speed], [vram_size], [vram_clock_speed], [vram_type], [gpu_type]) VALUES (10, 2, N'Radeon', 29.9900, N'4870      ', 750, 1024, 900, N'GDDR5     ', N'Dedicated ')
INSERT [dbo].[GPUs] ([gpu_id], [manufacturer_id], [model], [price], [gpu_model], [gpu_clock_speed], [vram_size], [vram_clock_speed], [vram_type], [gpu_type]) VALUES (25, 2, N'Radeon', 49.9900, N'4870      ', 750, 2048, 900, N'GDDR5     ', N'Dedicated ')
INSERT [dbo].[GPUs] ([gpu_id], [manufacturer_id], [model], [price], [gpu_model], [gpu_clock_speed], [vram_size], [vram_clock_speed], [vram_type], [gpu_type]) VALUES (26, 2, N'Radeon', 199.9900, N'5780      ', 1500, 2048, 1800, N'GDDR5     ', N'Dedicated ')
INSERT [dbo].[GPUs] ([gpu_id], [manufacturer_id], [model], [price], [gpu_model], [gpu_clock_speed], [vram_size], [vram_clock_speed], [vram_type], [gpu_type]) VALUES (27, 45, N'Geforce', 299.9900, N'680       ', 1000, 2048, 2000, N'GDDR5     ', N'Dedicated ')
INSERT [dbo].[GPUs] ([gpu_id], [manufacturer_id], [model], [price], [gpu_model], [gpu_clock_speed], [vram_size], [vram_clock_speed], [vram_type], [gpu_type]) VALUES (29, 45, N'Geforce 4 Ti', 10.0000, N'4200      ', 400, 512, 500, N'GDDR2     ', N'Dedicated ')
INSERT [dbo].[GPUs] ([gpu_id], [manufacturer_id], [model], [price], [gpu_model], [gpu_clock_speed], [vram_size], [vram_clock_speed], [vram_type], [gpu_type]) VALUES (31, 2, N'Radeon', 119.9900, N'6870      ', 1000, 1024, 1200, N'GDDR5     ', N'Dedicated ')
INSERT [dbo].[GPUs] ([gpu_id], [manufacturer_id], [model], [price], [gpu_model], [gpu_clock_speed], [vram_size], [vram_clock_speed], [vram_type], [gpu_type]) VALUES (32, 2, N'Radeon HD', 39.9900, N'7310      ', 500, 1024, 1000, N'GDDR5     ', N'Onboard   ')
SET IDENTITY_INSERT [dbo].[GPUs] OFF
/****** Object:  StoredProcedure [dbo].[GetRamModuleByID]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetRamModuleByID] 
	-- Add the parameters for the stored procedure here
	@ram_id int = 0	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	RamModules.model, RamModules.price, RamModules.ram_form_factor, RamModules.size, RamModules.bus_speed, RamModules.ddr_version, 
            RamModules.manufacturer_id, Manufacturers.name AS manufacturerName
	FROM	Manufacturers INNER JOIN RamModules ON Manufacturers.manufacturer_id = RamModules.manufacturer_id
	WHERE	RamModules.ram_id = @ram_id
END
GO
/****** Object:  StoredProcedure [dbo].[GetHDDByID]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robert Harris
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetHDDByID] 
	-- Add the parameters for the stored procedure here
	@hdd_id int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	HDDs.model, 
			HDDs.price, 
			HDDs.capacity, 
			HDDs.speed, 
			HDDs.hdd_type, 
			HDDs.hdd_form_factor, 
			HDDs.hdd_interface, 
			HDDs.manufacturer_id, 
            Manufacturers.name AS manufacturerName
	FROM	HDDs INNER JOIN Manufacturers ON HDDs.manufacturer_id = Manufacturers.manufacturer_id
	WHERE	HDDs.hdd_id = @hdd_id
END
GO
/****** Object:  StoredProcedure [dbo].[GetGPUByID]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robert Harris
-- Create date: 17.11.12
-- Description:	Returns a specified GPU
-- =============================================
CREATE PROCEDURE [dbo].[GetGPUByID] 
	-- Add the parameters for the stored procedure here
	@gpu_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT GPUs.model, GPUs.price, GPUs.gpu_model, GPUs.gpu_clock_speed, GPUs.vram_size, GPUs.vram_clock_speed, GPUs.vram_type, GPUs.gpu_type, 
                      GPUs.manufacturer_id, Manufacturers.name AS manufacturerName
	FROM   GPUs INNER JOIN Manufacturers ON GPUs.manufacturer_id = Manufacturers.manufacturer_id
	WHERE GPUs.gpu_id = @gpu_id
END
GO
/****** Object:  StoredProcedure [dbo].[GetCPUByID]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robert Harris
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetCPUByID] 
	-- Add the parameters for the stored procedure here
	@cpu_id int = 0	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   CPUs.model, CPUs.price, CPUs.clock_speed, CPUs.no_of_cores, CPUs.cpu_form_factor, CPUs.manufacturer_id, Manufacturers.name AS manufacturerName
	FROM     CPUs INNER JOIN Manufacturers ON CPUs.manufacturer_id = Manufacturers.manufacturer_id
	WHERE	 CPUs.cpu_id = @cpu_id
END
GO
/****** Object:  Table [dbo].[Desktops]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Desktops](
	[desktop_id] [int] IDENTITY(1,1) NOT NULL,
	[manufacturer_id] [int] NULL,
	[model] [nvarchar](50) NOT NULL,
	[price] [smallmoney] NOT NULL,
	[cpu_id] [int] NOT NULL,
	[monitor_id] [int] NULL,
 CONSTRAINT [PK_Desktops] PRIMARY KEY CLUSTERED 
(
	[desktop_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key for the Desktop.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Desktops', @level2type=N'COLUMN',@level2name=N'desktop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign Key to identify the Manufacturer of this Desktop.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Desktops', @level2type=N'COLUMN',@level2name=N'manufacturer_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The name of the Desktop. e.g Inspiron' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Desktops', @level2type=N'COLUMN',@level2name=N'model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The total price of the Desktop. e.g £999.99' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Desktops', @level2type=N'COLUMN',@level2name=N'price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foreign Key to identify the CPU in this Desktop.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Desktops', @level2type=N'COLUMN',@level2name=N'cpu_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Optional Foreign Key to identify a Monitor associated with this Desktop.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Desktops', @level2type=N'COLUMN',@level2name=N'monitor_id'
GO
SET IDENTITY_INSERT [dbo].[Desktops] ON
INSERT [dbo].[Desktops] ([desktop_id], [manufacturer_id], [model], [price], [cpu_id], [monitor_id]) VALUES (24, 70, N'CQ2910EA', 249.0000, 34, 1)
INSERT [dbo].[Desktops] ([desktop_id], [manufacturer_id], [model], [price], [cpu_id], [monitor_id]) VALUES (27, 53, N'Pavilion 3700', 699.9900, 14, 1)
SET IDENTITY_INSERT [dbo].[Desktops] OFF
/****** Object:  Table [dbo].[Laptops]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Laptops](
	[laptop_id] [int] IDENTITY(1,1) NOT NULL,
	[manufacturer_id] [int] NOT NULL,
	[model] [nvarchar](50) NOT NULL,
	[price] [smallmoney] NOT NULL,
	[weight] [tinyint] NOT NULL,
	[battery_life] [tinyint] NOT NULL,
	[display_size] [decimal](3, 1) NOT NULL,
	[cpu_id] [int] NOT NULL,
	[hdd_id] [int] NOT NULL,
	[gpu_id] [int] NOT NULL,
 CONSTRAINT [PK_Laptops] PRIMARY KEY CLUSTERED 
(
	[laptop_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key for the Laptop' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laptops', @level2type=N'COLUMN',@level2name=N'laptop_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The name of the Laptop. e.g Inspiron' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laptops', @level2type=N'COLUMN',@level2name=N'model'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The price of the Laptop. e.g £999.99' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laptops', @level2type=N'COLUMN',@level2name=N'price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The weight of the Laptop in Kg. e.g 4Kg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laptops', @level2type=N'COLUMN',@level2name=N'weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The battery life of the Laptop in Hours. e.g 6hrs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laptops', @level2type=N'COLUMN',@level2name=N'battery_life'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The display size of the laptop in inches. e.g 15.6" 17.0"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laptops', @level2type=N'COLUMN',@level2name=N'display_size'
GO
SET IDENTITY_INSERT [dbo].[Laptops] ON
INSERT [dbo].[Laptops] ([laptop_id], [manufacturer_id], [model], [price], [weight], [battery_life], [display_size], [cpu_id], [hdd_id], [gpu_id]) VALUES (8, 52, N'G580 Blue', 299.9900, 3, 5, CAST(15.6 AS Decimal(3, 1)), 35, 26, 32)
SET IDENTITY_INSERT [dbo].[Laptops] OFF
/****** Object:  Table [dbo].[LaptopRamModules]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LaptopRamModules](
	[laptop_id] [int] NOT NULL,
	[ram_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_LaptopRamModules] PRIMARY KEY CLUSTERED 
(
	[laptop_id] ASC,
	[ram_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[LaptopRamModules] ([laptop_id], [ram_id], [quantity]) VALUES (8, 43, 2)
/****** Object:  Table [dbo].[DesktopRamModules]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DesktopRamModules](
	[desktop_id] [int] NOT NULL,
	[ram_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_DesktopRamModules_1] PRIMARY KEY CLUSTERED 
(
	[desktop_id] ASC,
	[ram_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[DesktopRamModules] ([desktop_id], [ram_id], [quantity]) VALUES (24, 42, 2)
INSERT [dbo].[DesktopRamModules] ([desktop_id], [ram_id], [quantity]) VALUES (27, 41, 2)
/****** Object:  Table [dbo].[DesktopHDDs]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DesktopHDDs](
	[desktop_id] [int] NOT NULL,
	[hdd_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_DesktopHDDs_1] PRIMARY KEY CLUSTERED 
(
	[desktop_id] ASC,
	[hdd_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[DesktopHDDs] ([desktop_id], [hdd_id], [quantity]) VALUES (24, 27, 1)
INSERT [dbo].[DesktopHDDs] ([desktop_id], [hdd_id], [quantity]) VALUES (27, 11, 1)
/****** Object:  Table [dbo].[DesktopGPUs]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DesktopGPUs](
	[desktop_id] [int] NOT NULL,
	[gpu_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_DesktopGPUs_1] PRIMARY KEY CLUSTERED 
(
	[desktop_id] ASC,
	[gpu_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[DesktopGPUs] ([desktop_id], [gpu_id], [quantity]) VALUES (24, 32, 1)
INSERT [dbo].[DesktopGPUs] ([desktop_id], [gpu_id], [quantity]) VALUES (27, 31, 1)
/****** Object:  View [dbo].[ViewDesktopsTest]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewDesktopsTest]
AS
SELECT     dbo.Manufacturers.name, dbo.Desktops.model, dbo.Desktops.price, Manufacturers_1.name AS Expr3, dbo.CPUs.model AS Expr1, dbo.CPUs.price AS Expr2, 
                      dbo.CPUs.clock_speed, dbo.CPUs.no_of_cores, dbo.CPUs.cpu_form_factor, Manufacturers_2.name AS Expr4, dbo.GPUs.model AS Expr5, dbo.GPUs.price AS Expr6, 
                      dbo.GPUs.gpu_model, dbo.GPUs.gpu_clock_speed, dbo.GPUs.vram_size, dbo.GPUs.vram_clock_speed, dbo.GPUs.vram_type, dbo.GPUs.gpu_type, 
                      dbo.DesktopGPUs.quantity, Manufacturers_3.name AS Expr7, dbo.HDDs.model AS Expr8, dbo.HDDs.price AS Expr9, dbo.HDDs.capacity, dbo.HDDs.speed, 
                      dbo.HDDs.hdd_type, dbo.HDDs.hdd_form_factor, dbo.HDDs.hdd_interface, dbo.DesktopHDDs.quantity AS Expr10, Manufacturers_4.name AS Expr11, 
                      dbo.RamModules.model AS Expr12, dbo.RamModules.price AS Expr13, dbo.RamModules.bus_speed, dbo.RamModules.size, dbo.RamModules.ddr_version, 
                      dbo.RamModules.ram_form_factor, dbo.DesktopRamModules.quantity AS Expr14
FROM         dbo.Desktops INNER JOIN
                      dbo.Manufacturers ON dbo.Desktops.manufacturer_id = dbo.Manufacturers.manufacturer_id INNER JOIN
                      dbo.CPUs ON dbo.Desktops.cpu_id = dbo.CPUs.cpu_id INNER JOIN
                      dbo.Manufacturers AS Manufacturers_1 ON dbo.CPUs.manufacturer_id = Manufacturers_1.manufacturer_id INNER JOIN
                      dbo.DesktopGPUs ON dbo.Desktops.desktop_id = dbo.DesktopGPUs.desktop_id INNER JOIN
                      dbo.GPUs ON dbo.DesktopGPUs.gpu_id = dbo.GPUs.gpu_id INNER JOIN
                      dbo.Manufacturers AS Manufacturers_2 ON dbo.GPUs.manufacturer_id = Manufacturers_2.manufacturer_id INNER JOIN
                      dbo.DesktopHDDs ON dbo.Desktops.desktop_id = dbo.DesktopHDDs.desktop_id INNER JOIN
                      dbo.HDDs ON dbo.DesktopHDDs.hdd_id = dbo.HDDs.hdd_id INNER JOIN
                      dbo.Manufacturers AS Manufacturers_3 ON dbo.HDDs.manufacturer_id = Manufacturers_3.manufacturer_id INNER JOIN
                      dbo.DesktopRamModules ON dbo.Desktops.desktop_id = dbo.DesktopRamModules.desktop_id INNER JOIN
                      dbo.RamModules ON dbo.DesktopRamModules.ram_id = dbo.RamModules.ram_id INNER JOIN
                      dbo.Manufacturers AS Manufacturers_4 ON dbo.RamModules.manufacturer_id = Manufacturers_4.manufacturer_id
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
         Begin Table = "Desktops"
            Begin Extent = 
               Top = 187
               Left = 451
               Bottom = 365
               Right = 619
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Manufacturers"
            Begin Extent = 
               Top = 106
               Left = 234
               Bottom = 195
               Right = 402
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CPUs"
            Begin Extent = 
               Top = 204
               Left = 229
               Bottom = 386
               Right = 398
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Manufacturers_1"
            Begin Extent = 
               Top = 278
               Left = 2
               Bottom = 367
               Right = 170
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DesktopGPUs"
            Begin Extent = 
               Top = 275
               Left = 1008
               Bottom = 379
               Right = 1168
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GPUs"
            Begin Extent = 
               Top = 172
               Left = 1197
               Bottom = 386
               Right = 1374
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Manufacturers_2"
            Begin Extent = 
               Top = 285
               Left = 1406
               Bottom = 374
               Right = 1574
            End' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewDesktopsTest'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DesktopHDDs"
            Begin Extent = 
               Top = 0
               Left = 813
               Bottom = 104
               Right = 973
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "HDDs"
            Begin Extent = 
               Top = 0
               Left = 1018
               Bottom = 200
               Right = 1188
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Manufacturers_3"
            Begin Extent = 
               Top = 0
               Left = 1394
               Bottom = 89
               Right = 1562
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DesktopRamModules"
            Begin Extent = 
               Top = 7
               Left = 542
               Bottom = 111
               Right = 702
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RamModules"
            Begin Extent = 
               Top = 10
               Left = 308
               Bottom = 209
               Right = 478
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Manufacturers_4"
            Begin Extent = 
               Top = 6
               Left = 0
               Bottom = 95
               Right = 168
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
      Begin ColumnWidths = 29
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewDesktopsTest'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewDesktopsTest'
GO
/****** Object:  View [dbo].[ViewDesktopRamModules]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewDesktopRamModules]
AS
SELECT     dbo.Manufacturers.name, dbo.RamModules.model AS Expr1, dbo.RamModules.price, dbo.RamModules.bus_speed, dbo.RamModules.size, 
                      dbo.RamModules.ddr_version, dbo.RamModules.ram_form_factor, dbo.DesktopRamModules.quantity
FROM         dbo.DesktopRamModules INNER JOIN
                      dbo.Desktops ON dbo.DesktopRamModules.desktop_id = dbo.Desktops.desktop_id INNER JOIN
                      dbo.RamModules ON dbo.DesktopRamModules.ram_id = dbo.RamModules.ram_id INNER JOIN
                      dbo.Manufacturers ON dbo.RamModules.manufacturer_id = dbo.Manufacturers.manufacturer_id
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
         Configuration = "(H (1[50] 2[25] 3) )"
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
         Begin Table = "DesktopRamModules"
            Begin Extent = 
               Top = 6
               Left = 468
               Bottom = 200
               Right = 628
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Desktops"
            Begin Extent = 
               Top = 6
               Left = 89
               Bottom = 198
               Right = 257
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Manufacturers"
            Begin Extent = 
               Top = 66
               Left = 1128
               Bottom = 236
               Right = 1296
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RamModules"
            Begin Extent = 
               Top = 23
               Left = 865
               Bottom = 236
               Right = 1035
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
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
        ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewDesktopRamModules'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewDesktopRamModules'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewDesktopRamModules'
GO
/****** Object:  View [dbo].[ViewDesktopHDDs]    Script Date: 06/21/2013 17:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewDesktopHDDs]
AS
SELECT     dbo.Desktops.name AS Expr2, dbo.Manufacturers.name, dbo.HDDs.name AS Expr1, dbo.HDDs.price, dbo.HDDs.capacity, dbo.HDDs.hdd_type
FROM         dbo.DesktopHDDs INNER JOIN
                      dbo.Desktops ON dbo.DesktopHDDs.desktop_id = dbo.Desktops.desktop_id INNER JOIN
                      dbo.HDDs ON dbo.DesktopHDDs.hdd_id = dbo.HDDs.hdd_id INNER JOIN
                      dbo.Manufacturers ON dbo.HDDs.manufacturer_id = dbo.Manufacturers.manufacturer_id
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
         Begin Table = "DesktopHDDs"
            Begin Extent = 
               Top = 58
               Left = 377
               Bottom = 147
               Right = 537
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Desktops"
            Begin Extent = 
               Top = 84
               Left = 61
               Bottom = 252
               Right = 229
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "HDDs"
            Begin Extent = 
               Top = 58
               Left = 840
               Bottom = 251
               Right = 1008
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Manufacturers"
            Begin Extent = 
               Top = 242
               Left = 415
               Bottom = 331
               Right = 583
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
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewDesktopHDDs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'    End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewDesktopHDDs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewDesktopHDDs'
GO
/****** Object:  Check [CK_CPUs_ClockSpeed]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[CPUs]  WITH CHECK ADD  CONSTRAINT [CK_CPUs_ClockSpeed] CHECK  (([clock_speed]>=(1.00) AND [clock_speed]<=(4.00)))
GO
ALTER TABLE [dbo].[CPUs] CHECK CONSTRAINT [CK_CPUs_ClockSpeed]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([clock_speed]) >= 1.00 AND ([clock_speed]) <= 4.00' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'CONSTRAINT',@level2name=N'CK_CPUs_ClockSpeed'
GO
/****** Object:  Check [CK_CPUs_CPUFormFactor]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[CPUs]  WITH CHECK ADD  CONSTRAINT [CK_CPUs_CPUFormFactor] CHECK  (([cpu_form_factor]='Desktop' OR [cpu_form_factor]='Laptop'))
GO
ALTER TABLE [dbo].[CPUs] CHECK CONSTRAINT [CK_CPUs_CPUFormFactor]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([cpu_form_factor]=''Desktop'' OR [cpu_form_factor]=''Laptop'')' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'CONSTRAINT',@level2name=N'CK_CPUs_CPUFormFactor'
GO
/****** Object:  Check [CK_CPUs_NoOfCores]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[CPUs]  WITH CHECK ADD  CONSTRAINT [CK_CPUs_NoOfCores] CHECK  (([no_of_cores]='Single' OR [no_of_cores]='Dual' OR [no_of_cores]='Tri' OR [no_of_cores]='Quad' OR [no_of_cores]='Hex' OR [no_of_cores]='Octo'))
GO
ALTER TABLE [dbo].[CPUs] CHECK CONSTRAINT [CK_CPUs_NoOfCores]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint 
([no_of_cores]) = ''Single'' OR
([no_of_cores]) = ''Dual'' OR
([no_of_cores]) = ''Tri'' OR
([no_of_cores]) = ''Quad'' OR
([no_of_cores]) = ''Hex'' OR
([no_of_cores]) = ''Octo''' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'CONSTRAINT',@level2name=N'CK_CPUs_NoOfCores'
GO
/****** Object:  Check [CK_CPUs_Price]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[CPUs]  WITH CHECK ADD  CONSTRAINT [CK_CPUs_Price] CHECK  (([price]>=(0.00)))
GO
ALTER TABLE [dbo].[CPUs] CHECK CONSTRAINT [CK_CPUs_Price]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([price]) >= 0.00' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CPUs', @level2type=N'CONSTRAINT',@level2name=N'CK_CPUs_Price'
GO
/****** Object:  Check [CK_Desktops_Price]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Desktops]  WITH CHECK ADD  CONSTRAINT [CK_Desktops_Price] CHECK  (([price]>=(0.00)))
GO
ALTER TABLE [dbo].[Desktops] CHECK CONSTRAINT [CK_Desktops_Price]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([price] >= 0.00)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Desktops', @level2type=N'CONSTRAINT',@level2name=N'CK_Desktops_Price'
GO
/****** Object:  Check [CK_GPUs_GpuClockSpeed]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[GPUs]  WITH CHECK ADD  CONSTRAINT [CK_GPUs_GpuClockSpeed] CHECK  (([gpu_clock_speed]>=(100) AND [gpu_clock_speed]<=(2000)))
GO
ALTER TABLE [dbo].[GPUs] CHECK CONSTRAINT [CK_GPUs_GpuClockSpeed]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([gpu_clock_speed]>=(100) AND [gpu_clock_speed]<=(2000))' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'CONSTRAINT',@level2name=N'CK_GPUs_GpuClockSpeed'
GO
/****** Object:  Check [CK_GPUs_GPUType]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[GPUs]  WITH CHECK ADD  CONSTRAINT [CK_GPUs_GPUType] CHECK  (([gpu_type]='Dedicated' OR [gpu_type]='Onboard'))
GO
ALTER TABLE [dbo].[GPUs] CHECK CONSTRAINT [CK_GPUs_GPUType]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([gpu_type]) = ''Dedicated'' OR
([gpu_type]) = ''Onboard''' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'CONSTRAINT',@level2name=N'CK_GPUs_GPUType'
GO
/****** Object:  Check [CK_GPUs_Price]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[GPUs]  WITH CHECK ADD  CONSTRAINT [CK_GPUs_Price] CHECK  (([price]>=(0.00)))
GO
ALTER TABLE [dbo].[GPUs] CHECK CONSTRAINT [CK_GPUs_Price]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([price]) >= 0.00' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'CONSTRAINT',@level2name=N'CK_GPUs_Price'
GO
/****** Object:  Check [CK_GPUs_VRamClockSpeed]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[GPUs]  WITH CHECK ADD  CONSTRAINT [CK_GPUs_VRamClockSpeed] CHECK  (([vram_clock_speed]>=(100) AND [vram_clock_speed]<=(5000)))
GO
ALTER TABLE [dbo].[GPUs] CHECK CONSTRAINT [CK_GPUs_VRamClockSpeed]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([vram_clock_speed]>=(100) AND [vram_clock_speed]<=(5000))' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'CONSTRAINT',@level2name=N'CK_GPUs_VRamClockSpeed'
GO
/****** Object:  Check [CK_GPUs_VRamSize]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[GPUs]  WITH CHECK ADD  CONSTRAINT [CK_GPUs_VRamSize] CHECK  (([vram_size]>=(1) AND [vram_size]<=(4096)))
GO
ALTER TABLE [dbo].[GPUs] CHECK CONSTRAINT [CK_GPUs_VRamSize]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([vram_size]>=(1) AND [vram_size]<=(4096))' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'CONSTRAINT',@level2name=N'CK_GPUs_VRamSize'
GO
/****** Object:  Check [CK_GPUs_VRamType]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[GPUs]  WITH CHECK ADD  CONSTRAINT [CK_GPUs_VRamType] CHECK  (([vram_type]='GDDR1' OR [vram_type]='GDDR2' OR [vram_type]='GDDR3' OR [vram_type]='GDDR4' OR [vram_type]='GDDR5'))
GO
ALTER TABLE [dbo].[GPUs] CHECK CONSTRAINT [CK_GPUs_VRamType]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([vram_type]=''GDDR1'' OR [vram_type]=''GDDR2'' OR [vram_type]=''GDDR3'' OR [vram_type]=''GDDR4'' OR [vram_type]=''GDDR5'')' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GPUs', @level2type=N'CONSTRAINT',@level2name=N'CK_GPUs_VRamType'
GO
/****** Object:  Check [CK_HDDs_Capacity]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[HDDs]  WITH CHECK ADD  CONSTRAINT [CK_HDDs_Capacity] CHECK  (([capacity]='16' OR [capacity]='30' OR [capacity]='32' OR [capacity]='40' OR [capacity]='60' OR [capacity]='64' OR [capacity]='120' OR [capacity]='128' OR [capacity]='250' OR [capacity]='256' OR [capacity]='500' OR [capacity]='512' OR [capacity]='750' OR [capacity]='1024' OR [capacity]='2048'))
GO
ALTER TABLE [dbo].[HDDs] CHECK CONSTRAINT [CK_HDDs_Capacity]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([capacity]) = ''16'' OR
([capacity]) = ''30'' OR
([capacity]) = ''32'' OR
([capacity]) = ''40'' OR
([capacity]) = ''60'' OR
([capacity]) = ''64'' OR
([capacity]) = ''120'' OR
([capacity]) = ''128'' OR
([capacity]) = ''250'' OR
([capacity]) = ''256'' OR
([capacity]) = ''500'' OR
([capacity]) = ''512'' OR
([capacity]) = ''750'' OR
([capacity]) = ''1024'' OR
([capacity]) = ''2048''' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'CONSTRAINT',@level2name=N'CK_HDDs_Capacity'
GO
/****** Object:  Check [CK_HDDs_HDDFormFactor]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[HDDs]  WITH CHECK ADD  CONSTRAINT [CK_HDDs_HDDFormFactor] CHECK  (([hdd_form_factor]='DESKTOP' OR [hdd_form_factor]='LAPTOP'))
GO
ALTER TABLE [dbo].[HDDs] CHECK CONSTRAINT [CK_HDDs_HDDFormFactor]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([hdd_form_factor]=''DESKTOP'' OR [hdd_form_factor]=''LAPTOP'')' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'CONSTRAINT',@level2name=N'CK_HDDs_HDDFormFactor'
GO
/****** Object:  Check [CK_HDDs_HDDInterface]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[HDDs]  WITH CHECK ADD  CONSTRAINT [CK_HDDs_HDDInterface] CHECK  (([hdd_interface]='SATA' OR [hdd_interface]='IDE'))
GO
ALTER TABLE [dbo].[HDDs] CHECK CONSTRAINT [CK_HDDs_HDDInterface]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([hdd_interface] = ''SATA'' OR [hdd_interface] = ''IDE'')' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'CONSTRAINT',@level2name=N'CK_HDDs_HDDInterface'
GO
/****** Object:  Check [CK_HDDs_HDDType]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[HDDs]  WITH CHECK ADD  CONSTRAINT [CK_HDDs_HDDType] CHECK  (([hdd_type]='Mechanical' OR [hdd_type]='SSD'))
GO
ALTER TABLE [dbo].[HDDs] CHECK CONSTRAINT [CK_HDDs_HDDType]
GO
/****** Object:  Check [CK_HDDs_Price]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[HDDs]  WITH CHECK ADD  CONSTRAINT [CK_HDDs_Price] CHECK  (([price]>=(0.00)))
GO
ALTER TABLE [dbo].[HDDs] CHECK CONSTRAINT [CK_HDDs_Price]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([price]) >= 0.00' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'CONSTRAINT',@level2name=N'CK_HDDs_Price'
GO
/****** Object:  Check [CK_HDDs_Speed]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[HDDs]  WITH CHECK ADD  CONSTRAINT [CK_HDDs_Speed] CHECK  (([speed]=(3) OR [speed]=(6)))
GO
ALTER TABLE [dbo].[HDDs] CHECK CONSTRAINT [CK_HDDs_Speed]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([speed] = 3 OR [speed] = 6)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HDDs', @level2type=N'CONSTRAINT',@level2name=N'CK_HDDs_Speed'
GO
/****** Object:  Check [CK_Laptops_BatteryLife]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Laptops]  WITH CHECK ADD  CONSTRAINT [CK_Laptops_BatteryLife] CHECK  (([battery_life]>=(0)))
GO
ALTER TABLE [dbo].[Laptops] CHECK CONSTRAINT [CK_Laptops_BatteryLife]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([battery_life] >= 0)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laptops', @level2type=N'CONSTRAINT',@level2name=N'CK_Laptops_BatteryLife'
GO
/****** Object:  Check [CK_Laptops_DisplaySize]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Laptops]  WITH CHECK ADD  CONSTRAINT [CK_Laptops_DisplaySize] CHECK  (([display_size]='15.6' OR [display_size]='17.0'))
GO
ALTER TABLE [dbo].[Laptops] CHECK CONSTRAINT [CK_Laptops_DisplaySize]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([display_size] = ''15.6'' OR
[display_size] = ''17.0'')' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laptops', @level2type=N'CONSTRAINT',@level2name=N'CK_Laptops_DisplaySize'
GO
/****** Object:  Check [CK_Laptops_Price]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Laptops]  WITH CHECK ADD  CONSTRAINT [CK_Laptops_Price] CHECK  (([price]>=(0.00)))
GO
ALTER TABLE [dbo].[Laptops] CHECK CONSTRAINT [CK_Laptops_Price]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([price] >= 0.00)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laptops', @level2type=N'CONSTRAINT',@level2name=N'CK_Laptops_Price'
GO
/****** Object:  Check [CK_Laptops_Weight]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Laptops]  WITH CHECK ADD  CONSTRAINT [CK_Laptops_Weight] CHECK  (([weight]>=(0)))
GO
ALTER TABLE [dbo].[Laptops] CHECK CONSTRAINT [CK_Laptops_Weight]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([weight] >= 0)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laptops', @level2type=N'CONSTRAINT',@level2name=N'CK_Laptops_Weight'
GO
/****** Object:  Check [CK_Monitors_Price]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD  CONSTRAINT [CK_Monitors_Price] CHECK  (([price]>=(0.00)))
GO
ALTER TABLE [dbo].[Monitors] CHECK CONSTRAINT [CK_Monitors_Price]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([price]) >= 0.00' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitors', @level2type=N'CONSTRAINT',@level2name=N'CK_Monitors_Price'
GO
/****** Object:  Check [CK_Monitors_Size]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD  CONSTRAINT [CK_Monitors_Size] CHECK  (([size]='15' OR [size]='17' OR [size]='20' OR [size]='22' OR [size]='24' OR [size]='27' OR [size]='30'))
GO
ALTER TABLE [dbo].[Monitors] CHECK CONSTRAINT [CK_Monitors_Size]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([size]) = ''15'' OR
([size]) = ''17'' OR
([size]) = ''20'' OR
([size]) = ''22'' OR
([size]) = ''24'' OR
([size]) = ''27'' OR
([size]) = ''30''' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitors', @level2type=N'CONSTRAINT',@level2name=N'CK_Monitors_Size'
GO
/****** Object:  Check [CK_RamModules_BusSpeed]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[RamModules]  WITH CHECK ADD  CONSTRAINT [CK_RamModules_BusSpeed] CHECK  (([bus_speed]='200' OR [bus_speed]='333' OR [bus_speed]='400' OR [bus_speed]='533' OR [bus_speed]='667' OR [bus_speed]='800' OR [bus_speed]='1066' OR [bus_speed]='1333' OR [bus_speed]='1600' OR [bus_speed]='1866' OR [bus_speed]='2000' OR [bus_speed]='2133' OR [bus_speed]='2400' OR [bus_speed]='2666'))
GO
ALTER TABLE [dbo].[RamModules] CHECK CONSTRAINT [CK_RamModules_BusSpeed]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([bus_speed]=''200'' OR 
[bus_speed]=''333'' OR 
[bus_speed]=''400'' OR 
[bus_speed]=''533'' OR 
[bus_speed]=''667'' OR 
[bus_speed]=''800'' OR 
[bus_speed]=''1066'' OR 
[bus_speed]=''1333'' OR 
[bus_speed]=''1600'' OR 
[bus_speed]=''1866'' OR 
[bus_speed]=''2000'' OR 
[bus_speed]=''2133'' OR 
[bus_speed]=''2400'' OR 
[bus_speed]=''2666'')' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'CONSTRAINT',@level2name=N'CK_RamModules_BusSpeed'
GO
/****** Object:  Check [CK_RamModules_DDRVersion]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[RamModules]  WITH CHECK ADD  CONSTRAINT [CK_RamModules_DDRVersion] CHECK  (([ddr_version]='DDR' OR [ddr_version]='DDR2' OR [ddr_version]='DDR3'))
GO
ALTER TABLE [dbo].[RamModules] CHECK CONSTRAINT [CK_RamModules_DDRVersion]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([ddr_version]=''DDR'' OR 
[ddr_version]=''DDR2'' OR 
[ddr_version]=''DDR3'')' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'CONSTRAINT',@level2name=N'CK_RamModules_DDRVersion'
GO
/****** Object:  Check [CK_RamModules_Price]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[RamModules]  WITH CHECK ADD  CONSTRAINT [CK_RamModules_Price] CHECK  (([price]>=(0.00)))
GO
ALTER TABLE [dbo].[RamModules] CHECK CONSTRAINT [CK_RamModules_Price]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint ([price]) >= 0.00' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'CONSTRAINT',@level2name=N'CK_RamModules_Price'
GO
/****** Object:  Check [CK_RamModules_RamFormFactor]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[RamModules]  WITH CHECK ADD  CONSTRAINT [CK_RamModules_RamFormFactor] CHECK  (([ram_form_factor]='DIMM' OR [ram_form_factor]='SODIMM'))
GO
ALTER TABLE [dbo].[RamModules] CHECK CONSTRAINT [CK_RamModules_RamFormFactor]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([ram_form_factor]=''DIMM'' OR [ram_form_factor]=''SODIMM'')' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'CONSTRAINT',@level2name=N'CK_RamModules_RamFormFactor'
GO
/****** Object:  Check [CK_RamModules_Size]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[RamModules]  WITH CHECK ADD  CONSTRAINT [CK_RamModules_Size] CHECK  (([size]='64' OR [size]='128' OR [size]='256' OR [size]='512' OR [size]='1024' OR [size]='2048' OR [size]='4096' OR [size]='8192'))
GO
ALTER TABLE [dbo].[RamModules] CHECK CONSTRAINT [CK_RamModules_Size]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Check Constraint
([size]) = ''64'' OR
([size]) = ''128'' OR
([size]) = ''256'' OR
([size]) = ''512'' OR
([size]) = ''1024'' OR
([size]) = ''2048'' OR
([size]) = ''4096'' OR
([size]) = ''8192''' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RamModules', @level2type=N'CONSTRAINT',@level2name=N'CK_RamModules_Size'
GO
/****** Object:  ForeignKey [FK_CPUs_Manufacturers]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[CPUs]  WITH CHECK ADD  CONSTRAINT [FK_CPUs_Manufacturers] FOREIGN KEY([manufacturer_id])
REFERENCES [dbo].[Manufacturers] ([manufacturer_id])
GO
ALTER TABLE [dbo].[CPUs] CHECK CONSTRAINT [FK_CPUs_Manufacturers]
GO
/****** Object:  ForeignKey [FK_DesktopGPUs_Desktops1]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[DesktopGPUs]  WITH CHECK ADD  CONSTRAINT [FK_DesktopGPUs_Desktops1] FOREIGN KEY([desktop_id])
REFERENCES [dbo].[Desktops] ([desktop_id])
GO
ALTER TABLE [dbo].[DesktopGPUs] CHECK CONSTRAINT [FK_DesktopGPUs_Desktops1]
GO
/****** Object:  ForeignKey [FK_DesktopGPUs_GPUs1]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[DesktopGPUs]  WITH CHECK ADD  CONSTRAINT [FK_DesktopGPUs_GPUs1] FOREIGN KEY([gpu_id])
REFERENCES [dbo].[GPUs] ([gpu_id])
GO
ALTER TABLE [dbo].[DesktopGPUs] CHECK CONSTRAINT [FK_DesktopGPUs_GPUs1]
GO
/****** Object:  ForeignKey [FK_DesktopHDDs_Desktops1]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[DesktopHDDs]  WITH CHECK ADD  CONSTRAINT [FK_DesktopHDDs_Desktops1] FOREIGN KEY([desktop_id])
REFERENCES [dbo].[Desktops] ([desktop_id])
GO
ALTER TABLE [dbo].[DesktopHDDs] CHECK CONSTRAINT [FK_DesktopHDDs_Desktops1]
GO
/****** Object:  ForeignKey [FK_DesktopHDDs_HDDs1]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[DesktopHDDs]  WITH CHECK ADD  CONSTRAINT [FK_DesktopHDDs_HDDs1] FOREIGN KEY([hdd_id])
REFERENCES [dbo].[HDDs] ([hdd_id])
GO
ALTER TABLE [dbo].[DesktopHDDs] CHECK CONSTRAINT [FK_DesktopHDDs_HDDs1]
GO
/****** Object:  ForeignKey [FK_DesktopRamModules_Desktop]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[DesktopRamModules]  WITH CHECK ADD  CONSTRAINT [FK_DesktopRamModules_Desktop] FOREIGN KEY([desktop_id])
REFERENCES [dbo].[Desktops] ([desktop_id])
GO
ALTER TABLE [dbo].[DesktopRamModules] CHECK CONSTRAINT [FK_DesktopRamModules_Desktop]
GO
/****** Object:  ForeignKey [FK_DesktopRamModules_RamModules]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[DesktopRamModules]  WITH CHECK ADD  CONSTRAINT [FK_DesktopRamModules_RamModules] FOREIGN KEY([ram_id])
REFERENCES [dbo].[RamModules] ([ram_id])
GO
ALTER TABLE [dbo].[DesktopRamModules] CHECK CONSTRAINT [FK_DesktopRamModules_RamModules]
GO
/****** Object:  ForeignKey [FK_Desktop_CPU]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Desktops]  WITH CHECK ADD  CONSTRAINT [FK_Desktop_CPU] FOREIGN KEY([cpu_id])
REFERENCES [dbo].[CPUs] ([cpu_id])
GO
ALTER TABLE [dbo].[Desktops] CHECK CONSTRAINT [FK_Desktop_CPU]
GO
/****** Object:  ForeignKey [FK_Desktop_Monitor]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Desktops]  WITH CHECK ADD  CONSTRAINT [FK_Desktop_Monitor] FOREIGN KEY([monitor_id])
REFERENCES [dbo].[Monitors] ([monitor_id])
GO
ALTER TABLE [dbo].[Desktops] CHECK CONSTRAINT [FK_Desktop_Monitor]
GO
/****** Object:  ForeignKey [FK_Desktops_Manufacturers]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Desktops]  WITH CHECK ADD  CONSTRAINT [FK_Desktops_Manufacturers] FOREIGN KEY([manufacturer_id])
REFERENCES [dbo].[Manufacturers] ([manufacturer_id])
GO
ALTER TABLE [dbo].[Desktops] CHECK CONSTRAINT [FK_Desktops_Manufacturers]
GO
/****** Object:  ForeignKey [FK_GPUs_Manufacturers]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[GPUs]  WITH CHECK ADD  CONSTRAINT [FK_GPUs_Manufacturers] FOREIGN KEY([manufacturer_id])
REFERENCES [dbo].[Manufacturers] ([manufacturer_id])
GO
ALTER TABLE [dbo].[GPUs] CHECK CONSTRAINT [FK_GPUs_Manufacturers]
GO
/****** Object:  ForeignKey [FK_HDDs_Manufacturers]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[HDDs]  WITH CHECK ADD  CONSTRAINT [FK_HDDs_Manufacturers] FOREIGN KEY([manufacturer_id])
REFERENCES [dbo].[Manufacturers] ([manufacturer_id])
GO
ALTER TABLE [dbo].[HDDs] CHECK CONSTRAINT [FK_HDDs_Manufacturers]
GO
/****** Object:  ForeignKey [FK_LaptopRamModules_Laptops]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[LaptopRamModules]  WITH CHECK ADD  CONSTRAINT [FK_LaptopRamModules_Laptops] FOREIGN KEY([laptop_id])
REFERENCES [dbo].[Laptops] ([laptop_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LaptopRamModules] CHECK CONSTRAINT [FK_LaptopRamModules_Laptops]
GO
/****** Object:  ForeignKey [FK_LaptopRamModules_RamModules]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[LaptopRamModules]  WITH CHECK ADD  CONSTRAINT [FK_LaptopRamModules_RamModules] FOREIGN KEY([ram_id])
REFERENCES [dbo].[RamModules] ([ram_id])
GO
ALTER TABLE [dbo].[LaptopRamModules] CHECK CONSTRAINT [FK_LaptopRamModules_RamModules]
GO
/****** Object:  ForeignKey [FK_Laptop_CPU]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Laptops]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_CPU] FOREIGN KEY([cpu_id])
REFERENCES [dbo].[CPUs] ([cpu_id])
GO
ALTER TABLE [dbo].[Laptops] CHECK CONSTRAINT [FK_Laptop_CPU]
GO
/****** Object:  ForeignKey [FK_Laptop_GPU]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Laptops]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_GPU] FOREIGN KEY([gpu_id])
REFERENCES [dbo].[GPUs] ([gpu_id])
GO
ALTER TABLE [dbo].[Laptops] CHECK CONSTRAINT [FK_Laptop_GPU]
GO
/****** Object:  ForeignKey [FK_Laptop_HDD]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Laptops]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_HDD] FOREIGN KEY([hdd_id])
REFERENCES [dbo].[HDDs] ([hdd_id])
GO
ALTER TABLE [dbo].[Laptops] CHECK CONSTRAINT [FK_Laptop_HDD]
GO
/****** Object:  ForeignKey [FK_Laptops_Manufacturers]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Laptops]  WITH CHECK ADD  CONSTRAINT [FK_Laptops_Manufacturers] FOREIGN KEY([manufacturer_id])
REFERENCES [dbo].[Manufacturers] ([manufacturer_id])
GO
ALTER TABLE [dbo].[Laptops] CHECK CONSTRAINT [FK_Laptops_Manufacturers]
GO
/****** Object:  ForeignKey [FK_Monitors_Manufacturers]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD  CONSTRAINT [FK_Monitors_Manufacturers] FOREIGN KEY([manufacturer_id])
REFERENCES [dbo].[Manufacturers] ([manufacturer_id])
GO
ALTER TABLE [dbo].[Monitors] CHECK CONSTRAINT [FK_Monitors_Manufacturers]
GO
/****** Object:  ForeignKey [FK_RamModules_Manufacturers]    Script Date: 06/21/2013 17:51:37 ******/
ALTER TABLE [dbo].[RamModules]  WITH CHECK ADD  CONSTRAINT [FK_RamModules_Manufacturers] FOREIGN KEY([manufacturer_id])
REFERENCES [dbo].[Manufacturers] ([manufacturer_id])
GO
ALTER TABLE [dbo].[RamModules] CHECK CONSTRAINT [FK_RamModules_Manufacturers]
GO
