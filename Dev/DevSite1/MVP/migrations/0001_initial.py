# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='EntreprenuerAccount',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
            ],
        ),
        migrations.CreateModel(
            name='InvestorAccount',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
            ],
        ),
        migrations.CreateModel(
            name='UserAccount',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('username', models.CharField(max_length=150)),
                ('password', models.TextField()),
            ],
        ),
        migrations.AddField(
            model_name='investoraccount',
            name='user_accountID',
            field=models.ForeignKey(to='MVP.UserAccount'),
        ),
        migrations.AddField(
            model_name='entreprenueraccount',
            name='user_accountID',
            field=models.ForeignKey(to='MVP.UserAccount'),
        ),
    ]
