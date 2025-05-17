-- Insertar las 7 provincias de Costa Rica
INSERT INTO Provincia (nombre) VALUES 
('Alajuela'),
('Cartago'),
('Heredia'),
('Guanacaste'),
('Puntarenas'),
('Lim�n');
GO

Select * from Provincia;

-- Insertar sedes y recintos oficiales de la UCR

-- Alajuela
INSERT INTO Sede (nombre, provincia_id) VALUES
('Sede de Occidente - San Ram�n', 2),
('Recinto de Grecia', 2),
('Sede Interuniversitaria de Alajuela', 2);
GO

-- Cartago
INSERT INTO Sede (nombre, provincia_id) VALUES
('Sede del Atl�ntico - Turrialba', 3),
('Recinto de Para�so', 3);
GO

-- Guanacaste
INSERT INTO Sede (nombre, provincia_id) VALUES
('Sede de Guanacaste - Liberia', 5),
('Recinto de Santa Cruz', 5);
GO

-- Puntarenas
INSERT INTO Sede (nombre, provincia_id) VALUES
('Sede del Pac�fico - Puntarenas', 6),
('Sede del Sur - Golfito', 6),
('Recinto de Esparza', 6); 
GO

-- Lim�n
INSERT INTO Sede (nombre, provincia_id) VALUES
('Sede del Caribe - Lim�n', 7),
('Recinto de Gu�piles', 7),
('Recinto de Siquirres', 7);
GO