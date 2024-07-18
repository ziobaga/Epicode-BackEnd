using Microsoft.Data.SqlClient;
using ProgettoSettimanale.Models;
using ProgettoSettimanale.Service;

public class AuthService : IAuthService
{
    private readonly string _connectionString;
    private const string LOGIN_COMMAND = "SELECT Id, Username FROM Users WHERE Username = @Username AND Password = @Password";
    private const string ROLES_COMMAND = "SELECT ro.RoleName FROM Roles ro JOIN UserRoles ur ON ro.Id = ur.RoleId WHERE ur.UserId = @UserId";
    private const string REGISTER_COMMAND = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password); SELECT SCOPE_IDENTITY();";

    public AuthService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Authdb");
    }

    public Users Login(string username, string password)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(LOGIN_COMMAND, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var user = new Users
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = password
                            };
                            reader.Close();
                            return user;
                        }
                    }
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public Users Register(string username, string password)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(REGISTER_COMMAND, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); // Cripta la password se necessario
                    var userId = Convert.ToInt32(command.ExecuteScalar());
                    return new Users
                    {
                        Id = userId,
                        Username = username,
                        Password = password
                    };
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Registration failed: " + ex.Message);
        }
    }

    public List<string> GetUserRoles(int userId)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(ROLES_COMMAND, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        var roles = new List<string>();
                        while (reader.Read())
                        {
                            roles.Add(reader.GetString(0));
                        }
                        return roles;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Fetching roles failed: " + ex.Message);
        }
    }
}