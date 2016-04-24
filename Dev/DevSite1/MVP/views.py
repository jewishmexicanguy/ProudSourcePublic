from django.shortcuts import render
from django.http import HttpResponse, Http404
from django.template import RequestContext, loader
from django.contrib.auth.decorators import login_required
from django.utils.decorators import method_decorator
from django.views.generic import CreateView
from django.views.generic.list import ListView
from django.core.urlresolvers import reverse
from .models import InvestorAccount

# Create your views here.

def welcome(request):
    '''
    template = loader.get_template('MVP/welcome.html')
    context = RequestContext(request)
    return HttpResponse(template.render(context))
    '''
    return render(request, 'MVP/welcome.html')

def about(request):
    return render(request, 'MVP/about.html')

def contact(request):
    return render(request, 'MVP/contact.html')

def Error404(request):
    '''
    '''
    raise Http404("You broke it!")

class LoggedInMixin(object):
    '''
    '''
    @method_decorator(login_required)
    def dispatch(self, *args, **kwargs):
        return super(LoggedInMixin, self).dispatch(*args, **kwargs)

class ListAccountsView(LoggedInMixin, ListView):
    '''
    '''
    model = InvestorAccount
    template_name = 'accounts_dasboard.html'

    def get_queryset(self):
        '''
        '''
        return InvestorAccount.objects.filter(owner=self.request.user)

class CreateInvestorAccountView(LoggedInMixin, CreateView):
    '''
    '''
    model = InvestorAccount
    template_name = 'edit_InvestorAccount.html'

    def get_success_url(self):
        return reverse('accounts-list')
