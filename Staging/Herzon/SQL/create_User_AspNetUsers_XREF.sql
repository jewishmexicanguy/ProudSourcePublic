CREATE TABLE "Users_AspNetUsers_XREF"(
	"Users_AspNetUsers_XREF_ID" SERIAL NOT NULL,
	"User_Master_ID" INTEGER NOT NULL,
	"AspNetUsers_ID" CHARACTER VARYING NOT NULL,
	CONSTRAINT "pk_Users_AspNetUsers_XREF" PRIMARY KEY ("Users_AspNetUsers_XREF_ID"),
	CONSTRAINT "uq_Users_AspNetUsers_XREF_User_Master_ID" UNIQUE ("User_Master_ID"),
	CONSTRAINT "uq_Users_AspNetUsers_XREF_User_AspNetUsers_ID" UNIQUE ("AspNetUsers_ID")
)
WITH (
  OIDS=FALSE
);