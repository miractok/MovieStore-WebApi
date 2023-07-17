using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Queries.GetOrderDetails
{
    public class GetOrderDetailsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int OrderId { get; set; }
        public GetOrderDetailsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public OrderViewIdModel Handle()
        {
            var order = _context.Orders.Include(x=> x.Customer).Include(x=> x.Film).Where(order=> order.Id == OrderId).SingleOrDefault();

            if(order == null)
                throw new InvalidOperationException("The Id you entered does not match any Order relation.");

            OrderViewIdModel vm = _mapper.Map<OrderViewIdModel>(order); 
            
            return vm;
        }
    }

    public class OrderViewIdModel
    {
        public string NameSurname { get; set; }
        public string Film { get; set; }
        public int Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}