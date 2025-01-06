namespace KUX.Models.CzimAdmin.Reponse.Chat
{
    /// <summary>
    /// 群组管理员信息
    /// </summary>
    public class ChatGroupManagerVo
    {
        public string UserId { get; set; }
        public string Avatar { get; set; }
        public string NickName { get; set; }
        public bool IsManager { get; set; } = false;
        public bool IsOwner { get; set; } = false;
    }
}