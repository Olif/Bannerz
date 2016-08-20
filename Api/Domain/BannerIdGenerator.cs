using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Domain
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