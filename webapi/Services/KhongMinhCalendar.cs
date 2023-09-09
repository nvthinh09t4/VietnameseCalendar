using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webapi.Services
{
    
    public interface IKhongMinhHoroscopeCalendarService : IHoroscopeCalendarService
    {
    }

    public class KhongMinhCalendar : IKhongMinhHoroscopeCalendarService
    {
        //private ICalendarService _calendarService;

        //public KhongMinhCalendar(ICalendarService calendarServvice)
        //{
        //    _calendarService = calendarServvice;
        //}
        //public HoloscopeDate ToHoloscopeDate(int year, int month, int day)
        //{
        //    var lunarDate = _calendarService.ToLunarDay(day, month, year);
        //    return ToHoloscopeDate(lunarMonth: lunarDate.LunarInMonth, lunarDay: lunarDate.LunarInDate);
        //}

        public HoloscopeDate ToHoloscopeDate(int lunarMonth, int lunarDay)
        {
            var holoscopeDate = new HoloscopeDate();
            switch (lunarMonth % 3)
            {
                //1 4 7 10
                case 1:
                    switch (lunarDay % 6)
                    {
                        //1 7 13 19 25
                        case 1:
                            holoscopeDate.DateName = "Ngày Đường Phong";
                            holoscopeDate.Description = "Đây là ngày tốt để xuất hành, giúp gia chủ gặp được quý nhân phù trợ, cầu tài như ý muốn. Đặc biệt gặp được nhiều thăng tiến trong công việc, cuộc sống.";
                            holoscopeDate.Cons = new List<eConsHoloscope> {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.QuyNhanPhuTro,
                                eConsHoloscope.CauTaiNhuYMuon,
                                eConsHoloscope.ThangTien
                            };
                            break;
                        //2, 8, 14, 20, 26
                        case 2:
                            holoscopeDate.DateName = "Ngày Kim Thổ";
                            holoscopeDate.Description = "Là ngày xấu mà tuyệt đối không nên xuất hành, bởi ra đi thì nhỡ tàu, nhỡ xe, trên đường đi gặp điều bất lợi. Nói chung là sẽ đem đến nhiều điều không may mắn dành cho mỗi người.";
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                                eProsHoloscope.XuatHanh,
                                eProsHoloscope.GapDieuBatLoi,
                                eProsHoloscope.KhongMayMan
                            };
                            break;
                        //3, 9, 15, 21, 27
                        case 3:
                            holoscopeDate.DateName = "Ngày Kim Dương";
                            holoscopeDate.Description = "Là ngày xuất hành tốt, tài lộc thông suốt, gặp điều dữ hóa lành. Bên cạnh đó còn có quý nhân phù trợ, thăng tiến nhanh chóng.";
                            holoscopeDate.Cons = new List<eConsHoloscope> {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.QuyNhanPhuTro,
                                eConsHoloscope.ThangTien,
                                eConsHoloscope.TaiLocThongSuot,
                                eConsHoloscope.GapDuHoaLanh
                            };
                            break;
                        //4, 10, 16, 22, 28
                        case 4:
                            holoscopeDate.DateName = "Ngày Thuần Dương";
                            holoscopeDate.Description = "Đây là ngày mang ý nghĩa tốt, có nhiều điều thuận lợi. Nếu xuất hành vào ngày này thì sẽ cầu tài như ý, được người tốt giúp đỡ, đạt được nhiều thành tựu.";
                            holoscopeDate.Cons = new List<eConsHoloscope> {
                                eConsHoloscope.CauTaiNhuYMuon,
                                eConsHoloscope.NguoiTotGiupDo
                            };
                            break;
                        //5, 11, 17, 23, 29
                        case 5:
                            holoscopeDate.DateName = "Ngày Đạo Tặc";
                            holoscopeDate.Description = "Đây là một ngày rất xấu, nên tránh để hạn chế những điều không may mắn. Xuất hành vào ngày này còn bị hại, mất tiền của, tiêu hao tiền tài.";
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                                eProsHoloscope.XuatHanh,
                                eProsHoloscope.GapDieuBatLoi,
                                eProsHoloscope.KhongMayMan,
                                eProsHoloscope.BiHai,
                                eProsHoloscope.TieuHaoTienTai
                            };
                            break;
                        //6, 12, 18, 24, 30
                        case 0:
                            holoscopeDate.DateName = "Ngày Hảo Thương";
                            holoscopeDate.Description = "Là một ngày đẹp để bạn xuất hành, mọi việc thuận lợi như ý muốn.";
                            holoscopeDate.Cons = new List<eConsHoloscope> {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.MoiViecThuanLoi
                            };
                            break;
                    }
                    break;
                //2 5 8 10
                case 2:
                    switch (lunarDay)
                    {
                        case int n when n == 1 || n == 9 || n == 17 || n == 25:
                            holoscopeDate.DateName = "Ngày Thiên Đạo";
                            holoscopeDate.Description = "Đây là ngày xấu nên tránh khi xuất hành. Theo lịch Khổng Minh thì đây là ngày mà xuất hành cầu tài nên tránh. Gây ảnh hưởng đến sự nghiệp, tiền của.";
                            holoscopeDate.Cons = new List<eConsHoloscope> {
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                                eProsHoloscope.XuatHanh,
                                eProsHoloscope.TieuHaoTienTai
                            };
                            break;
                        case int n when n == 2 || n == 10 || n == 18 || n == 26:
                            holoscopeDate.DateName = "Ngày Thiên Môn";
                            holoscopeDate.Description = "Là ngày tốt để xuất hành trong tháng 2, 5, 8 và 11. Giúp đem đến nhiều thành đạt, cầu được ước thấy.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.MoiViecThuanLoi
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                            };
                            break;
                        case int n when n == 3 || n == 11 || n == 19 || n == 27:
                            holoscopeDate.DateName = "Ngày Thiên Đường";
                            holoscopeDate.Description = "Là ngày xuất hành tốt theo lịch Khổng Minh. Có quý nhân phù trợ, mọi điều như ý và buôn may bán đắt.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                                eConsHoloscope.QuyNhanPhuTro,
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.BuonMayBanDat
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                            };
                            break;
                        case int n when n == 4 || n == 12 || n == 20 || n == 28:
                            holoscopeDate.DateName = "Ngày Thiên Tài";
                            holoscopeDate.Description = "Nên xuất hành vào ngày này, bởi sẽ được người tốt giúp đỡ, gặp dữ hóa lành.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.NguoiTotGiupDo,
                                eConsHoloscope.GapDuHoaLanh
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                            };
                            break;
                        case int n when n == 5 || n == 13 || n == 21 || n == 29:
                            holoscopeDate.DateName = "Ngày Thiên Tặc";
                            holoscopeDate.Description = "Là ngày xấu khi xuất hành, cầu tài không được. Ngoài ra, mọi việc đều xấu, đi đường dễ bị mất cắp.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                                eProsHoloscope.XuatHanh,
                                eProsHoloscope.KhongMayMan,
                                eProsHoloscope.CauTaiKhongDuoc
                            };
                            break;
                        case int n when n == 6 || n == 14 || n == 22:
                            holoscopeDate.DateName = "Ngày Thiên Dương";
                            holoscopeDate.Description = "Là ngày tốt để xuất hành, cầu tài hoặc nhân duyên, vạn sự như ý.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.CauTaiNhuYMuon,
                                eConsHoloscope.CauNhanDuyenNhuYMuon,
                                eConsHoloscope.MoiViecThuanLoi
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                            };
                            break;
                        case int n when n == 7 || n == 15 || n == 23:
                            holoscopeDate.DateName = "Ngày Thiên Hầu";
                            holoscopeDate.Description = "Là ngày xấu, gặp nhiều thị phi, cãi cọ và xảy ra nhiều tai nạn nặng.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                                eProsHoloscope.XuatHanh,
                                eProsHoloscope.TaiNanNang,
                                eProsHoloscope.ThiPhi
                            };
                            break;
                        case int n when n == 8 || n == 16 || n == 24 || n == 30:
                            holoscopeDate.DateName = "Ngày Thiên Thương";
                            holoscopeDate.Description = "Đây là ngày tốt khi xuất hành tốt trong tháng 2, 5, 8 và 11. Giúp đem đến nhiều may mắn, tài lộc và sự thăng tiến.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.TaiLocThongSuot,
                                eConsHoloscope.ThangTien
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                            };
                            break;
                    }
                    break;
                //3 6 9 12
                case 0:
                    switch (lunarDay)
                    {
                        case int n when n == 1 || n == 9 || n == 17:
                            holoscopeDate.DateName = "Ngày Chu Tước";
                            holoscopeDate.Description = "Theo lịch Khổng Minh giờ tốt, ngày tốt của năm thì đây là ngày xấu không nên xuất hành. Bởi đem đến nhiều điều xấu, hay mất của, sự nghiệp không có sự phát triển.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                                eProsHoloscope.XuatHanh,
                                eProsHoloscope.TieuHaoTienTai
                            };
                            break;
                        case int n when n == 2 || n == 10 || n == 18 || n == 26:
                            holoscopeDate.DateName = "Ngày Bạch Hổ Đầu";
                            holoscopeDate.Description = "Là ngày tốt để xuất hành, cầu tài đều được, mọi sự hanh thông, may mắn.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.CauTaiNhuYMuon,
                                eConsHoloscope.MoiViecThuanLoi
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                            };
                            break;
                        case int n when n == 3 || n == 11 || n == 19 || n == 27:
                            holoscopeDate.DateName = "Ngày Bạch Hổ Kiếp";
                            holoscopeDate.Description = "Là ngày tốt, đẹp, cầu tài như ý. Đặc biệt, nên đi về hướng Bắc, hướng Nam để gặp nhiều thuận lợi, may mắn.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.CauTaiNhuYMuon,
                                eConsHoloscope.MoiViecThuanLoi
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                            };
                            break;
                        case int n when n == 4 || n == 12 || n == 20 || n == 28:
                            holoscopeDate.DateName = "Ngày Bạch Hổ Túc";
                            holoscopeDate.Description = "Là ngày tuyệt đối không nên đi xa, mọi sự không thành, là ngày xấu trong mọi việc.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                                eProsHoloscope.XuatHanh,
                                eProsHoloscope.GapDieuBatLoi,
                                eProsHoloscope.KhongMayMan
                            };
                            break;
                        case int n when n == 5 || n == 13 || n == 21 || n == 29:
                            holoscopeDate.DateName = "Ngày Bạch Hổ Túc";
                            holoscopeDate.Description = "Ngày xấu khi xuất hành, gặp nhiều điều xui, rủi ro.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                                eProsHoloscope.XuatHanh,
                                eProsHoloscope.GapDieuBatLoi,
                                eProsHoloscope.KhongMayMan
                            };
                            break;
                        case int n when n == 6 || n == 14 || n == 22:
                            holoscopeDate.DateName = "Ngày Thanh Long Đầu";
                            holoscopeDate.Description = "Là ngày tốt, nên xuất hành từ sáng sớm. Giúp mọi việc như ý, thắng lợi và may mắn.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.CauTaiNhuYMuon,
                                eConsHoloscope.MoiViecThuanLoi
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                            };
                            break;
                        case int n when n == 7 || n == 15 || n == 23 || n == 25:
                            holoscopeDate.DateName = "Ngày Thanh Long Kiếp";
                            holoscopeDate.Description = "Theo lịch Khổng Minh thì đây là ngày tốt, trăm sự như ý, tiền tài như nước.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                                eConsHoloscope.XuatHanh,
                                eConsHoloscope.CauTaiNhuYMuon,
                                eConsHoloscope.MoiViecThuanLoi
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                            };
                            break;
                        case int n when n == 8 || n == 16 || n == 24 || n == 30:
                            holoscopeDate.DateName = "Ngày Thanh Long Túc";
                            holoscopeDate.Description = "Là ngày xấu, không nên xuất hành đi xa. Bởi tài lộc không có, gặp nhiều điều không may mắn.";
                            holoscopeDate.Cons = new List<eConsHoloscope>
                            {
                            };
                            holoscopeDate.Pros = new List<eProsHoloscope>
                            {
                                eProsHoloscope.XuatHanh,
                                eProsHoloscope.KhongMayMan,
                            };
                            break;
                    }
                    break;
            }
            return holoscopeDate;
        }
    }
}
