CREATE DATABASE LOGINSYSTEM

CREATE TABLE ACCOUNTS_
(
 USERNAME NVARCHAR(30) NOT NULL,
 PASSWORD NVARCHAR(30) NOT NULL,
 BIOGRAPHY NVARCHAR(150) NULL,
)

ALTER TABLE ACCOUNTS_
ADD GENDER NCHAR(1) NULL

ALTER TABLE ACCOUNTS_
ADD CONSTRAINT AC_GENDER CHECK (GENDER BETWEEN 'M' AND 'F')

ALTER TABLE ACCOUNTS_
ADD CONSTRAINT DF_GENDER DEFAULT 'M' FOR GENDER 

CREATE TABLE PROFILEIMAGE
(
 USERNAME NVARCHAR(30) NULL,
 PROFILE_IMAGE IMAGE NOT NULL,
 IMAGE_NAME NVARCHAR(MAX) NULL,
 IMAGE_SIZE NVARCHAR(MAX) NULL,
 TIME_POSTED NVARCHAR(MAX) NULL,
 IMAGE_TYPE NVARCHAR(7) NULL
)

ALTER TABLE ACCOUNTS_
DROP CONSTRAINT DF_GENDER

ALTER TABLE ACCOUNTS_
DROP CONSTRAINT AC_GENDER