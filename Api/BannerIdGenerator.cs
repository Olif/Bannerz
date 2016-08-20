using Domain;

namespace Api
{
    public class BannerIdGenerator : IIdGenerator
    {
        private IBannerRepository _repo;

        public BannerIdGenerator(IBannerRepository repo)
        {
            _repo = repo;
        }

        public int Next()
        {
            // Let the minimum id be 1 
            return _repo.NextSequence() + 1;
        }
    }
}