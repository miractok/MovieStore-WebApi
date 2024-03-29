using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.OrderOperations.Queries.GetOrder
{
    public class GetOrderQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderQuery(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<OrderViewModel> Handle()
        {
            var orderList = _context.Orders.Include(x => x.Customer).Include(x => x.Film).OrderBy(x=> x.Id).ToList<Order>();
            List<OrderViewModel> vm = _mapper.Map<List<OrderViewModel>>(orderList);

            return vm;
        }

    }

    public class OrderViewModel
    {
        public string NameSurname { get; set; }
        public string Films { get; set; }
        public int Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}