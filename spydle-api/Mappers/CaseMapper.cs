using spydle_api.DTOs;
using spydle_api.Models;

namespace spydle_api.Mappers
{
    public static class CaseMapper
    {
        public static CaseDTO ToCaseDTO(this Case caseToConvert)
        {
            return new CaseDTO { IncludedCharacters = caseToConvert.IncludedCharacters, RngSeed = caseToConvert.RngSeed };
        }
    }
}
