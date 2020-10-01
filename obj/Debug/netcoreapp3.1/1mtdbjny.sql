IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES (N'4ba49888-bc69-4994-9c0b-a5d2b35e12a3', N'b227d6a2-4ff3-461d-a197-79c12bbf5692', N'Admin', N'Admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] ON;
INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES (N'd18bcb24-fe36-43e7-a340-d3dfe8c2566d', 0, N'7216cc3c-7461-48b4-96c0-253fbda507de', N'jroque@ods.org.hn', CAST(0 AS bit), CAST(0 AS bit), NULL, N'jroque@ods.org.hn', N'jroque@ods.org.hn', N'AQAAAAEAACcQAAAAEEV9HZ3yI2FFmcM/v9UPe8V1jh8F59bKBz+diW823itsSZf7TJwvp/m3AiTpCqHlYw==', NULL, CAST(0 AS bit), N'6a2c4562-e1cb-4920-8432-9a927e0f641b', CAST(0 AS bit), N'jroque@ods.org.hn');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
    SET IDENTITY_INSERT [AspNetUserClaims] ON;
INSERT INTO [AspNetUserClaims] ([Id], [ClaimType], [ClaimValue], [UserId])
VALUES (1, N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'Admin', N'd18bcb24-fe36-43e7-a340-d3dfe8c2566d');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
    SET IDENTITY_INSERT [AspNetUserClaims] OFF;

GO

            --migrationBuilder.DeleteData(
            --    table: "AspNetRoles",
            --    keyColumn: "Id",
            --    keyValue: "4ba49888-bc69-4994-9c0b-a5d2b35e12a3");

            --migrationBuilder.DeleteData(
            --    table: "AspNetUserClaims",
            --    keyColumn: "Id",
            --    keyValue: 1);

            --migrationBuilder.DeleteData(
            --    table: "AspNetUsers",
            --    keyColumn: "Id",
            --    keyValue: "d18bcb24-fe36-43e7-a340-d3dfe8c2566d");

