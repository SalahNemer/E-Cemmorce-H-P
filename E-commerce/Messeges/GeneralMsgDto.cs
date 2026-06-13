using System.Text.Json.Serialization;

namespace loginpage.DBcon
{
    public class GeneralMsgDto
    {
        public GeneralMsgDto() { }
        public GeneralMsgDto(string errorMsg, string errorDesc, string errorCode)
        {
            ErrorMsg = errorMsg;
            ErrorCode = errorCode;
            ErrorDesc = errorDesc;
        }

        public string ErrorMsg { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDesc { get; set; }

        [JsonIgnore]
        public bool IsSuccess => ErrorCode == "200";  
    }
}
