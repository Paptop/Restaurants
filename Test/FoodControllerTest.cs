using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Controllers;
using Restaurants.Domain;
using Restaurants.Persistence;
using Xunit;

namespace Test
{
    public class FoodControllerTest
    {
        private FoodController _controller { get; set; }
        private IRestaurantRepository _repo { get; set; }

        public FoodControllerTest()
        {
            _repo = new RestaurantRepoInMemory();
            _controller = new FoodController(_repo);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(0, 404)]
        public void Test_Details(int id, int expectCode)
        {
            //Act
            var result = _controller.Details(id);
            var okResult = result as ObjectResult;

            //Assert
            if(okResult == null)
            {
                var notFound = result as StatusCodeResult;
                Assert.NotNull(notFound);
                Assert.Equal(StatusCodes.Status404NotFound, notFound.StatusCode);
                Assert.Equal(notFound.StatusCode, expectCode);
            }
            else
            {
                Assert.NotNull(okResult);
                Assert.True(okResult is OkObjectResult);
                Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
                Assert.IsType<Restaurant>(okResult.Value);
                Assert.Equal(okResult.StatusCode, expectCode);

                //Check the id
                var val = (Restaurant)okResult.Value;
                Assert.Equal(val.Id, id);
            }
        }
        [Fact]
        public void Test_List()
        {
            //Act
            var result = _controller.List();

            var okResult = result as ObjectResult;

            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            IEnumerable<Restaurant> values = okResult.Value as IEnumerable<Restaurant>;
            Assert.NotNull(values);
            Assert.NotEmpty(values);
        }

        public static IEnumerable<object[]> TestAddData => new List<object[]>
        {
            new object[]
            {
                //normal
                new Restaurant { Id=12, Name="Test", Location="TestLocation", Description="TestDescription", Cuisine=CuisineType.Italian },
                StatusCodes.Status201Created
            },
            new object[]
            {
                //Empty name
                null,
                StatusCodes.Status400BadRequest
            }
        };


        [Theory]
        [MemberData(nameof(TestAddData))]
        public void Test_Add(Restaurant res, int expectedCode)
        {
            //Act
            var result = _controller.Add(res);
            var okResult = result as ObjectResult;

            //Assert
            if (okResult == null)
            {
                var badRequest = result as StatusCodeResult;
                Assert.NotNull(badRequest);
                Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
                Assert.Equal(expectedCode, badRequest.StatusCode);
            }
            else
            {
                Assert.NotNull(okResult);
                Assert.True(okResult is CreatedResult);

                Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
                Assert.Equal(expectedCode, okResult.StatusCode);

                Assert.IsType<Restaurant>(okResult.Value);
                //Check if present
                var val = (Restaurant)okResult.Value;
                Assert.True(_repo.TryGetById(val.Id, ref val));
            }
        }

        public static IEnumerable<object[]> TestUpdateData => new List<object[]>
        {
            new object[]
            {
                //ProperUpdate
                new Restaurant { Id=1, Name="UpdatedName", Location="TestLocation", Description="TestDescription", Cuisine=CuisineType.Italian },
                StatusCodes.Status200OK
            },
            new object[]
            {
                //No such id
                new Restaurant { Id=0, Name="Test", Location="TestLocation", Description="TestDescription", Cuisine=CuisineType.Italian },
                StatusCodes.Status400BadRequest
            },
            new object[]
            {
                //ProperUpdate
                null,
                StatusCodes.Status400BadRequest
            }

        };

        [Theory]
        [MemberData(nameof(TestUpdateData))]
        public void Test_Update(Restaurant expected, int expectedCode)
        {
            //Act
            var result = _controller.Update(expected);
            var okResult = result as ObjectResult;

            //Assert
            if (okResult == null)
            {
                var badRequest = result as StatusCodeResult;
                Assert.NotNull(badRequest);
                Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
                Assert.Equal(expectedCode, badRequest.StatusCode);
            }
            else
            {
                Assert.NotNull(okResult);
                Assert.True(okResult is OkObjectResult);

                Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
                Assert.Equal(expectedCode, okResult.StatusCode);

                Assert.IsType<Restaurant>(okResult.Value);
                //Check if equal
                var res = (Restaurant)okResult.Value;

                Assert.Equal(res.Cuisine,     expected.Cuisine);
                Assert.Equal(res.Name,        expected.Name);
                Assert.Equal(res.Location,    expected.Location);
                Assert.Equal(res.Cuisine,     expected.Cuisine);
                Assert.Equal(res.Description, expected.Description);
            }
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(0, 404)]
        public void Test_Remove(int id, int expectedCode)
        {
            //Act
            var result = _controller.Remove(id);
            var resultStatus = result as StatusCodeResult;

            //Assert
            if (resultStatus.StatusCode == StatusCodes.Status200OK)
            {
                //Check if not present
                Restaurant val = null;
                Assert.False(_repo.TryGetById(id, ref val));
            }
            else if(resultStatus.StatusCode == StatusCodes.Status404NotFound)
            {
                var notFound = result as StatusCodeResult;
                Assert.NotNull(notFound);
                Assert.Equal(StatusCodes.Status404NotFound, notFound.StatusCode);
            }

            Assert.Equal(resultStatus.StatusCode, expectedCode);
        }
    }
}
