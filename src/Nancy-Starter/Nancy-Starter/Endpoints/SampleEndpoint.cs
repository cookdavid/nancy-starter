using Nancy;

namespace Nancy_Starter.Endpoints
{
    public class SampleEndpoint : NancyModule
    {
        public SampleEndpoint() : base("/sample")
        {
            Get["/"] = _ => GetSampleData();
        }

        private Response GetSampleData()
        {
            return "hello world";
        }
    }
}