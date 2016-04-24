CREATE OR REPLACE FUNCTION delete_proc(
    atproc_id INTEGER
)
    RETURNS CHARACTER VARYING AS
$BODY$

DECLARE return_message CHARACTER VARYING;
DECLARE xref_id INTEGER;

BEGIN

    -- There should only be one relation on our proc_investor_xref table to one row on our proc_master table.    

    -- get the proc_investor_xref_id that is related to this proc from proc_master_id
    SELECT proc_master_id
    INTO xref_id 
    FROM proc_investor_xref WHERE proc_investor_xred_id = atproc_id;

    -- delete any references that exist for this proc on table proc_invesotr_xref    
    DELETE FROM proc_investor_xref WHERE proc_investor_xref_id = xref_id;

    -- delete this proc from table proc_master
    DELETE FROM proc_master WHERE proc_master_id = atproc_id;

    return_message:= 'PROC has been deleted.';    

    RETURN return_message;

END;
$BODY$
    LANGUAGE plpgsql VOLATILE
    COST 100;