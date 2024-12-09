using Desafio.Blog.Luza.Presentation.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddPresentationServices(builder.Configuration);
        builder.Services.AddSwaggerDocumentation();
        var app = builder.Build();
        app.UsePresentationPipeline();

        app.Run();
    }
}
