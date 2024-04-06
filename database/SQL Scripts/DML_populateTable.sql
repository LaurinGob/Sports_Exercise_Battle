INSERT INTO users (username, password) VALUES ('test', '123');
INSERT INTO history (fk_user_id, count, duration, recordentry) VALUES (1, 0, '2 Minutes 23 Seconds', false);

DELETE FROM user WHERE TRUE;