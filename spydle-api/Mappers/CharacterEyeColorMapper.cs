using spydle_api.DTOs;
using spydle_api.Models;

namespace spydle_api.Mappers
{
    public static class CharacterEyeColorMapper
    {
        public static EyeColorDTO ToEyeColorDTO(this CharacterEyeColor eyeColor)
        {
            return new EyeColorDTO { Id = eyeColor.Id, Red = eyeColor.Red, Green = eyeColor.Green, Blue = eyeColor.Blue };
        }
    }
}
