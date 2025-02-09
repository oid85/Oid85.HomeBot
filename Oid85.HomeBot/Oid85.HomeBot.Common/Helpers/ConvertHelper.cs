using Google.Protobuf.WellKnownTypes;
using Tinkoff.InvestApi.V1;

namespace Oid85.HomeBot.Common.Helpers;

public static class ConvertHelper
{
    public static string Base64Encode(string text) => 
        Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(text));

    public static string Base64Decode(string base64) => 
        System.Text.Encoding.UTF8.GetString(
            Convert.FromBase64String(base64));
}