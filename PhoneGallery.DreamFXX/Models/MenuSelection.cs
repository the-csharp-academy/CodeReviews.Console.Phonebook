namespace PhoneGallery.DreamFXX.Models;

public class MenuSelection
{
    public string Name { get; set; }
    public MenuSelection Menuselection { get; set; }


    public MenuSelection(string name, MenuSelection menuselection)
    {
        Name = name;
        Menuselection = menuselection;
    }
}

