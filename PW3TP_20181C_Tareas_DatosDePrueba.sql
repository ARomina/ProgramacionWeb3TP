USE [PW3TP_20181C_Tareas]
GO

/* Inserción de datos en tabla usuario */
INSERT INTO Usuario VALUES 
('Nahuel', 'Rapetti', 'nahuel@taskie.com', 'nahuelA1', 1, '2018-07-08', '2018-07-08', 'FE8254D0-3CFE-4BE4-8979-710BEA464735'),
('Romina', 'Barraza', 'romina@taskie.com', 'rominaA1', 1, '2018-07-08', '2018-07-08', 'FE8254D0-3CFE-4BE4-8979-710BEA464735'),
('Lucas', 'Miharu', 'lucas@taskie.com', 'lucasA1', 0, '2018-07-08', NULL, ''),
('Ramiro', 'Silva', 'ramiro@taskie.com', 'ramiroA1', 0, '2018-07-08', NULL, ''),
('Marta', 'Marta', 'marta@marta.com', 'Aa123', 1, '2018-07-08', '2018-07-08', 'FE8254D0-3CFE-4BE4-8979-710BEA464735'),
('Max', 'Max', 'max@max.com', 'Aa123', 0, '2018-07-08', NULL, '');

/* Inserción de carpetas Generales para los usuarios activos */
INSERT INTO Carpeta VALUES
(2016, 'General', NULL, '2018-07-08'),
(2017, 'General', NULL, '2018-07-08'),
(2020, 'General', NULL, '2018-07-08');

/* Inserción de carpetas para los usuarios activos */
INSERT INTO Carpeta VALUES
(2016, 'Mi viaje a Tailandia', 'Todo para mi viaje del año que viene', '2018-07-08'),
(2016, 'Reunión del trabajo', 'Reunión con los jefes de la otra empresa', '2018-07-08'),
(2016, 'Organización de la mudanza', 'La mudanza del próximo mes', '2018-07-08'),
(2016, 'Entrenamiento', NULL, '2018-07-08'),
(2017, 'Mi viaje a California', 'Viaje soñado', '2018-07-08'),
(2017, 'Organización de la reunión', NULL, '2018-07-08'),
(2020, 'La carpeta de Marta', 'Mi carpeta favorita', '2018-07-08'),
(2020, 'Tareas de primavera', NULL, '2018-07-08');

INSERT INTO Tarea VALUES 
(1022, 2016, 'Preparar el bolso', 'Hay mucho para guardar', 3.00, NULL, 1, 0, '2018-07-08'),
(1022, 2016, 'Comprar dólares', 'Para comprar cosas', 5.00, NULL, 1, 0, '2018-07-08'),
(1022, 2016, 'Buscar mapa', 'Para no perderme', 1.00, NULL, 3, 0, '2018-07-08'),
(1022, 2016, 'Comprar zapatillas', 'Que sean cómodas', 10.00, NULL, 4, 0, '2018-07-08'),
(1023, 2016, 'Comprar facturas', 'Con dulce de leche', 1.00, NULL, 1, 0, '2018-07-08'),
(1023, 2016, 'Preparar powerpoint', NULL, 3.00, NULL, 2, 0, '2018-07-08'),
(1024, 2016, 'Comprar cajas', NULL, 1.00, NULL, 1, 0, '2018-07-08'),
(1025, 2016, 'Hacer sentadillas', NULL, 2.00, NULL, 3, 0, '2018-07-08');

INSERT INTO Tarea VALUES 
(1026, 2017, 'Comprar dólares', NULL, 4.00, NULL, 2, 0, '2018-07-08'),
(1026, 2017, 'Comprar ropa', 'Buscar ropa de verano', 4.00, NULL, 2, 0, '2018-07-08'),
(1027, 2017, 'Preparar powerpoint', NULL, 3.00, NULL, 2, 0, '2018-07-08'),
(1027, 2017, 'Comprar cajas', NULL, 1.00, NULL, 1, 0, '2018-07-08');

INSERT INTO Tarea VALUES 
(1028, 2020, 'Limpiar la casa', NULL, 4.00, NULL, 2, 1, '2018-07-08'),
(1029, 2020, 'Regar las plantas', NULL, 2.00, NULL, 4, 1, '2018-07-08');

/* Inserción de comentarios en tareas */
INSERT INTO ComentarioTarea VALUES 
('Que sea un bolso grande', 1037, '2018-07-08'),
('Agregar bolsas', 1037, '2018-07-08');

INSERT INTO ComentarioTarea VALUES 
('Ir temprano', 1046, '2018-07-08'),
('Ordenar la información', 1047, '2018-07-08');

/* Inserción de archivos en tareas */
INSERT INTO ArchivoTarea VALUES 
('/archivos/tareas/1047/examen-net-senior_1ADD5F99-2E17-478C-990F-26257E63ED1F.doc', 1047, '2018-07-08'),
('/archivos/tareas/1047/examen-net-senior_1ADD5F99-2E17-478C-990F-26257E63ED1F.doc', 1047, '2018-07-08');

INSERT INTO ArchivoTarea VALUES 
('/archivos/tareas/1028/examen-net-senior_1ADD5F99-2E17-478C-990F-26257E63ED1F.doc', 1049, '2018-07-08'),
('/archivos/tareas/1028/examen-net-senior_1ADD5F99-2E17-478C-990F-26257E63ED1F.doc', 1049, '2018-07-08');

/* Sentencias de selección de prueba */
SELECT * FROM Usuario;
SELECT * FROM Carpeta;
SELECT * FROM Tarea;
SELECT * FROM ComentarioTarea;
SELECT * FROM ArchivoTarea;