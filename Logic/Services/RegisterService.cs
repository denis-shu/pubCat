using Bolt.Data;
using Bolt.Models;
using Bolt.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Logic.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly ApplicationDbContext db;

        public RegisterService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<ApplicationUser> GetAndUpdateUser(IndexViewModel model)
        {
            var user = GetUser(model);

            user.PhoneNumber = model.PhoneNumber;
            user.LastName = model.LastName;
            user.FirstName = model.FirstName;

            await db.SaveChangesAsync();

            return user;
        }

        private ApplicationUser GetUser(IndexViewModel model)
        {
            return db.Users
                   .Where(x => x.Email.Equals(model.Email))
                   .FirstOrDefault();
        }
    }
}
