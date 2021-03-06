USE [ShoeManagement]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/6/2021 3:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](10) NOT NULL,
	[IDNumber] [nvarchar](11) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order]    Script Date: 11/6/2021 3:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Total] [money] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 11/6/2021 3:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Total] [money] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_ORDER_DETAIL] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/6/2021 3:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[QuantityInStock] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/6/2021 3:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 11/6/2021 3:58:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Username] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[FullName] [nvarchar](40) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerID], [FullName], [Phone], [IDNumber]) VALUES (1, N'Tran Van A12', N'090011111', N'000000001')
INSERT [dbo].[Customer] ([CustomerID], [FullName], [Phone], [IDNumber]) VALUES (4, N'Nguyen Thi B', N'090011112', N'000000002')
INSERT [dbo].[Customer] ([CustomerID], [FullName], [Phone], [IDNumber]) VALUES (5, N'Vo VaN C', N'090011113', N'000000003')
INSERT [dbo].[Customer] ([CustomerID], [FullName], [Phone], [IDNumber]) VALUES (6, N'Phung D', N'090011114', N'000000004')
INSERT [dbo].[Customer] ([CustomerID], [FullName], [Phone], [IDNumber]) VALUES (7, N'Lai Van E', N'090011115', N'000000005')
INSERT [dbo].[Customer] ([CustomerID], [FullName], [Phone], [IDNumber]) VALUES (12, N'Dinh Thinh', N'0944209261', N'1')
INSERT [dbo].[Customer] ([CustomerID], [FullName], [Phone], [IDNumber]) VALUES (13, N'2', N'1', N'2')
INSERT [dbo].[Customer] ([CustomerID], [FullName], [Phone], [IDNumber]) VALUES (14, N'Hahah', N'123', N'123')
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderID], [CustomerID], [UserID], [Total], [OrderDate], [Status]) VALUES (3, 1, 3, 10000.0000, CAST(N'2021-11-06 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Order] ([OrderID], [CustomerID], [UserID], [Total], [OrderDate], [Status]) VALUES (13, 4, 3, 20000.0000, CAST(N'2021-11-06 01:00:00.000' AS DateTime), 1)
INSERT [dbo].[Order] ([OrderID], [CustomerID], [UserID], [Total], [OrderDate], [Status]) VALUES (14, 5, 3, 30000.0000, CAST(N'2021-11-06 02:00:00.000' AS DateTime), 1)
INSERT [dbo].[Order] ([OrderID], [CustomerID], [UserID], [Total], [OrderDate], [Status]) VALUES (15, 6, 3, 40000.0000, CAST(N'2021-11-06 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Order] ([OrderID], [CustomerID], [UserID], [Total], [OrderDate], [Status]) VALUES (20, 6, 3, 500.0000, CAST(N'2021-11-06 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Order] ([OrderID], [CustomerID], [UserID], [Total], [OrderDate], [Status]) VALUES (21, 6, 3, 10000.0000, CAST(N'2021-11-06 00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Order] ([OrderID], [CustomerID], [UserID], [Total], [OrderDate], [Status]) VALUES (22, 1, 3, 1.0000, CAST(N'2021-11-06 15:02:07.687' AS DateTime), 1)
INSERT [dbo].[Order] ([OrderID], [CustomerID], [UserID], [Total], [OrderDate], [Status]) VALUES (23, 1, 3, 12.0000, CAST(N'2021-11-06 15:13:04.990' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Order] OFF
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity], [Price], [Total], [Status]) VALUES (3, 1, 1, 1.0000, 1.0000, 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity], [Price], [Total], [Status]) VALUES (3, 2, 1, 1.0000, 1.0000, 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity], [Price], [Total], [Status]) VALUES (13, 1, 1, 11.0000, 1.0000, 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity], [Price], [Total], [Status]) VALUES (14, 2, 3, 11.0000, 13.0000, 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity], [Price], [Total], [Status]) VALUES (23, 1, 4, 1.0000, 4.0000, 1)
INSERT [dbo].[OrderDetail] ([OrderID], [ProductID], [Quantity], [Price], [Total], [Status]) VALUES (23, 2, 4, 2.0000, 8.0000, 1)
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [ProductName], [QuantityInStock], [Price], [Status]) VALUES (1, N'Name 1', 10, 1.0000, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [QuantityInStock], [Price], [Status]) VALUES (2, N'Name 2', 3, 2.0000, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [QuantityInStock], [Price], [Status]) VALUES (3, N'Name 3', 3, 3.0000, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [QuantityInStock], [Price], [Status]) VALUES (4, N'Name 4', 4, 4.0000, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [QuantityInStock], [Price], [Status]) VALUES (5, N'Name 5', 5, 5.0000, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [RoleName], [Status]) VALUES (1, N'Admin', 1)
INSERT [dbo].[Role] ([RoleID], [RoleName], [Status]) VALUES (2, N'Manager', 1)
INSERT [dbo].[Role] ([RoleID], [RoleName], [Status]) VALUES (3, N'Staff', 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [RoleId], [Username], [Password], [FullName], [Status]) VALUES (1, 1, N'admin123', N'admin123', N'Tai Khoan Admin', 1)
INSERT [dbo].[User] ([UserID], [RoleId], [Username], [Password], [FullName], [Status]) VALUES (2, 2, N'manager123', N'manager123', N'Tai Khoan Manager', 1)
INSERT [dbo].[User] ([UserID], [RoleId], [Username], [Password], [FullName], [Status]) VALUES (3, 3, N'staff123', N'staff123', N'Tai Khoan Staff', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Customer__564DB08A1BC154C6]    Script Date: 11/6/2021 3:58:30 PM ******/
ALTER TABLE [dbo].[Customer] ADD UNIQUE NONCLUSTERED 
(
	[IDNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UNIQUE_Product]    Script Date: 11/6/2021 3:58:30 PM ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [UNIQUE_Product] UNIQUE NONCLUSTERED 
(
	[ProductName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UNIQUE_USER]    Script Date: 11/6/2021 3:58:30 PM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UNIQUE_USER] UNIQUE NONCLUSTERED 
(
	[Username] ASC,
	[FullName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
