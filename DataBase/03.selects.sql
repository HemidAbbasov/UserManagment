SELECT u.id
     , u.user_name
     , u.surname
     , u.firstname
     , u.role_id
     , r.name   as role_name
  FROM users as u
       INNER JOIN roles as r ON u.role_id = r.id;

SELECT r.id
     , r.name
     , u.user_name
  FROM roles r
       LEFT JOIN users u ON r.id = u.role_id
 WHERE u.user_name IS NULL;
 

SELECT r.id
     , r.name
  FROM roles r
 WHERE NOT EXISTS (SELECT 1 FROM users u WHERE u.role_id = r.id);
 

SELECT u.id         AS user_id
     , u.user_name
     , r.id         AS role_id
     , r.name       AS role_name
  FROM users u
     , roles r;
     
SELECT r.id         AS role_id 
     , r.name       AS role_name
     , u1.user_name
  FROM roles r
       INNER JOIN (SELECT u.id, u.user_name, u.role_id 
                     FROM users u 
                    WHERE u.is_active = 0) u1 
               ON r.id = u1.role_id;
