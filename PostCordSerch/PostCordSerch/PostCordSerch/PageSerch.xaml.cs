using System.Text.Json.Nodes;

namespace PostCordSerch;

public partial class PageSerch : ContentPage
{
    
    public PageSerch()
	{
		InitializeComponent();
	}

    private async void ClickedButtonSerch(object sender, EventArgs e)
    {
        if (EntryPostCode.Text == null || EntryPostCode.Text.Length < 7)
        {
            await DisplayAlert("", "郵便番号を入力してください", "OK");
            return;
        }

        HttpClient client = new HttpClient();
        JsonNode PostInfo;  //Httpレスポンス

        string ApiAddress = $"https://zipcloud.ibsnet.co.jp/api/search?zipcode={EntryPostCode.Text}";

        try
        {
            client.Timeout = TimeSpan.FromSeconds(5); //タイムアウトを5秒に設定
            using HttpResponseMessage response = await client.GetAsync(ApiAddress);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            PostInfo = JsonNode.Parse(responseBody);
            Console.WriteLine(PostInfo);

            if (PostInfo["results"] == null) throw new Exception("error");

            JsonNode jnBuf = PostInfo["results"];
            JsonArray jaBuf = (JsonArray)PostInfo["results"];
            JsonValue jvBuf = (JsonValue)jaBuf[0]["address1"];

            await DisplayAlert("", jvBuf.ToString(), "OK");
        }
        catch
        {
            await DisplayAlert("", "取得に失敗しました", "OK");
        }
    }
}