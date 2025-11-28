using HUP.Application.DTOs.IdentityDtos.UserDtos;
using HUP.Core.Entities.Identity;

namespace HUP.Common.Helpers;

public static class UserHelper
{
    public static void ApplyPatch(User user, UpdateInfoDto dto, List<string>? onlyTheseFields = null)
    {
        //Mapping Dictionary
        var mapping = new Dictionary<string, string>
        {
            { "BirthPlace", "PersonalInfo.BirthPlace" },
            { "FullEnglishName", "PersonalInfo.FullEnglishName" },
            { "Address", "ContactInfo.Address" },
            { "City", "ContactInfo.City" },
            { "PhoneNumber", "ContactInfo.PhoneNumber" },
            { "Phone", "ContactInfo.Phone" },
            { "AltEmail", "ContactInfo.AltEmail" }
        };
        
        foreach (var prop in typeof(UpdateInfoDto).GetProperties())
        {
            // If a restriction list exists, and this property isn't in it, SKIP IT.
            if (onlyTheseFields != null && !onlyTheseFields.Contains(prop.Name))
            {
                continue;
            }

            var value = prop.GetValue(dto);
            //skip null values in dto
            if (value == null || (value is string str && string.IsNullOrEmpty(str)))
            {
                continue;
            }

            //update the user entity
            if (mapping.ContainsKey(prop.Name))
            {
                SetDeepValue(user, mapping[prop.Name], value);
            }
        }

        void SetDeepValue(object target, string path, object value)
        {
            var steps = path.Split('.');
            var current = target;

            for (int i = 0; i < steps.Length; i++)
            {
                string step = steps[i];
                var type = current.GetType();
                var propInfo = type.GetProperty(step);

                if (i == steps.Length - 1)
                {
                    propInfo?.SetValue(current, value);
                }
                else
                {
                    var nextObj = propInfo?.GetValue(current);
                    current = nextObj;
                }
            }

        }
    }
}