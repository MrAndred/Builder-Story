using System.Collections.Generic;

namespace BuilderStory
{
    public static class LocalizationUtil
    {
        public static IReadOnlyDictionary<string, string> Languages = new Dictionary<string, string>{
            {"ru", "Russian"},
            {"en", "English"},
            {"tr", "Turkish"}
        };
    }
}
