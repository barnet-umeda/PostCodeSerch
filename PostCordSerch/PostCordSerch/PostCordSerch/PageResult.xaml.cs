using System.Text.Json.Nodes;

namespace PostCordSerch;

public partial class PageResult : ContentPage
{
    //ダミーのレスポンスデータ
    //private JsonNode jnodeDummy =
    //    JsonNode.Parse("{\r\n\t\"message\": null,\r\n\t\"results\": [\r\n\t\t{\r\n\t\t\t\"address1\": \"住所１\",\r\n\t\t\t\"address2\": \"住所２\",\r\n\t\t\t\"address3\": \"住所３\",\r\n\t\t\t\"kana1\": \"ｶﾅ1\",\r\n\t\t\t\"kana2\": \"ｶﾅ2\",\r\n\t\t\t\"kana3\": \"kana3\",\r\n\t\t\t\"prefcode\": \"00\",\r\n\t\t\t\"zipcode\": \"0000000\"\r\n\t\t}\r\n\t],\r\n\t\"status\": 200\r\n}");

    //コンストラクタ
    public PageResult(JsonNode jnodeRespons)
    {
        InitializeComponent();

        //レスポンス格納変数にダミーを格納
        //JsonNode jnodeResultVal = jnodeDummy;

        //検索画面から渡されたレスポンスを格納
        JsonNode jnodeResultVal = jnodeRespons;

        //resultsの値を抽出
        JsonNode jnodeBuf = jnodeResultVal["results"];

        //それぞれの値を格納
        string postCodeVal = jnodeBuf[0]["zipcode"].ToString();
        string addressVal = jnodeBuf[0]["address1"].ToString() + 
                            jnodeBuf[0]["address2"].ToString() + 
                            jnodeBuf[0]["address3"].ToString();
        string prefectureCodeVal = jnodeBuf[0]["prefcode"].ToString();

        //値をラベルに表示
        LabelPostCode.Text = postCodeVal;
        LabelAddress.Text = addressVal;
        LabelPrefectureCode.Text = prefectureCodeVal;
    }

    //戻るボタンクリック
    private void ClickedResult(object sender, EventArgs e)
    {
        //前の画面に戻る
        Navigation.PopAsync();
    }
}