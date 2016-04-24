-- PostGreSQL code used to make ASP.Net's Identity/User Authentication process work with our data base
-- What we essentially do is create the schema that Microsoft's entity framework is expecting to see, instead of changing the code in the the entity framework.
 
CREATE SCHEMA IF NOT EXISTS dbo;

    CREATE TABLE IF NOT EXISTS dbo."AspNetUsers" (
        "Id"                   VARCHAR   NOT NULL,
        "Email"                VARCHAR   NULL,
        "EmailConfirmed"       BOOLEAN   NOT NULL,
        "PasswordHash"         VARCHAR   NULL,
        "SecurityStamp"        VARCHAR   NULL,
        "PhoneNumber"          VARCHAR   NULL,
        "PhoneNumberConfirmed" BOOLEAN   NOT NULL,
        "TwoFactorEnabled"     BOOLEAN   NOT NULL,
        "LockoutEndDateUtc"    TIMESTAMP NULL,
        "LockoutEnabled"       BOOLEAN   NOT NULL,
        "AccessFailedCount"    INT       NOT NULL,
        "UserName"             VARCHAR   NOT NULL,
        CONSTRAINT PK_AspNetUsers PRIMARY KEY ("Id"),
        CONSTRAINT UQ_AspNetUsers UNIQUE ("UserName")
    );

    CREATE TABLE IF NOT EXISTS dbo."AspNetUserLogins" (
        "LoginProvider" VARCHAR NOT NULL,
        "ProviderKey"   VARCHAR NOT NULL,
        "UserId"        VARCHAR NOT NULL,
        CONSTRAINT PK_AspNetUserLogins PRIMARY KEY ("LoginProvider", "ProviderKey", "UserId"),
        FOREIGN KEY ("UserId") REFERENCES dbo."AspNetUsers" ("Id") ON DELETE CASCADE
    );

    DO $$
        BEGIN
            CREATE INDEX IX_AspNetUserLogins_UserId ON dbo."AspNetUserLogins" ("UserId");
        EXCEPTION
            WHEN others THEN RAISE NOTICE 'Could not add IX_AspNetUserLogins_UserId. Does it already exist?';
        END
    $$;


    CREATE TABLE IF NOT EXISTS dbo."AspNetRoles" (
        "Id"   VARCHAR NOT NULL,
        "Name" VARCHAR NOT NULL,
        CONSTRAINT PK_AspNetRoles  PRIMARY KEY ("Id"),
        CONSTRAINT UQ_RoleName UNIQUE ("Name")
    );

    CREATE TABLE IF NOT EXISTS dbo."AspNetUserClaims" (
        "Id"         SERIAL,
        "UserId"     VARCHAR NOT NULL,
        "ClaimType"  VARCHAR NULL,
        "ClaimValue" VARCHAR NULL,
        CONSTRAINT PK_AspNetUserClaims PRIMARY KEY ("Id"),
        FOREIGN KEY ("UserId") REFERENCES dbo."AspNetUsers"("Id") ON DELETE CASCADE
    );

    DO $$
        BEGIN
            CREATE INDEX IX_AspNetUserClaims_UserId ON dbo."AspNetUserClaims" ("UserId");
        EXCEPTION
            WHEN others THEN RAISE NOTICE 'Could not add IX_AspNetUserClaims_UserId. Does it already exist?';
        END
    $$;

    CREATE TABLE IF NOT EXISTS dbo."AspNetUserRoles" (
        "UserId" VARCHAR NOT NULL,
        "RoleId" VARCHAR NOT NULL,
        CONSTRAINT PK_AspNetUserRoles PRIMARY KEY ("UserId", "RoleId"),
        FOREIGN KEY ("RoleId") REFERENCES dbo."AspNetRoles"("Id") ON DELETE CASCADE,
        FOREIGN KEY ("UserId") REFERENCES dbo."AspNetUsers"("Id") ON DELETE CASCADE
    );

    DO $$
        BEGIN
            CREATE INDEX IX_AspNetUserRoles_UserId ON dbo."AspNetUserRoles"("UserId");
        EXCEPTION
            WHEN others THEN RAISE NOTICE 'Could not add IX_AspNetUserRoles_UserId. Does it already exist?';
        END
    $$;


    DO $$
        BEGIN
            CREATE INDEX IX_AspNetUserRoles_RoleId ON dbo."AspNetUserRoles"("RoleId");
        EXCEPTION
            WHEN others THEN RAISE NOTICE 'Could not add IX_AspNetUserRoles_RoleId. Does it already exist?';
        END
    $$;