# ERP Driving_School
Тема: разработка системы формирования рассписания практических занятий в автошколе<br>
Разработчик: Вороненков Игорь ИП-20-3
-----
Данный проект предназначен для автоматизации составления рассписания практических занятий в автошколе.
В проекте присутствует база данных состоящая из следующих таблиц:
  - TTransports - таблица, хрянящаяя в себе данные о транспортных средствах, используемых на занятиях;
  - TPlaces - таблица, хрянящаяя в себе данные о площадках для практических занятий;
  - TCourses - таблица, хрянящаяя в себе данные о курсах обучения, предоставляемых автошколой;
  - TPersons - таблица, хрянящаяя в себе данные о персонах (людях), например ФИО или номер телефона;
  - TEmployees - таблица, хрянящаяя в себе данные о всех работниках автошколы (инструкторов) и обучающихся (студентов);
  - TLessons - таблица, хрянящаяя в себе данные о практических занятиях;
-----

# Ниже представлена ER-диаграмма базы данных:
```mermaid
erDiagram
    TTransports ||--o{ TLessons : FK_Transports
    TTransports {
        guid Id PK "not null"
        nvarchar Name "not null"
        nvarchar Number "not null"
        int GSBType "not null"
        datetimeoffset CreatedAt "not null"
        nvarchar CreatedBy "not null"
        datetimeoffset UpdatedAt "not null"
        nvarchar UpdatedBy "not null"
        datetimeoffset DeletedAd "null"
    }
    TPlaces ||--o{ TLessons : FK_Places
    TPlaces {
        guid Id PK "not null"
        nvarchar Name "not null"
        nvarchar Description "null"
        nvarchar Address "not null"
        datetimeoffset CreatedAt "not null"
        nvarchar CreatedBy "not null"
        datetimeoffset UpdatedAt "not null"
        nvarchar UpdatedBy "not null"
        datetimeoffset DeletedAd "null"
    }
    TCourses ||--o{ TLessons : FK_Courses
    TCourses {
        guid Id PK "not null"
        nvarchar Name "not null"
        nvarchar Description "not null"
        int Duration "not null"
        decimal Price "not null"
        datetimeoffset CreatedAt "not null"
        nvarchar CreatedBy "not null"
        datetimeoffset UpdatedAt "not null"
        nvarchar UpdatedBy "not null"
        datetimeoffset DeletedAd "null"
    }
    TPersons ||--o{ TEmployees : FK_Person
    TPersons {
        guid Id PK "not null"
        nvarchar LastName "not null"
        nvarchar FirstName "not null"
        nvarchar Patronymic "null"
        datetime DateOfBirthday "not null"
        nvarchar Passport "not null"
        nvarchar Phone "not null"
        datetimeoffset CreatedAt "not null"
        nvarchar CreatedBy "not null"
        datetimeoffset UpdatedAt "not null"
        nvarchar UpdatedBy "not null"
        datetimeoffset DeletedAd "null"
    }
    TEmployees ||--o{ TLessons : FK_Instructor
    TEmployees ||--o{ TLessons : FK_Student
    TEmployees {
        guid Id PK "not null"
        guid PersonId FK "not null"
        int EmployeeType "not null"
        nvarchar Email "not null"
        int Experience "not null"
        nvarchar Number "not null"
        datetimeoffset CreatedAt "not null"
        nvarchar CreatedBy "not null"
        datetimeoffset UpdatedAt "not null"
        nvarchar UpdatedBy "not null"
        datetimeoffset DeletedAd "null"
    }
    TLessons {
        guid Id PK "not null"
        datetimeoffset StartDate "not null"
        datetimeoffset EndDate "not null"
        guid PlaceId FK "not null"
        guid StudentId FK "not null"
        guid TransportId FK "not null"
        guid InstructorId FK "not null"
        guid CourceId FK "not null"
        datetimeoffset CreatedAt "not null"
        nvarchar CreatedBy "not null"
        datetimeoffset UpdatedAt "not null"
        nvarchar UpdatedBy "not null"
        datetimeoffset DeletedAd "null"
    }
```
-----
# SQL-скрипт по добавлению данных:
```
--Заполнение таблицы "Курсы"
INSERT INTO TCourses ([Id],[Name],[Description],[Duration],[Price],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt]) 
		VALUES ('807288A6-58A0-4704-A69D-52B5877AA098', 'Базовый', 'Базовый курс с длительность 4 мес', 70, 1950.0, SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
INSERT INTO TCourses ([Id],[Name],[Description],[Duration],[Price],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt]) 
		VALUES ('0B7C3EAE-725F-4D58-A9F3-F2279D2D5560', 'Ускоренный', 'Курс с длительность 2 мес с онлайн обучением', 50, 2580.0, SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);

--Заполнение таблицы "Площадки"
INSERT INTO TPlaces([Id],[Name],[Description],[Address],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('033F029F-6875-414A-90A9-FFE15E296457', 'Площадка типа А', 'Площадка для обучения езде на мототранспорте', 'СПБ, ул. Известная', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
INSERT INTO TPlaces([Id],[Name],[Description],[Address],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('D044721C-96AD-40D7-9BB4-DBEFA9626009', 'Площадка типа Б', 'Площадка для обучения езде на автотранспорте', 'СПБ, ул. Шаврова', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);

--Заполнение таблицы "Транспорт"
INSERT INTO TTransports ([Id],[Name],[Number],[GSBType],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('A3D2137D-598F-4056-8FFF-184ED69DA107', 'Kia Rio', 'сп045н178', 0, SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
INSERT INTO TTransports ([Id],[Name],[Number],[GSBType],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('BBB0F631-97DF-4188-9453-466AF36030E9', 'Toyota Camry', 'га359р198', 1, SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);

--Заполнение таблицы "Персоны"
INSERT INTO TPersons ([Id],[LastName],[FirstName],[Patronymic],[DateOfBirthday],[Passport],[Phone],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('F3894E44-B365-49BB-B165-2BFF526E1586', 'Иванов', 'Иван', 'Иванович', '2000-12-22', '00 00 123456', '89111234567', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
INSERT INTO TPersons ([Id],[LastName],[FirstName],[Patronymic],[DateOfBirthday],[Passport],[Phone],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('0E3A4BC7-C0C1-4383-90F3-AF9FB7677772', 'Петров', 'Петр', 'Васильевич', '1995-09-13', '09 87 654321', '89215244506', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
INSERT INTO TPersons ([Id],[LastName],[FirstName],[Patronymic],[DateOfBirthday],[Passport],[Phone],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('BCA7C2C3-B63B-44CA-86E7-6BA4305688B8', 'Кинин', 'Сергей', 'Алексеевич', '1987-03-22', '10 75 353178', '89512258010', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
INSERT INTO TPersons ([Id],[LastName],[FirstName],[Patronymic],[DateOfBirthday],[Passport],[Phone],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('8AB4ED5A-8697-4895-BF4B-E9DD3523E446', 'Коноплев', 'Анатолий', 'Александрович', '1979-08-01', '11 42 754611', '88005553535', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);

--Заполнение таблицы "Работники"
INSERT INTO TEmployees ([Id],[PersonId],[EmployeeType],[Email],[Experience],[Number],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('17D9020F-85A8-439B-8525-99C4683320F4', 'BCA7C2C3-B63B-44CA-86E7-6BA4305688B8', 0, 'kinin@mail.ru', 15, '4090-23', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
INSERT INTO TEmployees ([Id],[PersonId],[EmployeeType],[Email],[Experience],[Number],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('49955362-C7DD-4ED2-9B53-A812B41886F0', '0E3A4BC7-C0C1-4383-90F3-AF9FB7677772', 0, 'petrov@mail.ru', 12, '4091-23', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
INSERT INTO TEmployees ([Id],[PersonId],[EmployeeType],[Email],[Experience],[Number],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('4C0AC38D-E880-4D2E-B052-98A9BFBCDC14', 'F3894E44-B365-49BB-B165-2BFF526E1586', 1, 'ivanov@mail.ru', 0, 'student', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
INSERT INTO TEmployees ([Id],[PersonId],[EmployeeType],[Email],[Experience],[Number],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('913639EB-EC19-4CFA-8EC6-C29753D76ACB', '8AB4ED5A-8697-4895-BF4B-E9DD3523E446', 1, 'konoplev@mail.ru', 0, 'student', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);

--Заполнение таблицы "Занятия"
INSERT INTO TLessons ([Id],[StartDate],[EndDate],[PlaceId],[StudentId],[TransportId],[InstructorId],[CourceId],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('3859F951-D541-461A-A0A0-F96BBA77A519', '2023-12-25 12:00:00', '2023-12-25 13:00:00', 'D044721C-96AD-40D7-9BB4-DBEFA9626009', '913639EB-EC19-4CFA-8EC6-C29753D76ACB',
				'BBB0F631-97DF-4188-9453-466AF36030E9', '17D9020F-85A8-439B-8525-99C4683320F4', '0B7C3EAE-725F-4D58-A9F3-F2279D2D5560', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
INSERT INTO TLessons ([Id],[StartDate],[EndDate],[PlaceId],[StudentId],[TransportId],[InstructorId],[CourceId],[CreatedAt],[CreatedBy],[UpdatedAt],[UpdatedBy],[DeletedAt])
		VALUES ('C8A56613-0B53-49B5-B70D-DA480BDE0B63', '2023-12-25 13:00:00', '2023-12-25 15:00:00', 'D044721C-96AD-40D7-9BB4-DBEFA9626009', '4C0AC38D-E880-4D2E-B052-98A9BFBCDC14',
				'A3D2137D-598F-4056-8FFF-184ED69DA107', '49955362-C7DD-4ED2-9B53-A812B41886F0', '807288A6-58A0-4704-A69D-52B5877AA098', SYSDATETIMEOFFSET(), SYSTEM_USER, SYSDATETIMEOFFSET(), SYSTEM_USER, null);
```
-----
# Пример графика учебной езды для ученика:
![График учебной езды](https://github.com/Lendaar/Driving_School/assets/106811879/84766bbd-2b55-46c9-9f2e-2acbfe369d31)


