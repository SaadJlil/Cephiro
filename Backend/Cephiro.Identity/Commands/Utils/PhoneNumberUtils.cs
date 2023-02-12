using ErrorOr;
using PhoneNumbers;

namespace Cephiro.Identity.Commands.Utils;

public static class PhoneNumberUtils
{

    public static ErrorOr<PhoneNumber> IsValidNumber(string phoneNumber)
    {
        var util = PhoneNumberUtil.GetInstance();
        PhoneNumber numberProto;

        try 
        { 
            numberProto = util.Parse(phoneNumber, "ZZ"); 
        }

        catch (NumberParseException e) 
        { 
            return Error.Validation(e.Message);
        };

        if(!util.IsValidNumber(numberProto))
            return Error.Validation("");
        
        else return numberProto;
    }

    public static void ConvertToInternationalNumber(PhoneNumber numberProto, out string international)
    {
        var util = PhoneNumberUtil.GetInstance();
        
        international =  util.Format(numberProto, PhoneNumberFormat.INTERNATIONAL);
    }

    public static void ConvertToNationalNumber(PhoneNumber numberProto, out string country, out string national)
    {
        national =  numberProto.NationalNumber.ToString();
        country = numberProto.CountryCode.ToString();
    }
}