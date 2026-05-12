using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace spydle_api.Mappers
{
    public static class ModelStateMapper
    {
        public static string[] ToErrorCollection(this ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(v => v.Errors.Select(modelError => modelError.ErrorMessage)).ToArray();
        }
    }
}
