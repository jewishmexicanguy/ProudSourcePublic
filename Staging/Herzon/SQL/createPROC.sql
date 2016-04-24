CREATE OR REPLACE FUNCTION create_proc(
	atuser_master_id INTEGER,
	atproject_id INTEGER,
	atproc_begin_date TIMESTAMP WITH TIME ZONE,
	atproc_end_date TIMESTAMP WITH TIME ZONE,
	atprofit_percentage NUMERIC(4,2),
	atamount_invested INTEGER,
	atinvestor_id INTEGER
)
 RETURNS TABLE(
	proc_id INTEGER,
	proc_investor_xref_id INTEGER
	) AS
$BODY$

DECLARE new_PROC_id INTEGER;
DECLARE new_PROC_entrepreneur_xref_id INTEGER;

BEGIN
	-- Insert a record into our proc_master table
	INSERT INTO proc_master(
		create_user_master_id, 
		proc_begin_date, 
		proc_end_date, 
		profit_percentage, 
		payment_interval_id, 
		create_date_time, 
		amount_invested, 
		project_master_id, 
		project_accepted, 
		expired, 
		mod_user_master_id
	)
	VALUES(
		atuser_master_id,
		atproc_begin_date,
		atproc_end_date,
		atprofit_percentage,
		1,
		now(),
		atamount_invested,
		atproject_id,
		FALSE,
		FALSE,
		2
	);

	-- get the id of the record that was just inserted.
	SELECT currval(pg_get_serial_sequence('proc_master','proc_master_id'))
	INTO new_PROC_id
	FROM entrepreneur_master;

	-- insert a record into our proc_investor_xref table using the id we just got from the previous record insertion.
	INSERT INTO proc_investor_xref(
		proc_master_id, 
		investor_master_id, 
		investor_accepted, 
		mod_user_master_id
	) 
	VALUES(
		new_PROC_id, 
		atinvestor_id, 
		TRUE, 
		2
	);

	-- get the id of the record that we just inserted.
	SELECT currval(pg_get_serial_sequence('proc_investor_xref','proc_investor_xref_id'))
	INTO new_PROC_entrepreneur_xref_id
	FROM proc_investor_xref;

	CREATE TEMPORARY TABLE temp_relids(
		procid INTEGER,
		procinvestorid INTEGER
	);
	
	-- create a table to house the results we will query out of the end of this stored procedure.
	INSERT INTO temp_relids(
		procid,
		procinvestorid
	)
	VALUES (
		new_PROC_id,
		new_PROC_entrepreneur_xref_id
	);
	
	RETURN QUERY
	SELECT procid, procinvestorid
	FROM temp_relids;
	
	DROP TABLE temp_relids;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;