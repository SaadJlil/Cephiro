using System.Threading.Tasks;

namespace Cephiro.Identity.Commands.IExecutors;

public interface IUserAuthExecutor
{
    public Task RegisterNewUser();
    public Task SignUserIn();
    public Task SignUserOut();
}