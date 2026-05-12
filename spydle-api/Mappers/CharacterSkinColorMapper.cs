using spydle_api.DTOs;
using spydle_api.Models;

namespace spydle_api.Mappers
{
    public static class CharacterSkinColorMapper
    {
        public static SkinColorDTO ToSkinColorDTO(this CharacterSkinColor skinColor)
        {
            return new SkinColorDTO { Id = skinColor.Id, Red = skinColor.Red, Green = skinColor.Green, Blue = skinColor.Blue };
        }
    }
}
