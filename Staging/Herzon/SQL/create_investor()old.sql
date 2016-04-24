/*
* Author: Rahul Kanwar
* This is the stored procedure that was originally made by Rahul for the ProudSource DataBase
* It is here for historical and reference purposes
*/


-- Function: create_investor()

-- DROP FUNCTION create_investor();

CREATE OR REPLACE FUNCTION create_investor()
  RETURNS integer AS
$BODY$

DECLARE new_investor_master_id integer;
DECLARE new_user_master_id integer;
DECLARE new_investor_verified boolean;
DECLARE new_investor_public boolean;
DECLARE new_investor_image bytea;
DECLARE new_investor_image2 bytea;
DECLARE new_investor_document bytea;
DECLARE new_investor_document2 bytea;
DECLARE new_bitcoin_wallet_master_id integer;
DECLARE new_bank_account_master_id integer;
DECLARE new_success_percentage_to_date numeric (5,2);
DECLARE new_total_invested_to_date integer;
DECLARE new_mod_date_time timestamp with time zone;
DECLARE new_create_date_time timestamp with time zone;
DECLARE new_mod_user_master_id integer;
DECLARE new_create_user_master_id integer;


BEGIN
 new_user_master_id:= 18;
 new_investor_verified:= true;
 new_investor_public:= true;
 new_investor_image:= NULL;
 new_investor_image2:= NULL;
 new_investor_document:= NULL;
 new_investor_document2:= NULL;
 new_bitcoin_wallet_master_id:= 13579;
 new_bank_account_master_id:= NULL;
 new_success_percentage_to_date:= NULL;
 new_total_invested_to_date:= 0;
 new_mod_date_time:= now();
 new_create_date_time:= now();
 new_mod_user_master_id:= 16;
 new_create_user_master_id:=16;
 
    INSERT INTO investor_master(
	user_master_id,
	investor_verified,
	investor_public,
	investor_image,
	investor_image2,
	investor_document,
	investor_document2,
	bitcoin_wallet_master_id,
	bank_account_master_id,
	success_percentage_to_date,
	total_invested_to_date,
	mod_date_time,
	mod_user_master_id,
	create_date_time,
	create_user_master_id
    )
values (
  new_user_master_id,
  new_investor_verified,
  new_investor_public,
  new_investor_image,
  new_investor_image2,
  new_investor_document,
  new_investor_document2,
  new_bitcoin_wallet_master_id,
  new_bank_account_master_id,
  new_success_percentage_to_date,
  new_total_invested_to_date,
  new_mod_date_time,
  new_mod_user_master_id,
  new_create_date_time,
  new_create_user_master_id);

select MAX(investor_master_id) into new_investor_master_id
from investor_master;

RETURN new_investor_master_id;


END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION create_investor()
  OWNER TO "PSMaintStaging";
