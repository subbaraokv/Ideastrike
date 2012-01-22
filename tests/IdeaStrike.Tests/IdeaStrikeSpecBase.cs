using System.Collections.Generic;
using Autofac;
using Ideastrike.Nancy.Models;
using Ideastrike.Nancy.Models.Repositories;
using Moq;
using Nancy;
using Nancy.Testing;

namespace IdeaStrike.Tests
{
    public class IdeaStrikeSpecBase
    {
        public Mock<IActivityRepository> mockActivityRepo;
        public Mock<IFeatureRepository> mockFeatureRepo;
        public Mock<IIdeaRepository> mockIdeasRepo;
        public Mock<IdeastrikeContext> mockIdeaStrikeContext;
        public Mock<ISettingsRepository> mockSettingsRepo;
        public Mock<IUserRepository> mockUsersRepo;
        
        protected Response testResponse;
        protected INancyEngine engine;
        protected Browser browser;

        public IdeaStrikeSpecBase()
        {
            CreateMocks();
            ContainerBuilder builder = CreateContainerBuilder();

            var ideaStrikeTestBootstrapper = new IdeaStrikeTestBootStrapper(CreateContainerBuilder);
            ideaStrikeTestBootstrapper.Initialise();
            engine = ideaStrikeTestBootstrapper.GetEngine();
            browser = new Browser(ideaStrikeTestBootstrapper);
        }

        private void CreateMocks()
        {
            mockActivityRepo = new Mock<IActivityRepository>();
            mockFeatureRepo = new Mock<IFeatureRepository>();
            mockIdeasRepo = new Mock<IIdeaRepository>();
            mockSettingsRepo = new Mock<ISettingsRepository>();
            mockUsersRepo = new Mock<IUserRepository>();
            mockIdeaStrikeContext = new Mock<IdeastrikeContext>();
        }

        private ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance<ISettingsRepository>(mockSettingsRepo.Object);
            builder.RegisterInstance<IIdeaRepository>(mockIdeasRepo.Object);
            builder.RegisterInstance<IActivityRepository>(mockActivityRepo.Object);
            builder.RegisterInstance<IFeatureRepository>(mockFeatureRepo.Object);
            builder.RegisterInstance<IUserRepository>(mockUsersRepo.Object);
            builder.RegisterInstance<IdeastrikeContext>(mockIdeaStrikeContext.Object);
            
            return builder;
        }

        private static Request CreateTestRequest(string httpMethod, string route)
        {
            return new Request(httpMethod, route, "http");
        }

        protected static Request GetTestRequest(string route)
        {
            return CreateTestRequest("Get", route);
        }

        public static Request PostTestRequest(string route)
        {
            return CreateTestRequest("POST", route);
        }
    }
}

