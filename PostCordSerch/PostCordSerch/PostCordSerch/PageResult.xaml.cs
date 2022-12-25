using System.Text.Json.Nodes;

namespace PostCordSerch;

public partial class PageResult : ContentPage
{
    //���X�|���X�i�[�ϐ�
	private JsonNode jnodeResultVal;
    //�_�~�[�̃��X�|���X�f�[�^
    private JsonNode jnodeDummy = JsonNode.Parse("{\r\n\t\"message\": null,\r\n\t\"results\": [\r\n\t\t{\r\n\t\t\t\"address1\": \"�Z���P\",\r\n\t\t\t\"address2\": \"�Z���Q\",\r\n\t\t\t\"address3\": \"�Z���R\",\r\n\t\t\t\"kana1\": \"��1\",\r\n\t\t\t\"kana2\": \"��2\",\r\n\t\t\t\"kana3\": \"kana3\",\r\n\t\t\t\"prefcode\": \"00\",\r\n\t\t\t\"zipcode\": \"0000000\"\r\n\t\t}\r\n\t],\r\n\t\"status\": 200\r\n}");

    //�R���X�g���N�^
    public PageResult() 
    { 
		InitializeComponent();
        //���[�J���ϐ��Ƀ��X�|���X���i�[
        jnodeResultVal = jnodeDummy;


        JsonNode jnodeBuf = jnodeResultVal["results"];

        string postCodeVal = jnodeBuf[0]["zipcode"].ToString();
        string addressVal = jnodeBuf[0]["address1"].ToString() + jnodeBuf[0]["address2"].ToString() + jnodeBuf[0]["address3"].ToString();
        string prefectureCodeVal = jnodeBuf[0]["prefcode"].ToString();

        LabelPostCode.Text = postCodeVal;
        LabelAddress.Text = addressVal;
        LabelPrefectureCode.Text = prefectureCodeVal;
    }

    //�߂�{�^���N���b�N
    private async void ClickedResult(object sender, EventArgs e)
    {

    }
}