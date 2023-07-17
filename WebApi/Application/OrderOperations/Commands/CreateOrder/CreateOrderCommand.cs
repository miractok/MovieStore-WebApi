using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommand
    {
        public CreateOrderViewModel Model;
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommand(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var customer = _context.Customers.SingleOrDefault(s => s.Id == Model.CustomerId);
            var film = _context.Films.SingleOrDefault(s => s.Id == Model.FilmId);
            var order = _context.Orders.SingleOrDefault(s => s.CustomerId == Model.CustomerId && s.FilmId == Model.FilmId);

            if (order != null)
                throw new InvalidOperationException("Customer already bougth this film!");

            if (customer == null)
                throw new InvalidOperationException("Customer could not be found!");
            
            if (film == null)
                throw new InvalidOperationException("Film could not be found!");
            
            Order result = _mapper.Map<Order>(Model);
            result.PurchaseDate = DateTime.Now;

            _context.Orders.Add(result);
            _context.SaveChanges();
        }
    }

    public class CreateOrderViewModel
    {
        public int CustomerId { get; set; }
        public int FilmId { get; set; }
        DateTime PurchaseDate = DateTime.Now;
    }
}