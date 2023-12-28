using Driving_School.Api.ModelsRequest.Person;
using Driving_School.Api.ModelsRequest.Transport;
using Driving_School.Api.Validators.Person;
using Driving_School.Api.Validators.Transport;
using FluentValidation.TestHelper;
using Xunit;

namespace Driving_School.Api.Tests.Validators
{
    /// <summary>
    /// Тесты <see cref="PersonRequestValidator"/>
    /// </summary>
    public class PersonRequestValidatorTest
    {
        private readonly CreatePersonRequestValidator validatorCreateRequest;
        private readonly PersonRequestValidator validatorRequest;

        /// <summary>
        /// ctor
        /// </summary>
        public PersonRequestValidatorTest()
        {
            validatorRequest = new PersonRequestValidator();
            validatorCreateRequest = new CreatePersonRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new PersonRequest();

            // Act
            var validation = validatorRequest.TestValidate(model);

            // Assert
            validation.ShouldHaveValidationErrorFor(x => x.Id);
            validation.ShouldHaveValidationErrorFor(x => x.LastName);
            validation.ShouldHaveValidationErrorFor(x => x.FirstName);
            validation.ShouldHaveValidationErrorFor(x => x.DateOfBirthday);
            validation.ShouldHaveValidationErrorFor(x => x.Passport);
            validation.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.PersonRequest();

            // Act
            var validation = validatorRequest.TestValidate(model);

            // Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Id);
            validation.ShouldNotHaveValidationErrorFor(x => x.LastName);
            validation.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            validation.ShouldNotHaveValidationErrorFor(x => x.Patronymic);
            validation.ShouldNotHaveValidationErrorFor(x => x.DateOfBirthday);
            validation.ShouldNotHaveValidationErrorFor(x => x.Passport);
            validation.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreatePersonRequest();

            // Act
            var validation = validatorCreateRequest.TestValidate(model);

            // Assert
            validation.ShouldHaveValidationErrorFor(x => x.LastName);
            validation.ShouldHaveValidationErrorFor(x => x.FirstName);
            validation.ShouldHaveValidationErrorFor(x => x.DateOfBirthday);
            validation.ShouldHaveValidationErrorFor(x => x.Passport);
            validation.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.CreatePersonRequest();

            // Act
            var validation = validatorCreateRequest.TestValidate(model);

            // Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.LastName);
            validation.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            validation.ShouldNotHaveValidationErrorFor(x => x.Patronymic);
            validation.ShouldNotHaveValidationErrorFor(x => x.DateOfBirthday);
            validation.ShouldNotHaveValidationErrorFor(x => x.Passport);
            validation.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }
    }
}
