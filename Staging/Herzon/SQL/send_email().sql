/*
* Author:    Ryan Murphy
* Version:   0.1
* Last edit: 01-10-15
*
* This script is used to add emails that we send to the database
*/

CREATE OR REPLACE FUNCTION send_email(
	email_ID SERIAL NOT NULL,
	Creation_Time TIMESTAMP WITH TIME ZONE,
	Origin CHARACTER VARYING(255),
	Destination TEXT,
	Response BOOLEAN,
	Message_UID TEXT,
	Subject TEXT,
	Body TEXT,
	Sent BOOLEAN,
	Sent_Time TIMESTAMP WITH TIME ZONE
)
$BODY$

BEGIN

	INSERT INTO EmailMessage(
		Email_ID,
		Creation_Time,
		Origin,
		Destination,
		Inbound,
		Outbound,
		Response,
		Message_UID,
		Subject,
		Body,
		Application_Generated,
		Application_ID,
		Reference_ID,
		Reference_Type,
		Sent,
		Sent_Time,
		Processed,
		Processed_Time,
		Error,
		Error_Message
	)

	VALUES(
		email_ID,
		now(),
		Origin,
		Destination,
		FALSE,  -- We already know it's outbound
		TRUE, -- ^
		Response,
		Message_UID,
		Subject,
		Body,
		TRUE,
		Application_ID, -- TODO
		Reference_ID, -- TODO
		Reference_Type, -- TODO
		Sent,
		now(), 
		TRUE, -- We just inserted it, this should be false
		now(),
		FALSE -- No error?
	)

END;

$BODY$ -- TODO What is this
  LANGUAGE plpgsql VOLATILE -- TODO What is this?
  COST 100; -- TODO What is this
ALTER FUNCTION send_email()
  OWNER TO "PSMaintStaging";
