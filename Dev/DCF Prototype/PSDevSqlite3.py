'''
This is the first file of code begining for proudsource development.
This will consist of some objects that setup, manipulate and execute Sqlite3 Data Base commands.
'''
import sqlite3

'''
<summary>
This class serves as a connection base class meant to be inherited by erived types, it provides minimal access to some information details of DataBase PSDev.db
</summary>
'''
class ProudSourceConnect:
    def __init__(self):
        self.conn = sqlite3.connect('PSDev.db')
        self.c = self.conn.cursor()
        
    def get_Tables(self):
        # the * in the sql statement is a shortcut for get all columns from each row on this table
        # the table in this query stores all of the tables that exist in our Data Base
        for i in self.c.execute("SELECT * FROM sqlite_master"):
            print i
            
    def get_TempTables(self):
        # the table in this query shows all temporary tables that are in our Data Base
        for i in self.c.execute("SELECT * FROM sqlite_temp_master"):
            print i
         
'''
<summary>
This class inherits base class object ProudSourceConnect and exposes methods to retrive rows from table [Users] and the schema for table [Users]
</summary>
'''
class ProudSourceQuery(ProudSourceConnect):
    def get_UsersData(self):
        # this sql query will retrive all rows from our table Users and order them from last entry to first by the Id column
        for i in self.c.execute("SELECT * FROM Users ORDER BY Id DESC"):
            print i
            
    def get_UsersTableSchema(self):
        # this sql query is how to retrive and see the columns that exist for a table
        for i in self.c.execute("PRAGMA table_info(Users)"):
            print i

'''
<summary>
This class inherits base class object ProudSourceConnect and exposes a method for inserting dictionaries with specific keys into our tables
</summary>
'''
class ProudSourceInsert(ProudSourceConnect):
    def insert_User(self, dictionary):
        # define a list created out of accessing the values from our dictionary, this gives us the advantage of strongly defined refferences to data. Although numbers/indexes can be used key names are much easier to work with than numbers and as such is best practice and is easier to maintain and read
        values = [dictionary['FirstName'], dictionary['LastName'], dictionary['CreateTime'], dictionary['Confidence']]
        # feed our created ordered list into our table as this method call is overloaded to accept either one field or two fields.
        # when accepting two fields the function expects the second field to be the values related to the query string deffined or a list of values that are the values to be used by the sql command.
        # the question marks in the sql command are called parameters and are almost similar to variables in programing languages but diffirent.
        # when programatically inserting/updating rows into a table always use parameterization. It is best practice as it helps mitigate against sql injection attacks
        self.c.execute("INSERT INTO Users(FirstName, LastName, CreateTime, Confidence) VALUES (?,?,?,?)", values)
        self.conn.commit()

'''
<summary>
This class inherits base class object ProudSourceConnect and exposes methods that directly modify tables in our Data Base PSDev.db
A small not on the DELETE, DROP, TRUNCATE and CREATE commands. It is best practice to never impliment methods or functions that actually drop tables within any kind of production software
these methods are here for education and functional purposes so you can delete and create a table really easily and also to become comfortable with the syntax used in sqlite3
</summary>
'''
class ProudSourceAdminister(ProudSourceConnect):
    def delete_Table_Users(self):
        # this query will drop our table Users, effectivley deleting everything on that table and also making that table no loner an obect in our Data Base.
        # Don't ever drop tables unless you know what you are doing or are developing Data Base structures in a sandbox Data Base enviornment
        self.c.execute("DROP TABLE Users")
        self.conn.commit()
    
    def delete_rows_Users(self):
        # this query will delete rows from our table Users and clear it but the table is still there
        for i in self.c.execute("DELETE FROM Users"):
            print i
        self.conn.commit()
            
    def create_Table_Users(self):
        # this query will create table Users with a few constraints; it disallows the Id to not be null and also be unique. It also specifies which columns cannot be null values, which in this case is all of them
        for i in self.c.execute("CREATE TABLE Users (Id INTEGER PRIMARY KEY NOT NULL UNIQUE, FirstName TEXT NOT NULL, LastName TEXT NOT NULL, CreateTime TEXT NOT NULL, Confidence REAL)"):
            print i
        self.conn.commit()
            
example1 = {'FirstName': 'Marcus', 'LastName': 'Aurelious', 'CreateTime': '06-19-2015 07:11:546', 'Confidence': 3.0 }
example2 = {'FirstName': 'Herzon', 'LastName': 'Flores', 'CreateTime': '03-07-1991 04:24:500', 'Confidence': 2.0}
# instantiate class object
a = ProudSourceQuery()
# use instantiated object method to do something we have already defined
print "Select all the tables in our Data Base"
a.get_Tables()

print "Select Users table Schema"
a.get_UsersTableSchema()


# instantiate object
b = ProudSourceInsert()

# use methods of called object
# this call will insert a dictionary that has already been deffined
print "inserting a row using a dictionary"
b.insert_User(example1)
# test to see if a record was added to our tablr out of consuming the dictionary we placed in the previous method call
print "testing to see if our record was inserted"
a.get_UsersData()
# call method to insert another dictionary
print "inserting another row using a dictionary"
b.insert_User(example2)
# test to see if that record was added
print "testing to see if this row was also added to our table"
a.get_UsersData()

# Instantiate object
print "calling DB administrator object"
c = ProudSourceAdminister()
# call method that affects the DataBase
print "deleting rows on our Users table"
c.delete_rows_Users()
# Test to see if tables were affected
print "testing our delete command"
a.get_UsersData()
# call to delete our table
print "deleting table Users"
'''
making a function that deletes tables is never a good idea, this is only here for educational and demostrational purposes.
for the love of GOD do not drop tables in a production enviornment or a testing enviornment, only do that in a development enviornment.
'''
c.delete_Table_Users()
# call to see if tables in our Data Base have changed
print "testing to see if tables were deleted"
a.get_Tables()
# call method to create our table again
print "creating tables"
c.create_Table_Users()
# call method, this should return no results but it should not error out
print "testing to see if our table is back"
a.get_UsersData()
# insert our two dictionaries again
print "inserting dictionaries again"
b.insert_User(example1)
b.insert_User(example2)
# test to see that dictionaries were entered into our table
print "testing to see if dictionaries were added"
a.get_UsersData()