Create database BDHospital
GO

use BDHospital
GO


CREATE TABLE Especialidad (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Medico (
    ID_Medico INT IDENTITY(1,1) PRIMARY KEY,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    DNI VARCHAR(8) NOT NULL UNIQUE,
    Nro_Colegiatura VARCHAR(20) NOT NULL UNIQUE,
    Telefono VARCHAR(15),
    Especialidad_ID INT NOT NULL,
    FOREIGN KEY (Especialidad_ID) REFERENCES Especialidad(ID)
);

CREATE TABLE Paciente (
    ID_Paciente INT IDENTITY(1,1) PRIMARY KEY,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(50) NOT NULL,
    DNI VARCHAR(8) NOT NULL UNIQUE,
    Fecha_nacimiento DATE,
    Telefono VARCHAR(15)
);


--Usuario aqui esta afuera porque viene de otra bd
CREATE TABLE Cita (
    ID_Cita INT IDENTITY(1,1) PRIMARY KEY,
    ID_Medico INT NOT NULL,
    ID_Paciente INT NOT NULL,
    --Id_Usuario BIGINT NOT NULL,
    Fecha_Cita DATETIME NOT NULL,
    Motivo VARCHAR(255),
    Estado VARCHAR(20) DEFAULT 'PENDIENTE',
    FOREIGN KEY (ID_MEDICO) REFERENCES Medico(ID_MEDICO),
    FOREIGN KEY (ID_Paciente) REFERENCES Paciente(ID_PACIENTE)
    --FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id_Usuario)
);

CREATE TABLE Horarios_Atencion (
    ID_Horario INT IDENTITY(1,1) PRIMARY KEY,
    ID_MEDICO INT NOT NULL,
    Dia_Semana VARCHAR(9) CHECK (Dia_Semana IN ('LUNES', 'MARTES', 'MIERCOLES', 'JUEVES', 'VIERNES', 'SABADO', 'DOMINGO')) NOT NULL,
    Horario_Entrada TIME NOT NULL,
    Horario_Salida TIME NOT NULL,
    FOREIGN KEY (ID_MEDICO) REFERENCES Medico(ID_Medico),
    CONSTRAINT UC_Horario UNIQUE (ID_Medico, Dia_Semana, Horario_Entrada, Horario_Salida)
);

CREATE TABLE Comprobante_Pago (
    ID_Comprobante INT IDENTITY(1,1) PRIMARY KEY,
    ID_Cita INT NOT NULL UNIQUE,
    Nombre_Pagador VARCHAR(100) NOT NULL,
    Apellidos_Pagador VARCHAR(100) NOT NULL,
    DNI_Pagador VARCHAR(8),
    Contacto_Pagador VARCHAR(15),
    Fecha_Emision DATETIME NOT NULL DEFAULT GETDATE(),
    Monto DECIMAL(10,2) NOT NULL,
    Metodo_Pago VARCHAR(20) CHECK (Metodo_Pago IN ('EFECTIVO', 'TARJETA', 'TRANSFERENCIA')) NOT NULL,
    Estado VARCHAR(20) DEFAULT 'EMITIDO',
    FOREIGN KEY (ID_Cita) REFERENCES Cita(ID_Cita)
);


INSERT INTO Especialidad (Nombre)
VALUES 
('Medicina General'),
('Pediatría'),
('Cardiología'),
('Dermatología');

INSERT INTO Medico (Nombres, Apellidos, DNI, Nro_Colegiatura, Telefono, Especialidad_ID)
VALUES 
('Juan', 'Pérez', '12345678', '1234-5678', '987654321', 1),  
('Ana', 'Martínez', '23456789', '2345-6789', '987654322', 2),  
('Carlos', 'Gómez', '34567890', '3456-7890', '987654323', 3);  

INSERT INTO Paciente (Nombres, Apellidos, DNI, Fecha_nacimiento, Telefono)
VALUES 
('Luis', 'Ramírez', '45678901', '1990-05-10', '999999999'),
('María', 'Sánchez', '56789012', '1985-08-15', '988888888'),
('Pedro', 'López', '67890123', '2000-02-20', '977777777');

INSERT INTO Cita (ID_Medico, ID_Paciente, Fecha_Cita, Motivo, Estado)
VALUES 
(1, 1, CONVERT(DATETIME, '2025-12-15 10:00:00', 120), 'Consulta general', 'PENDIENTE'),
(2, 2, CONVERT(DATETIME, '2025-12-16 11:00:00', 120), 'Chequeo pediátrico', 'PENDIENTE'),
(3, 3, CONVERT(DATETIME, '2025-12-17 09:00:00', 120), 'Consulta cardiológica', 'PENDIENTE');



INSERT INTO Horarios_Atencion (ID_MEDICO, Dia_Semana, Horario_Entrada, Horario_Salida)
VALUES 
(1, 'LUNES', '08:00', '14:00'),
(1, 'MIERCOLES', '08:00', '14:00'),
(2, 'MARTES', '09:00', '15:00'),
(2, 'JUEVES', '09:00', '15:00'),
(3, 'VIERNES', '07:00', '13:00');

INSERT INTO Comprobante_Pago (ID_Cita, Nombre_Pagador, Apellidos_Pagador, DNI_Pagador, Contacto_Pagador, Monto, Metodo_Pago, Estado)
VALUES 
(6, 'Carlos', 'Ramírez', '45678901', '999999999', 100.00, 'EFECTIVO', 'EMITIDO'),
(7, 'Luisa', 'Sánchez', '56789012', '988888888', 120.00, 'TARJETA', 'EMITIDO'),
(8, 'María', 'López', '67890123', '977777777', 150.00, 'TRANSFERENCIA', 'EMITIDO');

