namespace Fungus
{
    public class ObjectClickedEvent
    {
        public Clickable2D ClickableObject;
        public ObjectClickedEvent(Clickable2D clickableObject)
        {
            ClickableObject = clickableObject;
        }
    }
}
