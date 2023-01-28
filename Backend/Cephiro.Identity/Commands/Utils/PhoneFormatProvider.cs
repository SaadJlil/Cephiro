using PhoneNumbers;

namespace Cephiro.Identity.Commands.Utils;

public static class PhoneFormatter
{
    public static string ConvertToInternationalNumber(string phoneNumber)
    {
        var util = PhoneNumberUtil.GetInstance();
        PhoneNumber numberProto = util.Parse(phoneNumber, "ZZ");

        return util.Format(numberProto, PhoneNumberFormat.INTERNATIONAL);
    }

    public static string? FormatPhoneNumber(string phoneNumber)
    {
        var util = PhoneNumberUtil.GetInstance();
        PhoneNumber numberProto = util.Parse(phoneNumber, "ZZ");

        if(numberProto is null) return null;

        return numberProto!.NationalNumber.ToString();
    }
}