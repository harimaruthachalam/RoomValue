USE [RoomValue]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 04/24/2015 19:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Login](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 04/24/2015 19:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserDetails](
	[ID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Expenditure]    Script Date: 04/24/2015 19:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Expenditure](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Item] [varchar](100) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaidBy] [int] NOT NULL,
	[PaidTo] [int] NOT NULL,
	[PayType] [varchar](5) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[BalanceAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Expenditure] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 04/24/2015 19:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Admin](
	[Password] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[AddMember]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[AddMember]
(
@Name varchar(50),
@Mail varchar(50),
@Phone varchar(50),
@Password varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @ID int;

    -- Insert statements for procedure here
    insert into dbo.Login(Password) values(@Password);
    select @ID= MAX(ID) from dbo.Login;
    insert into dbo.UserDetails(ID,Name,Email,Phone) values (@ID,@Name,@Mail,@Phone);
    select @ID;
END
GO
/****** Object:  StoredProcedure [dbo].[AcceptPayment]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[AcceptPayment]
	(
	@ExpenditureID int,
	@ID int
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    declare @IsAuthorized int
    select @IsAuthorized = ID from dbo.Expenditure where PaidBy=@ID and ID =@ExpenditureID;
    if(@IsAuthorized=@ExpenditureID)
    begin
    declare @BalAmount decimal(18,2)
    select @BalAmount = BalanceAmount from dbo.Expenditure where ID =@ExpenditureID;
    if(@BalAmount=0.0)
    begin
    update dbo.Expenditure set Amount=0, Status='CLOSED' where ID=@ExpenditureID;
    end
    else
    begin
    update dbo.Expenditure set Amount=BalanceAmount, Status='PENDING' where ID=@ExpenditureID;
    end
    select '1';
    end
    else
    begin
    select '0';
    end
END
GO
/****** Object:  StoredProcedure [dbo].[CombinedPayment]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[CombinedPayment]
	(
	@PayerID int,
	@PayeeID int
	)
AS
	begin
	update dbo.Expenditure set status='SUBMITTED',BalanceAmount=0 where PaidBy=@PayeeID and PaidTo=@PayerID and status='PENDING';
	end
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[ChangePassword]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[ChangePassword]
	(
	@ID int,
	@OldPassword varchar(50),
	@NewPassword varchar(50)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    declare @IsAuthorized varchar(50)
    select @IsAuthorized = Password from dbo.Login where ID =@ID;
    if(@IsAuthorized=@OldPassword)
    begin
    update dbo.Login set Password=@NewPassword where ID=@ID;
    select '1';
    end
    else
    begin
    select '0';
    end
END
GO
/****** Object:  StoredProcedure [dbo].[ChangeAdminPassword]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[ChangeAdminPassword]
	(
	@OldPassword varchar(50),
	@NewPassword varchar(50)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    declare @IsAuthorized varchar(50)
    select @IsAuthorized = Password from dbo.Admin;
    if(@IsAuthorized=@OldPassword)
    begin
    update dbo.Admin set Password=@NewPassword;
    select '1';
    end
    else
    begin
    select '0';
    end
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDetails]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UpdateUserDetails]
	(
	@ID int,
	@Mail varchar(50),
	@Phone varchar(50)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    update dbo.UserDetails set Email=@Mail, Phone=@Phone where ID=@ID;
END
GO
/****** Object:  StoredProcedure [dbo].[SinglePayment]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SinglePayment]
	(
	@ExpenditureID int,
	@Amount decimal(18,2)
	)
AS
	begin
	declare @CurrentAmount decimal(18,2)
	select @CurrentAmount = Amount from Expenditure where ID=@ExpenditureID;
	set @CurrentAmount=@CurrentAmount-@Amount;
	update Expenditure set status='SUBMITTED', BalanceAmount=@CurrentAmount where ID=@ExpenditureID;
	end
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[RemoveMember]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveMember]
		(
		@ID int
		)
AS
begin
declare @Validation int
select @Validation=ID from dbo.Expenditure where (PaidBy=@ID or PaidTo=@ID) and Status='PENDING';

if(@Validation>0)
begin
select '0'
end
else
begin
delete from dbo.Login where ID=@ID;
delete from dbo.UserDetails where ID=@ID;
select '1'
end
end
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[RejectPayment]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RejectPayment]
	(
	@ExpenditureID int,
	@ID int
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    declare @IsAuthorized int
    select @IsAuthorized = ID from dbo.Expenditure where PaidBy=@ID and ID =@ExpenditureID;
    if(@IsAuthorized=@ExpenditureID)
    begin
    update dbo.Expenditure set BalanceAmount=Amount, Status='PENDING' where ID=@ExpenditureID;
    select '1';
    end
    else
    begin
    select '0';
    end
END
GO
/****** Object:  StoredProcedure [dbo].[MembersIHaveToPay]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[MembersIHaveToPay]
	(
	@ID int
	)
AS
	begin
	select distinct(PaidBy) from dbo.Expenditure where PaidTo =@ID and Status='PENDING';
	end
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetUserDetails]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetUserDetails]
	(
	@ID int
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    select Email,Phone from dbo.UserDetails where ID=@ID;
END
GO
/****** Object:  StoredProcedure [dbo].[GetPassword]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetPassword]
	(
	@ID int
	)
AS
begin
select Password from dbo.Login where ID =@ID;
end
GO
/****** Object:  StoredProcedure [dbo].[GetNameFromID]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetNameFromID]
	(
	@ID int
	)
AS
begin
select Name from dbo.UserDetails where ID =@ID;
end
GO
/****** Object:  StoredProcedure [dbo].[GetMembersCount]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetMembersCount]
AS
begin
select count(ID) from dbo.UserDetails;
end
GO
/****** Object:  StoredProcedure [dbo].[GetMembers]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetMembers]
	(
	@ID int
	)
AS
begin
select ID,Name from dbo.UserDetails where id != @ID;
end
GO
/****** Object:  StoredProcedure [dbo].[GetHaveToPayList]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetHaveToPayList]
	(
	@PayerID int,
	@PayeeID int
	)
AS
	begin
	select ID, Item, Amount from dbo.Expenditure where PaidBy=@PayeeID and PaidTo=@PayerID and status='PENDING';
	end
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetGotPaidList]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetGotPaidList]
	(
	@ID int
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    select A.ID,A.Item,A.Amount,A.PaidAmount,B.Name from
	(SELECT ID,Item,Amount,(Amount-BalanceAmount) as PaidAmount,PaidTo from dbo.Expenditure where Status='SUBMITTED' and PaidBy=@ID) A
	join 
	(select ID,Name from UserDetails) B
	on A.PaidTo = B.ID;
END
GO
/****** Object:  StoredProcedure [dbo].[GetExpenditureDetails]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetExpenditureDetails]
	(
	@ID int
	)
AS

SELECT [Date]
      ,[Item]
      ,[Amount]
      ,[PaidBy]
      ,[PaidTo]
      ,[PayType]
      ,[Status]
      ,[BalanceAmount]
  FROM [dbo].[Expenditure] where (PaidBy=@ID or PaidTo=@ID) and Status!='CLOSED';
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetAllMembersFullDetails]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllMembersFullDetails]
AS
select A.ID, B.Name,B.Email,B.Phone from
	(select ID from dbo.Login) A join (select ID, Name,Phone,Email from dbo.UserDetails) B on A.ID=B.ID;
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetAllMembersDetails]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAllMembersDetails]
AS
select A.ID, B.Name from
	(select ID from dbo.Login) A join (select ID, Name from dbo.UserDetails) B on A.ID=B.ID
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetAllMembers]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetAllMembers]
AS
	select ID from dbo.Login;
	RETURN
GO
/****** Object:  StoredProcedure [dbo].[GetAdminPassword]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetAdminPassword]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    select password from dbo.Admin;
END
GO
/****** Object:  StoredProcedure [dbo].[ExpenditureMade]    Script Date: 04/24/2015 19:03:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ExpenditureMade]
	(
	@Item varchar(100),
	@Amount decimal(18,2),
	@PaidBy int,
	@PaidTo int,
	@PayType varchar(5)
	)
AS
begin
	insert into dbo.Expenditure (Date,Item,Amount,PaidBy,PaidTo,PayType,Status,BalanceAmount ) values 
	(getdate(), @Item,@Amount,@PaidBy,@PaidTo,@PayType,'PENDING',@Amount);
	end
		RETURN
GO
