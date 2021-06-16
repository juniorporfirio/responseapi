using System.Data;
using FluentValidation;

namespace JuniorPorfirio.ResponseApi.FluentValidation.UnitTests.Validations
{
	public class PeopleValidator : AbstractValidator<People>
	{
		public PeopleValidator()
		{
			RuleFor(x => x.FirstName)
			.NotEmpty()
			.WithMessage("Please specify a first name");

			RuleFor(x => x.LastName)
			.NotEmpty()
			.WithMessage("Please specify a last name");
		}

	}
}