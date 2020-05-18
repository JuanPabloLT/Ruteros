using Ruteros.Common.Enums;
using Ruteros.Web.Data.Entities;
using Ruteros.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruteros.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Marcos", "Del Rio", "marcosdelrio25@gmail.com", "350 634 2747", UserType.Admin);
            await CheckUserAsync("3030", "Juan Pablo", "Londoño Tobon", "pablo18970@gmail.com", "319 627 1487", UserType.Admin);
            var driver = await CheckUserAsync("2020", "Marcos", "Del Rio", "marcosdaviddelrio@hotmail.com", "350 634 2747", UserType.Driver);
            var driver2 = await CheckUserAsync("4040", "Marcos", "Del Rio", "marcosdel244642@correo.itm.edu.co", "350 634 2747", UserType.Driver);
            var driver3 = await CheckUserAsync("5050", "Juan Pablo", "Londoño Tobon", "pablo18970@hotmail.com", "319 627 1487", UserType.Driver);
            //await CheckTaxisAsync(driver, user1, user2);
        }

        private async Task<UserEntity> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Driver.ToString());
        }

    }

}
