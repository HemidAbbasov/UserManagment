-----------------------
-- TABLE: ROLES
-- DESC: Таблица(справочник) ролей пользователей
-----------------------
--DROP TABLE IF EXISTS roles;
CREATE TABLE roles (
   id   INTEGER           NOT NULL
                          PRIMARY KEY AUTOINCREMENT
 , name TEXT              NOT NULL
);

----------------------
-- TABLE: USERS
-- DESC: Таблица пользователей
----------------------
--DROP TABLE IF EXISTS users;
CREATE TABLE users (
   id             INTEGER            NOT NULL
                                     PRIMARY KEY AUTOINCREMENT
 , user_name      VARCHAR            NOT NULL
 , surname        VARCHAR            NOT NULL
 , firstname      VARCHAR            NOT NULL
 , patronymic     VARCHAR
 , email          VARCHAR            NOT NULL
 , birth_date     DATETIME2  
 , sex            CHAR
 , mobile_number  VARCHAR            NOT NULL
 , role_id        INTEGER            NOT NULL
 , deleted        TINYINT            NOT NULL DEFAULT 0
 , is_active      TINYINT            NOT NULL DEFAULT 0
 , CONSTRAINT fk_users_roles FOREIGN KEY (role_id) REFERENCES roles(id)
);

CREATE INDEX idx_users_role_id ON users(role_id);
CREATE INDEX idx_users_user_name ON users(user_name);
CREATE INDEX idx_users_deleted ON users(deleted);
CREATE INDEX idx_users_is_active ON users(is_active);