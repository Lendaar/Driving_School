using Driving_School.Api.ModelsRequest.Transport;
using Driving_School.Api.Validators.Transport;
using FluentValidation.TestHelper;
using Xunit;

namespace Driving_School.Api.Tests.Validators
{
    /// <summary>
    /// Тесты <see cref="TransportRequestValidator"/>
    /// </summary>
    public class TransportRequestValidatorTest
    {
        private readonly CreateTransportRequestValidator validatorCreateRequest;
        private readonly TransportRequestValidator validatorRequest;

        /// <summary>
        /// ctor
        /// </summary>
        public TransportRequestValidatorTest()
        {
            validatorRequest = new TransportRequestValidator();
            validatorCreateRequest = new CreateTransportRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new TransportRequest();

            // Act
            var validation = validatorRequest.TestValidate(model);

            // Assert
            validation.ShouldHaveValidationErrorFor(x => x.Id);
            validation.ShouldHaveValidationErrorFor(x => x.Name);
            validation.ShouldHaveValidationErrorFor(x => x.Number);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.TransportRequest();

            // Act
            var validation = validatorRequest.TestValidate(model);

            // Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Id);
            validation.ShouldNotHaveValidationErrorFor(x => x.Name);
            validation.ShouldNotHaveValidationErrorFor(x => x.Number);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateTransportRequest();

            // Act
            var validation = validatorCreateRequest.TestValidate(model);

            // Assert
            validation.ShouldHaveValidationErrorFor(x => x.Name);
            validation.ShouldHaveValidationErrorFor(x => x.Number);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.CreateTransportRequest();

            // Act
            var validation = validatorCreateRequest.TestValidate(model);

            // Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Name);
            validation.ShouldNotHaveValidationErrorFor(x => x.Number);
        }
    }
}
