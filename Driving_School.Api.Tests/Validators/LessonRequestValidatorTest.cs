using Driving_School.Api.ModelsRequest.Lesson;
using Driving_School.Api.Validators.Lesson;
using Driving_School.Repositories.Contracts.Interface;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Driving_School.Api.Tests.Validators
{
    public class LessonRequestValidatorTest
    {
        private readonly CreateLessonRequestValidator validationCreateRequest;
        private readonly LessonRequestValidator validationRequest;

        private readonly Mock<IEmployeeReadRepository> employeeReadRepositoryMock;
        private readonly Mock<IPlaceReadRepository> placeReadRepositoryMock;
        private readonly Mock<ITransportReadRepository> transportReadRepositoryMock;
        private readonly Mock<ICourseReadRepository> courseReadRepositoryMock;

        public LessonRequestValidatorTest()
        {
            employeeReadRepositoryMock = new Mock<IEmployeeReadRepository>();
            placeReadRepositoryMock = new Mock<IPlaceReadRepository>();
            transportReadRepositoryMock = new Mock<ITransportReadRepository>();
            courseReadRepositoryMock = new Mock<ICourseReadRepository>();
            validationCreateRequest = new CreateLessonRequestValidator(employeeReadRepositoryMock.Object, placeReadRepositoryMock.Object,
                transportReadRepositoryMock.Object, courseReadRepositoryMock.Object);
            validationRequest = new LessonRequestValidator(employeeReadRepositoryMock.Object, placeReadRepositoryMock.Object,
                transportReadRepositoryMock.Object, courseReadRepositoryMock.Object);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new LessonRequest();

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.Id);
            validation.ShouldHaveValidationErrorFor(x => x.StartDate);
            validation.ShouldHaveValidationErrorFor(x => x.EndDate);
            validation.ShouldHaveValidationErrorFor(x => x.Place);
            validation.ShouldHaveValidationErrorFor(x => x.Student);
            validation.ShouldHaveValidationErrorFor(x => x.Instructor);
            validation.ShouldHaveValidationErrorFor(x => x.Course);
            validation.ShouldHaveValidationErrorFor(x => x.Transport);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.LessonRequest();

            employeeReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Instructor, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            employeeReadRepositoryMock.Setup(x => x.AnyByIdWithInstructorAsync(model.Instructor, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            employeeReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Student, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            employeeReadRepositoryMock.Setup(x => x.AnyByIdWithStudentAsync(model.Student, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            placeReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Place, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            courseReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Course, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            transportReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Transport, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Id);
            validation.ShouldNotHaveValidationErrorFor(x => x.StartDate);
            validation.ShouldNotHaveValidationErrorFor(x => x.EndDate);
            validation.ShouldNotHaveValidationErrorFor(x => x.Place);
            validation.ShouldNotHaveValidationErrorFor(x => x.Student);
            validation.ShouldNotHaveValidationErrorFor(x => x.Instructor);
            validation.ShouldNotHaveValidationErrorFor(x => x.Course);
            validation.ShouldNotHaveValidationErrorFor(x => x.Transport);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateLessonRequest();

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.StartDate);
            validation.ShouldHaveValidationErrorFor(x => x.EndDate);
            validation.ShouldHaveValidationErrorFor(x => x.Place);
            validation.ShouldHaveValidationErrorFor(x => x.Student);
            validation.ShouldHaveValidationErrorFor(x => x.Instructor);
            validation.ShouldHaveValidationErrorFor(x => x.Course);
            validation.ShouldHaveValidationErrorFor(x => x.Transport);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.CreateLessonRequest();

            employeeReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Instructor, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            employeeReadRepositoryMock.Setup(x => x.AnyByIdWithInstructorAsync(model.Instructor, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            employeeReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Student, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            employeeReadRepositoryMock.Setup(x => x.AnyByIdWithStudentAsync(model.Student, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            placeReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Place, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            courseReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Course, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            transportReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.Transport, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.StartDate);
            validation.ShouldNotHaveValidationErrorFor(x => x.EndDate);
            validation.ShouldNotHaveValidationErrorFor(x => x.Place);
            validation.ShouldNotHaveValidationErrorFor(x => x.Student);
            validation.ShouldNotHaveValidationErrorFor(x => x.Instructor);
            validation.ShouldNotHaveValidationErrorFor(x => x.Course);
            validation.ShouldNotHaveValidationErrorFor(x => x.Transport);
        }
    }
}
