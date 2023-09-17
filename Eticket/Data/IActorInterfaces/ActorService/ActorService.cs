using Eticket.Data.BaseEntity;
using ETicket.Models;
using Microsoft.EntityFrameworkCore;

namespace ETicket.data.IActorInterfaces.ActorService
{
    public class ActorService : GenericEntityRepo<Actor>, IActor
    {
       

        public ActorService(EticketDbContext context ):base(context) { }
      

        
    }
}
