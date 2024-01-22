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
        public TestProductRepository()
        {
        }
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
            var count = 1;
            var productParameters = new ProductParameters
            {
                PageSize = count,
                PageNumber = 2
            };

            var listOfProducts = fixture.GetTestDbSetProducts();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: false);

            result.Should().HaveCount(count);
            result.MetaData.HasNext.Should().BeFalse();
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

        [Fact]
        public void Get_OnSucces_Return_PagedList_Products_With_ExpertId_Is_Null()
        {
            var fixture = new ProductFixture();
            var count = 2;
            var productParameters = new ProductParameters
            {
               
            };

            var listOfProducts = fixture.GetTestDbSetProducts();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: true);

            result.Should().HaveCount(count);
            result.Should().Contain(x => x.expert_id == null);            
        }

        [Fact]
        public void Get_OnSucces_Return_PagedList_Products_With_ExpertId_Is_23()
        {
            var fixture = new ProductFixture();
            var count = 1;
            var productParameters = new ProductParameters
            {
                ExpertId = 23
            };

            var listOfProducts = fixture.GetTestDbSetProducts();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: true);

            result.Should().HaveCount(count);
            result.Should().Contain(x => x.expert_id == productParameters.ExpertId);
        }

        [Fact]
        public void Get_OnSucces_Return_Null_PagedList_Products_With_ExpertId_Is_43()
        {
            var fixture = new ProductFixture();
            var productParameters = new ProductParameters
            {
                ExpertId = 43
            };

            var listOfProducts = fixture.GetTestDbSetProducts();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: true);

            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Get_OnSucces_Return_PagedList_Products_With_Name_Contains_S34()
        {
            var fixture = new ProductFixture();
            var expectedCount = 1;
            var productParameters = new ProductParameters
            {
                SearchName = "S34"
            };

            var listOfProducts = fixture.GetTestDbSetProducts();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: true);

            result.Should().Contain(x => x.product_name.Contains(productParameters.SearchName, StringComparison.CurrentCultureIgnoreCase));
            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Get_OnSucces_Return_PagedList_Products_With_Name_Contains_WhiteSpace()
        {
            var fixture = new ProductFixture();
            var expectedCount = 2;
            var productParameters = new ProductParameters
            {
                SearchName = "   "
            };

            var listOfProducts = fixture.GetTestDbSetProducts();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: true);

            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Get_OnSucces_Return_PagedList_Products_With_TypeId_2()
        {
            var fixture = new ProductFixture();
            var expectedCount = 1;
            var productParameters = new ProductParameters
            {
                ProductTypeId = 2
            };

            var listOfProducts = fixture.GetTestDbSetProducts();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: true);

            result.Should().Contain(x => x.pr_type_id == productParameters.ProductTypeId);
            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Get_OnSucces_Return_Empty_PagedList_Products_With_TypeId_245()
        {
            var fixture = new ProductFixture();
            var expectedCount = 0;
            var productParameters = new ProductParameters
            {
                ProductTypeId = 245
            };

            var listOfProducts = fixture.GetTestDbSetProducts();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: true);

            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Get_OnSucces_Return_PagedList_Products_With_Desc()
        { 
            var fixture = new ProductFixture();
            var expectedCount = 1;
            var productParameters = new ProductParameters
            {
                SearchDesc = "Экран"
            };

            var listOfProducts = fixture.GetTestDbSetProducts();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Product>()).Returns(listOfProducts);

            var productRepository = new ProductRepository(context.Object);

            var result = productRepository.GetProducts(productParameters, trackChanges: true);

            result.Should().HaveCount(expectedCount);
        }
    }
}
