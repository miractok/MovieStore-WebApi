using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.DirectorOperations.Commands.CreateCommand
{
    public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistDirectorNameIsGiven_InvalidOperationException_ShouldReturn()
        {
            //arrange
            var director = new Director() {NameSurname = "Test_WhenAlreadyExistDirectorNameIsGiven_InvalidOperationException_ShouldReturn"};
            _context.Directors.Add(director);
            _context.SaveChanges();

            CreateDirectorCommand command = new CreateDirectorCommand(_mapper,_context);
            command.Model = new CreateDirectorViewModel(){ NameSurname = director.NameSurname};
            //act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This Director already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldCreated()
        {
            //arrange
            CreateDirectorCommand command = new CreateDirectorCommand(_mapper,_context);
            CreateDirectorViewModel model = new CreateDirectorViewModel(){ NameSurname = "TestDirector"};
            command.Model = model;
            
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var director = _context.Directors.SingleOrDefault(director => director.NameSurname == model.NameSurname);

            director.Should().NotBeNull();
        }

    }
}