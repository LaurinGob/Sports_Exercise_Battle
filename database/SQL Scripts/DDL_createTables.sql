DELETE FROM users WHERE TRUE;

DROP TABLE history;
DROP TABLE users;

-- create users table
CREATE TABLE IF NOT EXISTS users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    passwordHash VARCHAR(60) NOT NULL,
    userELO INT DEFAULT 100,
    userToken VARCHAR(50),
    bio VARCHAR(50),
    image VARCHAR(50)
);

-- create history table with foreign key to users
CREATE TABLE  IF NOT EXISTS history (
    history_id SERIAL PRIMARY KEY,
    fk_user_id INTEGER REFERENCES users(user_id) ON DELETE CASCADE,
    entryDateTime TIMESTAMP DEFAULT current_timestamp,
    count INTEGER NOT NULL,
    duration INTERVAL NOT NULL,
    recordEntry BOOL NOT NULL
);

-- grant permissions to seb_connection for all created tables
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO seb_connection;
GRANT USAGE ON ALL SEQUENCES IN SCHEMA public TO seb_connection;

SELECT bio, image FROM users WHERE username = 'kienboec'