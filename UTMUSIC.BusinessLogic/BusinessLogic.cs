using UTMUSIC.BusinessLogic.Interfaces;

namespace UTMUSIC.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

    }
}