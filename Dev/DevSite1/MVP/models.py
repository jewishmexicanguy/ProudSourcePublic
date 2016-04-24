from django.db import models
# This line below will import the uers tble that is used for Django's authorization system
from django.contrib.auth.models import User

# Create your models here.
class UserAccount(models.Model):
    '''
    This class is supposed to represent a base user class that wuld be used to authenticate into our webiste
    '''
    username = models.CharField(max_length = 150)
    password = models.TextField()
    
class InvestorAccount(models.Model):
    '''
    This class models the entity ownership of an investor account owned by a particular user account.
    It appears as though it will be easier to relate this to the Django Auth users table
    '''
    #user_accountID = models.ForeignKey('UserAccount')
    owner = models.ForeignKey(User)
    investor_name = models.CharField(max_length = 255,)
    date_created = models.DateTimeField(auto_now_add = True)
    accessed_date = models.DateTimeField(auto_now = True)
    
    def __str__(self):
        
        return '.'.join([
            self.owner,
            self.investor_name,
        ])
    
    
class EntreprenuerAccount(models.Model):
    '''
    This class models the entity ownership of an entreprenuer account owned by a particular user account
    It appears as though it will be easier to relate this to the Django Auth users table
    '''
    #user_accountID = models.ForeignKey('UserAccount')
    owner = models.ForeignKey(User)
    entreprenuer_name = models.CharField(max_length = 255,)
    date_created = models.DateTimeField(auto_now_add = True)
    accessed_date = models.DateTimeField(auto_now = True)
    
    def __str__(self):
        
        return '.'.join([
            self.owner,
            self.entreprenuer_name,
        ])
