using Cephiro.Identity.Queries.Dtos;
using Cephiro.Identity.Domain.Entities; 

namespace Cephiro.Identity.Queries.IAccess; 

public interface IUserAccess
{
    public Task<IEnumerable<GetAllUsersOutputDto?>> GetAllUsers();
    public Task<GetUserByIdOutputDto?> GetUserById(GetUserByIdInputDto Dto);
    public Task<GetUserByPhoneOutputDto?> GetUserByPhone(GetUserByPhoneInputDto Dto);
    public Task<GetUserByEmailOutputDto?> GetUserByEmail(GetUserByEmailInputDto Dto);
}