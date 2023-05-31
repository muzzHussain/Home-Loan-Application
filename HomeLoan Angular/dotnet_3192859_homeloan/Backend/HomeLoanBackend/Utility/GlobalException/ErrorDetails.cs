
using System.Text.Json;

namespace Utility.Global_Exceptions
{
    /// <summary>
    ///     To show error message in JSON format
    /// </summary>
    internal class ErrorDetails
    {
        public int Status { get; set; }

        public object Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
