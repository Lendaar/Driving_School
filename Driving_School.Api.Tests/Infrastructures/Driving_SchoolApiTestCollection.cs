using Xunit;

namespace Driving_School.Api.Tests.Infrastructures
{
    [CollectionDefinition(nameof(Driving_SchoolApiTestCollection))]
    public class Driving_SchoolApiTestCollection
        : ICollectionFixture<Driving_SchoolApiFixture>
    {
    }
}
