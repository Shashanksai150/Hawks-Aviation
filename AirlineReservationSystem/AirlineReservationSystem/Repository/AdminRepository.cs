using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationSystem.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly HawksAvaitionDBContext _dbContext;
        private readonly IExceptionRepository _exceptionServices;
        public AdminRepository(HawksAvaitionDBContext dbContext, IExceptionRepository exceptionServices)
        {

            _dbContext = dbContext;
            _exceptionServices = exceptionServices;
        }
        public int AddNewAdmin(Admin admin)
        {

            int reponse = StatusCodes.Status501NotImplemented;
            Users user = new Users();
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Admin.Add(admin);
                _dbContext.SaveChanges();
                _dbContext.Database.RollbackTransaction();
                user.Username = admin.Username;
                user.Password = admin.Password;
                user.FirstName = admin.FirstName;
                user.LastName = admin.LastName;
                user.EmailId = admin.EmailId;
                user.Role = admin.Role;
                user.UserId = admin.AdminId;
                _dbContext.Database.BeginTransaction();
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                _dbContext.Database.RollbackTransaction();
                reponse = StatusCodes.Status200OK;

            }
            catch (Exception ex)
            {
                _dbContext.Dispose();
                //_dbContext.Database.CloseConnection();
                //_exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return reponse;
        }

        public Admin GetAdminById(int Id)
        {


            Admin admin = null;

            try
            {
                admin = _dbContext.Admin
                    .AsNoTracking()
                        .FirstOrDefault(c => c.AdminId == Id);
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return admin;
        }

        public int UpdateAdmin(Admin admin)
        {

            int response = StatusCodes.Status501NotImplemented;

            try
            {
                Admin admin1 = _dbContext.Admin
                    .AsNoTracking()
                .FirstOrDefault(c => c.AdminId == admin.AdminId);
                if (admin1 != null)
                {
                    _dbContext.Admin.Update(admin);
                    _dbContext.SaveChanges();
                    response = StatusCodes.Status200OK;
                }
                else
                {
                    response = StatusCodes.Status404NotFound;
                }
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }

            return response;
        }

        public int ChangePasswordAdmin(Login creds, string newPassword)
        {
            int response = StatusCodes.Status501NotImplemented;

            try
            {
                Admin admin1 = _dbContext.Admin
                                .AsNoTracking()
                    .FirstOrDefault(u =>
            (u.Username == creds.Username) && (u.Password == creds.Password));
                admin1.Username = creds.Username;
                admin1.Password = newPassword;

                Users user = _dbContext.Users
                                .AsNoTracking()
                                .FirstOrDefault(u =>
            (u.Username == creds.Username) && (u.Password == creds.Password));
                user.Username = creds.Username;
                user.Password = newPassword;
                
                if (admin1 != null)
                {
                    _dbContext.Admin.Update(admin1);
                    _dbContext.SaveChanges();
                    _dbContext.Users.Update(user);
                    _dbContext.SaveChanges();
                    response = StatusCodes.Status200OK;
                }
                else
                {
                    response = StatusCodes.Status404NotFound;
                }
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }

            return response;
        }
    }
}

