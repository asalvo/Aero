using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aero.Models
{
    /// <summary>
    /// A framework independent class that implements RFC7807, with the addition of an Errors dictionary.
    /// This class can be sub-classed to make a more specific problem details class with additional properties if needed.
    /// </summary>
    public class ProblemDetailsModel
    {
        public const string NotFoundType = "general/404";
        public const string UnspecifiedErrorKey = "_General_";
        public const string UnspecifiedErrorType = "general/100";
        public const string UnspecifiedErrorTitle = "Unspecified Error";

        /// <summary>
        /// A parameterless constructor for System.Text.Json. Intellisense will make it look like
        /// if you use this that you will get the default values for Title and Type, but you won't
        /// </summary>
        public ProblemDetailsModel()
        {
            //Required for System.Text.Json 
        }

        public ProblemDetailsModel(string title = UnspecifiedErrorTitle, string type = UnspecifiedErrorType)
        {
            //Initialize by default, but we will have a public setter for easy initialization in our extension method
            Errors = new Dictionary<string, List<string>>();

            Type = type;
            Title = title;
        }

        public string Type { get; set; }
        public string Title { get; set; }
        public int? Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }

        public Dictionary<string, List<string>> Errors { get; set; }

        /// <summary>
        /// Adds a general error with a key value of "_General_"
        /// </summary>
        public void AddError(string message)
        {
            AddError(UnspecifiedErrorKey, message);
        }

        /// <summary>
        /// Adds an error with the specified Key which can be used for matching up to ModelState errors.
        /// </summary>
        public void AddError(string key, string message)
        {
            List<string> errorList;

            if (!Errors.ContainsKey(key))
            {
                errorList = new List<string>();
                Errors.Add(key, errorList);
            }
            else
            {
                errorList = Errors[key];
            }

            if (errorList.All(x => x != message))
            {
                errorList.Add(message);
            }
        }

        public string ToLogString(bool includeDetail = false, bool includeErrors = false)
        {
            //Should always have Title and type
            var sb = new StringBuilder($"PdTitle: {Title}, PdType: {Type}");

            if (Status.HasValue)
            {
                sb.AppendFormat(", PdStatus: {0}", Status);
            }

            if (!string.IsNullOrWhiteSpace(Instance))
            {
                sb.AppendFormat(", PdInstance: {0}", Instance);
            }

            if (!string.IsNullOrWhiteSpace(Detail) && includeDetail)
            {
                sb.AppendFormat(", PdDetail: {0}", Detail);
            }

            if (Errors.Count > 0 && includeErrors)
            {
                foreach (var key in Errors.Keys)
                {
                    sb.AppendFormat(", PdE.{0}: {1}", key, string.Join("|", Errors[key]));
                }
            }

            return sb.ToString();
        }
    }
}
