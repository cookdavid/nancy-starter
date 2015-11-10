using Nancy;

namespace Nancy_Starter.Endpoints
{
    public class SampleEndpoint : NancyModule
    {
        public SampleEndpoint() : base("/sample")
        {
            Get["/"] = _ => GetSsampleData();
        }

        private Response GetSsampleData()
        {
            return "hello world";
        }
    }
}