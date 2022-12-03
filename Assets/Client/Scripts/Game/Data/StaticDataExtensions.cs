namespace Game
{
    public static class StaticDataExtensions
    {
        public static CrapData GetCrapByName(this StaticData data, string crapName)
        {
            foreach(var crap in data.AllCrap)
            {
                if(crap.name == crapName)
                {
                    return crap;
                }
            }

            return default;
        }
    }
}