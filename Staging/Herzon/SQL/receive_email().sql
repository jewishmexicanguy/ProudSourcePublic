/*
* Author:    Ryan Murphy
* Version:   0.1
* Last edit: 01-10-15
*
* This script is used to insert mail messages into our mail database.
*/

CREATE OR REPLACE FUNCTION receive_email(
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
		Error,
		Error_Message
	)

	VALUES(
		email_ID,
		Creation_Time,
		Origin,
		Destination,
		TRUE,  -- We already know it's inbound
		FALSE, -- ^
		Response,
		Message_UID,
		Subject,
		Body,
		FALSE,
		Application_ID, -- TODO
		Reference_ID, -- TODO
		Reference_Type, -- TODO
		Sent,
		Sent_Time, 
		FALSE, -- We just inserted it, this should be false
		now(), -- TODO This isn't right, what is the processed time when it has
				-- not been processed
		FALSE -- No error?
	)

END;
$BODY$ -- TODO What is this
  LANGUAGE plpgsql VOLATILE -- TODO What is this?
  COST 100; -- TODO What is this
ALTER FUNCTION create_email()
  OWNER TO "PSMaintStaging";
