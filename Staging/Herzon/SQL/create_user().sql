CREATE OR REPLACE FUNCTION create_user(
	atemail CHARACTER VARYING
)
  RETURNS integer AS
$BODY$

DECLARE new_user_id INTEGER;

BEGIN

	INSERT INTO user_master ( 
		email 
	) 
	VALUES ( 
		atemail 
	);

	SELECT CURRVAL(pg_get_serial_sequence('user_master','user_master_id'))
	INTO new_user_id
	FROM user_master;
	
	--Insert a new record into our "Users_AspNetUsers_XREF" table using the GUID from our dbo."AspNetUsers" by using our atemail parameter.
	INSERT INTO "Users_AspNetUsers_XREF"(
		"User_Master_ID",
		"AspNetUsers_ID"
	) 
	VALUES (
		new_user_id,
		(
			SELECT "Id" 
			FROM dbo."AspNetUsers" 
			WHERE "UserName" = atemail
		)
	);
	
	RETURN new_user_id;

END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;