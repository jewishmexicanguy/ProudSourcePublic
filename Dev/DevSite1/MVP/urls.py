# -*- coding: utf-8 -*-
"""
Created on Wed Aug  5 15:52:41 2015

@author: herzonflores
"""
from django.conf.urls import url

from . import views

urlpatterns = [
    url(r'^$', views.welcome, name = 'welcome'),
    url(r'^about', views.about, name = 'about'),
    url(r'^InvestorAccounts/$', views.ListAccountsView.as_view(),
        name = 'accounts-list',),
    url(r'^newInvestorAccount/$', views.CreateInvestorAccountView.as_view(),
        name='investor-account-new',),
    url(r'^login/$', 'django.contrib.auth.views.login'),
    url(r'^logout/$', 'django.contrib.auth.views.logout'),
    url(r'^Error$', views.Error404, name = '404 not found'),
    url(r'^contact', views.contact, name = 'contact'),
]
