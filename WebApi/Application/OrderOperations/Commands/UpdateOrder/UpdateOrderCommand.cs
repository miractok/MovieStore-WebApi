using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DataId { get; set; }
        public UpdateOrderModel Model { get; set; }

        public UpdateOrderCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var order = _context.Orders.SingleOrDefault(x=> x.Id == DataId);
            if(order == null)
                throw new InvalidOperationException("Order could not be found!");

            var customer = _context.Customers.SingleOrDefault(x => x.Id == Model.CustomerId);
            if(customer == null)
                throw new InvalidOperationException("Customer could not be found!");

            var film = _context.Films.SingleOrDefault(x => x.Id == Model.FilmId);
            if(film == null)
                throw new InvalidOperationException("Film could not be found!");

            order.FilmId = Model.FilmId != default ? Model.FilmId : order.FilmId;
            order.CustomerId = Model.CustomerId != default ? Model.CustomerId : order.CustomerId;
            order.IsActive = Model.IsActive;

            _context.Orders.Update(order);
            _context.SaveChanges();
        }

    }

    public class UpdateOrderModel
    {
        public int CustomerId { get; set; }
        public int FilmId { get; set; }
        public bool IsActive { get; set; }
    }
}