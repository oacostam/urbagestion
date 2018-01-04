using System;

namespace Urbagestion.Util
{
    public static class EnumExtension
    {
        public static string ToEnumName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }
    }
}
