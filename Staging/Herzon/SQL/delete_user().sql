CREATE OR REPLACE FUNCTION delete_user(
	atuserid INTEGER
) RETURNS VOID AS
$BODY$

BEGIN

	-- We must first try and see if this reccord is still related to records that are still active.

	-- select some rows that may be related to this row from investors accounts.

	-- entrepreneur
	
	--Update the row on our user table and set the column
	UPDATE user_master
	SET user_delayed = TRUE
	WHERE user_master_id = atuserid;
	
END;
$BODY$
	LANGUAGE plpgsql VOLATILE
	COST 100;