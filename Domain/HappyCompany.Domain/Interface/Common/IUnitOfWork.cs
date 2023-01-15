using HappyCompany.Domain.Interface.Repos;

namespace HappyCompany.Domain.Interface.Common
{
    public interface IUnitOfWork
    {
        IWarehouseRepository warehouseRepository { get; }
        IUserRepository userRepository { get; }
        IItemRepository itemRepository { get; }
        bool Complete { get; }
    }
}
