using System.Text.Json.Nodes;

namespace PostCordSerch;

public partial class PageSerch : ContentPage
{
    
    public PageSerch()
	{
		InitializeComponent();
	}

    //�����{�^���N���b�N�C�x���g
    private async void ClickedButtonSerch(object sender, EventArgs e)
    {
        if (EntryPostCode.Text == null || EntryPostCode.Text.Length < 7)
        {
            await DisplayAlert("", "�X�֔ԍ�����͂��Ă�������", "OK");
            return;
        }

        try
        {
            //API�A�h���X�{�X�֔ԍ�
            string ApiAddress = $"https://zipcloud.ibsnet.co.jp/api/search?zipcode={EntryPostCode.Text}";
            //API�A�N�Z�X��
            HttpClient client = new HttpClient();
            //�^�C���A�E�g��5�b�ɐݒ�
            client.Timeout = TimeSpan.FromSeconds(5);
            //using�X�e�[�g�����g���g�p���ăA�N�Z�X��錾(����I����A�����j���ƂȂ�)
            using HttpResponseMessage response = await client.GetAsync(ApiAddress);
            response.EnsureSuccessStatusCode();
            //API����̃��X�|���X���i�[
            string responseBody = await response.Content.ReadAsStringAsync();
            //VisualStudio�̏o�͂Ƀ��X�|���X��\��
            Console.WriteLine(responseBody);

            //���X�|���X��JsonNode�ɕϊ�(JsonNode�Ō��ʕ\����ʂɓn��)
            JsonNode jnodeResponseBody = JsonNode.Parse(responseBody);

            //���X�|���X�ɏZ���f�[�^�������ꍇ
            if (jnodeResponseBody["results"] == null || (int)jnodeResponseBody["status"] != 200)
            {
                //�G���[�𔭐�������B
                throw new Exception("error");
            }

            //�������Ɍ��ʕ\����ʂ��Ăяo���������K�v
            await Navigation.PushAsync(new PageResult(jnodeResponseBody));

        }
        catch
        {
            //�G���[���b�Z�[�W�\��
            await DisplayAlert("", "�擾�Ɏ��s���܂���", "OK");
        }
    }
}