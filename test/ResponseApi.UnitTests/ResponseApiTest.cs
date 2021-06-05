using System;
using System.Collections.Generic;
using Xunit;

namespace JuniorPorfirio.ResponseApi.UnitTests
{

    public class ResponseApiTest
    {
        [Fact]
        public void Should_return_sucess_with_response()
        {
            //Arrange
            var body = "Hello, world !";
            //Act
            var response = ResponseApi<string>.Success(body);

            //Assert
            Assert.Equal(body, response.GetValue());
            Assert.Equal(ResponseApiStatusCode.Success, response.Status);

        }

        [Fact]
        public void Should_return_created_with_response()
        {
            //Arrange
            var body = "Hello, world !";

            //Act
            var response = ResponseApi<string>.Created(body);

            //Assert
            Assert.Equal(body, response.GetValue());
            Assert.Equal(ResponseApiStatusCode.Created, response.Status);

        }

        [Fact]
        public void Should_return_error_with_response()
        {
            //Arrange
            var message ="Error" ;

            //Act
            var response = ResponseApi<string>.Error(message);

            //Assert
            Assert.Null(response.GetValue());
            Assert.Equal(message, response.Message);
            Assert.Equal(ResponseApiStatusCode.Error, response.Status);

        }

        [Fact]
        public void Should_return_invalid_with_response()
        {
            //Arrange
            var body = new Dictionary<string,string> {
                {"Name", "Name is required." }
            };

            //Act
            var response = ResponseApi<string>.Invalid(body);

            //Assert
            Assert.Equal(body, response.Errors);
            Assert.Equal(ResponseApiStatusCode.Invalid, response.Status);

        }

        [Fact]
        public void Should_return_unauthorazed_with_response()
        {
            //Arrange
            var message = "Unauthorized user";
            //Act
            var response = ResponseApi<string>.Unauthorized(message);

            //Assert
            Assert.Equal(message, response.Message);
            Assert.Equal(ResponseApiStatusCode.Unauthorized, response.Status);

        }

        [Fact]
        public void Should_return_notfound_with_response()
        {
            //Arrange & Act
            var response = ResponseApi<string>.NotFound();

            //Assert
            Assert.Equal(ResponseApiStatusCode.NotFound, response.Status);

        }

        [Fact]
        public void Should_validate_null_with_response_succes()
        {
            //Arrange
            var message = "Hello, world";
            //Act
            var response = ResponseApi<string>.Against(message).IsNull();

            //Assert
            Assert.Equal(message, response.GetValue());
            Assert.Equal(ResponseApiStatusCode.Success, response.Status);

        }

        [Fact]
        public void Should_validate_null_with_response_notfound()
        {
            //Arrange
            string message = null;
            //Act
            var response = ResponseApi<string>.Against(message).IsNull();

            //Assert
            Assert.Null(response.GetValue());
            Assert.Equal(ResponseApiStatusCode.NotFound, response.Status);

        }

        [Fact]
        public void Should_validate_any_with_response_success()
        {
            //Arrange
            var body = new List<string> { "Hello", "World" };
            //Act
            var response = ResponseApi<IEnumerable<string>>.Against(body).IsAny();

            //Assert
            Assert.Equal(body, response.GetValue());
            Assert.Equal(ResponseApiStatusCode.Success, response.Status);

        }

        [Fact]
        public void Should_validate_any_with_response_notfound()
        {
            //Arrange
            var body = new List<string>();
            //Act
            var response = ResponseApi<IEnumerable<string>>.Against(body).IsAny();

            //Assert
            Assert.Null(response.GetValue());
            Assert.Equal(ResponseApiStatusCode.NotFound, response.Status);

        }

        [Fact]
        public void Should_validate_function_response_success()
        {
            //Arrange
            var value = "Hello, world";
            Func<string> body = () => value;
            //Act
            var response = ResponseApi<string>.IsError(body);

            //Assert
            Assert.Equal(value, response.GetValue());
            Assert.Equal(ResponseApiStatusCode.Success, response.Status);

        }

        [Fact]
        public void Should_validate_function_response_error()
        {
            //Arrange
            var value = "Error";
            Func<string> body = () => throw new Exception(value);
            //Act
            var response = ResponseApi<string>.IsError(body);

            //Assert
            Assert.Null(response.GetValue());
            Assert.Equal(value, response.Message);
            Assert.Equal(ResponseApiStatusCode.Error, response.Status);

        }
    }
}
