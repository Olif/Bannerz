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
            return _repo.NextSequence();
        }
    }
}