using Eticket.Data.BaseEntity;
using ETicket.data;
using ETicket.Models;

namespace Eticket.Data.CinemaService
{
    public class CinemaService:GenericEntityRepo<Cinema>,ICinema
    {
        public CinemaService( EticketDbContext context):base(context)
        {
                
        }
    }
}
