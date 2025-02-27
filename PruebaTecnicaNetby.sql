-- Creaci�n de la base de datos
CREATE DATABASE DynamicFormDb;
GO

USE DynamicFormDb;
GO

-- Creaci�n de la tabla Forms
CREATE TABLE [dbo].[Forms] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_Forms] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Creaci�n de la tabla FormFields
CREATE TABLE [dbo].[FormFields] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Label] NVARCHAR(MAX) NOT NULL,
    [FieldType] NVARCHAR(MAX) NOT NULL,
    [FormId] INT NOT NULL,
    CONSTRAINT [PK_FormFields] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormFields_Forms_FormId] FOREIGN KEY ([FormId]) REFERENCES [dbo].[Forms] ([Id]) ON DELETE CASCADE
);
GO

-- Creaci�n de la tabla FormResponses
CREATE TABLE [dbo].[FormResponses] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [FormId] INT NOT NULL,
    [ResponseData] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_FormResponses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormResponses_Forms_FormId] FOREIGN KEY ([FormId]) REFERENCES [dbo].[Forms] ([Id]) ON DELETE CASCADE
);
GO

-- Inserci�n de datos iniciales para "Personas"
INSERT INTO [dbo].[Forms] ([Name]) VALUES ('Personas');
DECLARE @PersonasId INT = SCOPE_IDENTITY();

INSERT INTO [dbo].[FormFields] ([Label], [FieldType], [FormId]) VALUES
    ('Nombres', 'text', @PersonasId),
    ('Apellido', 'text', @PersonasId),
    ('Fecha de Nacimiento', 'date', @PersonasId),
    ('Estatura (cm)', 'number', @PersonasId),
    ('G�nero', 'text', @PersonasId),
    ('Email', 'text', @PersonasId),
    ('Tel�fono', 'number', @PersonasId);
GO

-- Inserci�n de datos iniciales para "Mascotas"
INSERT INTO [dbo].[Forms] ([Name]) VALUES ('Mascotas');
DECLARE @MascotasId INT = SCOPE_IDENTITY();

INSERT INTO [dbo].[FormFields] ([Label], [FieldType], [FormId]) VALUES
    ('Nombre', 'text', @MascotasId),
    ('Especie', 'text', @MascotasId),
    ('Raza', 'text', @MascotasId),
    ('Color', 'text', @MascotasId),
    ('Edad', 'number', @MascotasId),
    ('Fecha de Adopci�n', 'date', @MascotasId),
    ('Vacunado', 'checkbox', @MascotasId);
GO