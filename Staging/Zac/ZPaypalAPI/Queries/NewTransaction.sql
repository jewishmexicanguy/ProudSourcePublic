INSERT INTO Transactions (
	Account_ID, 
	Category_Type_ID, 
	Transaction_Type_ID, 
	Date_of_Transaction,
	Description,
	Amount,
	Current_Balance,	
	Transaction_State,
	Currency_Type_ID
)

VALUES ( 1, 3, 1, GETDATE(), 'Test Transaction', 1, 0, 'Pending', 2);