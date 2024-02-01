DROP DATABASE IF EXISTS menugenius;

CREATE DATABASE menugenius
	CHARACTER SET utf8mb4
	COLLATE utf8mb4_hungarian_ci;

USE menugenius;

-- tables
-- Table: allergen
CREATE TABLE allergens (
    id bigint NOT NULL AUTO_INCREMENT,
    code dec(3,1)  NOT NULL,
    name varchar(30)  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: ingredient
CREATE TABLE ingredients (
    id bigint  NOT NULL AUTO_INCREMENT,
    name varchar(100)  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: ingredient_allergen
CREATE TABLE ingredient_allergen (
    ingredient_id bigint  NOT NULL,
    allergen_id bigint  NOT NULL,
    FOREIGN KEY (ingredient_id) REFERENCES ingredients(id),
    FOREIGN KEY (allergen_id) REFERENCES allergens(id),
    CONSTRAINT PRIMARY KEY (ingredient_id, allergen_id)
);

-- Table: category
CREATE TABLE categories (
    id bigint  NOT NULL AUTO_INCREMENT,
    name varchar(20)  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: product
CREATE TABLE products (
    id bigint  NOT NULL AUTO_INCREMENT,
    name varchar(255) NOT NULL,
    description text NULL,
    category_id int NOT NULL,
    packing varchar(20) NOT NULL,
    price int  NOT NULL,
    is_food bool  NOT NULL,
    image varchar(50)  NOT NULL,
    FOREIGN KEY (category_id) REFERENCES categories(id),
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: product_ingredient
CREATE TABLE product_ingredient (
    product_id bigint  NOT NULL,
    ingredient_id bigint  NOT NULL,
    FOREIGN KEY (product_id) REFERENCES products(id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredients(id),
    CONSTRAINT PRIMARY KEY (product_id, ingredient_id)
);

-- Table: desk
CREATE TABLE desks (
    id bigint  NOT NULL AUTO_INCREMENT,
    number_of_seats int  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: reservation
CREATE TABLE reservations (
    id bigint  NOT NULL AUTO_INCREMENT,
    number_of_guests int  NOT NULL,
    checkin_date datetime  NOT NULL,
    checkout_date datetime  NULL,
    name varchar(50)  NOT NULL,
    phone varchar(20)  NOT NULL,
    desk_id bigint  NOT NULL,
    user_id bigint NULL,
    closed bool DEFAULT 0,
    comment  text NULL,
    FOREIGN KEY (desk_id) REFERENCES desks(id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: user
CREATE TABLE users (
    id bigint  NOT NULL AUTO_INCREMENT,
    name varchar(50)  NOT NULL,
    email varchar(30)  NOT NULL,
    email_verified_at datetime DEFAULT NULL
    password text  NOT NULL,
    phone varchar(20)  NOT NULL,
    admin bool  NOT NULL,
    remember_token varchar(100) DEFAULT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: product_user
CREATE TABLE product_user (
    id bigint NOT NULL AUTO_INCREMENT,
    product_id bigint NOT NULL,
    user_id bigint NOT NULL,
    favorite bool NOT NULL,
    stars int NULL,
    FOREIGN KEY (product_id) REFERENCES products(id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: order
CREATE TABLE purchases (
    id bigint  NOT NULL AUTO_INCREMENT,
    date_time datetime  NOT NULL,
    total_pay int  NOT NULL,
    status enum('ordered', 'cooked', 'served')  NOT NULL,
    paid bool  NOT NULL,
    user_id int  NULL,
    desk_id int  NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (desk_id) REFERENCES desks(id),
    CONSTRAINT PRIMARY KEY (id)
);

-- Table: product_purchase
CREATE TABLE product_purchase (
    id bigint  NOT NULL AUTO_INCREMENT,
    product_id int  NOT NULL,
    purchase_id int  NOT NULL,
    quantity int  NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);

CREATE TABLE event_logs (
    id bigint NOT NULL AUTO_INCREMENT PRIMARY KEY,
    event_type VARCHAR(30) NOT NULL,
    affected_table VARCHAR(30) NOT NULL,
    affected_id int NOT NULL,
    event text,
    date datetime NOT NULL,
    user_id int NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE images (
    id bigint NOT NULL AUTO_INCREMENT,
    img_name varchar(50) NOT NULL,
    img_data longblob NOT NULL,
    CONSTRAINT PRIMARY KEY (id)
);