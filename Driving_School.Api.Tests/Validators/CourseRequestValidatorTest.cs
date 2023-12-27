using Driving_School.Api.ModelsRequest.Course;
using Driving_School.Api.Validators.Course;
using FluentValidation.TestHelper;
using Xunit;

namespace Driving_School.Api.Tests.Validators
{
    public class CourseRequestValidatorTest
    {
        private readonly CreateCourseRequestValidator validationCreateRequest;
        private readonly CourseRequestValidator validationRequest;

        public CourseRequestValidatorTest()
        {
            validationCreateRequest = new CreateCourseRequestValidator();
            validationRequest = new CourseRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new CourseRequest();

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.Id);
            validation.ShouldHaveValidationErrorFor(x => x.Name);
            validation.ShouldHaveValidationErrorFor(x => x.Description);
            validation.ShouldHaveValidationErrorFor(x => x.Duration);
            validation.ShouldHaveValidationErrorFor(x => x.Price);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.CourseRequest();

            //Act
            var validation = await validationRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Id);
            validation.ShouldNotHaveValidationErrorFor(x => x.Name);
            validation.ShouldNotHaveValidationErrorFor(x => x.Description);
            validation.ShouldNotHaveValidationErrorFor(x => x.Duration);
            validation.ShouldNotHaveValidationErrorFor(x => x.Price);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateCourseRequest();

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveValidationErrorFor(x => x.Name);
            validation.ShouldHaveValidationErrorFor(x => x.Description);
            validation.ShouldHaveValidationErrorFor(x => x.Duration);
            validation.ShouldHaveValidationErrorFor(x => x.Price);
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.CreateCourseRequest();

            //Act
            var validation = await validationCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveValidationErrorFor(x => x.Name);
            validation.ShouldNotHaveValidationErrorFor(x => x.Description);
            validation.ShouldNotHaveValidationErrorFor(x => x.Duration);
            validation.ShouldNotHaveValidationErrorFor(x => x.Price);
        }
    }
}
