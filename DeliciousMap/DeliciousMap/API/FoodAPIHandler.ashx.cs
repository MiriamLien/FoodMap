using DeliciousMap.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace DeliciousMap.API
{
    /// <summary>
    /// FoodAPIHandler 的摘要描述
    /// </summary>
    public class FoodAPIHandler : IHttpHandler
    {
        /// <summary> 取得美食地圖的資料 </summary>
        /// <param name="queryParameter"></param>
        /// <returns></returns>
        private List<MapContentModels> GetFoodMapList(string queryParameter)
        {
            // 原始資料
            List<MapContentModels> list = new List<MapContentModels>()
            {
                new MapContentModels() { ID = new Guid("ecea583fff6e4f61913943fea1b06217"), Longitude = 120.205983327337, Latitude = 22.9943357858017, Title = "無名米糕 (炮店米糕)", Body = @"「無名米糕」是隱藏在巷弄的台南人氣小吃，因為巷口有間賣煙火的商行掛著炮店招牌，所以無名米糕也被人稱作「炮店米糕」。"},
                new MapContentModels() { ID = new Guid("b9286b21912d408598444c262c1bd7b6"), Longitude = 120.1947711, Latitude = 22.9983651, Title = "阿江鱔魚意麵", Body = @"僅次於牛肉湯的「鱔魚意麵」也是另一個經典的台南小吃"},
                new MapContentModels() { ID = new Guid("6f1d9ef203c644559c6a17437aa4bf4c"), Longitude = 120.1970155, Latitude = 22.9898024, Title = "糯夫米糕", Body = @"「糯夫米糕」可說是無人不知無人不曉的攤車米糕．在功俸三太子的昆沙宮旁，開了一間老宅文青感十足的店面，適中分量的米糕，黏稠到無法粒粒分明，與入味又豐富的配料相容，豬肉口味的米糕有豬皮鮮嫩的點綴，麻油香郁、聞香十里"},
                new MapContentModels() { ID = new Guid("acf46026a8dd441d8fd0b35af8772daa"), Longitude = 120.1623593, Latitude = 22.9874915, Title = "友愛鹽酥雞", Body = @"友愛鹽酥雞是來台南一定要吃的宵夜，更是台灣鹽酥雞創始店，從民國68年開店至今，是許多台南人從小吃到大的回憶，現在已經從友愛街搬家至中正路上，有先進的E化點餐機、清爽活潑的店面空間，以及講究衛生的食材擺設及保存空間，肥腸、軟骨、雞皮、雞心、魷魚、四季豆等等，多達四十種的食材供應，保冷之外更保鮮，油炸食的自動控溫、起鍋後放入濾油機，讓食物吃起來不油膩，撒上胡椒鹽，就是這最經典復刻的一味！"},
                new MapContentModels() { ID = new Guid("e6e19161dab648399860e02e91771d6c"), Longitude = 120.197692530764, Latitude = 22.9945142553446, Title = "阿伯炭烤黑輪甜不辣", Body = @"在邱家小捲米粉斜對面有一家小攤販【阿伯炭烤黑輪甜不辣】只賣甜不辣、黑輪兩種食物，即使找不到攤子，也會令人不禁聞到陣陣飄來的碳烤香。使用手工製作的旗魚魚漿為原料，先酥炸後再用炭火直烤上色，最後撒上點白胡椒，外皮酥脆焦香且口感厚實，現烤現吃不油膩，是國華街散步美食的經典代表之一。"},
                new MapContentModels() { ID = new Guid("3816713ffd3b4e80b3ccb354cce1146d"), Longitude = 120.195186284585, Latitude = 22.9902042116376, Title = "矮仔成蝦仁飯", Body = @"說到台南府城小吃就不能提這獨特的「蝦仁飯」，其中從西元1922年大正時期營業至今的【矮仔成蝦仁飯】已將邁入百年老店，店面也剛換新，讓用餐環境變得更加舒適。蝦仁飯是由高湯與白飯拌炒而成，口感類似日式炊飯，鋪上約十隻真材實料的蝦仁，配上米飯每一口都是幸福，另外建議可以加顆香煎半熟鴨蛋蓋在飯上，配上爆漿的蛋黃更是令人垂涎。"},
                new MapContentModels() { ID = new Guid("bdbf94738235446784be8c2acccc5f26"), Longitude = 120.196097739764, Latitude = 22.9907062241852, Title = "阿明豬心冬粉", Body = @"台南最夯的宵夜美食非「阿明豬心冬粉」了，看見店門口長長的排隊人潮，就是它美味的絕佳證明。

                單純的販售著豬心、豬肝、腰只等豬內臟，搭配上招牌豬腳、腦髓及鴨翅飯，附上薑絲醬油膏沾醬，就是道道暖胃的美食！招牌豬心冬粉，先切成薄片、再高湯川燙，厚度剛好入口的豬心配上湯頭的清爽甘甜，吃一口就懂這些人為甚麼如此趨之若鶩了。"},
                new MapContentModels() { ID = new Guid("00a5066616ba4b4d8d87a5f45ae79ded"), Longitude = 120.1950464072, Latitude = 22.990987202984, Title = "石頭鄉悶烤烤玉米", Body = @"台南「石頭鄉悶烤烤玉米」已有30多年歷史，老闆對玉米的品質控管嚴格，以及特別繁複的燒烤方式，使其成為台南在地人超推的必吃美食。將新鮮玉米埋在熱石堆裡乾燜至熟，吃起來也比生烤玉米多汁也更加易嚼，再以炭火燒烤塗醬入味，在獨門醬料下保留了玉米的原味與水分，又有炭烤的香氣，實在是香Q美味！招牌沙茶玉米之外，另有奶油、椰香、焦糖醬爆玉米、蒜香等多種不同口味選擇。"},
                new MapContentModels() { ID = new Guid("7960ac6d12854a76b1899a4749e75b8f"), Longitude = 120.19743002627, Latitude = 22.9936886368347, Title = "邱家小卷米粉", Body = @"提到國華街最有人氣的美食，絕對是「邱家小卷米粉」，新鮮Q彈的小卷搭配上清甜的湯頭，是在地人與外地觀光客都愛的好滋味！

                店內寬敞卻時刻座無虛席，好在翻桌率高，等候時間不需太長就能享用新鮮好味道。長得像米苔目的粗米粉，硬中帶點Q，配上給料豪不吝嗇的超彈牙小卷，新鮮且甘甜，小卷湯可以同時嚐到整隻小卷與可遇不可求的小卷蛋，建議先吃原味再沾上辣醬或胡椒，讓小卷的鮮甜更加提升。"},
                new MapContentModels() { ID = new Guid("72cb52de87b04ac98fa39de2d5d3681d"), Longitude = 120.197583741988, Latitude = 22.9944255218865, Title = "炸雞洋行", Body = @"台南經典不敗的排隊美食「炸雞洋行」，也是小編每次去台南必吃的首選！以氣炸料理聞名，讓人吮指回味的好名聲。氣壓式的炸雞將肉汁完全鎖在雞肉裡，一支支金黃炸雞，依分量分為八兩雞與三兩腿，薄博的酥脆外皮、鮮嫩多汁的肉質，還有夠味的雙截辣翅，一口接著一口，痛快滿足！"},
                new MapContentModels() { ID = new Guid("cc3652e3023e41a8b7d078ab676c42c8"), Longitude = 120.199107637914, Latitude = 22.9978438638937, Title = "富盛號碗粿", Body = @"開業已經70多年的歷史的「富盛號碗粿」，是台南永樂市場、國華街一帶的必吃美食。木頭裝潢的店面，有古早味的感覺，碗粿使用高品種的台灣再來米、嚴選溫體豬後腿肉、天然火燒的蝦仁，自製滷肉臊，最後淋上獨家醬汁與蒜泥，軟軟的碗粿幾十年不變的好味道。另外，以大骨及蔬菜熬製而成的湯底再加上旗魚肉漿製成的魚丸煮熟後勾芡，搭配香菜、黑醋及胡椒，魚羹口感紮實、湯頭酸甜濃郁而爽口！"},
                new MapContentModels() { ID = new Guid("1ab173fc756941ad8bfe900e594a0060"), Longitude = 120.197878195585, Latitude = 22.9901586018298, Title = "阿堂鹹粥", Body = @"位在西門圓環上的「阿堂鹹粥」，好名聲讓天未亮就有饕客願意犧牲睡眠來排隊，料多實在的綜合鹹粥，集合了店裡四種鹹粥的鮮味，加入土魠魚肉、虱目魚肉、虱目魚肚以及魚皮還有蚵仔，費工處理過的虱目魚，還融入了新鮮油蔥酥及碧綠的韭菜末，米粒分明的每一口，都吃的到口感與湯的鮮甜，搭上炸的酥脆的老油條，泡在鹹粥中糊潤柔軟，真的超好吃！"},
                new MapContentModels() { ID = new Guid("18ad0577e83e41428d3b25dd43b0a3b0"), Longitude = 120.168725780295, Latitude = 22.9991925853045, Title = "王氏魚皮", Body = @"被網友稱為平價無雷的「王氏魚皮」，一看菜單價格真的超親民！說到魚皮大家是否感到困惑到底是個怎樣的食物呢？其實他不單只有皮，連肉帶皮的口感咬下軟嫩又帶香氣，是來台南必吃的招牌虱目魚料理之一，如果點虱目魚湯還可免費加湯喔！"},
            };

            // 如果傳入空白，代表不過濾
            if (string.IsNullOrWhiteSpace(queryParameter))
                return list;

            // 依條件進行過濾
            List<MapContentModels> retList = new List<MapContentModels>();
            foreach (MapContentModels item in list)
            {
                if (item.Title.Contains(queryParameter) ||
                    item.Body.Contains(queryParameter))
                    retList.Add(item);
            }

            return retList;
        }

        public void ProcessRequest(HttpContext context)
        {
            string[] query = context.Request.QueryString.GetValues("q");

            List<MapContentModels> list =
                (query == null || query.Length == 0)
                    ? this.GetFoodMapList(string.Empty)
                    : this.GetFoodMapList(query[0]);

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.ContentType = "application/json";
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}