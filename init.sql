CREATE DATABASE IF NOT EXISTS database_blog CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE database_blog;


DROP TABLE IF EXISTS Posts;
DROP TABLE IF EXISTS Categories;
DROP TABLE IF EXISTS Users;


CREATE TABLE Categories (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL UNIQUE,
    Description VARCHAR(500)
) CHARACTER SET utf8mb4;

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(100) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash LONGTEXT NOT NULL,
    Role LONGTEXT NOT NULL,
    CreatedAt DATETIME(6) NOT NULL
) CHARACTER SET utf8mb4;

CREATE TABLE Posts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(200) NOT NULL,
    Content LONGTEXT NOT NULL,
    CreatedAt DATETIME(6) NOT NULL,
    UpdatedAt DATETIME(6) NOT NULL,
    Tag LONGTEXT,
    CategoryId INT NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE RESTRICT,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE RESTRICT
) CHARACTER SET utf8mb4;

INSERT INTO Categories (Id, Name, Description) VALUES
    (1, 'Technology', 'Posts about technology and software development'),
    (2, 'Design', 'Posts about design and user experience'),
    (3, 'AI', 'Posts about artificial intelligence and machine learning');
