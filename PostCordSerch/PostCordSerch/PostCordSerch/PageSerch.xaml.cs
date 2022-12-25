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
            await DisplayAlert("", "�X�֔ԍ�����͂��Ă�������", "OK");
            return;
        }

        HttpClient client = new HttpClient();
        JsonNode PostInfo;  //Http���X�|���X

        string ApiAddress = $"https://zipcloud.ibsnet.co.jp/api/search?zipcode={EntryPostCode.Text}";

        try
        {
            client.Timeout = TimeSpan.FromSeconds(5); //�^�C���A�E�g��5�b�ɐݒ�
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
            await DisplayAlert("", "�擾�Ɏ��s���܂���", "OK");
        }
    }
}