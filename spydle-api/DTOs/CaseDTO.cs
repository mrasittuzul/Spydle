namespace spydle_api.DTOs
{
    public class CaseDTO
    {
        public int InterrogationCount { get; set; }
        public int RegularInterrogationSuspectCount { get; set; }
        public int FinalInterrogationSuspectCount { get; set; }
        public int RngSeed { get; set; }
        public int[] IncludedCharacters { get; set; } = null!;
    }
}
