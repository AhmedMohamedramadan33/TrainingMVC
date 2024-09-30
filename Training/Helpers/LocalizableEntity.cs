 using System.Globalization;

namespace Training.Helpers
{
    public class LocalizableEntity
    {
        public string Localize(string NameAr,string NameEn)
        {
            CultureInfo cultureInfo= Thread.CurrentThread.CurrentCulture;
            if(cultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar"))
            {
                return NameAr;
            }
            return NameEn;
        }
    }
}
