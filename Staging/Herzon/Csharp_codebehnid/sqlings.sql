--SELECT * FROM entrepreneur_master

-- ALTER TABLE entrepreneur_master 
-- ALTER entrepreneur_image DROP NOT NULL

-- ALTER TABLE entrepreneur_master 
-- ALTER mod_user_id DROP NOT NULL

-- select now()

-- ALTER TABLE investor_master
-- ALTER mod_user_id DROP NOT NULL

SELECT * FROM user_master

-- SELECT * FROM entrepreneur_master E WHERE E.entrepreneur_master_id = @UserID
-- 
-- SELECT * FROM investor_master I WHERE I.investor_master_id = @UserID
-- 
-- INSERT INTO entrepreneur_master ( user_master_id, create_date_time ) VALUES ( @UserID, now() )
-- 
-- INSERT INTO investor_master ( user_master_id, create_date_time ) VALUES ( @UserID, now() )

INSERT INTO investor_master ( user_master_id, create_date_time ) VALUES ( 2, now() )

INSERT INTO entrepreneur_master ( user_master_id, create_date_time ) VALUES ( 2, now() )