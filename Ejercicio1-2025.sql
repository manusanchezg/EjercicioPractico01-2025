CREATE DATABASE PRACTICA_01_2025;
GO

USE PRACTICA_01_2025;
GO

CREATE TABLE Articulos(
id_articulo INT IDENTITY(1,1),
nombre varchar(100) not null,
precio_unitario Decimal(18, 2) not null,
activo BIT NOT NULL

CONSTRAINT PK_Libros PRIMARY KEY (id_articulo));
GO

CREATE TABLE Formas_Pagos(
id_forma_pago int IDENTITY(1,1),
forma_pago varchar(100) not null,
CONSTRAINT PK_Formas_Pagos PRIMARY KEY (id_forma_pago));
GO

CREATE TABLE Facturas(
nro_factura int IDENTITY(1,1),
fecha date not null,
id_forma_pago int not null,
cliente varchar(100),
CONSTRAINT PK_Facturas PRIMARY KEY (nro_factura),
CONSTRAINT FK_Facturas_Formas_Pagos FOREIGN KEY (id_forma_pago)
REFERENCES Formas_Pagos (id_forma_pago));
GO

CREATE TABLE Detalles_Facturas(
id_detalle_factura int IDENTITY(1,1),
id_articulo INT not null,
cantidad int not null,
nro_factura int not null,
CONSTRAINT PK_Detalles_Facturas PRIMARY KEY (id_detalle_factura),
CONSTRAINT FK_Detalles_Facturas_Articulos FOREIGN KEY (id_articulo)
REFERENCES Articulos (id_articulo),
CONSTRAINT FK_Detalles_Facturas_Facturas FOREIGN KEY (nro_factura)
REFERENCES Facturas (nro_factura));
GO

INSERT INTO Formas_Pagos (forma_pago)
VALUES ('Debito'),
       ('Credito'),
       ('Efectivo'),
       ('Transferencia');
GO

CREATE PROCEDURE OBTENER_LIBROS
AS
BEGIN
    SELECT * FROM Articulos
END
GO

CREATE PROCEDURE OBTENER_ARTICULO_X_ID
@id_articulo INT
AS
BEGIN
    SELECT * FROM Articulos
	WHERE id_articulo = @id_articulo
END
GO

CREATE PROCEDURE MODIFICAR_ARTICULO
@id_articulo INT,
@nombre varchar(100),
@precio_unitario DECIMAL(18, 2)
AS 
BEGIN 
	IF NOT EXISTS (SELECT 1 FROM Articulos WHERE @id_articulo = id_articulo)
	BEGIN
	     INSERT INTO Articulos (nombre, precio_unitario)
	     VALUES (@nombre, @precio_unitario)
	END
	ELSE
	BEGIN	    
        UPDATE Articulos
	    SET nombre = @nombre, precio_unitario = @precio_unitario
	    WHERE id_articulo = @id_articulo
    END
END
GO

CREATE PROCEDURE DAR_BAJA_LIBRO
@id_articulo INT
AS
BEGIN
    UPDATE Articulos 
	SET activo = 0
	WHERE id_articulo = @id_articulo
END	
GO

CREATE PROCEDURE OBTENER_FACTURAS
AS
BEGIN 
    SELECT * FROM Facturas f
    JOIN Formas_Pagos fp ON fp.id_forma_pago = f.id_forma_pago
END
GO

CREATE PROCEDURE OBTENER_FACTURA_X_ID
@nro_factura int
AS
BEGIN
    SELECT * FROM Facturas WHERE nro_factura = @nro_factura
END
GO

CREATE PROCEDURE MODIFICAR_FACTURAS
@nro_factura int = 0,
@fecha date, 
@id_forma_pago int,
@cliente varchar(100),
@new_id int OUTPUT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Facturas WHERE nro_factura = @nro_factura)
	BEGIN
	    INSERT INTO Facturas (fecha, id_forma_pago, cliente)
	    VALUES (@fecha, @id_forma_pago, @cliente)

		SET @new_id = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
	    UPDATE Facturas
		SET fecha = @fecha, id_forma_pago = @id_forma_pago, cliente = @cliente
		WHERE nro_factura = @nro_factura

		SET @new_id = @nro_factura
	END
END	
GO

CREATE PROCEDURE AGREGAR_DETALLE
@id_articulo INT,
@cantidad int,
@nro_factura int
AS
BEGIN 
    INSERT INTO Detalles_Facturas (id_articulo, cantidad, nro_factura)
	VALUES (@id_articulo, @cantidad, @nro_factura)
END
GO

CREATE PROCEDURE ELIMINAR_FACTURA
@nro_factura int
AS
BEGIN 
    BEGIN TRY
	    BEGIN TRANSACTION;

		DELETE Detalles_Facturas
	    WHERE nro_factura = @nro_factura
	    DELETE Facturas 
	    WHERE nro_factura = @nro_factura

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
	    ROLLBACK TRANSACTION;
		THROW;
    END CATCH
END
GO

CREATE PROCEDURE OBTENER_METODOS_PAGOS
AS
BEGIN
    SELECT * FROM Formas_Pagos
END
GO

CREATE PROCEDURE OBTENER_DETALLES_FACTURAS
@nro_factura int
AS
BEGIN
    SELECT * FROM Detalles_Facturas df
    JOIN Articulos l ON l.id_articulo = df.id_articulo
    WHERE df.nro_factura = @nro_factura
END
GO

CREATE PROCEDURE OBTENER_ULTIMA_FACTURA
AS
BEGIN
    SELECT TOP 1 nro_factura FROM Facturas ORDER BY nro_factura DESC
END
GO