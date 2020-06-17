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
    public class RoleStore : IRoleStore<ApplicationRole>
    {
        private readonly string _ApplicationConnection;

        public RoleStore(IConfiguration configuration)
        {
            _ApplicationConnection = configuration.GetConnectionString("ApplicationConnection");
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole applicationRole, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[RoleCreate]";

                    _SqlCommand.Parameters.AddWithValue("@Name", applicationRole.Name);
                    _SqlCommand.Parameters.AddWithValue("@NormalizedName", applicationRole.NormalizedName);

                    applicationRole.Id = Convert.ToInt32(_SqlCommand.ExecuteScalar());
                }
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole applicationRole, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[RoleUpdate]";

                    _SqlCommand.Parameters.AddWithValue("@Id", applicationRole.Id);
                    _SqlCommand.Parameters.AddWithValue("@Name", applicationRole.Name);
                    _SqlCommand.Parameters.AddWithValue("@NormalizedName", applicationRole.NormalizedName);

                    applicationRole.Id = Convert.ToInt32(_SqlCommand.ExecuteScalar());
                }
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole applicationRole, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[RoleDelete]";
                    _SqlCommand.Parameters.AddWithValue("@Id", applicationRole.Id);

                    _SqlCommand.ExecuteNonQuery();
                }
            }

            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(ApplicationRole applicationRole, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationRole.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(ApplicationRole applicationRole, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationRole.Name);
        }

        public Task SetRoleNameAsync(ApplicationRole applicationRole, string roleName, CancellationToken cancellationToken)
        {
            applicationRole.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole applicationRole, CancellationToken cancellationToken)
        {
            return Task.FromResult(applicationRole.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole applicationRole, string normalizedName, CancellationToken cancellationToken)
        {
            applicationRole.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationRole _ApplicationRole = new ApplicationRole();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[RoleRetrieve]";
                    _SqlCommand.Parameters.AddWithValue("@Id", roleId);

                    using (SafeDataReader _SafeDataReader = new SafeDataReader(await _SqlCommand.ExecuteReaderAsync()))
                    {
                        if (_SafeDataReader.Read())
                        {
                            _ApplicationRole.Id = _SafeDataReader.GetInt32("Id");
                            _ApplicationRole.Name = _SafeDataReader.GetString("Name");
                            _ApplicationRole.NormalizedName = _SafeDataReader.GetString("NormalizedName");
                        }
                    }
                }
            }

            return _ApplicationRole;
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationRole _ApplicationRole = new ApplicationRole();

            using (SqlConnection _SqlConnection = new SqlConnection(_ApplicationConnection))
            {
                await _SqlConnection.OpenAsync(cancellationToken);
                using (SqlCommand _SqlCommand = _SqlConnection.CreateCommand())
                {
                    _SqlCommand.CommandType = CommandType.StoredProcedure;
                    _SqlCommand.CommandText = "[dbo].[RoleRetrieveByNormilizedName]";
                    _SqlCommand.Parameters.AddWithValue("@NormalizedName", normalizedRoleName.ToUpper());

                    using (SafeDataReader _SafeDataReader = new SafeDataReader(await _SqlCommand.ExecuteReaderAsync()))
                    {
                        if (_SafeDataReader.Read())
                        {
                            _ApplicationRole.Id = _SafeDataReader.GetInt32("Id");
                            _ApplicationRole.Name = _SafeDataReader.GetString("Name");
                            _ApplicationRole.NormalizedName = _SafeDataReader.GetString("NormalizedName");
                        }
                    }
                }
            }

            return _ApplicationRole;
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
