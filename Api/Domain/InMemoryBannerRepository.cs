using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Domain
{
    public class InMemoryBannerRepository : IBannerRepository
    {
        private IDictionary<int, Banner> _ratesTable;
        private int _cumulativeCount = 0;

        public InMemoryBannerRepository()
        {
            _ratesTable = new ConcurrentDictionary<int, Banner>();
        }

        public InMemoryBannerRepository(IDictionary<int, Banner> ratesTable)
        {
            _ratesTable = ratesTable;
        }

        public Banner Add(Banner banner)
        {
            _ratesTable.Add(banner.Id, banner);
            _cumulativeCount += 1;
            return banner;
        }

        public Banner Delete(Banner banner)
        {
            _ratesTable.Remove(banner.Id);
            return banner;
        }

        public Banner Find(int id)
        {
            if(_ratesTable.ContainsKey(id))
            {
                return _ratesTable[id];
            }

            return null;
        }

        public int NextSequence()
        {
            return _cumulativeCount;
        }
    }
}