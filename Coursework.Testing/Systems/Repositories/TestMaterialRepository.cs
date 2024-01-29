using Coursework.DataApp;
using Coursework.Repository;
using Coursework.Repository.Extensions.FilterParameters;
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
    public class TestMaterialRepository
    {
        [Fact]
        public void Get_OnSucces_List_Materials()
        {
            var fixture = new MaterialFixture();
            var count = 5;

            var listOfMaterials = fixture.GetRandomDbSetMaterials(count);

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Material>()).Returns(listOfMaterials);

            var materialRepository = new MaterialRepository(context.Object);

            var result = materialRepository.GetMaterials(new MaterialParameters(), trackChanges: false);

            result.Should().BeOfType<List<Material>>();
            result.Should().HaveCount(count);
        }

        [Fact]
        public void Get_OnSucces_List_Products_With_SearchName_Count_2()
        {
            var fixture = new MaterialFixture();
            var expectedCount = 2;
            var materialParameters = new MaterialParameters
            {
                MaterialTitle = "Экра   "
            };

            var listOfMaterials = fixture.GetTestDbSetMaterials();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Material>()).Returns(listOfMaterials);

            var materialRepository = new MaterialRepository(context.Object);

            var result = materialRepository.GetMaterials(materialParameters, trackChanges: false);

            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Get_OnSucces_List_Products_With_SearchName_Count_4()
        {
            var fixture = new MaterialFixture();
            var expectedCount = 4;
            var materialParameters = new MaterialParameters
            {
                MaterialTitle = "  "
            };

            var listOfMaterials = fixture.GetTestDbSetMaterials();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Material>()).Returns(listOfMaterials);

            var materialRepository = new MaterialRepository(context.Object);

            var result = materialRepository.GetMaterials(materialParameters, trackChanges: false);

            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Get_OnSucces_List_Products_With_MaterialType_34_Count_2()
        {
            var fixture = new MaterialFixture();
            var expectedCount = 2;
            var materialParameters = new MaterialParameters
            {
                MaterialTypeId = 34
            };

            var listOfMaterials = fixture.GetTestDbSetMaterials();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Material>()).Returns(listOfMaterials);

            var materialRepository = new MaterialRepository(context.Object);

            var result = materialRepository.GetMaterials(materialParameters, trackChanges: false);

            result.Should().HaveCount(expectedCount);
            result.Should().Contain(x => x.mat_type_id == materialParameters.MaterialTypeId);
        }

        [Fact]
        public void Get_OnSucces_List_Products_With_MaterialType_0_Count_0()
        {
            var fixture = new MaterialFixture();
            var expectedCount = 0;
            var materialParameters = new MaterialParameters
            {
                MaterialTypeId = 0
            };

            var listOfMaterials = fixture.GetTestDbSetMaterials();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Material>()).Returns(listOfMaterials);

            var materialRepository = new MaterialRepository(context.Object);

            var result = materialRepository.GetMaterials(materialParameters, trackChanges: false);

            result.Should().HaveCount(expectedCount);
        }


        [Fact]
        public void Get_OnSucces_List_Products_With_MinPrice_1000_Count_2()
        {
            var fixture = new MaterialFixture();
            var expectedCount = 2;
            var materialParameters = new MaterialParameters
            {
                MinPrice = 1000
            };

            var listOfMaterials = fixture.GetTestDbSetMaterials();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Material>()).Returns(listOfMaterials);

            var materialRepository = new MaterialRepository(context.Object);

            var result = materialRepository.GetMaterials(materialParameters, trackChanges: false);

            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Get_OnSucces_List_Products_With_MinPrice_BelowZero_Count_4()
        {
            var fixture = new MaterialFixture();
            var expectedCount = 4;
            var materialParameters = new MaterialParameters
            {
                MinPrice = -2
            };

            var listOfMaterials = fixture.GetTestDbSetMaterials();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Material>()).Returns(listOfMaterials);

            var materialRepository = new MaterialRepository(context.Object);

            var result = materialRepository.GetMaterials(materialParameters, trackChanges: false);

            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Get_OnSucces_List_Products_With_MinPrice_1000_And_MaxPrice_2000_Count_1()
        {
            var fixture = new MaterialFixture();
            var expectedCount = 1;
            var materialParameters = new MaterialParameters
            {
                MinPrice = 1000,
                MaxPrice = 2000
            };

            var listOfMaterials = fixture.GetTestDbSetMaterials();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Material>()).Returns(listOfMaterials);

            var materialRepository = new MaterialRepository(context.Object);

            var result = materialRepository.GetMaterials(materialParameters, trackChanges: false);

            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Get_OnSucces_List_Products_With_MinPrice_2000_And_MaxPrice_1000_Count_4()
        {
            var fixture = new MaterialFixture();
            var expectedCount = 4;
            var materialParameters = new MaterialParameters
            {
                MinPrice = 2000,
                MaxPrice = 1000
            };

            var listOfMaterials = fixture.GetTestDbSetMaterials();

            var context = new Mock<CourseworkEntities>();
            context.Setup(x => x.Set<Material>()).Returns(listOfMaterials);

            var materialRepository = new MaterialRepository(context.Object);

            var result = materialRepository.GetMaterials(materialParameters, trackChanges: false);

            result.Should().HaveCount(expectedCount);
        }
    }
}
