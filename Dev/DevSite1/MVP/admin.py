from django.contrib import admin

from .models import UserAccount, InvestorAccount, EntreprenuerAccount
# Register your models here.

admin.site.register(UserAccount)
admin.site.register(InvestorAccount)
admin.site.register(EntreprenuerAccount)
