using PhoneGallery.DreamFXX.Data;

namespace PhoneGallery.DreamFXX.Services;

public class PhoneGalleryService
{
    private readonly PhoneGalleryContext _context;

    public PhoneGalleryService(PhoneGalleryContext context)
    {
        _context = context;
    }

}

