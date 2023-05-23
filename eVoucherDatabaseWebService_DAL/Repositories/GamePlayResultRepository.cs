using eVoucher_DAL.InfraStructure;


namespace eVoucher_DAL.Repositories
{
    public interface IGamePlayResultRepository : IRepository<GamePlayResult> { }
    public class GamePlayResultRepository : RepositoryBase<GamePlayResult>, IGamePlayResultRepository
    {
        public GamePlayResultRepository(eVoucherDbContext context) : base(context) { }
    }
}
