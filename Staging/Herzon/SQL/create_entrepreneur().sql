/*
* Author: Herzon Flores
* This is a modified version of the create_entrepreneur() function created by Rahul.
* Modifications here have been made that make this script perform only it's	
* essential duties of creating a new entrepreneur profile and associating it with 
* the user who initiated it's creation.
* 10-21-2015 02:26
*/

CREATE OR REPLACE FUNCTION create_entrepreneur(user_master_id INTEGER, new_entrepreneur_profile_name CHARACTER VARYING(255))
  RETURNS INTEGER AS
$BODY$

DECLARE new_entrepreneur_master_id INTEGER;

BEGIN
 
    INSERT INTO entrepreneur_master(
		user_master_id,
		entrepreneur_public,
		create_date_time,
		entrepreneur_profile_name,
		mod_user_master_id,
		entrepreneur_active
    )
	
	VALUES(
		user_master_id,
		FALSE,
		now(),
		new_entrepreneur_profile_name,
		2,
		TRUE
	);

	SELECT MAX(entrepreneur_master_id) 
	INTO new_entrepreneur_master_id
	FROM entrepreneur_master;

	RETURN new_entrepreneur_master_id;


END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION create_entrepreneur()
  OWNER TO "PSMaintStaging";