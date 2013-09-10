IF NOT EXISTS (SELECT * FROM hibernate_unique_key)
  INSERT INTO hibernate_unique_key (next_hi) VALUES (1)