use VirtualClassrom



create table Institution(
	id int primary key identity(1,1),
	code varchar(50) unique,
	institution_name varchar(50),
	institution_address varchar(60)
);

create table Classroom(
	id int primary key identity(1,1),
	code varchar(50) unique,
	classroom_number int,
	number_of_seats int,
	type_of_classroom int,
	institution_id int,
	foreign key(institution_id) references institution(id)
);

create table CalendarInfo(
	id int primary key identity(1,1),
	code varchar(50) unique,
	start_time smalldatetime,
	end_time smalldatetime,
	day_of_week int,
	type_of_classroom int, 
	assigned_user int,
	classroom_id int,
	foreign key(classroom_id) references classroom(id)
);

--create table UserX(
--	id int primary key identity(1,1),
--	uname varchar (15),
--	usurname varchar (25),
--	email varchar(30),
--	username varchar (50) unique,
--	user_password varchar (20),
--	roleId int,
--	profesor_Id int
--);

	