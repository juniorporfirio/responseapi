using Xunit;
using JuniorPorfirio.ResponseApi;
using JuniorPorfirio.ResponseApi.FluentValidation.UnitTests;
using JuniorPorfirio.ResponseApi.FluentValidation.UnitTests.Validations;
using JuniorPorfirio.ResponseApi.FluentValidation;

namespace ResponseApi.FluentValidation.UnitTests
{
	public class ResponseApiFluentValidationTest
	{

		[Fact]
		public void Should_use_fluentvalidation_to_return_invalid()
		{
			//Arrange
			var people = new People();

			//Act
			var validator = new PeopleValidator();
			var validate = validator.Validate(people);
			var response = ResponseApi<People>.Against(people).IsInvalid(validate);

			//Arrange
			Assert.Equal(ResponseApiStatusCode.Invalid, response.Status);
			Assert.Equal(response.Errors, validate.AsMessages());

		}

		[Fact]
		public void Should_use_fluentvalidation_to_return_success()
		{
			//Arrange
			var people = new People() { FirstName = "Steve", LastName = "Gordon" };

			//Act
			var validator = new PeopleValidator();
			var validate = validator.Validate(people);
			var response = ResponseApi<People>.Against(people).IsInvalid(validate);

			//Arrange
			Assert.Equal(ResponseApiStatusCode.Success, response.Status);
			Assert.True(validate.IsValid);
			Assert.Empty(response.Errors);

		}
	}
}