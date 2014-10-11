using System;
using System.Collections.Generic;
using System.Text;

namespace Newegg.Website.HttpModule.FilterForbiddenWordMobule
{
    public static class ForbiddenWord
    {
        public static string Filter(string original)
        {
            return original.Replace("FORBIDDEN_WORD", "***");
        }
    }
}
