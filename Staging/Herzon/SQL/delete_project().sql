CREATE OR REPLACE FUNCTION delete_project(
    atproject_id INTEGER
)
    RETURNS CHARACTER VARYING AS
$BODY$

DECLARE return_message CHARACTER VARYING;

BEGIN

    -- We need to check to see if there are any procs that exist for this project
	
	-- Create a temporary table and select into it any PROC records that exist that are related to the project in question and that have been accepted by a project.
	-- In short find if any active PROCS tied to this project
    CREATE TEMPORARY TABLE temp_proc_active AS
    SELECT PROCM.proc_master_id
    FROM project_master PM
    JOIN proc_master PROCM
        ON PROCM.project_master_id = PM.project_master_id
    WHERE PM.project_master_id = atproject_id
		AND PROCM.project_accepted IS TRUE;
	
	-- Create a temporary table that we will select references to our project that are inactive.
	CREATE TEMPORARY TABLE temp_proc_inactive AS
	SELECT PROCM.proc_master_id
    FROM project_master PM
    JOIN proc_master PROCM
        ON PROCM.project_master_id = PM.project_master_id
    WHERE PM.project_master_id = atproject_id
		AND PROCM.project_accepted IS NOT TRUE;
		
	-- If there records on our temporrary inactive reference table then we need to remove them as well.
	FOR TPI.proc_master_id IN
		SELECT TPI.proc_master_id
		FROM temp_proc_inactive TPI
	LOOP
		DELETE FROM proc_master
		WHERE TPI.proc_master_id = proc_master_id
	END LOOP;
	
	-- If there are no records on our temp tables then delete this project
	IF (SELECT COUNT(*) FROM temp_proc_ids) < 1
		THEN
			DELETE FROM project_master
			WHERE project_master_id = atproject_id;
			
			return_message:= 'Project Deleted';
	END IF;
	
	IF (SELECT COUNT(*) FROM temp_proc_ids) > 0
		THEN
			return_message:= 'You have active PROCs tied to this project.';
	END IF;
	
	DROP TABLE  temp_proc_active;
	
	DROP TABLE temp_proc_inactive;
	
	RETURN return_message;
	
END;
$BODY$
    LANGUAGE plpgsql VOLATILE
    COST 100;