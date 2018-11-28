public class ColourActivator : AbstractActivator {

    public ColourEnum colour = ColourEnum.red;

    protected override void BeforeActivatedSent()
    {
        ColourHandler.AddCount(colour, 1);
    }

    protected override void BeforeDeactivatedSent()
    {
        ColourHandler.AddCount(colour, -1);
    }
}
