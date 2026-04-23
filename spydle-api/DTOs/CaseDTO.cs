namespace spydle_api.DTOs
{
    public class CaseDTO
    {
        public int RngSeed { get; set; }
        public int[] IncludedCharacters { get; set; } = null!;
    }
}
