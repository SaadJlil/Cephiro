using Cephiro.Identity.Application.Shared.Contracts;
using PhoneNumbers;

namespace Cephiro.Identity.Contracts.Internal;

public struct PhoneNumberInternal
{
    public PhoneNumber? PhoneNumber { get; set; }
    public Error? Error { get; set; }
}
