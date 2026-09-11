CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Locations] (
    [Id] int NOT NULL IDENTITY,
    [Latitude] float NOT NULL,
    [Longitude] float NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Website] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Locations] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Tags] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Role] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Accommodations] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [AccommodationType] int NOT NULL,
    [LocationId] int NOT NULL,
    [PricePerNight] decimal(18,2) NOT NULL,
    [MaxGuests] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Accommodations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Accommodations_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Attractions] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [LocationId] int NOT NULL,
    [OpeningTime] time NULL,
    [ClosingTime] time NULL,
    [EntranceFee] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Attractions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Attractions_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Restaurants] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [FoodType] int NOT NULL,
    [LocationId] int NOT NULL,
    [OpeningTime] time NULL,
    [ClosingTime] time NULL,
    [AveragePricePerPerson] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Restaurants] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Restaurants_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [TripPlans] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [PlanName] nvarchar(max) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [MemberCount] int NOT NULL,
    [BudgetAmount] decimal(18,2) NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_TripPlans] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TripPlans_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AttractionCategories] (
    [Id] int NOT NULL IDENTITY,
    [AttractionId] int NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_AttractionCategories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AttractionCategories_Attractions_AttractionId] FOREIGN KEY ([AttractionId]) REFERENCES [Attractions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AttractionCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AttractionDistances] (
    [Id] int NOT NULL IDENTITY,
    [SourceAttractionId] int NOT NULL,
    [DestinationAttractionId] int NOT NULL,
    [DistanceKm] float NOT NULL,
    [EstimatedMinutes] int NOT NULL,
    CONSTRAINT [PK_AttractionDistances] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AttractionDistances_Attractions_DestinationAttractionId] FOREIGN KEY ([DestinationAttractionId]) REFERENCES [Attractions] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AttractionDistances_Attractions_SourceAttractionId] FOREIGN KEY ([SourceAttractionId]) REFERENCES [Attractions] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [AttractionTags] (
    [Id] int NOT NULL IDENTITY,
    [AttractionId] int NOT NULL,
    [TagId] int NOT NULL,
    CONSTRAINT [PK_AttractionTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AttractionTags_Attractions_AttractionId] FOREIGN KEY ([AttractionId]) REFERENCES [Attractions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AttractionTags_Tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tags] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [CrowdReports] (
    [Id] int NOT NULL IDENTITY,
    [AttractionId] int NOT NULL,
    [CongestionLevel] int NOT NULL,
    [ReportTime] datetime2 NOT NULL,
    CONSTRAINT [PK_CrowdReports] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CrowdReports_Attractions_AttractionId] FOREIGN KEY ([AttractionId]) REFERENCES [Attractions] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Events] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [AttractionId] int NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Events_Attractions_AttractionId] FOREIGN KEY ([AttractionId]) REFERENCES [Attractions] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [UserActivityLogs] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ActivityType] int NOT NULL,
    [RelatedAttractionId] int NULL,
    [Timestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_UserActivityLogs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserActivityLogs_Attractions_RelatedAttractionId] FOREIGN KEY ([RelatedAttractionId]) REFERENCES [Attractions] ([Id]),
    CONSTRAINT [FK_UserActivityLogs_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Favorites] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [AttractionId] int NULL,
    [RestaurantId] int NULL,
    [AccommodationId] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Favorites] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Favorites_Accommodations_AccommodationId] FOREIGN KEY ([AccommodationId]) REFERENCES [Accommodations] ([Id]),
    CONSTRAINT [FK_Favorites_Attractions_AttractionId] FOREIGN KEY ([AttractionId]) REFERENCES [Attractions] ([Id]),
    CONSTRAINT [FK_Favorites_Restaurants_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id]),
    CONSTRAINT [FK_Favorites_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [PlaceImages] (
    [Id] int NOT NULL IDENTITY,
    [ImageUrl] nvarchar(max) NOT NULL,
    [AttractionId] int NULL,
    [RestaurantId] int NULL,
    [AccommodationId] int NULL,
    CONSTRAINT [PK_PlaceImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PlaceImages_Accommodations_AccommodationId] FOREIGN KEY ([AccommodationId]) REFERENCES [Accommodations] ([Id]),
    CONSTRAINT [FK_PlaceImages_Attractions_AttractionId] FOREIGN KEY ([AttractionId]) REFERENCES [Attractions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PlaceImages_Restaurants_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id])
);
GO


CREATE TABLE [Reviews] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [AttractionId] int NULL,
    [RestaurantId] int NULL,
    [AccommodationId] int NULL,
    [Rating] int NOT NULL,
    [Comment] nvarchar(max) NOT NULL,
    [IsApproved] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reviews_Accommodations_AccommodationId] FOREIGN KEY ([AccommodationId]) REFERENCES [Accommodations] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Attractions_AttractionId] FOREIGN KEY ([AttractionId]) REFERENCES [Attractions] ([Id]),
    CONSTRAINT [FK_Reviews_Restaurants_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [TripAccommodations] (
    [Id] int NOT NULL IDENTITY,
    [TripPlanId] int NOT NULL,
    [AccommodationId] int NOT NULL,
    [CheckInDate] datetime2 NOT NULL,
    [CheckOutDate] datetime2 NOT NULL,
    CONSTRAINT [PK_TripAccommodations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TripAccommodations_Accommodations_AccommodationId] FOREIGN KEY ([AccommodationId]) REFERENCES [Accommodations] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TripAccommodations_TripPlans_TripPlanId] FOREIGN KEY ([TripPlanId]) REFERENCES [TripPlans] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [TripExpenses] (
    [Id] int NOT NULL IDENTITY,
    [TripPlanId] int NOT NULL,
    [ExpenseCategory] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [ExpenseDate] datetime2 NOT NULL,
    CONSTRAINT [PK_TripExpenses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TripExpenses_TripPlans_TripPlanId] FOREIGN KEY ([TripPlanId]) REFERENCES [TripPlans] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [TripPlanItems] (
    [Id] int NOT NULL IDENTITY,
    [TripPlanId] int NOT NULL,
    [AttractionId] int NULL,
    [RestaurantId] int NULL,
    [AccommodationId] int NULL,
    [DayNumber] int NOT NULL,
    [Sequence] int NOT NULL,
    [ScheduledTime] time NULL,
    CONSTRAINT [PK_TripPlanItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TripPlanItems_Accommodations_AccommodationId] FOREIGN KEY ([AccommodationId]) REFERENCES [Accommodations] ([Id]),
    CONSTRAINT [FK_TripPlanItems_Attractions_AttractionId] FOREIGN KEY ([AttractionId]) REFERENCES [Attractions] ([Id]),
    CONSTRAINT [FK_TripPlanItems_Restaurants_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants] ([Id]),
    CONSTRAINT [FK_TripPlanItems_TripPlans_TripPlanId] FOREIGN KEY ([TripPlanId]) REFERENCES [TripPlans] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Notifications] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [Type] int NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [RelatedTripPlanId] int NULL,
    [RelatedEventId] int NULL,
    [RelatedAttractionId] int NULL,
    [IsRead] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Notifications_Attractions_RelatedAttractionId] FOREIGN KEY ([RelatedAttractionId]) REFERENCES [Attractions] ([Id]),
    CONSTRAINT [FK_Notifications_Events_RelatedEventId] FOREIGN KEY ([RelatedEventId]) REFERENCES [Events] ([Id]),
    CONSTRAINT [FK_Notifications_TripPlans_RelatedTripPlanId] FOREIGN KEY ([RelatedTripPlanId]) REFERENCES [TripPlans] ([Id]),
    CONSTRAINT [FK_Notifications_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [UserActivityLogTags] (
    [Id] int NOT NULL IDENTITY,
    [UserActivityLogId] int NOT NULL,
    [TagId] int NOT NULL,
    CONSTRAINT [PK_UserActivityLogTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserActivityLogTags_Tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tags] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserActivityLogTags_UserActivityLogs_UserActivityLogId] FOREIGN KEY ([UserActivityLogId]) REFERENCES [UserActivityLogs] ([Id]) ON DELETE CASCADE
);
GO


CREATE UNIQUE INDEX [IX_Accommodations_LocationId] ON [Accommodations] ([LocationId]);
GO


CREATE UNIQUE INDEX [IX_AttractionCategories_AttractionId_CategoryId] ON [AttractionCategories] ([AttractionId], [CategoryId]);
GO


CREATE INDEX [IX_AttractionCategories_CategoryId] ON [AttractionCategories] ([CategoryId]);
GO


CREATE INDEX [IX_AttractionDistances_DestinationAttractionId] ON [AttractionDistances] ([DestinationAttractionId]);
GO


CREATE UNIQUE INDEX [IX_AttractionDistances_SourceAttractionId_DestinationAttractionId] ON [AttractionDistances] ([SourceAttractionId], [DestinationAttractionId]);
GO


CREATE UNIQUE INDEX [IX_Attractions_LocationId] ON [Attractions] ([LocationId]);
GO


CREATE UNIQUE INDEX [IX_AttractionTags_AttractionId_TagId] ON [AttractionTags] ([AttractionId], [TagId]);
GO


CREATE INDEX [IX_AttractionTags_TagId] ON [AttractionTags] ([TagId]);
GO


CREATE INDEX [IX_CrowdReports_AttractionId] ON [CrowdReports] ([AttractionId]);
GO


CREATE INDEX [IX_Events_AttractionId] ON [Events] ([AttractionId]);
GO


CREATE INDEX [IX_Favorites_AccommodationId] ON [Favorites] ([AccommodationId]);
GO


CREATE INDEX [IX_Favorites_AttractionId] ON [Favorites] ([AttractionId]);
GO


CREATE INDEX [IX_Favorites_RestaurantId] ON [Favorites] ([RestaurantId]);
GO


CREATE UNIQUE INDEX [IX_Favorites_UserId_AccommodationId] ON [Favorites] ([UserId], [AccommodationId]) WHERE [AccommodationId] IS NOT NULL;
GO


CREATE UNIQUE INDEX [IX_Favorites_UserId_AttractionId] ON [Favorites] ([UserId], [AttractionId]) WHERE [AttractionId] IS NOT NULL;
GO


CREATE UNIQUE INDEX [IX_Favorites_UserId_RestaurantId] ON [Favorites] ([UserId], [RestaurantId]) WHERE [RestaurantId] IS NOT NULL;
GO


CREATE INDEX [IX_Notifications_RelatedAttractionId] ON [Notifications] ([RelatedAttractionId]);
GO


CREATE INDEX [IX_Notifications_RelatedEventId] ON [Notifications] ([RelatedEventId]);
GO


CREATE INDEX [IX_Notifications_RelatedTripPlanId] ON [Notifications] ([RelatedTripPlanId]);
GO


CREATE INDEX [IX_Notifications_UserId] ON [Notifications] ([UserId]);
GO


CREATE INDEX [IX_PlaceImages_AccommodationId] ON [PlaceImages] ([AccommodationId]);
GO


CREATE INDEX [IX_PlaceImages_AttractionId] ON [PlaceImages] ([AttractionId]);
GO


CREATE INDEX [IX_PlaceImages_RestaurantId] ON [PlaceImages] ([RestaurantId]);
GO


CREATE UNIQUE INDEX [IX_Restaurants_LocationId] ON [Restaurants] ([LocationId]);
GO


CREATE INDEX [IX_Reviews_AccommodationId] ON [Reviews] ([AccommodationId]);
GO


CREATE INDEX [IX_Reviews_AttractionId] ON [Reviews] ([AttractionId]);
GO


CREATE INDEX [IX_Reviews_RestaurantId] ON [Reviews] ([RestaurantId]);
GO


CREATE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
GO


CREATE INDEX [IX_TripAccommodations_AccommodationId] ON [TripAccommodations] ([AccommodationId]);
GO


CREATE INDEX [IX_TripAccommodations_TripPlanId] ON [TripAccommodations] ([TripPlanId]);
GO


CREATE INDEX [IX_TripExpenses_TripPlanId] ON [TripExpenses] ([TripPlanId]);
GO


CREATE INDEX [IX_TripPlanItems_AccommodationId] ON [TripPlanItems] ([AccommodationId]);
GO


CREATE INDEX [IX_TripPlanItems_AttractionId] ON [TripPlanItems] ([AttractionId]);
GO


CREATE INDEX [IX_TripPlanItems_RestaurantId] ON [TripPlanItems] ([RestaurantId]);
GO


CREATE INDEX [IX_TripPlanItems_TripPlanId] ON [TripPlanItems] ([TripPlanId]);
GO


CREATE INDEX [IX_TripPlans_UserId] ON [TripPlans] ([UserId]);
GO


CREATE INDEX [IX_UserActivityLogs_RelatedAttractionId] ON [UserActivityLogs] ([RelatedAttractionId]);
GO


CREATE INDEX [IX_UserActivityLogs_UserId] ON [UserActivityLogs] ([UserId]);
GO


CREATE INDEX [IX_UserActivityLogTags_TagId] ON [UserActivityLogTags] ([TagId]);
GO


CREATE UNIQUE INDEX [IX_UserActivityLogTags_UserActivityLogId_TagId] ON [UserActivityLogTags] ([UserActivityLogId], [TagId]);
GO


