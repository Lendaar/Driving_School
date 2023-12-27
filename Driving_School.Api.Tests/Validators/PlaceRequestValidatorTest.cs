using Driving_School.Api.ModelsRequest.Place;
using Driving_School.Api.Validators.Place;
using FluentValidation.TestHelper;
using Xunit;

namespace Driving_School.Api.Tests.Validators
{
    /// <summary>
    /// Тесты <see cref="PlaceRequestValidator"/>
    /// </summary>
    public class PlaceRequestValidatorTest
    {
        private readonly CreatePlaceRequestValidator validatorCreateRequest;
        private readonly PlaceRequestValidator validatorRequest;

        /// <summary>
        /// ctor
        /// </summary>
        public PlaceRequestValidatorTest()
        {
            validatorRequest = new PlaceRequestValidator();
            validatorCreateRequest = new CreatePlaceRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new PlaceRequest();

            // Act
            var validation = validatorRequest.TestValidate(model);

            // Assert
            validation.ShouldHaveValidationErrorFor(x => x.Id);
            validation.ShouldHaveValidationErrorFor(x => x.Name);
            validation.ShouldHaveValidationErrorFor(x => x.Address);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.PlaceRequest();

            // Act
            var validation = validatorRequest.TestValidate(model);

            // Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Id);
            validation.ShouldNotHaveValidationErrorFor(x => x.Name);
            validation.ShouldNotHaveValidationErrorFor(x => x.Description);
            validation.ShouldNotHaveValidationErrorFor(x => x.Address);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreatePlaceRequest();

            // Act
            var validation = validatorCreateRequest.TestValidate(model);

            // Assert
            validation.ShouldHaveValidationErrorFor(x => x.Name);
            validation.ShouldHaveValidationErrorFor(x => x.Address);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.CreatePlaceRequest();

            // Act
            var validation = validatorCreateRequest.TestValidate(model);

            // Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Name);
            validation.ShouldNotHaveValidationErrorFor(x => x.Description);
            validation.ShouldNotHaveValidationErrorFor(x => x.Address);
        }
    }
}
