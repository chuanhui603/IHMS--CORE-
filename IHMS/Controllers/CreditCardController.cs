using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using IHMS.Models;

namespace IHMS.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly IhmsContext _db;

        public CreditCardController(IhmsContext db)
        {
            _db = db;
        }

        // step1 : 網頁導入傳值到前端
        public ActionResult Index()
        {
            // 產生一個長度為 20 的隨機字串作為訂單編號
            string orderId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
            int pointcountid = _db.PointRecords.Max(pr => pr.PointrecordId);
            int pointcount = (_db.PointRecords.FirstOrDefault(pr => pr.PointrecordId == pointcountid).Count) * 500;





            // 需填入你的網址，這裡假設使用 localhost:44325
            string website = $"https://localhost:7127";

            // 建立綠界付款所需的參數字典
            var order = new Dictionary<string, string>
            {
                // 綠界需要的參數
                { "MerchantTradeNo",  orderId}, // 訂單編號
                { "MerchantTradeDate",  DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}, // 訂單成立時間
                { "TotalAmount",  $"{pointcount}"}, // 訂單金額
                { "TradeDesc",  "無"}, // 交易描述
                { "ItemName",  "測試商品"}, // 商品名稱
                { "ExpireDate",  "3"}, // 訂單有效期限
                { "CustomField1",  ""}, // 自訂欄位1
                { "CustomField2",  ""}, // 自訂欄位2
                { "CustomField3",  ""}, // 自訂欄位3
                { "CustomField4",  ""}, // 自訂欄位4
                { "ReturnURL",  $"{website}/api/Ecpay/AddPayInfo"}, // 付款完成後返回網址
                { "OrderResultURL","https://ihms.club/"}, // 訂單處理結果網址
                { "PaymentInfoURL","http://localhost:5174"}, // 付款資訊回傳網址
                { "ClientRedirectURL","https://ihms.club/"}, // 客戶端返回網址
                { "MerchantID",  "2000132"}, // 商店代號
                { "IgnorePayment",  "GooglePay#WebATM#CVS#BARCODE"}, // 忽略的付款方式
                { "PaymentType",  "aio"}, // 付款方式
                { "ChoosePayment",  "ALL"}, // 可用的付款方式
                { "EncryptType",  "1"}, // 交易資料加密方式
            };

            // 計算檢查碼並放入參數中
            order["CheckMacValue"] = GetCheckMacValue(order);
            return View(order); // 回傳包含參數的 View
        }

        // 計算檢查碼
        private string GetCheckMacValue(Dictionary<string, string> order)
        {
            // 將參數按照字母順序排序並串接成字串
            var param = order.Keys.OrderBy(x => x).Select(key => key + "=" + order[key]).ToList();
            var checkValue = string.Join("&", param);

            // 測試用的 HashKey
            var hashKey = "5294y06JbISpM5x9";
            // 測試用的 HashIV
            var HashIV = "v77hoKGq4kWxNNIS";

            // 加上 HashKey 與 HashIV 並進行 URL 編碼
            checkValue = $"HashKey={hashKey}" + "&" + checkValue + $"&HashIV={HashIV}";
            checkValue = HttpUtility.UrlEncode(checkValue).ToLower();

            // 計算 SHA256 雜湊值
            checkValue = GetSHA256(checkValue);

            return checkValue.ToUpper(); // 回傳大寫的檢查碼
        }

        // 計算 SHA256 雜湊值
        private string GetSHA256(string value)
        {
            var result = new StringBuilder();
            var sha256 = SHA256Managed.Create();
            var bts = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bts);
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}