-- Insertar las 7 provincias de Costa Rica
INSERT INTO Provincia (nombre) VALUES 
('Alajuela'),
('Cartago'),
('Heredia'),
('Guanacaste'),
('Puntarenas'),
('Limón');
GO

Select * from Provincia;

-- Insertar sedes y recintos oficiales de la UCR

-- Alajuela
INSERT INTO Sede (nombre, provincia_id) VALUES
('Sede de Occidente - San Ramón', 2),
('Recinto de Grecia', 2),
('Sede Interuniversitaria de Alajuela', 2);
GO

-- Cartago
INSERT INTO Sede (nombre, provincia_id) VALUES
('Sede del Atlántico - Turrialba', 3),
('Recinto de Paraíso', 3);
GO

-- Guanacaste
INSERT INTO Sede (nombre, provincia_id) VALUES
('Sede de Guanacaste - Liberia', 5),
('Recinto de Santa Cruz', 5);
GO

-- Puntarenas
INSERT INTO Sede (nombre, provincia_id) VALUES
('Sede del Pacífico - Puntarenas', 6),
('Sede del Sur - Golfito', 6),
('Recinto de Esparza', 6); 
GO

-- Limón
INSERT INTO Sede (nombre, provincia_id) VALUES
('Sede del Caribe - Limón', 7),
('Recinto de Guápiles', 7),
('Recinto de Siquirres', 7);
GO