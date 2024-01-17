using Coursework.DataApp;
using Coursework.Repository;
using Coursework.Repository.Extensions.FilterParameters;
using Coursework.Repository.Pagination;
using Coursework.Testing.Fixtures;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Testing.Systems.Repositories
{

    public class TestClientRepository
    {
        [Fact]
        public void Get_OnSucces_ReturnTypeOf_PagedList()
        {
            var fixture = new ClientFixture();

            var length = 10;
            var clientParameters = new ClientParameters()
            {
                PageSize = 10
            };

            var randomClients = fixture.GetRandomDbSetClients(length);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Client>()).Returns(randomClients);

            var clientRepository = new ClientRepository(context.Object);

            var pagedListClients = clientRepository.GetClients(clientParameters, trackChanges: false);

            pagedListClients.Should().BeOfType<PagedList<Client>>();
            pagedListClients.Should().HaveCount(length);
        }

        [Fact]
        public void Get_OnSucces_Return_10_Clients()
        {
            var fixture = new ClientFixture();

            var count = 23;
            var clientParameters = new ClientParameters()
            {
                PageSize = 10
            };

            var randomClients = fixture.GetRandomDbSetClients(count);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Client>()).Returns(randomClients);

            var clientRepository = new ClientRepository(context.Object);

            var pagedListClients = clientRepository.GetClients(clientParameters, trackChanges: false);
            
            pagedListClients.Should().HaveCount(clientParameters.PageSize);
            pagedListClients.MetaData.HasNext.Should().BeTrue();
        }

        [Fact]
        public void Get_OnSucces_Return_1_Client_WithPhoneNum()
        {
            var fixture = new ClientFixture();

            var clientParameters = new ClientParameters()
            {
                PhoneNum = "+7 2"
            };

            var randomClients = fixture.GetDbSetTestClients();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Client>()).Returns(randomClients);

            var clientRepository = new ClientRepository(context.Object);

            var pagedListClients = clientRepository.GetClients(clientParameters, trackChanges: false);

            pagedListClients.Should().HaveCount(1);
            pagedListClients.MetaData.HasNext.Should().BeFalse();
        }

        [Fact]
        public void Get_OnSucces_Return_3_Clients_With_ClientParams_HasPrevious_True()
        {
            var fixture = new ClientFixture();
            var count = 13;
            var clientParameters = new ClientParameters()
            {
                PageNumber = 2,
                PageSize = 3
            };

            var randomClients = fixture.GetRandomDbSetClients(count);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Client>()).Returns(randomClients);

            var clientRepository = new ClientRepository(context.Object);

            var pagedListClients = clientRepository.GetClients(clientParameters, trackChanges: false);

            pagedListClients.Should().HaveCount(clientParameters.PageSize);
            pagedListClients.MetaData.HasNext.Should().BeTrue();
            pagedListClients.MetaData.HasPrevious.Should().BeTrue();
        }

        [Fact]
        public void Get_OnSucces_Return_1_Clients_With_Id_3()
        {
            var fixture = new ClientFixture();
            var clientParameters = new ClientParameters()
            {
                Id = 3
            };

            var randomClients = fixture.GetDbSetTestClients();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Client>()).Returns(randomClients);

            var clientRepository = new ClientRepository(context.Object);

            var pagedListClients = clientRepository.GetClients(clientParameters, trackChanges: false);

            pagedListClients.Should().HaveCount(1);
            pagedListClients.MetaData.HasNext.Should().BeFalse();
            pagedListClients.MetaData.HasPrevious.Should().BeFalse();
        }

        [Fact]
        public void Get_OnSucces_Return_16_Clients()
        {
            var fixture = new ClientFixture();
            var count = 16;

            var randomClients = fixture.GetRandomDbSetClients(count);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Client>()).Returns(randomClients);

            var clientRepository = new ClientRepository(context.Object);

            var pagedListClients = clientRepository.GetClients(trackChanges: false);

            pagedListClients.Should().HaveCount(count);
        }
    }
}
