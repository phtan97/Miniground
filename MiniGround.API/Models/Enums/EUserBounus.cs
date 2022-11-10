using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Models.Enums
{
    /// <summary>
    /// đây là thứ tự thành viên nhận hoa hồng đã sắp xếp. 
    /// Nếu thay đổi thứ tự các level sẽ ảnh hưởng đến logic thực hiện chia hoa hồng trước đó.
    /// Xem hàm ShareBounusAfterMatchEnd.
    /// Giá trị mỗi level = value / 100
    /// </summary>
    public enum EUserBounus
    {
        Level4 = 10,
        Level3 = 10,
        Level2 = 5,
        Level1 = 7
    }
}
