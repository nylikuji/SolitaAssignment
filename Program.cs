
namespace SolitaAssignment
{
    public class SolitaAssignment
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
