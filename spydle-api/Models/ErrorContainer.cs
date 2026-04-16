using System.Text.Json;

namespace spydle_api.Models
{
    public class ErrorContainer
    {
        public string[] Errors { get; }

        public ErrorContainer(params string[] errors)
        {
            Errors = errors;
        }
    }
}
