CREATE OR REPLACE FUNCTION create_project(
    atuser_id integer,
    atentrepreneur_id integer,
    atproject_description character varying,
    atinvestment_goal numeric)
  RETURNS integer AS
$BODY$

DECLARE new_project_master_id INTEGER;

BEGIN

	INSERT INTO project_master(
		project_description,
		create_user_master_id,
		mod_user_master_id,
		create_date_time,
		entrepreneur_master_id,
		investment_goal
	)
	VALUES(
		atproject_description,
		atuser_id,
		2,
		now(),
		atentrepreneur_id,
		atinvestment_goal
	);

	SELECT CURRVAL(pg_get_serial_sequence('project_master','project_master_id'))
	INTO new_project_master_id
	FROM project_master;

	RETURN new_project_master_id;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION create_project(integer, integer, character varying, numeric)
  OWNER TO "PSMaintStaging";