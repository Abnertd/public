<%@ Page Language="C#" %>

<script runat="server">
    Member member;
    protected void Page_Load(object sender, EventArgs e)
    {
        member = new Member();
        member.Member_Login_Check("/member/message_list.aspx?action=list");
        Member MyApp = new Member();
        string action = Request["action"];
        switch (action)
        {
            //ɾ��ѡ�е���Ϣ
            case "messagesmove":
                MyApp.MemberMessges_Del(false);
                break;
            //ɾ���û�Ա���ܵ����е���Ϣ
            case "allmessagesmove":
                MyApp.MemberMessges_Del(true);
                break;
            //���Ϊ�Ѷ�
            case "messagesIsRead":
                MyApp.MemberMessges_IsRead(1);
                break;
            //���Ϊδ��
            case "messagesIsUnRead":
                MyApp.MemberMessges_IsRead(0);
                break;
            //ȫ�����Ϊ�Ѷ�
            case "allmessagesIsRead":
                MyApp.AllMemberMessges_IsRead(1);
                break;
        }

    }
</script>
