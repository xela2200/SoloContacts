using System;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using SoloContacts.Library.Models;
using SoloContacts.Core.Data;

namespace SoloContacts.Library
{
    public class UserStore : IUserStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IUserPhoneNumberStore<ApplicationUser>,
        IUserTwoFactorStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
    {
        private readonly string _ApplicationConnection;

        public UserStore(IConfiguration configuration)
        {
            _ApplicationConnection = configuration.GetConnectionString("ApplicationConnection");
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserCreate]";

                    _SqlCommand.Parameters.AddWithValue("@UserName", applicationUser.UserName);
                    _SqlCommand.Parameters.AddWithValue("@NormalizedUserName", applicationUser.NormalizedUserName);
                    _SqlCommand.Parameters.AddWithValue("@Email", applicationUser.Email);
                    _SqlCommand.Parameters.AddWithValue("@NormalizedEmail", applicationUser.NormalizedEmail);
                    _SqlCommand.Parameters.AddWithValue("@EmailConfirmed", applicationUser.EmailConfirmed);
                    _SqlCommand.Parameters.AddWithValue("@PasswordHash", applicationUser.PasswordHash);
                    _SqlCommand.Parameters.AddWithValue("@PhoneNumber", applicationUser.PhoneNumber);
                    _SqlCommand.Parameters.AddWithValue("@PhoneNumberConfirmed", applicationUser.PhoneNumberConfirmed);
                    _SqlCommand.Parameters.AddWithValue("@TwoFactorEnabled", applicationUser.TwoFactorEnabled);

                    applicationUser.Id = Convert.ToInt32(_SqlCommand.ExecuteScalar());
                }
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserDelete]";
                    _SqlCommand.Parameters.AddWithValue("@Id", applicationUser.Id);

                    _SqlCommand.ExecuteNonQuery();
                }
            }

            return IdentityResult.Success;
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUser _ApplicationUser = new ApplicationUser();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserRetrieve]";
                    _SqlCommand.Parameters.AddWithValue("@Id", userId);

                    using (SafeDataReader _SafeDataReader = new SafeDataReader(await _SqlCommand.ExecuteReaderAsync()))
                    {
                        if (_SafeDataReader.Read())
                        {
                            _ApplicationUser.Id = _SafeDataReader.GetInt32("Id");
                            _ApplicationUser.UserName = _SafeDataReader.GetString("UserName");
                            _ApplicationUser.NormalizedUserName = _SafeDataReader.GetString("NormalizedUserName");
                            _ApplicationUser.Email = _SafeDataReader.GetString("Email");
                            _ApplicationUser.NormalizedEmail = _SafeDataReader.GetString("NormalizedEmail");
                            _ApplicationUser.EmailConfirmed = _SafeDataReader.GetBoolean("EmailConfirmed");
                            _ApplicationUser.PasswordHash = _SafeDataReader.GetString("PasswordHash");
                            _ApplicationUser.PhoneNumber = _SafeDataReader.GetString("PhoneNumber");
                            _ApplicationUser.PhoneNumberConfirmed = _SafeDataReader.GetBoolean("PhoneNumberConfirmed");
                            _ApplicationUser.TwoFactorEnabled = _SafeDataReader.GetBoolean("TwoFactorEnabled");
                        }
                    }
                }
            }

            return _ApplicationUser;
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUser _ApplicationUser = new ApplicationUser();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserRetrieveByName]";
                    _SqlCommand.Parameters.AddWithValue("@NormalizedUserName", normalizedUserName);

                    using (SafeDataReader _SafeDataReader = new SafeDataReader(await _SqlCommand.ExecuteReaderAsync()))
                    {
                        if (_SafeDataReader.Read())
                        {
                            _ApplicationUser.Id = _SafeDataReader.GetInt32("Id");
                            _ApplicationUser.UserName = _SafeDataReader.GetString("UserName");
                            _ApplicationUser.NormalizedUserName = _SafeDataReader.GetString("NormalizedUserName");
                            _ApplicationUser.Email = _SafeDataReader.GetString("Email");
                            _ApplicationUser.NormalizedEmail = _SafeDataReader.GetString("NormalizedEmail");
                            _ApplicationUser.EmailConfirmed = _SafeDataReader.GetBoolean("EmailConfirmed");
                            _ApplicationUser.PasswordHash = _SafeDataReader.GetString("PasswordHash");
                            _ApplicationUser.PhoneNumber = _SafeDataReader.GetString("PhoneNumber");
                            _ApplicationUser.PhoneNumberConfirmed = _SafeDataReader.GetBoolean("PhoneNumberConfirmed");
                            _ApplicationUser.TwoFactorEnabled = _SafeDataReader.GetBoolean("TwoFactorEnabled");
                        }
                    }
                }
            }

            return _ApplicationUser;
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.Id.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.UserName);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser applicationUser, string normalizedName, CancellationToken cancellationToken)
        {
            applicationUser.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(ApplicationUser applicationUser, string userName, CancellationToken cancellationToken)
        {
            applicationUser.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserUpdate]";

                    _SqlCommand.Parameters.AddWithValue("@Id", applicationUser.Id);
                    _SqlCommand.Parameters.AddWithValue("@UserName", applicationUser.UserName);
                    _SqlCommand.Parameters.AddWithValue("@NormalizedUserName", applicationUser.NormalizedUserName);
                    _SqlCommand.Parameters.AddWithValue("@Email", applicationUser.Email);
                    _SqlCommand.Parameters.AddWithValue("@NormalizedEmail", applicationUser.NormalizedEmail);
                    _SqlCommand.Parameters.AddWithValue("@EmailConfirmed", applicationUser.EmailConfirmed);
                    _SqlCommand.Parameters.AddWithValue("@PasswordHash", applicationUser.PasswordHash);
                    _SqlCommand.Parameters.AddWithValue("@PhoneNumber", applicationUser.PhoneNumber);
                    _SqlCommand.Parameters.AddWithValue("@PhoneNumberConfirmed", applicationUser.PhoneNumberConfirmed);
                    _SqlCommand.Parameters.AddWithValue("@TwoFactorEnabled", applicationUser.TwoFactorEnabled);

                    _SqlCommand.ExecuteScalar();
                }
            }

            return IdentityResult.Success;
        }

        public Task SetEmailAsync(ApplicationUser applicationUser, string email, CancellationToken cancellationToken)
        {
            applicationUser.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser applicationUser, bool confirmed, CancellationToken cancellationToken)
        {
            applicationUser.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUser _ApplicationUser = new ApplicationUser();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserRetrievebyEmail]";
                    _SqlCommand.Parameters.AddWithValue("@NormalizedEmail", normalizedEmail);

                    using (SafeDataReader _SafeDataReader = new SafeDataReader(await _SqlCommand.ExecuteReaderAsync()))
                    {
                        if (_SafeDataReader.Read())
                        {
                            _ApplicationUser.Id = _SafeDataReader.GetInt32("Id");
                            _ApplicationUser.UserName = _SafeDataReader.GetString("UserName");
                            _ApplicationUser.NormalizedUserName = _SafeDataReader.GetString("NormalizedUserName");
                            _ApplicationUser.Email = _SafeDataReader.GetString("Email");
                            _ApplicationUser.NormalizedEmail = _SafeDataReader.GetString("NormalizedEmail");
                            _ApplicationUser.EmailConfirmed = _SafeDataReader.GetBoolean("EmailConfirmed");
                            _ApplicationUser.PasswordHash = _SafeDataReader.GetString("PasswordHash");
                            _ApplicationUser.PhoneNumber = _SafeDataReader.GetString("PhoneNumber");
                            _ApplicationUser.PhoneNumberConfirmed = _SafeDataReader.GetBoolean("PhoneNumberConfirmed");
                            _ApplicationUser.TwoFactorEnabled = _SafeDataReader.GetBoolean("TwoFactorEnabled");
                        }
                    }
                }
            }

            return _ApplicationUser;
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser applicationUser, string normalizedEmail, CancellationToken cancellationToken)
        {
            applicationUser.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(ApplicationUser applicationUser, string phoneNumber, CancellationToken cancellationToken)
        {
            applicationUser.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser applicationUser, bool confirmed, CancellationToken cancellationToken)
        {
            applicationUser.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUser applicationUser, bool enabled, CancellationToken cancellationToken)
        {
            applicationUser.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(ApplicationUser applicationUser, string passwordHash, CancellationToken cancellationToken)
        {
            applicationUser.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationUser.PasswordHash != null);
        }

        public async Task AddToRoleAsync(ApplicationUser applicationUser, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserRoleCreateByRoleName]";

                    _SqlCommand.Parameters.AddWithValue("@UserId", applicationUser.Id);
                    _SqlCommand.Parameters.AddWithValue("@NormalizedName", roleName.ToUpper());

                    _SqlCommand.ExecuteScalar();
                }
            }
        }

        public async Task RemoveFromRoleAsync(ApplicationUser applicationUser, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserRoleCreateByRoleName]";

                    _SqlCommand.Parameters.AddWithValue("@UserId", applicationUser.Id);
                    _SqlCommand.Parameters.AddWithValue("@NormalizedName", roleName.ToUpper());

                    _SqlCommand.ExecuteNonQuery();
                }
            }
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<string> _Result = new List<string>();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserRoleRetrieveList]";
                    _SqlCommand.Parameters.AddWithValue("@UserId", applicationUser.Id);

                    using (SafeDataReader _SafeDataReader = new SafeDataReader(await _SqlCommand.ExecuteReaderAsync()))
                    {
                        while (_SafeDataReader.Read())
                        {
                            _Result.Add(_SafeDataReader.GetString("RoleName"));
                        }
                    }
                }

                return _Result;
            }
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser applicationUser, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            bool _Results = true;

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserRoleExists]";

                    _SqlCommand.Parameters.AddWithValue("@UserId", applicationUser.Id);
                    _SqlCommand.Parameters.AddWithValue("@NormalizedName", roleName.ToUpper());

                    _Results = (int)_SqlCommand.ExecuteScalar() == 1;
                }
            }

            return _Results;
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<ApplicationUser> _Result = new List<ApplicationUser>();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[UserRetrieveListByRole]";
                    _SqlCommand.Parameters.AddWithValue("@NormalizedName", roleName.ToUpper());

                    using (SafeDataReader _SafeDataReader = new SafeDataReader(await _SqlCommand.ExecuteReaderAsync()))
                    {
                        while (_SafeDataReader.Read())
                        {
                            _Result.Add(new ApplicationUser()
                            {
                                Id = _SafeDataReader.GetInt32("Id"),
                                UserName = _SafeDataReader.GetString("UserName"),
                                NormalizedUserName = _SafeDataReader.GetString("NormalizedUserName"),
                                Email = _SafeDataReader.GetString("Email"),
                                NormalizedEmail = _SafeDataReader.GetString("NormalizedEmail"),
                                EmailConfirmed = _SafeDataReader.GetBoolean("EmailConfirmed"),
                                PasswordHash = _SafeDataReader.GetString("PasswordHash"),
                                PhoneNumber = _SafeDataReader.GetString("PhoneNumber"),
                                PhoneNumberConfirmed = _SafeDataReader.GetBoolean("PhoneNumberConfirmed"),
                                TwoFactorEnabled = _SafeDataReader.GetBoolean("TwoFactorEnabled")
                            });
                        }
                    }
                }

                return _Result;
            }
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
