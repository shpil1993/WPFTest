namespace WPFTest.Rest.Helpers
{
    public class LocalizationChecker
    {
        public static string CheckLocalization(int lang, dynamic entity)
        {
            switch (lang)
            {
                case 1:
                    return entity.Txt1;
                case 2:
                    return entity.Txt2;
                case 3:
                    return entity.Txt3;
                case 4:
                    return entity.Txt4;
                default:
                    return entity.Txt4;
            }
        }
    }
}
