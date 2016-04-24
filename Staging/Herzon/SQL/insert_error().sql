/*
* Author:    Ryan Murphy
* Version:   0.1
* Last edit: 01-10-15
*
* This script is used to add error messages to emails, should an error come up
*/

CREATE OR REPLACE FUNCTION insert_error(
	email_ID SERIAL NOT NULL,
	error_message TEXT
)
$BODY$

BEGIN

	UPDATE EmailMessage
	SET Error=true, Error_Message=error_message
	WHERE Email_ID=email_ID;

END;

$BODY$ -- TODO What is this
  LANGUAGE plpgsql VOLATILE -- TODO What is this?
  COST 100; -- TODO What is this
ALTER FUNCTION insert_error()
  OWNER TO "PSMaintStaging";
