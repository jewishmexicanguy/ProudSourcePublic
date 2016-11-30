using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace ProudSourcePrime.Models
{
    public class UserStore<T> : IUserStore<T> where T : ApplicationUser
    {
        System.Threading.Tasks.Task IUserStore<T>.CreateAsync(T user)
        {
            // Register New User
        }

        System.Threading.Tasks.Task IUserStore<T>.DeleteAsync(T user)
        {
            // Delete User
        }

        System.Threading.Tasks.Task IUserStore<T>.FindByIdAsync(string userId)
        {
            // Find user by Id
        }

        System.Threading.Tasks.Task IUserStore<T>.FindByNameAsync(string userName)
        {
            // Find user by name (email)
        }

        System.Threading.Tasks.Task IUserStore<T>.UpdateAsync(T user)
        {
            // Update User profile
        }

        void IDisposable.Dispose()
        {

        }
    }
}