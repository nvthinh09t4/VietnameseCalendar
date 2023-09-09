using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using webapi.Extensions;

namespace webapi.Services
{
    public enum eConsHoloscope
    {
        [Display(Name = "Xuất hành")]
        XuatHanh,
        [Display(Name = "Quý nhân phù trợ")]
        QuyNhanPhuTro,
        [Display(Name = "Cầu tài như ý muốn")]
        CauTaiNhuYMuon,
        [Display(Name = "Cầu nhân duyên như ý muốn")]
        CauNhanDuyenNhuYMuon,
        [Display(Name = "Thăng tiến")]
        ThangTien,
        [Display(Name = "Tài lộc thông suốt")]
        TaiLocThongSuot,
        [Display(Name = "Gặp dữ hóa lành")]
        GapDuHoaLanh,
        [Display(Name = "Mọi việc thuận lợi như ý muốn")]
        MoiViecThuanLoi,
        [Display(Name = "Người tốt giúp đỡ")]
        NguoiTotGiupDo,
        [Display(Name = "Buôn may bán đắt")]
        BuonMayBanDat,
    }

    public enum eProsHoloscope
    {
        [Display(Name = "Xuất hành")]
        XuatHanh,
        [Display(Name = "Gặp điều bất lợi")]
        GapDieuBatLoi,
        [Display(Name = "Nhiều điều không may mắn")]
        KhongMayMan,
        [Display(Name = "Bị hại")]
        BiHai,
        [Display(Name = "Mất tiền của, tiêu hao tiền tài")]
        TieuHaoTienTai,
        [Display(Name = "Cầu tài không được")]
        CauTaiKhongDuoc,
        [Display(Name = "Gặp nhiều thị phi, cãi cọ")]
        ThiPhi,
        [Display(Name = "Xảy ra nhiều tai nạn nặng")]
        TaiNanNang,
    }

    public class HoloscopeDate
    {
        public string DateName { get; set; }
        public string Description { get; set; }
        public bool IsAGoodDay => Cons?.Count > 0;
        [JsonIgnore]
        public List<eConsHoloscope> Cons { get; set; }
        [JsonIgnore]
        public List<eProsHoloscope> Pros { get; set; }
        public List<String>? ConsArr => Cons?.Select(x => EnumHelper<eConsHoloscope>.GetDisplayValue(x)).ToList();
        public List<String>? ProsArr => Pros?.Select(x => EnumHelper<eProsHoloscope>.GetDisplayValue(x)).ToList();
    }

    public interface IHoroscopeCalendarService
    {
        //HoloscopeDate ToHoloscopeDate(int year, int month, int day);
        HoloscopeDate ToHoloscopeDate(int lunarMonth, int lunarDay);
    }
}
