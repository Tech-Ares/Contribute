using KUX.Models.Enums;

namespace KUX.Models.CzimAdmin.Request.CzimMember
{
    /// <summary>
    /// 会员扫码加好友\群\聊天室
    /// </summary>
    public class ScanQrCodeDto
    {
        /// <summary>
        /// 扫码类型
        /// </summary>
        public CzTypeEnum CzType { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string QrCode { get; set; }
    }
}