using spydle_api.Constants;

namespace spydle_api.Services
{
    public static class CharacterService
    {
        private static readonly CharacterTraitMask[] _traitMasks = Enum.GetValues<CharacterTraitMask>();

        public static bool AreCharactersSharingTraits(int firstCharCode, int secondCharCode)
        {
            
            for (int i = 0; i < _traitMasks.Length; i++)
            {
                if ((firstCharCode & (int)_traitMasks[i]) == (secondCharCode & (int)_traitMasks[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}