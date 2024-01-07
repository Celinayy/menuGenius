DROP DATABASE IF EXISTS menu_genius;

CREATE DATABASE menu_genius
	CHARACTER SET utf8mb4
	COLLATE utf8mb4_hungarian_ci;

USE menu_genius;

-- tables
-- Table: allergen
CREATE TABLE allergen (
    id int NOT NULL AUTO_INCREMENT,
    code dec(3,1)  NOT NULL,
    name varchar(30)  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: ingredient
CREATE TABLE ingredient (
    id int  NOT NULL AUTO_INCREMENT,
    name varchar(100)  NOT NULL,
    inStock decimal(4,2)  NOT NULL,
    qUnit varchar(10)  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: ingredient_allergen
CREATE TABLE ingredient_allergen (
    ingredientId int  NOT NULL,
    allergenId int  NOT NULL,
    FOREIGN KEY (ingredientId) REFERENCES ingredient(id),
    FOREIGN KEY (allergenId) REFERENCES allergen(id),
    CONSTRAINT PRIMARY KEY (ingredientId, allergenId)
);

-- Table: category
CREATE TABLE category (
    id int  NOT NULL AUTO_INCREMENT,
    name varchar(20)  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: product
CREATE TABLE product (
    id int  NOT NULL AUTO_INCREMENT,
    name varchar(255) NOT NULL,
    description text NULL,
    categoryId int NOT NULL,
    packing varchar(20) NOT NULL,
    price int  NOT NULL,
    isFood bool  NOT NULL,
    image varchar(50)  NOT NULL,
    FOREIGN KEY (categoryId) REFERENCES category(id),
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: product_ingredient
CREATE TABLE product_ingredient (
    productId int  NOT NULL,
    ingredientId int  NOT NULL,
    FOREIGN KEY (productId) REFERENCES product(id),
    FOREIGN KEY (ingredientId) REFERENCES ingredient(id),
    CONSTRAINT PRIMARY KEY (productId, ingredientId)
);

-- Table: desk
CREATE TABLE desk (
    id int  NOT NULL AUTO_INCREMENT,
    numberOfSeats int  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: reservation
CREATE TABLE reservation (
    id int  NOT NULL AUTO_INCREMENT,
    numberOfGuests int  NOT NULL,
    arrivalTime timestamp  NOT NULL,
    getawayTime timestamp  NULL,
    name varchar(50)  NOT NULL,
    phone varchar(20)  NOT NULL,
    deskId int  NOT NULL,
    FOREIGN KEY (deskId) REFERENCES desk(id),
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: user
CREATE TABLE user (
    id int  NOT NULL AUTO_INCREMENT,
    name varchar(50)  NOT NULL,
    email varchar(30)  NOT NULL,
    password text  NOT NULL,
    phone varchar(20)  NOT NULL,
    admin bool  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: order
CREATE TABLE purchase (
    id int  NOT NULL AUTO_INCREMENT,
    date timestamp  NOT NULL,
    totalPay int  NOT NULL,
    status enum('ordered', 'cooked', 'served')  NOT NULL,
    paid bool  NOT NULL,
    userId int  NULL,
    deskId int  NOT NULL,
    FOREIGN KEY (userId) REFERENCES user(id),
    FOREIGN KEY (deskId) REFERENCES desk(id),
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: product_purchase
CREATE TABLE product_purchase (
    id int  NOT NULL AUTO_INCREMENT,
    productId int  NOT NULL,
    purchaseId int  NOT NULL,
    quantity int  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

CREATE TABLE eventLog (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    eventType VARCHAR(30) NOT NULL,
    affectedTable VARCHAR(30) NOT NULL,
    affectedId INT NOT NULL,
    event text,
    date datetime NOT NULL,
    userId int NOT NULL,
    FOREIGN KEY (userId) REFERENCES user(id)
);