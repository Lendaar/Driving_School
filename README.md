# ERP Driving_School

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
        guid PersonId "not null"
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
