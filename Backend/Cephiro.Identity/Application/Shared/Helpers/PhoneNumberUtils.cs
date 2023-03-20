using Cephiro.Identity.Contracts.Internal;
using PhoneNumbers;

namespace Cephiro.Identity.Application.Shared.Helpers;

public static class PhoneNumberUtils
{

    public static PhoneNumberInternal ValidNumber(string phoneNumber)
    {
        PhoneNumberInternal result = new() {PhoneNumber = null, Error = null};

        var util = PhoneNumberUtil.GetInstance();

        try 
        { 
            result.PhoneNumber = util.Parse(phoneNumber, "ZZ"); 
        }

        catch (NumberParseException e) 
        { 
            result.Error = new() { Code = 400, Message = e.Message};
            return result;
        };

        if(!util.IsValidNumber(result.PhoneNumber))
        {
            result.PhoneNumber = null;
            result.Error = new() { Code = 400, Message = "This phone number is not valid"};
            return result;
        }

        else return result;
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