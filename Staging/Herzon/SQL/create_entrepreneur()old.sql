/*
* Author: Rahul Kanwar
* This is the stored procedure that was originally made by Rahul for the ProudSource DataBase
* It is here for historical and reference purposes
*/

-- Function: create_entrepreneur()

-- DROP FUNCTION create_entrepreneur();

CREATE OR REPLACE FUNCTION create_entrepreneur()
  RETURNS integer AS
$BODY$

DECLARE new_entrepreneur_master_id integer;
DECLARE new_user_master_id integer;
DECLARE new_entrepreneur_verified boolean;
DECLARE new_entrepreneur_public boolean;
DECLARE new_entrepreneur_image bytea;
DECLARE new_entrepreneur_image2 bytea;
DECLARE new_entrepreneur_document bytea;
DECLARE new_entrepreneur_document2 bytea;
DECLARE new_entrepreneur_document3 bytea;
DECLARE new_mod_date_time timestamp with time zone;
DECLARE new_create_date_time timestamp with time zone;
DECLARE new_mod_user_master_id integer;
DECLARE new_create_user_master_id integer;


BEGIN
 new_user_master_id:= 15;
 new_entrepreneur_verified:= false;
 new_entrepreneur_public:=true;
 new_entrepreneur_image:= NULL;
 new_entrepreneur_image2:= NULL;
 new_entrepreneur_document:= NULL;
 new_entrepreneur_document2:= NULL;
 new_entrepreneur_document3:= NULL;
 new_mod_date_time:= now();
 new_create_date_time:= now();
 new_mod_user_master_id:= 13;
 new_create_user_master_id:=13;
 
    INSERT INTO entrepreneur_master(
	user_master_id,
	entrepreneur_verified,
	entrepreneur_public,
	entrepreneur_image,
	entrepreneur_image2,
	entrepreneur_document,
	entrepreneur_document2,
	entrepreneur_document3,
	mod_date_time,
	mod_user_master_id,
	create_date_time,
	create_user_master_id
    )
values (
  new_user_master_id,
  new_entrepreneur_verified,
  new_entrepreneur_public,
  new_entrepreneur_image,
  new_entrepreneur_image2,
  new_entrepreneur_document,
  new_entrepreneur_document2,
  new_entrepreneur_document3,
  new_mod_date_time,
  new_mod_user_master_id,
  new_create_date_time,
  new_create_user_master_id);

select MAX(entrepreneur_master_id) into new_entrepreneur_master_id
from entrepreneur_master;

RETURN new_entrepreneur_master_id;


END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION create_entrepreneur()
  OWNER TO "PSMaintStaging";
