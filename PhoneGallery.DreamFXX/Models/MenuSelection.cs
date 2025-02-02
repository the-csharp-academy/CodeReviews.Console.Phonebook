namespace PhoneGallery.DreamFXX.Models;

public class MenuSelection
{
    public string Name { get; set; }
    public Action Action { get; set; }

    public MenuSelection(string name, Action action)
    {
        Name = name;
        Action = action;
    }
}

