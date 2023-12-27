using Driving_School.Api.ModelsRequest.Employee;
using Driving_School.Api.Validators.Employee;
using Driving_School.Repositories.Contracts.Interface;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Driving_School.Api.Tests.Validators
{
    public class EmployeeRequestValidatorTest
    {
        private readonly CreateEmployeeRequestValidator validationCreateRequest;
        private readonly EmployeeRequestValidator validationRequest;

        private readonly Mock<IPersonReadRepository> personReadRepositoryMock;
        public EmployeeRequestValidatorTest()
        {
            personReadRepositoryMock = new Mock<IPersonReadRepository>();
            validationCreateRequest = new CreateEmployeeRequestValidator(personReadRepositoryMock.Object);
            validationRequest = new EmployeeRequestValidator(personReadRepositoryMock.Object);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new EmployeeRequest();

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.Id);
            validation.ShouldHaveValidationErrorFor(x => x.Email);
            validation.ShouldHaveValidationErrorFor(x => x.Number);
            validation.ShouldHaveValidationErrorFor(x => x.Person);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.EmployeeRequest();

            personReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Person, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Id);
            validation.ShouldNotHaveValidationErrorFor(x => x.Email);
            validation.ShouldNotHaveValidationErrorFor(x => x.Experience);
            validation.ShouldNotHaveValidationErrorFor(x => x.Number);
            validation.ShouldNotHaveValidationErrorFor(x => x.Person);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateEmployeeRequest();

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.Email);
            validation.ShouldHaveValidationErrorFor(x => x.Number);
            validation.ShouldHaveValidationErrorFor(x => x.Person);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.CreateEmployeeRequest();

            personReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Person, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Email);
            validation.ShouldNotHaveValidationErrorFor(x => x.Experience);
            validation.ShouldNotHaveValidationErrorFor(x => x.Number);
            validation.ShouldNotHaveValidationErrorFor(x => x.Person);
        }
    }
}
