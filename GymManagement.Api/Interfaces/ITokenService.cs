namespace GymManagement.Api.Interfaces;

public interface ITokenService
{
    string CreateToken(string username, string role);
}