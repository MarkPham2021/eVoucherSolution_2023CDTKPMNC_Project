using eVoucher_DAL.InfraStructure;
using eVoucher_DTO.Models;

namespace eVoucher_DAL.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
    }

    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(eVoucherDbContext context) : base(context)
        {
        }
    }
}