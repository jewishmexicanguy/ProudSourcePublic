/*
* Author:    Ryan Murphy
* Version:   0.1
* Last edit: 01-10-15
*
* This script is used to change emails from not processed to processed
*/

CREATE OR REPLACE FUNCTION process_email(
	email_ID SERIAL NOT NULL,
)
$BODY$

BEGIN

	-- TODO is this right?
	UPDATE  EmailMessage
	SET Processed = true
	WHERE Email_ID=email_ID && Processed == false

END;

$BODY$ -- TODO What is this
  LANGUAGE plpgsql VOLATILE -- TODO What is this?
  COST 100; -- TODO What is this
ALTER FUNCTION process_email()
  OWNER TO "PSMaintStaging";
