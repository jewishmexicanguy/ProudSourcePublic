# -*- coding: utf-8 -*-
"""
Created on Tue Jun 23 04:36:44 2015

@author: herzonflores
"""

from PSDevDB_Manage import PSDevDB_Connect, PSDevDB_Manage

class PSDev_Inserter(PSDevDB_Connect):
    def CreateUser(self, UserDATA):
        '''
        Insert a new User and LoginUser records and associate both of them to each other.
        <parameter> 
            dict with keys "FirstName", "LastName", "CreationDate", "LastLogIn", "Email", "Password"
            and has no empty values        
        </parameter>
        '''
        validated = False
        length = len(UserDATA)
        verified_length = 0
        keys = ["FirstName", "LastName", "CreationDate", "LastLogIn", "Email", "Password"]
        
        if (isinstance(UserDATA, dict)):
            # check dictionary keys to make  sure dictionary recived is the correct kind of dictionary
            for i in keys:
                if (i in UserDATA.keys()):
                    verified_length = verified_length + 1
                    continue
                
                else:
                    return
                    
        if (length == verified_length + 1):
            validated = True
        
        if (validated):
            # Insert User
            a = [UserDATA["FirstName"], UserDATA["LastName"], UserDATA["CreationDate"], UserDATA["LastLogIn"]]
            self.c.execute("INSERT INTO Users([FirstName], [LastName], [CreationDate], [LastLogIn]) VALUES(?,?,?,?)", a)
            
            # get row id of User inserted            
            userID = self.conn.lastrowid()
            
            # Insert UserLogin
            b = [UserDATA["Email"], hash(UserDATA["Password"])]
            self.c.execute("INSERT INTO UserLogin([Email], [PassHash]) VALUES(?,?)", b)
            
            # Get row id of UserLogin inserted
            user_loginID = self.conn.lastrowid()
            
            # Insert UserID and UserLogin into UserLogin_Users_XREF table as a record
            c = [userID, user_loginID]
            self.c.execute("INSERT INTO UserLogin_Users_XREF([UserId], [UserLoginId]) VALUES(?,?)", c)
            
            return
            
        else:
            return
            
    def CreateAccount(self, AccountDATA):
        '''
        Insert a new Account, associate to User
        '''
        for i in AccountDATA:
            print i
            
    def CreateTransaction(self, TransDATA):
        '''
        Insert a new Transaction, tie to an Account
        '''
        for i in TransDATA:
            print i
            
    def AddUser_Entrepreneur(self, EntrepreneurDATA):
        '''
        Insert a new Entrepreneur, associate to a User
        '''
        for i in EntrepreneurDATA:
            print i
            
    def CreateProject(self, ProjectDATA):
        '''
        Insert a new Project, associate to an Entrepreneur
        '''
        for i in ProjectDATA:
            print i
            
    def AddProjectComment(self, CommentDATA):
        '''
        Insert a new Project comment to a Project
        '''
        for i in CommentDATA:
            print i
            
    def AddContact(self, ContactDATA):
        '''
        Insert a new Contact to a User
        '''
        for i in ContactDATA:
            print i
            
    def AddUser_Investor(self, InvestorDATA):
        '''
        Insert a new Investor, associate to a User
        '''
        for i in InvestorDATA:
            print i
            
    def Reset_LoginPassword(self, LoginUserDATA):
        '''
        Reset LoginUser password
        '''
        for i in LoginUserDATA:
            print i
            
a = PSDevDB_Manage()
a.SelectSchema()