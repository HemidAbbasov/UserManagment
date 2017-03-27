-----------------------
-- TABLE: ROLES
-- DESC: Таблица(справочник) ролей пользователей
-----------------------
--DROP TABLE IF EXISTS roles;
CREATE TABLE roles (
   id   INTEGER           NOT NULL
                          PRIMARY KEY AUTOINCREMENT
 , name VARCHAR2(50)       NOT NULL
);

----------------------
-- TABLE: USERS
-- DESC: Таблица пользователей
----------------------
--DROP TABLE IF EXISTS users;
CREATE TABLE users (
   id             INTEGER            NOT NULL
                                     PRIMARY KEY AUTOINCREMENT
 , user_name      VARCHAR2(20)       NOT NULL
 , surname        VARCHAR2(50)       NOT NULL
 , firstname      VARCHAR2(50)       NOT NULL
 , patronymic     VARCHAR2(50)       
 , email          VARCHAR2(50)       NOT NULL
 , birth_date     DATETIME2  
 , sex            CHAR(1)       
 , mobile_number  VARCHAR2(17)       NOT NULL
 , role_id        INTEGER            NOT NULL
 , deleted        INTEGER(1)         NOT NULL DEFAULT 0
 , is_active      INTEGER(1)         NOT NULL DEFAULT 0
 , CONSTRAINT fk_users_roles FOREIGN KEY (role_id) REFERENCES roles(id)
);

CREATE INDEX idx_users_role_id ON users(role_id);
CREATE INDEX idx_users_user_name ON users(user_name);
CREATE INDEX idx_users_deleted ON users(deleted);
CREATE INDEX idx_users_is_active ON users(is_active);