using System.Text.Json.Nodes;

namespace PostCordSerch;

public partial class PageResult : ContentPage
{
    //�_�~�[�̃��X�|���X�f�[�^
    //private JsonNode jnodeDummy =
    //    JsonNode.Parse("{\r\n\t\"message\": null,\r\n\t\"results\": [\r\n\t\t{\r\n\t\t\t\"address1\": \"�Z���P\",\r\n\t\t\t\"address2\": \"�Z���Q\",\r\n\t\t\t\"address3\": \"�Z���R\",\r\n\t\t\t\"kana1\": \"��1\",\r\n\t\t\t\"kana2\": \"��2\",\r\n\t\t\t\"kana3\": \"kana3\",\r\n\t\t\t\"prefcode\": \"00\",\r\n\t\t\t\"zipcode\": \"0000000\"\r\n\t\t}\r\n\t],\r\n\t\"status\": 200\r\n}");

    //�R���X�g���N�^
    public PageResult(JsonNode jnodeRespons)
    {
        InitializeComponent();

        //���X�|���X�i�[�ϐ��Ƀ_�~�[���i�[
        //JsonNode jnodeResultVal = jnodeDummy;

        //������ʂ���n���ꂽ���X�|���X���i�[
        JsonNode jnodeResultVal = jnodeRespons;

        //results�̒l�𒊏o
        JsonNode jnodeBuf = jnodeResultVal["results"];

        //���ꂼ��̒l���i�[
        string postCodeVal = jnodeBuf[0]["zipcode"].ToString();
        string addressVal = jnodeBuf[0]["address1"].ToString() + 
                            jnodeBuf[0]["address2"].ToString() + 
                            jnodeBuf[0]["address3"].ToString();
        string prefectureCodeVal = jnodeBuf[0]["prefcode"].ToString();

        //�l�����x���ɕ\��
        LabelPostCode.Text = postCodeVal;
        LabelAddress.Text = addressVal;
        LabelPrefectureCode.Text = prefectureCodeVal;
    }

    //�߂�{�^���N���b�N
    private void ClickedResult(object sender, EventArgs e)
    {
        //�O�̉�ʂɖ߂�
        Navigation.PopAsync();
    }
}