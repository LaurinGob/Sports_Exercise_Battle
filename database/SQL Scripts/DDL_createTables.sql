-- delete all users > this cascades to history
DELETE FROM users WHERE TRUE;

-- drop all views/tables
DROP VIEW IF EXISTS get_stats;
DROP VIEW IF EXISTS get_score;
DROP VIEW IF EXISTS get_history;
DROP TABLE IF EXISTS history;
DROP TABLE IF EXISTS users;

-- create users table
CREATE TABLE IF NOT EXISTS users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    passwordHash VARCHAR(60) NOT NULL,
    userELO INT DEFAULT 100,
    userToken VARCHAR(50),
    bio VARCHAR(50) DEFAULT '',
    image VARCHAR(50) DEFAULT '',
    profileName VARCHAR(50) DEFAULT ''
);

-- create history table with foreign key to users
CREATE TABLE IF NOT EXISTS history (
    history_id SERIAL PRIMARY KEY,
    fk_user_id INTEGER REFERENCES users(user_id) ON DELETE CASCADE,
    entryDateTime TIMESTAMP DEFAULT current_timestamp,
    count INTEGER NOT NULL,
    duration INTERVAL NOT NULL,
    recordEntry BOOL NOT NULL
);

-- create view for elo and total count (intended for individual user)
CREATE VIEW get_stats AS
SELECT u.username, u.userelo, COALESCE(SUM(h.count),0) AS totalcount FROM users AS u
LEFT JOIN history AS h ON u.user_id = h.fk_user_id
GROUP BY 1, 2 ORDER BY 2, 3 DESC;

-- create view for elo and total count of all users
CREATE VIEW get_score AS
SELECT u.profileName, u.userelo, COALESCE(SUM(h.count),0) AS totalcount FROM users AS u
LEFT JOIN history AS h ON u.user_id = h.fk_user_id
GROUP BY 1, 2 ORDER BY 2, 3 DESC;

-- create view for history entries
CREATE VIEW get_history AS
SELECT u.username, h.count, h.duration FROM users AS u
INNER JOIN history AS h ON u.user_id = h.fk_user_id
ORDER BY 2, 3 DESC;

-- grant permissions to seb_connection for all created tables
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO seb_connection;
GRANT USAGE ON ALL SEQUENCES IN SCHEMA public TO seb_connection;