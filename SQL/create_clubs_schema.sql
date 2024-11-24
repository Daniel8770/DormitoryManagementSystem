create schema [Clubs]

go




IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('Clubs.BookableResource') AND type = 'U')
begin
	create table [Clubs].[BookableResource](
		[Id] uniqueidentifier not null primary key,
		[Name] nvarchar(100) not null,
		[OpenDate] datetime2 not null,
		[EndDate] datetime2 not null,
		[Rules] nvarchar(max) not null,
		[MaxBookingsPerMember] int null
	)
end;

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('Clubs.Unit') AND type = 'U')
begin
	create table [Clubs].[Unit](
		[Id] int not null,
		[BookableResourceId] uniqueidentifier not null
			foreign key references [Clubs].[BookableResource](Id)
			on delete cascade,
		[Name] nvarchar(100) not null,
		CONSTRAINT [UnitPK] PRIMARY KEY ([Id], [BookableResourceId])
	)
end;

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('Clubs.Member') AND type = 'U')
begin
	create table [Clubs].[Member](
		[Id] uniqueidentifier not null primary key
	) 
end;

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('Clubs.Booking') AND type = 'U')
begin
	create table [Clubs].[Booking](
		[Id] int not null,
		[BookableResourceId] uniqueidentifier not null
			foreign key references [Clubs].[BookableResource](Id)
			on delete cascade,
		[MemberId] uniqueidentifier not null
			foreign key references [Clubs].[Member](Id)
			on delete cascade,
		[UnitId] int not null,
		[DateBooked] datetime2 not null,
		[StartDate] datetime2 not null,
		[EndDate] datetime2 not null,
		CONSTRAINT [BookingPK] PRIMARY KEY ([Id], [BookableResourceId]),
		CONSTRAINT [BookingUnitFK] FOREIGN KEY (UnitId, BookableResourceId) REFERENCES [Clubs].[Unit](Id, BookableResourceId),
		Days int null,
		Hours int null
	)
end;