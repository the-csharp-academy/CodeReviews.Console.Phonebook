using PhoneBook.Model;

namespace PhoneBook.EntityDTOMapping;

internal class ContactMapper
{
    public static List<PhoneBookDTO> PhoneBookList(List<PhoneBookDetails> phoneBookDetails)
    {
        return phoneBookDetails
            .Select((pb, index) => new PhoneBookDTO
            {
                DisplayId = index + 1,
                Name = pb.Name ?? string.Empty,
                PhoneNumber = pb.PhoneNumber ?? string.Empty,
                EmailAddress = pb.EmailAddress ?? string.Empty
            })
            .ToList();
    }
}
