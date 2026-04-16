using spydle_api.Models;
using System.Text.Json;

namespace spydle_api.Mappers
{
    public static class ErrorContainerMapper
    {
        public static string ToJson(this ErrorContainer errorContainer)
        {
            return JsonSerializer.Serialize(errorContainer);
        }
    }
}
