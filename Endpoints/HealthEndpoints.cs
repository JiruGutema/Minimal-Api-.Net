namespace EndPoints
{
    public class HealthEndpoints
    {
        public IResult GetRootEndpoint()
        {
            Console.WriteLine("Health check endpoint called");
            return Results.Ok("API is running smoothly!");
        }
    }
}
