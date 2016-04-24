/* CRUD
we need procedures for use between applications and the group of related tables
	emailmessage
	user_emailmessage_xref
	entrepreneur_emailmessage_xref
	investor_emailmessage_xref
	project_emailmessage_xref
	proc_emailmessage_xref

CREATE we need to be able to insert records into emailmessage that are incoming emails.
		''' plpgsql function ''' */
		CREATE OR REPLACE FUNCTION insert_inbound_Email(
			atorigin CHARACTER VARYING,
			atdestination TEXT,
			atmessage_uid INTEGER,
			atsubject TEXT,
			atbody TEXT
		)
		RETURNS INTEGER AS
		$BODY$
		
		DECLARE new_email_id INTEGER;
		
		BEGIN
		
			INSERT INTO emailmessage(
				origin,
				destination,
				inbound,
				outbound,
				message_uid,
				subject,
				body
			)
			VALUES(
				atorigin,
				atdestination,
				TRUE,
				FALSE,
				atmessage_uid,
				atsubject,
				atbody
			);
			
			SELECT CURRVAL(pg_get_serial_sequence('emailmessage','email_id'))
			INTO new_email_id
			FROM emailmessage;
			
			RETURN new_email_id;
			
		END;
		$BODY$
			LANGUAGE plpgsql VOLATILE
			COST 100;
	/*  we need to insert records into emailmessage that will become outgoing emails.
		when this is done a relational record needs to be added to the kind of account that is sending the message.
		user_emailmessage_xref 1
		entrepreneur_emailmessage_xref 2
		investor_emailmessage_xref 3 
		project_emailmessage_xref 4
		proc_emailmessage_xref 5
		''' plpgsql function ''' */
		CREATE OR REPLACE FUNCTION insert_outbound_Email(
			atorigin CHARACTER VARYING,
			atdestination TEXT,
			atsubject TEXT,
			atbody TEXT,
			ataccounttype INTEGER,
			ataccountid INTEGER
		)
		RETURNS TABLE(
			email_id INTEGER,
			account_type INTEGER,
			account_xref_id INTEGER
		) AS
		$BODY$
				
		DECLARE new_email_id INTEGER;
				
		BEGIN

			INSERT INTO emailmessage(
				origin,
				destination,
				inbound,
				outbound,
				subject,
				body
			)
			VALUES(
				atorigin,
				atdestination,
				FALSE,
				TRUE,
				atsubject,
				atbody
			);
					
			-- get the id of the emailmessage we just inserted for use later.
			SELECT CURRVAL(pg_get_serial_sequence('emailmessage','email_id'))
			INTO new_email_id
			FROM emailmessage;
				
			-- lets create a temp table to store what will happen in our if .. then statements
			CREATE TEMPORARY TABLE temp_relids(
				email INTEGER,
				accounttype INTEGER,
				accountxref INTEGER
			);
				
			-- cases where ataccounttype goes from 1 .. 5, this will change what table a relation is added to for this new email entry
			IF ataccounttype = 1
			THEN
			-- insert a record into our user_emailmessage_xref table
				INSERT INTO user_emailmessage_xref(
					user_id,
					emailmessage_id
				)
				VALUES(
					ataccountid,
					new_email_id
				);
						
				-- insert a record into our temp table temp_relids
				INSERT INTO temp_relids(
					email,
					accounttype,
					accountxref
				)
				VALUES(
					new_email_id,
					ataccounttype,
					(SELECT CURRVAL(pg_get_serial_sequence('user_emailmessage_xref','user_email_xref_id')))
				);
			END IF;
					
			IF ataccounttype = 2
			THEN
			-- insert a record into our entrepreneur_emailmessage_xref table
				INSERT INTO entrepreneur_emailmessage_xref(
					entrepreneur_id,
					emailmessage_id
				)
				VALUES(
					ataccountid,
					new_email_id
				);

				-- insert a record into our temp table temp_relids
				INSERT INTO temp_relids(
					email,
					accounttype,
					accountxref
				)
				VALUES(
					new_email_id,
					ataccounttype,
					(SELECT CURRVAL(pg_get_serial_sequence('entrepreneur_emailmessage_xref','entrepreneur_email_xref_id')))
				);
			END IF;
			
			IF ataccounttype = 3
			THEN
			-- insert a record into our investor_emailmessage_xref table
				INSERT INTO investor_emailmessage_xref(
					investor_id,
					emailmessage_id
				)
				VALUES(
					ataccountid,
					new_email_id
				);

				-- insert a record into our temp table temp_relids
				INSERT INTO temp_relids(
					email,
					accounttype,
					accountxref
				)
				VALUES(
					new_email_id,
					ataccounttype,
					(SELECT CURRVAL(pg_get_serial_sequence('investor_emailmessage_xref','investor_email_xref_id')))
				);
			END IF;
					
			IF ataccounttype = 4
			THEN
			-- insert a record into our project_emailmessage_xref table
				INSERT INTO project_emailmessage_xref(
					project_id,
					emailmessage_id
				)
				VALUES(
					ataccountid,
					new_email_id
				);

				-- insert a record into our temp table temp_relids
				INSERT INTO temp_relids(
					email,
					accounttype,
					accountxref
				)
				VALUES(
					new_email_id,
					ataccounttype,
					(SELECT CURRVAL(pg_get_serial_sequence('project_emailmessage_xref','project_email_xref_id')))
				);
			END IF;
					
			IF ataccounttype = 5
			THEN
			-- insert a record into our proc_emailmessage_xref table
				INSERT INTO proc_emailmessage_xref(
					proc_id,
					emailmessage_id
				)
				VALUES(
					ataccountid,
					new_email_id
				);

				-- insert a record into our temp table temp_relids
				INSERT INTO temp_relids(
					email,
					accounttype,
					accountxref
				)
				VALUES(
					new_email_id,
					ataccounttype,
					(SELECT CURRVAL(pg_get_serial_sequence('proc_emailmessage_xref','proc_email_xref_id')))
				);
			END IF;
					
			RETURN QUERY
			SELECT email, accounttype, accountxref
			FROM temp_relids;
					
			DROP TABLE temp_relids;
		END;
		$BODY$
			LANGUAGE plpgsql VOLATILE
			COST 100;
	
/*	READ
	we need to select out records from this table that will become emails to be send out.
	''' query ''' */
	SELECT email_id, creation_time, origin, destination, subject, body
	FROM emailmessage 
	WHERE outbound IS TRUE 
		AND processed IS NOT TRUE
		AND error IS NOT TRUE
	
	/*
	we need to select the largest UID for email messages on our emailmessage table to keep track of our mailbox.
	''' query ''' */
	SELECT email_id, creation_time, message_uid 
	FROM emailmessage
	WHERE message_uid = (
		SELECT MAX(message_uid) 
		FROM emailmessage
	);
	
	/*
	we need to be able to select a single record from our emailmessage table using the primary key; this will be used during message processing.
	this query will require that the parameter @Id has an integer value.
	''' query ''' */
	SELECT email_id, creation_time, message_uid, origin, destination, inbound, outbound, subject, body 
	FROM emailmessage
	WHERE email_id = @Id; 
	
/* UPDATE
	we need to update emails that have been inserted as processed.
		when they are processed we will need to also add a relation to the aprropriate account
		user_emailmessage_xref
		entrepreneur_emailmessage_xref
		investor_emailmessage_xref
		project_emailmessage_xref
		proc_emailmessage_xref
		''' query ''' */
		CREATE OR REPLACE FUNCTION update_processed_inbound_email(
			IN atemailid integer,
			IN ataccounttype integer,
			IN ataccountid integer)
		  RETURNS INTEGER AS
		$BODY$
				
		DECLARE new_email_account_xref_id INTEGER;
				
		BEGIN
			-- Update this email record in our emailmessage table.
			UPDATE emailmessage
			SET sent = FALSE,
				processed = TRUE,
				processed_time = now()
			WHERE email_id = atemailid;
				
			-- cases where ataccounttype goes from 1 .. 5, this will change what table a relation is added to for this new email entry
			IF ataccounttype = 1
			THEN
			-- insert a record into our user_emailmessage_xref table
				INSERT INTO user_emailmessage_xref(
					user_id,
					emailmessage_id
				)
				VALUES(
					ataccountid,
					atemailid
				);
						
				-- get the xref id from user_emailmessage_xref
				SELECT CURRVAL(pg_get_serial_sequence('user_emailmessage_xref','user_email_xref_id'))
				INTO new_email_account_xref_id;
				
			END IF;
					
			IF ataccounttype = 2
			THEN
			-- insert a record into our entrepreneur_emailmessage_xref table
				INSERT INTO entrepreneur_emailmessage_xref(
					entrepreneur_id,
					emailmessage_id
				)
				VALUES(
					ataccountid,
					atemailid
				);

				-- get the xref id from entrepreneur_emailmessage_xref
				SELECT CURRVAL(pg_get_serial_sequence('entrepreneur_emailmessage_xref','entrepreneur_email_xref_id'))
				INTO new_email_account_xref_id;
				
			END IF;
			
			IF ataccounttype = 3
			THEN
			-- insert a record into our investor_emailmessage_xref table
				INSERT INTO investor_emailmessage_xref(
					investor_id,
					emailmessage_id
				)
				VALUES(
					ataccountid,
					atemailid
				);

				-- get the xref id from investor_emailmessage_xref
				SELECT CURRVAL(pg_get_serial_sequence('investor_emailmessage_xref','investor_email_xref_id'))
				INTO new_email_account_xref_id;
				
			END IF;
					
			IF ataccounttype = 4
			THEN
			-- insert a record into our project_emailmessage_xref table
				INSERT INTO project_emailmessage_xref(
					project_id,
					emailmessage_id
				)
				VALUES(
					ataccountid,
					atemailid
				);

				-- get the xref id from project_emailmessage_xref
				SELECT CURRVAL(pg_get_serial_sequence('project_emailmessage_xref','project_email_xref_id'))
				INTO new_email_account_xref_id;
				
			END IF;
					
			IF ataccounttype = 5
			THEN
			-- insert a record into our proc_emailmessage_xref table
				INSERT INTO proc_emailmessage_xref(
					proc_id,
					emailmessage_id
				)
				VALUES(
					ataccountid,
					atemailid
				);

				-- get the xref id from proc_emailmessage_xref
				SELECT CURRVAL(pg_get_serial_sequence('proc_emailmessage_xref','proc_email_xref_id'))
				INTO new_email_account_xref_id;
			END IF;

			RETURN new_email_account_xref_id;
			
		END;
		$BODY$
		  LANGUAGE plpgsql VOLATILE
		  COST 100
		
	/* we need to update emailmessages as an error where something goes wrong 
	''' query ''' */
	CREATE OR REPLACE FUNCTION update_email_error(
		atemailid INTEGER,
		aterrormsg TEXT
	)
	RETURNS VOID AS
	$BODY$

	BEGIN
		-- update our email record with error text and set the error bit column to TRUE for the appropriate record.
		UPDATE emailmessage
		SET error = TRUE,
		error_message = aterrormsg,
		processed = FALSE
		WHERE email_id = atemailid;

	END;
	$BODY$
		LANGUAGE plpgsql VOLATILE
		COST 100;
	
	/*
	we need to update records that are emails that have been sent out as sent.
	''' query ''' */
	CREATE OR REPLACE FUNCTION update_sent_outbound_email(
		atemailid INTEGER
	)
	RETURNS VOID AS
	$BODY$

	BEGIN
		-- update our email record as sent, add the timestamp and set processed to true and it's timestamp.
		UPDATE emailmessage
		SET sent = TRUE,
			sent_time = now(),
			processed = TRUE,
			processed_time = now()
			WHERE email_id = atemailid;

	END;
	$BODY$
		LANGUAGE plpgsql VOLATILE
		COST 100;
	
/*  DELETE
	I don't think we will be deleting records actually
	*/