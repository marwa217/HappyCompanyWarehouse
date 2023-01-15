using HappyCompany.Application.Repos;
using HappyCompany.Domain.Data;

namespace HappyCompany.Application.Helpers
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public ApplicationDbContext _context = new ApplicationDbContext();
        private readonly ILogger _logger;

        public UnitOfWork( ILogger logger)
        {
            warehouseRepository = new WarehouseRepository(_context);
            userRepository = new UserRepository(_context);
            itemRepository = new ItemRepository(_context);
            _logger = logger;

        }

        public IWarehouseRepository warehouseRepository { get; }

        public IUserRepository userRepository { get; }

        public IItemRepository itemRepository { get; }

        public bool Complete
        {
            get
            {
                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.Error("an error occured in UnitOfWork Class while saving Entity", ex);
                    return false;
                }
            }
        }



        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
