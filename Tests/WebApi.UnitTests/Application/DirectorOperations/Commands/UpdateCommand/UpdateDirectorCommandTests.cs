using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.DirectorOperations.Commands.UpdateCommand
{
    public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDirectorIdIsInvalid_InavlidOperationException_ShouldBeReturn()
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = 459;

            FluentActions
                .Invoking(() => command.Handle())
                .Should()
                .Throw<InvalidOperationException>()
                .And.Message.Should()
                .Be("Director could bot be found!");
        }

        [Fact]
        public void WhenAlreadyExistDirectorNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange (Hazırlık)
            var director = new Director() {NameSurname = "Test_WhenAlreadyExistDirectorNameIsGiven_InvalidOperationException_ShouldReturn1"};
            _context.Directors.Add(director);
            _context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.Model = new UpdateDirectorViewModel() {  NameSurname = director.NameSurname };
            command.DirectorId = 1;

            //act & asssert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director already exists!");
            
        }
        
        //Happy Case
        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeUpdated()
        {
            //arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            UpdateDirectorViewModel model = new UpdateDirectorViewModel() {NameSurname="TestDirectorName"};
            command.Model = model;
            command.DirectorId = 1;

            //act

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var updatedirector = _context.Directors.SingleOrDefault(director => director.NameSurname == model.NameSurname);
            updatedirector.Should().NotBeNull();
        }
    }
}