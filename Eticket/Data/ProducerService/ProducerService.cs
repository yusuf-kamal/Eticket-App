using Eticket.Data.BaseEntity;
using ETicket.data;
using ETicket.Models;

namespace Eticket.Data.ProducerService
{
    public class ProducerService:GenericEntityRepo<Producer>,IProducer
    {
        public ProducerService(EticketDbContext context) : base(context){ }
        
    }
}
