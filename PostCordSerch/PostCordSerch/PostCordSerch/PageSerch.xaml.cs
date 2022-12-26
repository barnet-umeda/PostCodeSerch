using System.Text.Json.Nodes;

namespace PostCordSerch;

public partial class PageSerch : ContentPage
{
    
    public PageSerch()
	{
		InitializeComponent();
	}

    //検索ボタンクリックイベント
    private async void ClickedButtonSerch(object sender, EventArgs e)
    {
        if (EntryPostCode.Text == null || EntryPostCode.Text.Length < 7)
        {
            await DisplayAlert("", "郵便番号を入力してください", "OK");
            return;
        }

        try
        {
            //APIアドレス＋郵便番号
            string ApiAddress = $"https://zipcloud.ibsnet.co.jp/api/search?zipcode={EntryPostCode.Text}";
            //APIアクセス先
            HttpClient client = new HttpClient();
            //タイムアウトを5秒に設定
            client.Timeout = TimeSpan.FromSeconds(5);
            //usingステートメントを使用してアクセスを宣言(動作終了後、自動破棄となる)
            using HttpResponseMessage response = await client.GetAsync(ApiAddress);
            response.EnsureSuccessStatusCode();
            //APIからのレスポンスを格納
            string responseBody = await response.Content.ReadAsStringAsync();
            //VisualStudioの出力にレスポンスを表示
            Console.WriteLine(responseBody);

            //レスポンスをJsonNodeに変換(JsonNodeで結果表示画面に渡す)
            JsonNode jnodeResponseBody = JsonNode.Parse(responseBody);

            //レスポンスに住所データが無い場合
            if (jnodeResponseBody["results"] == null || (int)jnodeResponseBody["status"] != 200)
            {
                //エラーを発生させる。
                throw new Exception("error");
            }

            //★ここに結果表示画面を呼び出す処理が必要
            await Navigation.PushAsync(new PageResult(jnodeResponseBody));

        }
        catch
        {
            //エラーメッセージ表示
            await DisplayAlert("", "取得に失敗しました", "OK");
        }
    }
}