create database TallerElectronicos
go

use TallerElectronicos 
go

create table Usuarios
(
UsuarioID int primary key identity (1,1),
Nombre varchar(50),
CorreoElectronico varchar(100),
Telefono varchar(30),
Estado varchar (50) check (Estado = 'Activo' or Estado = 'Inactivo'),
)
--go
--alter table Usuarios 
--add Estado varchar(10) check(Estado = 'Activo' or Estado = 'Inactivo')

-- Agregar datos a la tabla
--insert into Usuarios(Nombre, CorreoElectronico, Telefono) values ('Fiorella', 'fiorella0909@gmail.com','8778 6447','Activo')
--insert into Usuarios(Nombre, CorreoElectronico, Telefono) values ('Maria', 'maria123@gmail.com','7658 4536','Activo')
--insert into Usuarios(Nombre, CorreoElectronico, Telefono) values ('Ana', 'anamr90@gmail.com','8329 0201','Activo')

-- Consultar todos los datos de la tabla
--select * from Usuarios

--update Usuarios set Estado = 'activo' where UsuarioID = 1

--select Usuarios.UsuarioID, Usuarios.Nombre, Usuarios.CorreoElectronico, Usuarios.Estado
--from Usuarios
--where Usuarios.Estado = 'activo' -- Permite visualizar solo los usuarios activos


-- Consultar con filtro
--select UsuarioID, Nombre from Usuarios

-- Consultar producto por index 

-- Borrar un dato de la tabla Usuarios
--delete Usuarios where UsuarioID = 1

-- Borrar toda la tabla y reiniciar id
--truncate table Usuarios

-- Modificar datos de la tabla Usuarios
--update Usuarios set Nombre = 'Keany', CorreoElectronico = 'keanymenaberrocal13@gmail.com', Telefono = '8756 6436'

 create table Equipos 
(
EquipoID int primary key identity(1,1),
TipoEquipo varchar(100),
Modelo varchar(100),
UsuarioID int foreign key references Usuarios(UsuarioID),
Estado varchar (50) check (Estado = 'Activo' or Estado = 'Inactivo')
)
--go
--alter table Equipos 
--add Estado varchar(10) check(Estado = 'Activo' or Estado = 'Inactivo')

--update Equipos set Estado = 'Activo' where EquipoID = 1

--select Equipos.EquipoID, Equipos.TipoEquipo, Equipos.Modelo, Equipos.UsuarioID, Equipos.Estado
--from Equipos
--where Equipos.Estado = 'Activo' -- Permite visualizar solo los equipos activos

-- Agregar 
--insert into  Equipos(TipoEquipo, Modelo, UsuarioID, Estado) values ('Refrigeradora', 'Samsung', 1, 'Activo')
--insert into  Equipos(TipoEquipo, Modelo, UsuarioID, Estado) values ('Tablet', 'Apple', 2, 'Inactivo')

-- Consultar todo
--select * from Equipos

-- Consultar con filtro
--select * from Equipos where TipoEquipo = 'Electrodomestico'
--select * from Equipos where Modelo like '%s%'

-- Modificar 

--update Usuarios set Estado = 'Activo' where UsuarioID = 1


-- Eliminar tabla y reinicia EquipoID
--truncate table Equipos

create table Reparaciones
(
ReparacionID int primary key identity(1,1),
EquipoID int foreign key references Equipos(EquipoID),
FechaSolicitud date not null,
Estado varchar(50) check (Estado = 'Activo' or Estado = 'Inactivo')
)
--go

--update Reparaciones set Estado = 'Activo' where ReparacionID = 6
--update Reparaciones set Estado = 'Inactivo' where ReparacionID = 3


--select Reparaciones.ReparacionID, Reparaciones.EquipoID, Reparaciones.FechaSolicitud, Reparaciones.Estado
--from Reparaciones
--where Reparaciones.Estado = 'Activo' -- Permite visualizar solo las reparaciones listas


-- Agregar 
--insert into Reparaciones(EquipoID, FechaSolicitud, Estado) values (1,'2024/11/12', 'Activo')


-- Consultar todo 
--select * from Reparaciones

-- Consultar con filtro 
--select * from Reparaciones where Estado = 'pendiente'

-- Modificar campo
--update Reparaciones set EquipoID = 2, FechaSolicitud = '2024/11/11', Estado = 'En proceso' where ReparacionID = 2

-- Borrar tabla y reiniciar ReparacionID
--truncate table Reparaciones

create table DetallesReparacion
(
DetalleID int primary key identity(1,1),
ReparacionID int foreign key references Reparaciones(ReparacionID),
Descripcion varchar(200),
FechaInicio date,
FechaFin date,
Estado varchar (50) check (Estado = 'Activo' or Estado = 'Inactivo')
)
--go
--alter table DetallesReparacion
--add Estado varchar(10) check(Estado = 'Activo' or Estado = 'Inactivo')

--update DetallesReparacion set Estado = 'Activo' where DetalleID = 3

--select DetallesReparacion.DetalleID, DetallesReparacion.ReparacionID, DetallesReparacion.Descripcion, DetallesReparacion.FechaFin, DetallesReparacion.FechaFin, DetallesReparacion.Estado
--from DetallesReparacion
--where DetallesReparacion.Estado = 'Activo' -- Permite visualizar solo los equipos activos

-- Agregar 
--insert into DetallesReparacion(ReparacionID, Descripcion, FechaInicio, FechaFin) values (1, 'No enfria correctamente', '2024/11/13', '2024/11/17')
--insert into DetallesReparacion(ReparacionID, Descripcion, FechaInicio, FechaFin) values (2, 'No enciende', '2024/11/7', '2024/11/10')
--insert into DetallesReparacion(ReparacionID, Descripcion, FechaInicio, FechaFin) values (3, 'Pantalla quebrada', '2024/11/12', '2024/11/15')

-- Consultar todo
--select * from DetallesReparacion

-- Modificar con filtro
--update DetallesReparacion set Descripcion = 'Pantalla quebrada y no enciende' where ReparacionID = 3

-- Borrar toda la tabla y reiniciar ReparacionID
--truncate table DetallesReparacion

create table Tecnicos
(
TecnicoID int primary key identity(1,1),
Nombre varchar(70),
Especialidad varchar(100),
Estado varchar (50) check (Estado = 'Activo' or Estado = 'Inactivo')
)
--go
--alter table Tecnicos 
--add Estado varchar(10) check(Estado = 'Activo' or Estado = 'Inactivo')

--update Tecnicos set Estado = 'Activo' where TecnicoID = 1

--select Tecnicos.TecnicoID, Tecnicos.Nombre, Tecnicos.Especialidad, Tecnicos.Estado
--from Tecnicos
--where Tecnicos.Estado = 'activo' -- Permite visualizar solo los tecnicos activos

-- Agregar 
--insert into Tecnicos(Nombre, Especialidad) values('Daniel', 'Electrodomesticos')
--insert into Tecnicos(Nombre, Especialidad) values('Mario', 'Televisores')
--insert into Tecnicos(Nombre, Especialidad) values('Paula', 'Dispositivos moviles')

-- Consultar todo
--select * from Tecnicos

-- Modificaron filtro
--update Tecnicos set Nombre = 'Axel' where tecnicoID = 1

-- Borrar tabla y reiniciar TecnicoID
--truncate table Tecnicos

create table Asignaciones
(
AsignacionID int primary key identity(1,1),
ReparacionID int foreign key references Reparaciones(ReparacionID),
TecnicoID int foreign key references Tecnicos(TecnicoID),
FechaAsignacion date,
Estado varchar (50) check (Estado = 'Activo' or Estado = 'Inactivo')
)

--go
--alter table Asignaciones
--add Estado varchar(10) check(Estado = 'Activo' or Estado = 'Inactivo')

--update Asignaciones set Estado = 'Activo' where AsignacionID = 3

--select Asignaciones.AsignacionID, Asignaciones.ReparacionID, Asignaciones.TecnicoID, Asignaciones.FechaAsignacion, Asignaciones.Estado
--from Asignaciones
--where Asignaciones.Estado = 'Activo' -- Permite visualizar solo los equipos activos

-- Agregar 
--insert into Asignaciones(ReparacionID, TecnicoID, FechaAsignacion) values(1, 1, '2024/11/13')
--insert into Asignaciones(ReparacionID, TecnicoID, FechaAsignacion) values(2, 2, '2024/11/7')
--insert into Asignaciones(ReparacionID, TecnicoID, FechaAsignacion) values(3, 3, '2024/11/12')

-- Consultar todo
--select * from Asignaciones

-- Consultar con filtro
--select * from Asignaciones where FechaAsignacion like '%no%'

-- Modificar 
--update Asignaciones set FechaAsignacion = '2024/11/11' where tecnicoID = 3

-- Borrar la tabla y reiniciar AsignacionID
--truncate table Asignaciones 

--create table Estados
--(
--Id int primary key, 
--Descripcion varchar (10)
--)

--insert into Estados values (1, 'Activo'),(2,'Inactivo')

create procedure ingresarUsuario
@Nombre varchar(100),
@CorreoElectronico varchar(100),
@Telefono varchar(100),
@Estado varchar(50)
as 
	begin 
		insert into Usuarios values (@Nombre,@CorreoElectronico,@Telefono,@Estado)
	end

	exec ingresarUsuario 'Maria','maria123@gmail.com','8465 2347','Activo',



create procedure borrarUsuario
@UsuarioID int

as 
	begin 
		update Usuarios set Estado = 'Inactivo' where UsuarioID = @UsuarioID
	end

create procedure consultarUsuarios
as 
	begin 
		select * from Usuarios where Estado = 'Activo'
	end

	exec consultarUsuarios

create procedure consultarUsuariosFiltro
@UsuarioID int
as 
	begin 
		select * from Usuarios where UsuarioID = @UsuarioID
	end

	exec consultarUsuariosFiltro 6

create procedure modificarUsuario
@UsuarioID int,
@Nombre varchar(100),
@CorreoElectronico varchar(100),
@Telefono varchar(50),
@Estado varchar(50)
as 
	begin 
	update Usuarios set Nombre = @Nombre where UsuarioID = @UsuarioID
	update Usuarios set CorreoElectronico = @CorreoElectronico where UsuarioID = @UsuarioID
	update Usuarios set Telefono = @Telefono where UsuarioID = @UsuarioID
	update Usuarios set Estado = @Estado where UsuarioID = @UsuarioID
	
	end
	
	exec modificarUsuario 6, 'keany', 'fiorella1320@gmail.com','7893 2345', 'Activo'

create procedure ingresarEquipo
@TipoEquipo varchar(100),
@Modelo varchar(100),
@UsuarioID int,
@Estado varchar(50)

as 
	begin 
		insert into Equipos values (@TipoEquipo,@Modelo,@UsuarioID,@Estado)
	end

	exec ingresarEquipo 'Laptop','HP',1,'Activo'

create procedure borrarEquipo
@EquipoID int

as 
	begin 
	update Equipos set Estado = 'Inactivo' where EquipoId = @EquipoId 
	end

create procedure consultarEquipo
as 
	begin 
		select * from Equipos where Estado = 'Activo'
	end

create procedure consultarEquipoFiltro
as 
	begin 
		select * from Equipos where Estado = 'Activo'
	end

	exec consultarEquipo

	exec consultarEquipoFiltro

create procedure modificarEquipo
@EquipoID int,
@TipoEquipo varchar(100),
@Modelo varchar(100),
@UsuarioID int,
@Estado varchar(50)
as 
	begin 
	update Equipos set TipoEquipo = @TipoEquipo where EquipoID = @EquipoID
	update Equipos set Modelo = @Modelo where EquipoID = @EquipoID
	update Equipos set Estado = @Estado where EquipoID = @EquipoID
	end

	exec modificarEquipo 1, 'Aspiradora inteligente', 'Xiaomi',1, 'Activo'

create procedure ingresarReparacion
@EquipoID int,
@FechaSolicitud date,
@Estado varchar(50)

as 
	begin 
		insert into Reparaciones values (@EquipoID,@FechaSolicitud,@Estado)
	end

	exec ingresarReparacion 1,'2024/11/8','Activo'

create procedure borrarReparacion
@ReparacionID int

as 
	begin 
		update Reparaciones set Estado = 'Inactivo' where ReparacionID = @ReparacionID 
	end

create procedure consultarReparacion
as 
	begin 
		select * from Reparaciones where Estado = 'Activo'
	end

	exec consultarReparacion 

create procedure consultarReparacionFiltro
@ReparacionID int 
as 
	begin 
		select * from Reparaciones where ReparacionID = @ReparacionID
	end

	exec consultarReparacionFiltro

create procedure modificarReparacion
@ReparacionID int,
@EquipoID int,
@FechaSolicitud date,
@Estado varchar(60)
as 
	begin 
	update Reparaciones set FechaSolicitud = @FechaSolicitud where ReparacionID = @ReparacionID
	update Reparaciones set Estado = @Estado where ReparacionID = @ReparacionID
	
	end

	exec modificarReparacion 2, 2, '2024/11/5','Pendiente' 

create procedure ingresarDetalleReparacion
@ReparacionID int,
@Descripcion varchar(200),
@FechaInicio date,
@FechaFin date,
@Estado varchar(50)

as 
	begin 
		insert into DetallesReparacion values (@ReparacionID,@Descripcion,@FechaInicio,@FechaFin,@Estado)
	end

	exec ingresarDetalleReparacion 1, 'no se','2024/11/8','2024/11/12', 'Activo'

create procedure borrarDetalleReparacion
@DetalleID int

as 
	begin 
		update DetallesReparacion set Estado = 'Inactivo' where DetalleID = @DetalleID 
	end

create procedure consultarDetalleReparacion
as 
	begin 
		select * from DetallesReparacion where Estado = 'Activo'
	end

create procedure consultarDetalleReparacionFiltro
@DetalleID int
as 
	begin 
		select * from DetallesReparacion where DetalleID = @DetalleID
	end

create procedure modificarDetalleReparacion
@DetalleID int,
@ReparacionID int,
@Descripcion varchar(200),
@FechaInicio date,
@FechaFin date,
@Estado varchar(50)
as 
	begin 
	update DetallesReparacion set FechaInicio = @FechaInicio where DetalleID = @DetalleID
	update DetallesReparacion set FechaFin = @FechaFin where DetalleID = @DetalleID
	update DetallesReparacion set Descripcion = @Descripcion where DetalleID = @DetalleID
	update DetallesReparacion set Estado = @Estado where DetalleID = @DetalleID
	end

	exec modificarDetalleReparacion 4, 1, 'Si se','2024/10/2' , '2024/10/5', 'Activo'

create procedure ingresarTecnicos
@Nombre varchar(70),
@Especialidad varchar(100),
@Estado varchar(50)

as 
	begin 
		insert into Tecnicos values (@Nombre,@Especialidad,@Estado)
	end

	exec ingresarTecnicos 'Abraham','Electronicos','Activo'

create procedure borrarTecnicos
@TecnicoID int

as 
	begin 
		update Tecnicos set Estado = 'Inactivo' where TecnicoID = @TecnicoID
	end

create procedure consultarTecnicos
as 
	begin 
		select * from Tecnicos where Estado = 'Activo'
	end

create procedure consultarTecnicosFiltro
@TecnicoID int
as 
	begin 
		select * from Tecnicos where TecnicoID = @TecnicoID
	end

create procedure modificarTecnicos
@TecnicoID int,
@Nombre varchar(70),
@Especialidad varchar(100),
@Estado varchar(50)
as 
	begin 
	update Tecnicos set Nombre = @Nombre where TecnicoID = @TecnicoID
	update Tecnicos set Especialidad = @Especialidad where TecnicoID = @TecnicoID
	update Tecnicos set Estado = @Estado where TecnicoID = @TecnicoID
	end

	exec modificarTecnicos 4, 'Abraham', 'Computadoras', 'Activo'

create procedure ingresarAsignacion
@ReparacionID int,
@TecnicoID int,
@FechaAsignacion date,
@Estado varchar(50)
as 
	begin 
		insert into Asignaciones values (@ReparacionID, @TecnicoID, @FechaAsignacion,@Estado)
	end

	exec ingresarAsignacion 1, 4, '2024/10/15','Activo'

create procedure borrarAsignacion
@AsignacionID int

as 
	begin 
		update Asignaciones set Estado = 'Inactivo' where AsignacionID = @AsignacionID
	end

create procedure consultarAsignacion
as 
	begin 
		select * from Asignaciones where Estado = 'Activo'
	end

create procedure consultarAsignacionFiltro
@AsignacionID int
as 
	begin 
		select * from Asignaciones where AsignacionID = @AsignacionID
	end

	exec consultarAsignacionFiltro

create procedure modificarAsignacion
@AsignacionID int,
@ReparacionID int,
@TecnicoID int,
@FechaAsignacion date,
@Estado varchar(50)
as 
	begin 
	update Asignaciones set ReparacionID = @ReparacionID where @AsignacionID = @AsignacionID
	update Asignaciones set TecnicoID = @TecnicoID where AsignacionID = @AsignacionID
	update Asignaciones set FechaAsignacion = @FechaAsignacion where AsignacionID = @AsignacionID
	update Asignaciones set Estado = @Estado where AsignacionID = @AsignacionID
	end

	exec modificarAsignacion 1, 1, 4, '2024/11/17', 'Activo'

create table Estado
(
id int primary key,
descripcion varchar(20)
)

insert into Estado values (1,'Activo'),(2,'Inactivo')







