using Microsoft.EntityFrameworkCore;
using PhoneGallery.DreamFXX.Data;
using PhoneGallery.DreamFXX.Models;

namespace PhoneGallery.DreamFXX.Services;

public class PhoneGalleryService
{
    private readonly PhoneGalleryContext _context;

    public PhoneGalleryService(PhoneGalleryContext context)
    {
        _context = context;
    }


}

