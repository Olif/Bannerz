using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    public interface IBannerRepository
    {
        Banner Find(int id);

        Banner Add(Banner banner);

        Banner Delete(Banner banner);

        int NextSequence();
    }
}