CREATE DATABASE IF NOT EXISTS miappdb;
CREATE USER IF NOT EXISTS 'miappuser'@'%' IDENTIFIED BY 'miapppassword';
GRANT ALL PRIVILEGES ON miappdb.* TO 'miappuser'@'%';
FLUSH PRIVILEGES;