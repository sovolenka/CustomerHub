-- Users Table
CREATE TABLE users (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    email TEXT NOT NULL,
    password TEXT NOT NULL
);

-- Characteristics Table
CREATE TABLE characteristics (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    product_type TEXT NOT NULL,
    category TEXT NOT NULL,
    description TEXT,
    manufacturer TEXT NOT NULL,
    country TEXT NOT NULL,
    manufacture_date TEXT, -- SQLite doesn't have a DATE type, but you can store dates as TEXT in the format 'YYYY-MM-DD'.
    characteristics_status TEXT CHECK(characteristics_status IN ('new', 'used')),
    products_id INTEGER,
    FOREIGN KEY(products_id) REFERENCES products(id) ON DELETE CASCADE
);

-- Products Table
CREATE TABLE products (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    price REAL,
    clients_id INTEGER,
    users_id INTEGER,
    characteristics_id INTEGER,
    FOREIGN KEY(clients_id) REFERENCES clients(id) ON DELETE CASCADE,
    FOREIGN KEY(users_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY(characteristics_id) REFERENCES characteristics(id) ON DELETE CASCADE
);

-- Clients Table
CREATE TABLE clients (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    first_name TEXT NOT NULL,
    second_name TEXT NOT NULL,
    third_name TEXT NOT NULL,
    phone_number TEXT UNIQUE NOT NULL,
    email TEXT NOT NULL,
    address TEXT NOT NULL,
    factory TEXT NOT NULL,
    date_added TEXT,
    client_status TEXT,
    users_id INTEGER,
    products_id INTEGER,
    reminders_id INTEGER,
    FOREIGN KEY(users_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY(products_id) REFERENCES products(id) ON DELETE CASCADE,
    FOREIGN KEY(reminders_id) REFERENCES reminders(id) ON DELETE CASCADE
);

-- Reminders Table
CREATE TABLE reminders (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    note TEXT,
    reminder TEXT,  -- SQLite doesn't have a DATETIME type. You can use TEXT in the format 'YYYY-MM-DD HH:MM:SS'.
    clients_id INTEGER,
    users_id INTEGER,
    FOREIGN KEY(clients_id) REFERENCES clients(id) ON DELETE CASCADE,
    FOREIGN KEY(users_id) REFERENCES users(id) ON DELETE CASCADE
);

