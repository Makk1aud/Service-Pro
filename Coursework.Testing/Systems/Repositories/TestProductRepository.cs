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
    public class TestProductRepository
    {
        [Fact]
        public void Get_OnSucces_PagedList_Products()
        {
            var fixture = new ProductFixture();
            var count = 5;

            var listOfProducts = fixture.GetRandomDbSetProducts(count);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(trackChanges: false);

            result.Should().BeOfType<List<Product>>();
        }

        [Fact]
        public void Get_OnSucces_Return_Products_Count_34()
        {
            var fixture = new ProductFixture();
            var count = 34;

            var listOfProducts = fixture.GetRandomDbSetProducts(count);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(trackChanges: false);

            result.Should().HaveCount(count);
            result.Should().BeOfType<List<Product>>();
        }

        [Fact]
        public void Get_OnSucces_Return_PagedList_Products_Count_4()
        {
            var fixture = new ProductFixture();
            var count = 4;
            var productParameters = new ProductParameters
            {
                PageSize = 4,
                PageNumber = 2
            };

            var listOfProducts = fixture.GetRandomDbSetProducts(13);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: false);

            result.Should().HaveCount(count);
            result.MetaData.HasNext.Should().BeTrue();
            result.MetaData.HasPrevious.Should().BeTrue();
        }

        [Fact]
        public void Get_OnSucces_Return_PagedList_Products_Count_0()
        {
            var fixture = new ProductFixture();
            var count = 0;
            var productParameters = new ProductParameters
            {
                PageSize = 3,
                PageNumber = 6
            };

            var listOfProducts = fixture.GetRandomDbSetProducts(13);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: false);

            result.Should().HaveCount(count);
            result.MetaData.HasNext.Should().BeFalse();
            result.MetaData.HasPrevious.Should().BeFalse();
        }
    }
}
