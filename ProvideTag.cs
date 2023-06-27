using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSC
{
    public static class ProvideTag
    {
        public static Dictionary<string, string> GetAllTags()
        {
            return new Dictionary<string, string>()
                {{"//link","href" },
                { "//a", "href" },
                { "//img[contains(@class,'')]", "data-src" },
                    {"//picture/source[@srcset]","srcset" },
                    {"//script","src" },
                    { "//link[@rel='stylesheet']", "href" },
                    { "//link[@rel='preload']", "href" },
                    {"//style","href" },
                    
                    { "//img", "src" },

                    { "//script[@src]", "src" }


                };
        }

    }
}
