namespace spydle_api.Constants
{
    public enum CharacterTraitMask
    {
        Sex = 0b_0000_0000_0000_0001,
        Age = 0b_0000_0000_0000_0010, // Old, Young
        SkinColor = 0b_0000_0000_0000_1100,
        EyeColor = 0b_0000_0000_0011_0000,
    }
}
