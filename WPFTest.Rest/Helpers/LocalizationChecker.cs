namespace WPFTest.Rest.Helpers
{
    public class LocalizationChecker
    {
        public static string CheckLocalization(int lang, dynamic entity)
        {
            string result;
            switch (lang)
            {
                case 1:
                    result = entity.Txt1;
                    break;
                case 2:
                    result = entity.Txt2;
                    break;
                case 3:
                    result = entity.Txt3;
                    break;
                case 4:
                    result = entity.Txt4;
                    break;
                default:
                    result = entity.Txt1;
                    break;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = entity.Txt1;
            }

            return result;
        }
    }
}
