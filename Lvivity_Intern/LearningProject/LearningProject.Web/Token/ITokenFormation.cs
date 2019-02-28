using LearningProject.Data.Models;

namespace LearningProject.Web.Token
{
    public interface ITokenFormation
    {
        string GetToken(User user);
    }
}
