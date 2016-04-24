# -*- coding: utf-8 -*-
"""
Created on Sat Jun 20 22:37:34 2015

@author: herzonflores
<Info Doc>
    <summary>
        Python Class objects that manage core Data Base structures for "Proud Source".
    </summary>
    
    class PSDev_Connect
    base object that connects to 'Proud_Source_Dev.db' and that is inherited by derived Data Base classes.

        Constructor method __init__(), defines a connection property and a cursor property for this object
    
    class PSDevDB_Manage
    inherits PSDev_Connect, this class houses all methods that affect the Data Base tables. 

        method CreatSchema(), creates tables that will form the foundation of ProudSource backend
    
        method DeleteSchema(), drops all of the tables in our data base. Only use if you do not care about your tables and whatever data may be inside of them.
    
        method SelectSchema(), prints out the table names and the schema that is used by each table
    
</Info Doc>
"""
import sqlite3 as sql
'''
class PSDev_Connect
    base object that connects to 'Proud_Source_Dev.db' and that is inherited by derived Data Base classes.
'''
class PSDevDB_Connect:
    '''
    Constructor method __init__(), defines a connection property and a cursor property for this object
    '''
    def __init__(self):
        self.conn = sql.connect("Proud_Source_Dev.db")
        self.c = self.conn.cursor()

'''
class PSDevDB_Manage
    inherits PSDev_Connect, this class houses all methods that affect the Data Base tables. 
'''
class PSDevDB_Manage(PSDevDB_Connect):
    def CreateSchema(self):
        '''
        method CreatSchema(), creates tables that will form the foundation of ProudSource backend
        '''
        # Create table Users
        self.conn.executescript('''
            CREATE TABLE Users(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [FirstName] TEXT NOT NULL,
                [LastName] TEXT NOT NULL,
                [CreationDate] TEXT NOT NULL,
                [LastLogIn] TEXT
            )
        ''')
        print "Created table Users"       
        
        # Create table Projects        
        self.conn.executescript('''
            CREATE TABLE Projects(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [ProjectName] TEXT NOT NULL,
                [CreationDate] TEXT NOT NULL,
                [Active] INTEGER NOT NULL,
                [ExpireDate] TEXT,
                [Description] BLOB
            )
        ''')
        print "Created table Projects"        
        
        # Create table ProjectComments
        self.conn.executescript('''
            CREATE TABLE ProjectComments(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [ProjectId] INTEGER NOT NULL,
                [Comment] TEXT NOT NULL,
                [CreationDate] TEXT NOT NULL,
                FOREIGN KEY ([ProjectID]) REFERENCES Projects([Id])
            )
        ''')
        print "Created table ProjectComments"
        
        # Create table Contacts
        self.conn.executescript('''
            CREATE TABLE Contacts(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [Email] TEXT NOT NULL,
                [IsPrimary] INTEGER NOT NULL
            )
        ''')
        print "Created table Contacts"
        
        # Create table Users_Contacts_XREF
        self.conn.executescript('''
            CREATE TABLE Users_Contacts_XREF(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [UserID] INTEGER NOT NULL,
                [ContactID] INTEGER NOT NULL,
                FOREIGN KEY ([UserID]) REFERENCES Users([Id]),
                FOREIGN KEY ([ContactID]) REFERENCES Contacts([Id])
            )
        ''')
        print "Created table Users_Contacts_XREF"
        
        # Create table EntrepreneurProfile
        self.conn.executescript('''
            CREATE TABLE EntrepreneurProfile(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [UserID] INTEGER NOT NULL,
                [InventorBlurb] TEXT,
                [InventorPicture] BLOB,
                FOREIGN KEY ([UserID]) REFERENCES Users([Id])               
            )
        ''')
        print "Created table EntrepreneurProfile"
        
        # Create table EntrepreneurProfile_Projects_XREF
        self.conn.executescript('''
            CREATE TABLE EntrepreneurProfile_Projects_XREF(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [UserID] INTEGER NOT NULL,
                [ProjectID] INTEGER NOT NULL,
                FOREIGN KEY ([UserID]) REFERENCES EntrepreneurProfile([Id]),
                FOREIGN KEY ([ProjectID]) REFERENCES Projects([Id])
            )
        ''')
        print "Created table EntrepreneurProfile_Projects_XREF"
        
        # Create table InvestorProfile
        self.conn.executescript('''
            CREATE TABLE InvestorProfile(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [IsAnonymous] INTEGER NOT NULL
            )        
        ''')
        print "Created table InvestorProfile"
        
        # Create table Users_InvestorProfile_XREF
        self.conn.executescript('''
            CREATE TABLE Users_InvestorProfile_XREF(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [InvestorId] INTEGER NOT NULL,
                [UserId] INTEGER NOT NULL,
                FOREIGN KEY ([UserId]) REFERENCES Users([Id]),
                FOREIGN KEY ([InvestorId]) REFERENCES InvestorProfile([Id])
            )
        ''')
        print "Created table Users_InvestorProfile_XREF"
        
        # Create table UserLogin
        self.conn.executescript('''
            CREATE TABLE UserLogin(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [PassHash] BLOB NOT NULL,
                [Email] TEXT NOT NULL
            )        
        ''')
        print "Created table UserLogin"
        
        # Create table UserLogin_Users_XREF
        self.conn.executescript('''
            CREATE TABLE UserLogin_Users_XREF(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [UserId] INTEGER NOT NULL,
                [UserLoginId] INTEGER NOT NULL,
                FOREIGN KEY ([UserId]) REFERENCES Users([Id]),
                FOREIGN KEY ([UserLoginId]) REFERENCES UserLogin([Id])                    
            )     
        ''')        
        print "Created table UserLogin_Users_XREF"
        
        # Create table Accounts        
        self.conn.executescript('''
            CREATE TABLE Accounts(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [CreationDate] TEXT NOT NULL,
                [Total] REAL NOT NULL,
                [AccessedDate] TEXT NOT NULL
            )
        ''')
        print "Created table Accounts"
        
        # Create table Accounts_User_XREF
        self.conn.executescript('''
            CREATE TABLE Accounts_User_XREF(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [UserId] INT NOT NULL,
                [AccountId] INT NOT NULL,
                FOREIGN KEY ([UserId]) REFERENCES Users([Id]),
                FOREIGN KEY ([AccountId]) REFERENCES Accounts([Id])
            )        
        ''')
        
        # Creat table Unit
        self.conn.executescript('''
            CREATE TABLE Unit(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [UnitSymbol] TEXT NOT NULL
        ''')
        print "Created Table Unit"
        
        # Create table Transactions
        self.conn.executescript('''
            CREATE TABLE Transactions(
                [Id] INTEGER PRIMARY KEY NOT NULL UNIQUE,
                [AccountId] INT NOT NULL,
                [UserId] INT NOT NULL,
                [UnitID] INT NOT NULL,
                [TransactionDate] TEXT NOT NULL,
                [UnitAmmount] REAL NOT NULL,
                FOREIGN KEY ([AccountId]) REFERENCES Accounts([Id]),
                FOREIGN KEY ([UserId]) REFERENCES Users([Id]), 
                FOREIGN KEY ([UnitId]) REFERENCES Unit([Id])                       
            )        
        ''')
        print "Created table Transactions"
        
        # commit changes        
        self.conn.commit()
        
    def DeleteSchema(self):
        '''
        method DeleteSchema(), drops all of the tables in our data base. Only use if you do not care about your tables and whatever data may be inside of them.
        '''
            
        ListDrop = []
        for i in self.c.execute("SELECT [tbl_name] FROM sqlite_master WHERE [type] = 'table'"):
            ListDrop.append(str(i)[2:-2])
        
        for i in ListDrop:
            self.c.execute("DROP TABLE " + i)
            self.conn.commit()
            print "Dropped table " + i[1:-1]
        
    def SelectSchema(self):
        '''
        method SelectSchema(), prints out the table names and the schema that is used by each table
        '''
        # get a list of tuples by querying the master table to get the table names.     
        tables = []
        for i in self.c.execute("SELECT [tbl_name] FROM sqlite_master WHERE [type] = 'table'"):
            tables.append(i)
        # iterate over list of tables and retrive the schema for those tables, store results in a list of tuples.
        results = []
        for i in tables:
            columns = []
            # we have to masage how sqlite3 gives back results as tuples instead of as strings
            for k in self.c.execute("PRAGMA table_info("+ str(i)[2:-2] +")"):
                columns.append(k)
            results.append(columns)
        # Display the list by printing to output
        j = -1
        for i in tables:
            #print "Table " + i[0] + " has columns []"
            print "-" * 15 + "| " + i[0] + " |" + "-" * 15
            j = j + 1
            for k in results[j]:
                print "|----> ["+ k[2] + "] : " + k[1]
            print "-" * (34 + len(i[0]))
    