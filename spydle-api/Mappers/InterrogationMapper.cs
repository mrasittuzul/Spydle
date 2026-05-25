using spydle_api.DTOs;
using spydle_api.Entities;
using spydle_api.Helpers;

namespace spydle_api.Mappers
{
    public static class InterrogationMapper
    {
        public static InterrogationDTO ToDTO(this Interrogation interrogation)
        {
            return new InterrogationDTO()
            {
                DateTimeInMiliseconds = TimeHelper.GetTimeSinceUnixEpochInMilisecondsFromDateTime(interrogation.DateTime),
                Suspects = interrogation.Suspects,
                FoundMatchingTrait = interrogation.FoundMatchingTrait
            };
        }
    }
}
