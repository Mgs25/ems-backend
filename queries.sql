use EMS;

-- CREATE TABLE Users (
-- 	UserID int IDENTITY(1, 1) PRIMARY KEY,
-- 	Username varchar(16) NOT NULL,
-- 	MailAddress varchar(30) NOT NULL,
-- 	PasswordHash varchar(255) NOT NULL,
-- 	PasswordSalt varchar(255) NOT NULL
-- );

-- CREATE TABLE Events (
-- 	EventID int IDENTITY(1, 1) PRIMARY KEY,
-- 	Title varchar(255) not null,
-- 	Description varchar(max) not null,
-- 	Category varchar(255) not null,
-- 	Cover varchar(max),
-- 	Venue varchar(255) not null,
-- 	DateTime datetime not null,
-- 	ParticipationLimit int
-- );

-- create table Enrollments (
-- 	EnrollmentID int Identity(1, 1) primary key,
-- 	UserID int null,
-- 	EventID int null,
-- 	constraint FK_Enrollment_User foreign key (UserID)
-- 	references Users (UserID)
-- 	on delete cascade
-- 	on update cascade,
-- 	constraint FK_Enrollment_Event foreign key (EventID)
-- 	references Events (EventID)
-- 	on delete cascade
-- 	on update cascade
-- );

-- insert into Users values (
-- 	'Gowtham',
-- 	'gowthram@gmail.com',
-- 	'testhash',
-- 	'testsalt'
-- );

-- insert into Events values (
-- 	'TechFest',
-- 	'Just a regular fest',
-- 	'Fest',
-- 	'someCover',
-- 	'Coimbatore',
-- 	convert(datetime,'18-06-12 10:34:09 PM',5),
-- 	120
-- );

-- insert into Enrollments values (
--     2,
--     1
-- );

-- select * from Enrollments;