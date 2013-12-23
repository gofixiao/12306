using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace APP.Webfrom
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string str = "CustomerUsedPointAmount=0&OrderMemo=%25u53D1%25u7968%25u5185%25u5BB9%25u4E0D%25u652F%25u6301%25u4FEE%25u6539%25uFF1B%25u6536%25u8D27%25u4EBA%25u3001%25u914D%25u9001%25u65B9%25u5F0F%25u3001%25u652F%25u4ED8%25u65B9%25u5F0F%25u7B49%25u4EE5%25u4E0A%25u8FF0%25u9009%25u5B9A%25u503C%25u4E3A%25u51C6%25uFF0C%25u5728%25u6B64%25u5907%25u6CE8%25u65E0%25u6548&CampusAgentCustomerID=&IsUsedPrePay=0&PayTypeId=68&AllShipType=1%3A21%2C&CurShipTypeId=0&CurSellType=0&checkOutAction=CreateSalesOrder&giftCardCode=&giftCardPwd=&giftCardTranNum=0&invoiceType=0&invoiceData=%257B%2522InvoiceType%2522%253A0%252C%2522CompanyName%2522%253A%2522%25u4E2A%25u4EBA%2522%252C%2522CompanyPhone%2522%253A%2522%2522%252C%2522TaxNumber%2522%253A%2522%2522%252C%2522CompanyAddress%2522%253A%2522%2522%257D&AllDateKey=1%3A2013-11-30%3A0%2C&NeedCheckGiftCardInvoice=false&InstallMentInfo=&buytype=0&ChinaPostPackData=%257B%2522CanAgentFund%2522%253A0%257D&Alllocate=%255B%257B%2522Cid%2522%253A1%252C%2522Nid%2522%253A51%252C%2522Sid%2522%253A21%252C%2522Pid%2522%253A62660%252C%2522Aid%2522%253A2622%252C%2522WName%2522%253A%2522%25u4E0A%25u6D77%25u4ED3%2522%252C%2522Mid%2522%253A0%257D%255D&OzzoFreeShipping=1%3A21%2C&SplitOptionList=&PromotionCode=&EnergySubsidyData=&IsPayTypeView=1&CouponCode=";
            var list = str.Split('&');
            string cookie = "1384421697828; BrowsedProductSysNoList=62660%2b305714%2b51754%2b287640%2b301681%2b305982%2b305981%2b129226; __utma=84673030.2115229329.1384421709.1385703348.1385707550.26; __utmz=84673030.1385630021.21.4.utmcsr=prewww.newegg.cn|utmccn=(referral)|utmcmd=referral|utmcct=/Product/A02-004-2NRP10.htm; SampleRateStatus=true; NAwardCookie=; NNeweggGift=%5b%5d; ClientGUID=ClientGUID=e50d35f3-f543-4d1f-bd16-b88004e8d281; __sync=1; AreaCode=2622; __NVTC=215220744.2001.706414655.1385447211.1385704619.1385707548.25; s_pers=%20s_ev19%3D%255B%255B%2527afc-_-youdao-_-18640243212661-_-newegg%2527%252C%25271385519249505%2527%255D%255D%7C1543285649505%3B%20productnum%3D12%7C1388283172877%3B%20s_eVar24%3DRegistered%7C1385794885705%3B%20s_prop29%3D1385708485717%7C1480316485717%3B%20s_prop29_s%3DLess%2520than%25201%2520day%7C1385710285717%3B%20s_fid%3D10B0327BAEA31829-13CE8344A713C260%7C1448780526133%3B%20s_vs%3D1%7C1385710326140%3B%20gpv%3Dcheckout%7C1385710326154%3B%20s_nr%3D1385708526165-Repeat%7C1417244526165%3B%20gpvch%3Dshopping%2520process%7C1385710326173%3B; NewAdvEffectMonitor=Advalue=CPS-%5F-smzdm-%5F-smzdm-%5F-eventcode&Type=cmp; Area=SH; gobackurl=http%3A//www.newegg.cn/product/33-C01-229.htm%3Fneg_sp%3DBlackFriday2013-_-SecKill-_-33-C01-229; server=DC%3aE5%2cSITE%3aE5-04%2cQ%3aE5QRY06%2cExp%3a635213340844089683%2cEncrData%3a1BngCXsC4Pbt9fOQ20koIA%3d%3d; NCartCookie=%7b%22CIS%22%3a%5b%7b%22CID%22%3a%2262660%22%2c%22CQ%22%3a1%2c%22CT%22%3a%22P%22%2c%22P%22%3a%22149.00%22%2c%22PID%22%3a62660%2c%22Q%22%3a1%2c%22SC%22%3atrue%2c%22Wid%22%3a0%7d%5d%2c%22PC%22%3a1%7d; LoginValidate=CID=a157147899&CustomerSysNo=490732238042556&Key=oSsWl%2b20zi5wKC0UXb8cd55g55gV%2b4N1&LoginTime=130301815603392089&ThirdPartCustomerName=&ThirdPartName=&ThirdPartPassportUserName=&ThirdPartSubName=&Type=; Count=2; s_sess=%20s_cc%3Dtrue%3B%20s_sq%3Dneweggchinaprod%253D%252526pid%25253Dcheckout%252526pidt%25253D1%252526oid%25253Djavascript%2525253AajaxCreateOrder.submit%25252528%25252529%2525253B%252526ot%25253DA%3B; __utmc=84673030; ASP.NET_SessionId=uk0wmdnmrss1xi5515ivy055; ThirdPartyLogin=ReturnUrl=https%3a%2f%2fsecure.newegg.cn%2fshopping%2fCheckOut.aspx&TenpayLogin=&AlipayLogin=&PassportName=&LogonType=None&ThirdPartyEmail=&ThirdPartyID=&ThirdPartyName=&ThirdPartySubName=; NSC_tfdvsf.ofxfhh.do-wtws=ffffffffaf1c0e1f45525d5f4f58455e445a4a423660; advmonitor=n%21@P%21%3B/; DataInfo=Card=1&refUri=; CustomerLogin=ID=a157147899&SysNo=891782333012858&Key=xooJGLw6ZHk%3d&Rank=%e9%9d%92%e9%93%9c%e4%bc%9a%e5%91%98&LoginTime=20131129145240; ItemGiftCookie=cart=; NShopping=%5b%7b%22I%22%3a%5b%7b%22N%22%3a301681%2c%22Q%22%3a1%7d%5d%2c%22O%22%3a12632163%7d%2c%7b%22I%22%3a%5b%7b%22N%22%3a305714%2c%22Q%22%3a1%7d%5d%2c%22O%22%3a12632885%7d%5d; PromotionCodeCookie=; NeweggIntalmentGift=; __utmb=84673030.7.10.1385707550; Shipping=PayTypeId=68&ShippingAddressID=5742425";
            foreach (string item in list)
            {
                var a = Encoding.UTF8.GetBytes(item);

                Response.Write(HttpUtility.UrlDecode(item)+"<br/>");
            }
            var list2 = cookie.Split(';');
            foreach (string item in list2)
            {
                Response.Write(HttpUtility.UrlDecode(item) + "<br/>");
            }
            
        }
    }
}
