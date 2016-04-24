/*
* Author: Herzon Flores
* This is a modified version of the create_investor() function created by Rahul.
* Modifications here have been made that make this script perform only it's	
* essential duties of creating a new Investor profile and associating it with 
* the user who initiated it's creation.
* 10-17-2015 18:31
*/

CREATE OR REPLACE FUNCTION create_investor(user_master_id INTEGER, new_investor_profile_name CHARACTER VARYING(255))
  RETURNS INTEGER AS
$BODY$

DECLARE new_investor_master_id INTEGER;

BEGIN
 
    INSERT INTO investor_master(
		user_master_id,
		investor_public,
		create_date_time,
		investor_profile_name,
		mod_user_master_id,
		investor_active
    )
	
	VALUES(
		user_master_id,
		FALSE,
		now(),
		new_investor_profile_name,
		2,
		TRUE
	);

	SELECT MAX(investor_master_id) 
	INTO new_investor_master_id
	FROM investor_master;

	RETURN new_investor_master_id;


END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION create_investor()
  OWNER TO "PSMaintStaging";